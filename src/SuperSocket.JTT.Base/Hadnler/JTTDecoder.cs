using Flee.PublicTypes;
using SuperSocket.JTT.Base.Extension;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTT.Base.Hadnler
{
    /// <summary>
    /// JTT协议解码器
    /// </summary>
    public abstract class JTTDecoder : IJTTDecoder
    {
        public JTTDecoder(IJTTProtocol _protocol)
        {
            protocol = _protocol;

            handler = protocol.GetHandler();

            zhcnencoding = handler.GetZHCNEncoding();

            expressionContext = new ExpressionContext();
            expressionContext.Imports.AddType(typeof(Math));
        }

        #region 公共方法

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual IJTTPackageInfo Decode(ref ReadOnlySequence<byte> buffer, object context)
        {
            var packageInfo = (IJTTPackageInfo)Activator.CreateInstance(protocol.PackageInfoType);
            packageInfo.Success = false;
            packageInfo.Step = DecoderStep.None;

            try
            {
                packageInfo.Step = DecoderStep.AnalysisBuffer;

                packageInfo.HeadFlag = handler.GetHeadFlagValue();
                packageInfo.EndFlag = handler.GetEndFlagValue();

                if (buffer.IsEmpty)
                    throw new JTTException("解码失败, buffer是空的.", buffer.ToArray());

                var bytes = handler.UnEscape(buffer.ToArray());

                if (protocol.Structures?.Any() != true)
                    throw new JTTException($"未找到任何结构信息, 请检查协议的Json配置文件.", buffer.ToArray());

                if (!handler.CheckCrcCode(bytes, out byte[] data, out byte[] crc_code, out byte[] crc_code_compute))
                    throw new JTTException($"CRC校验失败, 实际值 [{string.Join(", ", crc_code.Select(o => o.GetHexString()))}]和计算值 [{string.Join(", ", crc_code_compute.Select(o => o.GetHexString()))}]不一致.", buffer.ToArray());

                packageInfo.Buffer = new ReadOnlySequence<byte>(data);
                packageInfo.Crc_Code = new ReadOnlyMemory<byte>(crc_code);

                packageInfo.Step = DecoderStep.AnalysisStructure;

                var offset = 0;

                Analysis(packageInfo, data, ref offset);

                packageInfo.Success = true;
                packageInfo.Step = DecoderStep.Done;
            }
            catch (ApplicationException ex)
            {
                packageInfo.Exception = ex;
            }

            return packageInfo;
        }

        /// <summary>
        /// 分析结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="bytes">转义后的数据</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public virtual void Analysis(IJTTPackageInfo packageInfo, ReadOnlySpan<byte> bytes, ref int offset)
        {
            foreach (var structure in protocol.Structures)
            {
                AnalysisStructure(packageInfo, packageInfo, structure, bytes, ref offset);
            }
        }

        /// <summary>
        /// 分析结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构信息</param>
        /// <param name="bytes">转义后的数据</param>
        /// <param name="offset">偏移量</param>
        public virtual void AnalysisStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure, ReadOnlySpan<byte> bytes, ref int offset)
        {
            try
            {
                if (structure.StructureType == StructureType.empty)
                    return;

                if (!obj.ContainsProperty(structure.Property.Split('.')))
                    throw new JTTException($"在类型 {obj.GetType().FullName}中未找到指定属性 {structure.Property}.");

                if (structure.IsCollection)
                {
                    IList collection = null;

                    var count = (UInt32)Convert.ChangeType(obj.GetPropertyValue(structure.Collection.CountProperty.Split('.')), typeof(UInt32));

                    for (int i = 0; i < count; i++)
                    {
                        object value = Analysis(bytes, ref offset);

                        if (value == null)
                            return;

                        if (collection == null)
                            collection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(value.GetType()));

                        collection.Add(value);
                    }

                    obj.SetValueToProperty(structure.Property.Split('.'), collection);
                }
                else
                {
                    object value = Analysis(bytes, ref offset);
                    if (value == null)
                        return;

                    obj.SetValueToProperty(structure.Property.Split('.'), value);
                }
            }
            catch (Exception ex)
            {
                throw new JTTException($"分析结构时发生错误, structureId: {structure.Id}.", ex);
            }

            object Analysis(ReadOnlySpan<byte> bytes, ref int offset)
            {
                Decrypt(packageInfo, bytes, structure, offset);

                switch (structure.StructureType)
                {
                    case StructureType.@internal:
                        return AnalysisInternalStructure(packageInfo, obj, structure, bytes, ref offset);
                    case StructureType.additional:
                        AnalysisAdditionalStructure(packageInfo, obj, structure, bytes, ref offset);
                        return null;
                    case StructureType.@normal:
                    default:
                        return AnalysisNormalStructure(obj, structure, bytes, ref offset);
                }
            }
        }

        /// <summary>
        /// 分析普通结构
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="bytes">转义后的数据</param>
        /// <param name="offset">偏移量</param>
        /// <returns>获得的数据</returns>
        public virtual object AnalysisNormalStructure(object obj, StructureInfo structure, ReadOnlySpan<byte> bytes, ref int offset)
        {
            object value;

            //取值
            var _bytes = new byte[structure.Length.Value];
            Buffer.BlockCopy(
                bytes.ToArray(),
                offset,
                _bytes,
                0,
                structure.Length.Value);

            //解码
            value = handler.Decode(_bytes, structure.Encode);

            var tasks = new List<Action>
            {
                () =>
                {
                    //动态计算
                    if (structure.NeedCompile)
                        CompileDynamic(obj, structure, value);
                },
                () =>
                { 
                    //数据映射
                    if (structure.NeedDataMapping)
                        DataMapping(obj, structure, _bytes, value);
                },
                ()=>
                {
                    //位标识数组转为结构体数据
                    if (structure.NeedToFlagStruct)
                        ToFlagStruct(obj, structure, value);
                }
            };

            if (!structure.CompileFirst)
                tasks.Reverse();

            tasks.ForEach(o => o.Invoke());

            offset += structure.Length.Value;
            return value;
        }

        /// <summary>
        /// 分析内部结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="bytes">转义后的数据</param>
        /// <param name="offset">偏移量</param>
        /// <returns>内部结构映射的实例</returns>
        public virtual object AnalysisInternalStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure, ReadOnlySpan<byte> bytes, ref int offset)
        {
            try
            {
                object value;
                var internalKey = string.Empty;
                if (structure.InternalKey == null)
                    internalKey = structure.Internal.First().Key;
                else
                {
                    object propertyValue;
                    if (structure.InternalKey.Property.IndexOf('.') == 0)
                        propertyValue = obj.GetPropertyValue(structure.InternalKey.Property.TrimStart('.').Split('.'));
                    else
                        propertyValue = packageInfo.GetPropertyValue(structure.InternalKey.Property.Split('.'));

                    if (structure.InternalKey.Encode != null)
                        propertyValue = handler.Encode(propertyValue, structure.InternalKey.Encode);
                    if (structure.InternalKey.Decode != null)
                        propertyValue = handler.Decode((byte[])propertyValue, structure.InternalKey.Decode);

                    internalKey = (string)propertyValue;
                }

                if (!protocol.InternalEntitysMappings.ContainsKey(internalKey))
                    throw new JTTException($"未找到指定的内部结构, InternalKey: {internalKey}.");

                //实例化内部结构的映射的实体类
                var internalEntityMapping = protocol.InternalEntitysMappings[internalKey];

                if (structure.InternalLength?.ContainsKey(internalKey) == true)
                {
                    object length;
                    if (structure.InternalLength[internalKey].Property.IndexOf('.') == 0)
                        length = obj.GetPropertyValue(structure.InternalLength[internalKey].Property.TrimStart('.').Split('.'));
                    else
                        length = packageInfo.GetPropertyValue(structure.InternalLength[internalKey].Property.Split('.'));

                    structure.Length = (int)Convert.ChangeType(length, typeof(UInt32));
                }
                else
                    structure.Length = internalEntityMapping.Length;

                var internalEntityAssembly = string.IsNullOrEmpty(internalEntityMapping.Assembly) ?
                    Assembly.GetExecutingAssembly() :
                    Assembly.Load(internalEntityMapping.Assembly);

                value = internalEntityAssembly.CreateInstance(internalEntityMapping.TypeName, true);

                //递归处理内部结构
                var internalOffset = offset;
                foreach (var internalStructure in structure.Internal[internalKey])
                {
                    if (internalOffset - offset == structure.Length)
                        break;

                    AnalysisStructure(packageInfo, value, internalStructure, bytes, ref internalOffset);
                };
                offset = internalOffset;

                return value;
            }
            catch (Exception ex)
            {
                throw new JTTException($"分析内部结构时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        /// <summary>
        /// 分析附加信息
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="bytes">转义后的数据</param>
        /// <param name="offset">偏移量</param>
        public virtual void AnalysisAdditionalStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure, ReadOnlySpan<byte> bytes, ref int offset)
        {
            do
            {
                try
                {
                    var _bytes = new byte[structure.Additional.Length];
                    Buffer.BlockCopy(
                        bytes.ToArray(),
                        offset,
                        _bytes,
                        0,
                        structure.Additional.Length);

                    var key = (string)handler.Decode(_bytes, structure.Additional.Decode);
                    if (!structure.Additional.Structures.ContainsKey(key))
                        break;

                    offset += structure.Additional.Length;

                    Buffer.BlockCopy(
                        bytes.ToArray(),
                        offset,
                        _bytes,
                        0,
                        1);

                    var length = (byte)handler.Decode(_bytes, new CodeInfo() { CodeType = CodeType.@byte });

                    offset++;

                    var additionalStructure = structure.Additional.Structures[key];
                    additionalStructure.Length = length;

                    AnalysisStructure(packageInfo, obj, additionalStructure, bytes, ref offset);
                }
                catch (Exception ex)
                {
                    throw new JTTException($"分析附加结构时发生错误, structureId: {structure.Id}.", ex);
                }
            } while (offset < bytes.Length);
        }

        /// <summary>
        /// 计算动态表达式
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="value">值</param>
        /// <param name="valueType">值类型</param>
        /// <param name="resultType">计算结果数据类型</param>
        /// <returns></returns>
        public virtual void CompileDynamic(object obj, StructureInfo structure, object value, Type valueType = null, Type resultType = null)
        {
            try
            {
                expressionContext.Variables.Clear();
                expressionContext.Variables[structure.Property] = value;
                if (valueType != null)
                    expressionContext.Variables.DefineVariable(structure.Property, valueType);
                if (resultType != null)
                    expressionContext.Options.ResultType = resultType;

                var result = expressionContext.CompileDynamic(structure.Compile.Expression).Evaluate();

                obj.SetValueToProperty(structure.Compile.Property.Split('.'), result);
            }
            catch (Exception ex)
            {
                throw new JTTException($"计算动态表达式时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        /// <summary>
        /// 数据映射
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="bytes">原始值</param>
        /// <param name="value">解码值</param>
        /// <returns></returns>
        public virtual void DataMapping(object obj, StructureInfo structure, ReadOnlySpan<byte> bytes, object value)
        {
            try
            {
                if (!protocol.DataMappings.ContainsKey(structure.DataMapping.Key))
                    throw new JTTException($"未找到指定的数据映射标识, DataMappingKey: {structure.DataMapping.Key}.");

                object matching;
                if (structure.DataMapping.UseDecodeValue)
                {
                    matching = value;
                    if (structure.DataMapping.Encode != null)
                        matching = handler.Encode(matching, structure.DataMapping.Encode);

                    if (structure.DataMapping.Decode != null)
                        matching = handler.Decode((byte[])matching, structure.DataMapping.Decode);
                }
                else
                {
                    if (structure.DataMapping.Decode != null)
                        matching = handler.Decode(bytes.ToArray(), structure.DataMapping.Decode);
                    else
                        matching = bytes.ToArray();
                }

                var result = protocol.DataMappings[structure.DataMapping.Key]
                     .Find(o => o.Matching.Equals(matching));

                if (result == null)
                    throw new JTTException($"数据映射时发生错误，未匹配到数据, DataMappingKey: {structure.DataMapping.Key}, Matching: {matching}.");

                obj.SetValueToProperty(structure.DataMapping.SetProperty.Split('.'), result.Value);
            }
            catch (Exception ex)
            {
                throw new JTTException($"数据映射时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        /// <summary>
        /// 将位标识数组转为结构体数据
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <param name="value">解码值</param>
        /// <returns></returns>
        public virtual void ToFlagStruct(object obj, StructureInfo structure, object value)
        {
            try
            {
                if (structure.Encode.CodeType != CodeType.binary)
                    throw new JTTException($"数据的编码方式应该为 {CodeType.binary}.");

                var flagStructAssembly = string.IsNullOrEmpty(structure.FlagStruct.Assembly) ?
                    Assembly.GetExecutingAssembly() :
                    Assembly.Load(structure.FlagStruct.Assembly);

                var flagStructType = flagStructAssembly.GetType(structure.FlagStruct.TypeName);

                var flagStruct = ((BitArray)value).GetFlagStruct(flagStructType);

                obj.SetValueToProperty(structure.FlagStruct.Property.Split('.'), flagStruct);
            }
            catch (Exception ex)
            {
                throw new JTTException($"将位标识数据转为结构体数据时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="buffer">数据</param>
        /// <param name="structure">结构</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public abstract void Decrypt(IJTTPackageInfo packageInfo, ReadOnlySpan<byte> buffer, StructureInfo structure, int offset);

        #endregion

        #region 私有成员

        /// <summary>
        /// JTT协议
        /// </summary>
        protected readonly IJTTProtocol protocol;

        /// <summary>
        /// JTT协议处理类
        /// </summary>
        protected readonly IJTTProtocolHandler handler;

        /// <summary>
        /// 汉字编码
        /// </summary>
        protected readonly Encoding zhcnencoding;

        /// <summary>
        /// 计算表达式上下文
        /// </summary>
        protected readonly ExpressionContext expressionContext;

        #endregion
    }
}
