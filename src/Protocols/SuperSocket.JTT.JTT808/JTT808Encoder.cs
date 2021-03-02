using SuperSocket.JTT.JTT808.Internal;
using SuperSocket.JTT.Base.Extension;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuperSocket.JTT.JTT808
{
    public class JTT808Encoder : JTTEncoder
    {
        public JTT808Encoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt808protocol = protocol as JTT808Protocol;

            msg_sn = UInt16.MinValue;
        }

        #region 公共方法

        public override int Encode(IBufferWriter<byte> writer, IJTTPackageInfo pack)
        {
            var length = base.Encode(writer, pack);

            var jtt808packageInfo = pack as JTT808PackageInfo;

            jtt808packageInfo.SubPackages?.ForEach(o =>
            {
                writer.Write(o);
                length += o.Length;
            });

            return length;
        }

        public override void SetupPackInfo(IJTTPackageInfo packageInfo)
        {
            var jtt808packageInfo = packageInfo as JTT808PackageInfo;

            if (jtt808packageInfo.JTT808MessageHeader == null)
                throw new JTTException("设置消息包时发生错误：消息头不可为空[调用JTT808ProtocolHandler.GetMessageHeader()方法可获取初始化消息头].");

            //消息报文序列号
            jtt808packageInfo.JTT808MessageHeader.Msg_SN = GetMsgSN();
        }

        public override byte[] Analysis(IJTTPackageInfo packageInfo)
        {
            var jtt808packageInfo = packageInfo as JTT808PackageInfo;

            //先消息体
            var bytes_Body = AnalysisBodyStructure(packageInfo);

            byte[] bytes_Body_other = null;

            //如果数据过长，则需要分包（单包数据长度限制为1023）
            if (bytes_Body.Length > 1023)
            {
                bytes_Body_other = bytes_Body.Skip(1023).ToArray();
                bytes_Body = bytes_Body.Take(1023).ToArray();
            }

            //消息体长度
            jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo.Length = (UInt16)bytes_Body.Length;

            //后消息头
            var bytes_Header = AnalysisHeaderStructure(packageInfo);

            if (bytes_Body_other != null)
            {
                jtt808packageInfo.SubPackages = new List<byte[]>();

                //分包
                for (int i = 1023; i < bytes_Body_other.Length;)
                {
                    var take = bytes_Body_other.Length - i;

                    if (take > 1023)
                        take = 1023;

                    var bytes_Body_sub = bytes_Body_other.Skip(i).Take(take).ToArray();

                    i += 1023;

                    byte[] bytes_Header_sub;

                    if (take == 1023)
                        bytes_Header_sub = bytes_Header.ToArray();
                    else
                    {
                        var messageHeader = jtt808packageInfo.JTT808MessageHeader;
                        messageHeader.MsgBodyPropertyInfo.Length = (UInt16)take;
                        bytes_Header_sub = AnalysisHeaderStructure(messageHeader);
                    }

                    var bytes_full_sub = bytes_Header_sub.Concat(bytes_Body_sub).ToArray();

                    var bytes_sub = handler.GetHeadFlagValue().ToArray()
                        .Concat(bytes_full_sub)
                        .Concat(handler.Escape(handler.ComputeCrcValue(bytes_full_sub)))
                        .Concat(handler.GetEndFlagValue().ToArray())
                        .ToArray();

                    jtt808packageInfo.SubPackages.Add(bytes_sub);
                }
            }

            return bytes_Header.Concat(bytes_Body).ToArray();
        }

        public override void Encrypt(IJTTPackageInfo packageInfo, byte[] buffer, StructureInfo structure)
        {
            try
            {
                throw new NotImplementedException("还未实现RSA加密.");
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
        readonly JTT808Protocol jtt808protocol;

        /// <summary>
        /// 报文序列号
        /// </summary>
        UInt16 msg_sn;

        /// <summary>
        /// 获取报文序列号
        /// </summary>
        /// <returns></returns>
        UInt16 GetMsgSN()
        {
            if (msg_sn == UInt16.MaxValue)
                msg_sn = UInt16.MinValue;

            return ++msg_sn;
        }

        /// <summary>
        /// 分析消息体结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        byte[] AnalysisBodyStructure(IJTTPackageInfo packageInfo)
        {
            var jtt808packageInfo = packageInfo as JTT808PackageInfo;

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

            return result;
        }

        /// <summary>
        /// 分析消息头结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        byte[] AnalysisHeaderStructure(IJTTPackageInfo packageInfo)
        {
            var jtt808packageInfo = packageInfo as JTT808PackageInfo;

            //设置消息体属性值
            jtt808packageInfo.JTT808MessageHeader.MsgBodyProperty = jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo.GetValue();

            using var ms = new MemoryStream();

            var header = protocol.Structures.FirstOrDefault(o => o.IsHeader);

            if (header == null)
                throw new JTTException("分析结构时发生错误: 未找到消息头的结构信息.");

            ms.Write(AnalysisStructure(packageInfo, packageInfo, header));

            var result = ms.ToArray();

            return result;
        }

        /// <summary>
        /// 分析消息头结构
        /// </summary>
        /// <param name="header">消息头</param>
        /// <returns></returns>
        byte[] AnalysisHeaderStructure(JTT808MessageHeader header)
        {
            return AnalysisHeaderStructure(new JTT808PackageInfo { JTT808MessageHeader = header });
        }

        #endregion
    }
}
