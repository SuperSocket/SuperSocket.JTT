using Flee.PublicTypes;
using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Hadnler;
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

namespace SuperSocket.JTT809
{
    public class JTT809Encoder : JTTEncoder
    {
        public JTT809Encoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt809protocol = protocol as JTT809Protocol;

            msg_sn = UInt32.MinValue;
        }

        #region 公共方法

        public override void SetupPackInfo(IJTTPackageInfo packageInfo)
        {
            var jtt809packageInfo = packageInfo as JTT809PackageInfo;

            if (jtt809packageInfo.JTT809MessageHeader == null)
                throw new JTTException("设置消息包时发生错误：消息头不可为空[调用JTT809ProtocolHandler.GetMessageHeader()方法可获取初始化消息头].");

            //消息报文序列号
            jtt809packageInfo.JTT809MessageHeader.Msg_SN = GetMsgSN();
            //发送时间
            jtt809packageInfo.JTT809MessageHeader.Time = (UInt64)DateTime.Now.ToFileTimeUtc();
        }

        public override byte[] Analysis(IJTTPackageInfo packageInfo)
        {
            //先消息体
            var bytes_Body = AnalysisBodyStructure(packageInfo);

            //后消息头
            var bytes_Header = AnalysisHeaderStructure(packageInfo);

            return bytes_Header.Concat(bytes_Body).ToArray();
        }

        public override void Encrypt(IJTTPackageInfo packageInfo, byte[] buffer, StructureInfo structure)
        {
            try
            {
                if (protocol.Encrypt.Targets?.ContainsKey(structure.Id) != true)
                    return;

                var encryptProperty = protocol.Encrypt.Targets[structure.Id];
                if (!(bool)packageInfo.GetPropertyValue(encryptProperty.Flag.Split('.')))
                    return;

                //生成Key
                var key_bytes = new byte[4];
                new Random().NextBytes(key_bytes);
                var key = BitConverter.ToUInt32(key_bytes);

                //写入Key
                packageInfo.SetValueToProperty(encryptProperty.Key.Split('.'), key);

                using MemoryStream ms_encrypt = new MemoryStream();
                var writer = new BinaryWriter(ms_encrypt);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    var reader = new BinaryReader(ms);

                    while (ms.Position < ms.Length)
                    {
                        var @char = reader.ReadChar();

                        //将待传输的数据与伪随机码按字节进行异或运算
                        key = protocol.Encrypt.IA1 * (key % protocol.Encrypt.M1) + protocol.Encrypt.IC1;
                        @char ^= (Char)((key >> 20) & 0xff);

                        //写入加密的数据
                        writer.Write(@char);
                    }
                }
                buffer = ms_encrypt.ToArray();
            }
            catch (Exception ex)
            {
                throw new JTTException($"加密时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// JTT协议
        /// </summary>
        readonly JTT809Protocol jtt809protocol;

        /// <summary>
        /// 报文序列号
        /// </summary>
        UInt32 msg_sn;

        /// <summary>
        /// 获取报文序列号
        /// </summary>
        /// <returns></returns>
        uint GetMsgSN()
        {
            if (msg_sn == UInt32.MaxValue)
                msg_sn = UInt32.MinValue;

            return ++msg_sn;
        }

        /// <summary>
        /// 分析消息体结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        byte[] AnalysisBodyStructure(IJTTPackageInfo packageInfo)
        {
            var jtt809packageInfo = packageInfo as JTT809PackageInfo;

            using var ms = new MemoryStream();

            var body = protocol.Structures.FirstOrDefault(o => o.IsBody);

            if (body == null)
                throw new JTTException("分析结构时发生错误: 未找到消息体的结构信息.");

            ////写入协议号
            //var typeName = packInfo.MessageBody.GetType().FullName;
            //if (!protocol.InternalEntitysMappings.Any(o => o.Value.TypeName == typeName))
            //    throw new JTTException($"分析结构时发生错误: 未找到消息体的内部结构映射配置, TypeName: {typeName}.");
            //var key = protocol.InternalEntitysMappings.First(o => o.Value.TypeName == typeName).Key;
            //packInfo.SetValueToProperty(body.InternalKey.Property.Split('.'), key);

            ms.Write(AnalysisStructure(packageInfo, packageInfo, body));

            var result = ms.ToArray();

            //消息长度
            jtt809packageInfo.JTT809MessageHeader.Msg_Length = (UInt32)result.Length;

            return result;
        }

        /// <summary>
        /// 分析消息头结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        byte[] AnalysisHeaderStructure(IJTTPackageInfo packageInfo)
        {
            var jtt809packageInfo = packageInfo as JTT809PackageInfo;

            using var ms = new MemoryStream();

            var header = protocol.Structures.FirstOrDefault(o => o.IsHeader);

            if (header == null)
                throw new JTTException("分析结构时发生错误: 未找到消息头的结构信息.");

            //消息长度
            jtt809packageInfo.JTT809MessageHeader.Msg_Length += (UInt32)(header.Length ?? 0);

            ms.Write(AnalysisStructure(packageInfo, packageInfo, header));

            var result = ms.ToArray();

            return result;
        }

        #endregion
    }
}
