using SuperSocket.JTT.JTT808.Const;
using SuperSocket.JTT.JTT808.Internal;
using SuperSocket.JTT.JTTBase.Hadnler;
using SuperSocket.JTT.JTTBase.Interface;
using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT808
{
    /// <summary>
    /// JTT808协议处理类
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTT808ProtocolHandler : JTTProtocolHandler
    {
        public JTT808ProtocolHandler(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt808protocol = protocol as JTT808Protocol;
        }

        #region 公共方法

        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <param name="tel">终端手机号码</param>
        /// <param name="version">协议版本号（为null时使用默认配置）</param>
        /// <param name="encrypt">报文是加密（默认值 false）</param>
        /// <returns></returns>
        public JTT808MessageHeader GetMessageHeader(string tel, byte? version = null, bool encrypt = false)
        {
            return new JTT808MessageHeader
            {
                Tel = tel,
                Version = version ?? jtt808protocol.DefaultVersion,
                MsgBodyPropertyInfo = new MsgBodyProperty
                {
                    EncryptType = encrypt ? EncryptType.RSA : EncryptType.不加密,
                    VersionFlag = true
                }
            };
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// JTT协议
        /// </summary>
        readonly JTT808Protocol jtt808protocol;

        #endregion
    }
}
