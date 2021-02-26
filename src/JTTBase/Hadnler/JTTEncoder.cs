using Flee.PublicTypes;
using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTTBase.Hadnler
{
    /// <summary>
    /// JTT协议编码器
    /// </summary>
    public abstract class JTTEncoder : IJTTEncoder
    {
        public JTTEncoder(IJTTProtocol _protocol)
        {
            protocol = _protocol;

            handler = protocol.GetHandler();

            zhcnencoding = handler.GetZHCNEncoding();

            expressionContext = new ExpressionContext();
            expressionContext.Imports.AddType(typeof(Math));
        }

        #region 公共方法

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="pack"></param>
        /// <returns></returns>
        public virtual int Encode(IBufferWriter<byte> writer, IJTTPackageInfo pack)
        {
            if (pack == null)
                throw new JTTException("消息包是空的.");

            if (protocol.Structures?.Any() != true)
                throw new JTTException($"未找到任何结构信息, 请检查协议的Json配置文件.");

            var packageInfo = pack;

            SetupPackInfo(packageInfo);

            //帧头
            packageInfo.HeadFlag = handler.GetHeadFlagValue();
            writer.Write(packageInfo.HeadFlag.ToArray());

            //消息头+消息体
            var body = Analysis(packageInfo);
            packageInfo.Buffer = new ReadOnlySequence<byte>(body);
            var body_Escaped = handler.Escape(body);
            writer.Write(body_Escaped);

            //CRC校验码
            var crc = handler.ComputeCrcValue(body);
            packageInfo.Crc_Code = new ReadOnlyMemory<byte>(crc);
            var crc_Escaped = handler.Escape(crc);
            writer.Write(crc_Escaped);

            //帧尾
            packageInfo.EndFlag = handler.GetEndFlagValue();
            writer.Write(packageInfo.EndFlag.ToArray());

            return packageInfo.HeadFlag.Length + body_Escaped.Length + crc_Escaped.Length + packageInfo.EndFlag.Length;
        }

        /// <summary>
        /// 设置消息包
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        public abstract void SetupPackInfo(IJTTPackageInfo packageInfo);

        /// <summary>
        /// 分析结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        public abstract byte[] Analysis(IJTTPackageInfo packageInfo);

        /// <summary>
        /// 计算动态表达式
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="structure">结构</param>
        /// <param name="valueType">值类型</param>
        /// <param name="resultType">计算结果数据类型</param>
        /// <returns></returns>
        public virtual object CompileDynamic(object value, StructureInfo structure, Type valueType = null, Type resultType = null)
        {
            try
            {
                expressionContext.Variables.Clear();
                expressionContext.Variables[structure.Property] = value;
                if (valueType != null)
                    expressionContext.Variables.DefineVariable(structure.Property, valueType);
                if (resultType != null)
                    expressionContext.Options.ResultType = resultType;

                return expressionContext.CompileDynamic(structure.Compile.RestoreExpression).Evaluate();
            }
            catch (Exception ex)
            {
                throw new JTTException($"计算动态表达式时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        #region 由于映射值可能存在重复, 所以不再进行反向映射, 请直接对原始值赋值

        ///// <summary>
        ///// 数据映射
        ///// </summary>
        ///// <param name="obj">当前对象</param>
        ///// <param name="value">值</param>
        ///// <param name="structure">结构</param>
        ///// <returns></returns>
        //public virtual object DataMapping(object obj, object value, StructureInfo structure)
        //{
        //    try
        //    {
        //        if (!protocol.DataMappings.ContainsKey(structure.DataMapping.Key))
        //            throw new JTTException($"未找到指定的数据映射标识, DataMappingKey: {structure.DataMapping.Key}.");

        //        var matching = value;
        //        if (!structure.DataMapping.UseDecodeValue)
        //        {
        //            if (structure.DataMapping.Encode != null)
        //                matching = handler.Encode(matching, structure.DataMapping.Encode, structure.Length);
        //            if (structure.DataMapping.Decode != null)
        //                matching = handler.Decode((byte[])matching, structure.DataMapping.Decode);
        //        }

        //        var data = protocol.DataMappings[structure.DataMapping.Key]
        //                .Find(o => o.Matching.ToString() == matching.ToString());

        //        if (data == null)
        //            throw new JTTException($"数据映射时发生错误，未匹配到数据, DataMappingKey: {structure.DataMapping.Key}, Value: {value}.");

        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new JTTException($"数据映射时发生错误, structureId: {structure.Id}.", ex);
        //    }
        //}

        #endregion

        /// <summary>
        /// 将结构体数据转为位标识数组
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <returns></returns>
        public virtual object ToFlagStruct(object obj, StructureInfo structure)
        {
            try
            {
                if (structure.Encode.CodeType != CodeType.binary)
                    throw new JTTException($"数据的编码方式应该为 {CodeType.binary}.");

                var flagStructAssembly = string.IsNullOrEmpty(structure.FlagStruct.Assembly) ?
                    Assembly.GetExecutingAssembly() :
                    Assembly.Load(structure.FlagStruct.Assembly);

                var flagStructType = flagStructAssembly.GetType(structure.FlagStruct.TypeName);

                var value = obj.GetPropertyValue(structure.FlagStruct.Property.Split('.'));

                var type = value.GetType();
                if (type != flagStructType)
                    throw new JTTException($"数据的类型 {type.FullName}与配置所指定的类型 {flagStructType.FullName}不匹配.");

                return value.GetBitArray(flagStructType);
            }
            catch (Exception ex)
            {
                throw new JTTException($"将位标识数据转为结构体数据时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="buffer">数据</param>
        /// <param name="structure">结构</param>
        /// <returns></returns>
        public abstract void Encrypt(IJTTPackageInfo packageInfo, byte[] buffer, StructureInfo structure);

        #endregion

        #region 私有方法

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

        /// <summary>
        /// 分析结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <returns></returns>
        protected byte[] AnalysisStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure)
        {
            try
            {
                if (structure.StructureType == StructureType.empty)
                    return Array.Empty<byte>();

                if (structure.StructureType != StructureType.additional
                    && !obj.ContainsProperty(structure.Property.Split('.')))
                    throw new JTTException($"在类型 {obj.GetType().FullName}中未找到指定属性 {structure.Property}.");

                byte[] value = Array.Empty<byte>();

                if (structure.IsCollection)
                {
                    var list = (IList)obj.GetPropertyValue(structure.Property.Split('.'));

                    var bytes = new List<byte>();

                    var count = (list?.Count) ?? 0;

                    if (count > 0)
                        foreach (var item in list)
                        {
                            var item_bytes = Analysis(item);
                            bytes.AddRange(item_bytes);
                        }

                    //var fieldWithMultiLevel = structure.Collection.CountProperty.Split('.');
                    //var countProperty = obj.GetProperty(fieldWithMultiLevel);
                    //obj.SetValueToProperty(fieldWithMultiLevel, Convert.ChangeType(count, countProperty.PropertyType));

                    value = bytes.ToArray();
                }
                else
                    value = Analysis(obj);

                Encrypt(packageInfo, value, structure);

                return value;
            }
            catch (Exception ex)
            {
                throw new JTTException($"分析结构时发生错误, structureId: {structure.Id}.", ex);
            }

            byte[] Analysis(object obj)
            {
                return structure.StructureType switch
                {
                    StructureType.@internal => AnalysisInternalStructure(packageInfo, obj, structure),
                    StructureType.additional => AnalysisAdditionalStructure(packageInfo, obj, structure),
                    _ => AnalysisNormalStructure(obj, structure),
                };
            }
        }

        /// <summary>
        /// 分析普通结构
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="structure">结构</param>
        /// <returns>获得的数据</returns>
        protected byte[] AnalysisNormalStructure(object obj, StructureInfo structure)
        {
            var value = obj.GetPropertyValue(structure.Property.Split('.'));

            var tasks = new List<Action>
            {
                () =>
                {
                    //动态计算
                    if (structure.NeedCompile)
                       value = CompileDynamic(value, structure);
                },
                () =>
                {
                    ////数据映射
                    //if (structure.NeedDataMapping)
                    //  value =  DataMapping(obj, value, structure);
                },
                () =>
                {
                    //结构体数据转为位标识数组
                    if (structure.NeedToFlagStruct)
                       value = ToFlagStruct(obj, structure);
                }
            };

            if (structure.CompileFirst)
                tasks.Reverse();

            tasks.ForEach(o => o.Invoke());

            //if (value == null && structure.NeedDataMapping && !structure.DataMapping.UseDecodeValue)
            //    return (byte[])value;

            return handler.Encode(value, structure.Encode, structure.Length);
        }

        /// <summary>
        /// 分析内部结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="obj">当前实例</param>
        /// <param name="structure">结构</param>
        /// <returns>内部结构映射的实例</returns>
        protected byte[] AnalysisInternalStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure)
        {
            try
            {
                object internalEntity;
                if (structure.IsCollection)
                    internalEntity = obj;
                else
                    internalEntity = obj.GetPropertyValue(structure.Property.Split('.'));

                var typeName = internalEntity.GetType().FullName;

                if (!protocol.InternalEntitysMappings.Any(o => o.Value.TypeName == typeName))
                    throw new JTTException($"未找到指定的内部结构, structureId: {structure.Id}, TypeName: {typeName}.");

                var internalEntityMapping = protocol.InternalEntitysMappings.First(o => o.Value.TypeName == typeName);

                if (structure.InternalLength?.ContainsKey(internalEntityMapping.Key) == true)
                {
                    object length;
                    if (structure.InternalLength[internalEntityMapping.Key].Property.IndexOf('.') == 0)
                        length = obj.GetPropertyValue(structure.InternalLength[internalEntityMapping.Key].Property.TrimStart('.').Split('.'));
                    else
                        length = packageInfo.GetPropertyValue(structure.InternalLength[internalEntityMapping.Key].Property.Split('.'));

                    structure.Length = (int)Convert.ChangeType(length, typeof(UInt32));
                }
                else
                    structure.Length = internalEntityMapping.Value.Length;

                if (structure.InternalKey != null)
                {
                    object propertyValue = internalEntityMapping.Key;

                    if (structure.InternalKey.Encode != null)
                        propertyValue = handler.Encode(propertyValue, structure.InternalKey.Decode);
                    if (structure.InternalKey.Decode != null)
                        propertyValue = handler.Decode((byte[])propertyValue, structure.InternalKey.Encode);

                    if (structure.InternalKey.Property.IndexOf('.') == 0)
                        obj.SetValueToProperty(structure.InternalKey.Property.TrimStart('.').Split('.'), propertyValue);
                    else
                        packageInfo.SetValueToProperty(structure.InternalKey.Property.Split('.'), propertyValue);
                }

                using var ms = new MemoryStream();
                //处理内部结构
                foreach (var internalStructure in structure.Internal[internalEntityMapping.Key])
                {
                    if (ms.Length == structure.Length)
                        break;

                    ms.Write(AnalysisStructure(packageInfo, internalEntity, internalStructure));
                }
                return ms.ToArray();
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
        protected byte[] AnalysisAdditionalStructure(IJTTPackageInfo packageInfo, object obj, StructureInfo structure)
        {
            try
            {
                using var ms = new MemoryStream();
                foreach (var additionalStructure in structure.Additional.Structures)
                {
                    if (!obj.ContainsProperty(additionalStructure.Value.Property.Split('.')))
                        continue;

                    if (obj.GetPropertyValue(additionalStructure.Value.Property.Split('.')) == null)
                        continue;

                    var value = AnalysisStructure(packageInfo, obj, additionalStructure.Value);

                    ms.Write(handler.Encode(additionalStructure.Key, structure.Additional.Encode, structure.Length));
                    ms.Write(handler.Encode(Convert.ToByte(value.Length), new CodeInfo() { CodeType = CodeType.@byte }));
                    ms.Write(value);
                }
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                throw new JTTException($"分析附加结构时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        #endregion
    }
}
