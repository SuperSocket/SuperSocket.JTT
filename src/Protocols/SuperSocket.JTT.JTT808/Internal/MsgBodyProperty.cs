using SuperSocket.JTT.Base.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocket.JTT.JTT808.Internal
{
    /// <summary>
    /// 消息体属性
    /// </summary>
    /// <remarks>JTT808-2019表图2</remarks>
    public class MsgBodyProperty
    {
        public MsgBodyProperty()
        {

        }

        public MsgBodyProperty(UInt16 value)
        {
            Length = (UInt16)(value & 0x3ff);
            EncryptType = ((value & 0x1c00) >> 10) == 1
                ? Const.EncryptType.RSA
                : Const.EncryptType.不加密;
            SubPackage = ((value & 0x2000) >> 13) == 1;
            VersionFlag = ((value & 0x4000) >> 14) == 1;
            Retain = ((value & 0x8000) >> 15) == 1;
        }

        /// <summary>
        /// 获取消息体属性值
        /// </summary>
        /// <returns></returns>
        public UInt16 GetValue()
        {
            return (UInt16)(
                  ((Retain ? 1 : 0) << 15)
                  | ((VersionFlag ? 1 : 0) << 14)
                  | ((SubPackage ? 1 : 0) << 13)
                  | ((EncryptType == Const.EncryptType.RSA ? 1 : 0) << 10)
                  | Length
                  );
        }

        /// <summary>
        /// 消息体长度
        /// </summary>
        public UInt16 Length { get; set; }

        /// <summary>
        /// 数据加密方式
        /// </summary>
        /// <remarks>
        /// <para>bit10-bit12为数据加密标识位；</para>
        /// <para>当次三位都为0，表示消息体不加密；</para>
        /// <para>当第10位为1，表示消息体经过RSA算法加密；</para>
        /// <para>其他位为保留位</para>
        /// </remarks>
        public string EncryptType { get; set; }

        /// <summary>
        /// <para>true 消息体为长消息，进行分包发送处理，具体分包信息由消息包封装项决定</para>
        /// <para>false 消息头中无消息包封装项字段</para>
        /// </summary>
        public bool SubPackage { get; set; }

        /// <summary>
        /// <para>true 表示协议为2011年的版本，
        /// 该版本未引入版本标识功能</para>
        /// <para>false 表示协议已经引入版本标识功能，
        /// 并且在终端鉴权报文的鉴权码后跟随着协议版本号，
        /// 引入版本标识功能后初始版本号为1，
        /// 后续每次关键性修订版本号都会递增</para>
        /// </summary>
        public bool VersionFlag { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        public bool Retain { get; set; }
    }
}
