using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody
{
    /// <summary>
    /// 主链路时效口令交互消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_AUTHORIZE_MSG"/></para>
    /// <para>共2+字节</para>
    /// <para>JTT1078-2016 10.1章节</para>
    /// </remarks>
    public class UpAuthorizeMsgBody : IJTTMessageBody
    {
        /// <summary>
        /// 子业务类型标识
        /// <see cref="Const.SubDataType"/>
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 DataType { get; set; }

        /// <summary>
        /// 子业务类型标识
        /// </summary>
        /// <remarks>映射值</remarks>
        public string DataType_Mapping { get; set; }

        /// <summary>
        /// 子业务数据体
        /// </summary>
        /// <remarks>
        /// <para>时效口令上报消息数据体<see cref="Internal.AuthorizeStartupBody"/></para>
        /// <para>时效口令请求消息数据体<see cref="Internal.AuthorizeStartupRequestBody"/></para>
        /// </remarks>
        public IJTTMessageBody SubBody { get; set; }
    }
}
