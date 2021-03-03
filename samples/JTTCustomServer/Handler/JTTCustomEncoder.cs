using JTTCustomServer.Model;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using System;
using System.Buffers;
using System.IO;
using System.Linq;

namespace JTTCustomServer.Handler
{
    /// <summary>
    /// 自定义JTT协议编码器
    /// </summary>
    public class JTTCustomEncoder : JTTEncoder
    {
        public JTTCustomEncoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jttCustomprotocol = protocol as JTTCustomProtocol;

            msg_sn = UInt16.MinValue;
        }

        #region 公共方法

        public override void SetupPackInfo(IJTTPackageInfo packageInfo)
        {
            var jttCustompackageInfo = packageInfo as JTTCustomPackageInfo;

            if (jttCustompackageInfo.JTTCustomMessageHeader == null)
                throw new JTTException("设置消息包时发生错误：消息头不可为空[调用JTTCustomProtocolHandler.GetMessageHeader()方法可获取初始化消息头].");

            //消息报文序列号
            jttCustompackageInfo.JTTCustomMessageHeader.Msg_SN = GetMsgSN();
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
                //无需加密
                return;
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
#pragma warning disable IDE0052 // 删除未读的私有成员
        readonly JTTCustomProtocol jttCustomprotocol;
#pragma warning restore IDE0052 // 删除未读的私有成员

        /// <summary>
        /// 报文序列号
        /// </summary>
        UInt16 msg_sn;

        /// <summary>
        /// 获取报文序列号
        /// </summary>
        /// <returns></returns>
        ushort GetMsgSN()
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
            var jttCustomPackageInfo = packageInfo as JTTCustomPackageInfo;

            using var ms = new MemoryStream();

            var body = protocol.Structures.FirstOrDefault(o => o.IsBody);

            if (body == null)
                throw new JTTException("分析结构时发生错误: 未找到消息体的结构信息.");

            ms.Write(AnalysisStructure(packageInfo, packageInfo, body));

            var result = ms.ToArray();

            //消息体长度写入消息头
            jttCustomPackageInfo.JTTCustomMessageHeader.Msg_Length = (UInt16)result.Length;

            return result;
        }

        /// <summary>
        /// 分析消息头结构
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        byte[] AnalysisHeaderStructure(IJTTPackageInfo packageInfo)
        {
            var jttCustomPackageInfo = packageInfo as JTTCustomPackageInfo;

            using var ms = new MemoryStream();

            var header = protocol.Structures.FirstOrDefault(o => o.IsHeader);

            if (header == null)
                throw new JTTException("分析结构时发生错误: 未找到消息头的结构信息.");

            //追加消息头长度
            jttCustomPackageInfo.JTTCustomMessageHeader.Msg_Length += (UInt16)(header.Length ?? 0);

            ms.Write(AnalysisStructure(packageInfo, packageInfo, header));

            var result = ms.ToArray();

            return result;
        }

        #endregion
    }
}
