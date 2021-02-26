using SuperSocket.JTTBase.Hadnler;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT809
{
    /// <summary>
    /// JTT809协议处理类
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTT809ProtocolHandler : JTTProtocolHandler
    {
        public JTT809ProtocolHandler(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt809protocol = protocol as JTT809Protocol;
        }

        #region 公共方法

        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <param name="gnsscenterID">下级平台接入码</param>
        /// <param name="version">协议版本号（为null时使用默认配置）</param>
        /// <param name="encrypt">报文是加密（默认值 false）</param>
        /// <param name="encrtptKey">数据加密的密钥（encrypt为true时需要设置此参数）</param>
        /// <returns></returns>
        public JTT809MessageHeader GetMessageHeader(UInt32 gnsscenterID, byte[] version = null, bool encrypt = false, UInt32 encrtptKey = 0)
        {
            return new JTT809MessageHeader
            {
                Encrypt_Flag = encrypt,
                Encrtpt_Key = encrtptKey,
                Msg_GnsscenterID = gnsscenterID,
                Version_Flag = jtt809protocol.DefaultVersionFlag
            };
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// JTT协议
        /// </summary>
        readonly JTT809Protocol jtt809protocol;

        #endregion
    }
}
