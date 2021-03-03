﻿using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;

namespace JTTServer.MessageBody
{
    /// <summary>
    /// 获取转发终端清单请求应答消息数据体
    /// </summary>
    public class GetForwardEndpointReplyBody : IJTTMessageBody
    {
        /// <summary>
        /// 终端总数
        /// </summary>
        /// <remarks>4字节</remarks>
        public UInt32 Total { get; set; }

        /// <summary>
        /// 终端信息
        /// </summary>
        public List<ForwardEndpoint> Endpoints { get; set; }
    }
}
