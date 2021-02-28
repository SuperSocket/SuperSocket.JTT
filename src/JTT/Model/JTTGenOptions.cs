using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Model
{
    /// <summary>
    /// JTT配置
    /// </summary>
    public class JTTGenOptions
    {
        /// <summary>
        /// 服务器配置
        /// </summary>
        public JTTServerOptions ServerOptions { get; set; } = new JTTServerOptions();

        /// <summary>
        /// 日志配置
        /// </summary>
        public JTTLoggingOptions LoggingOptions { get; set; } = new JTTLoggingOptions();

        /// <summary>
        /// 协议配置
        /// </summary>
        public JTTProtocolOptions ProtocolOptions { get; set; } = new JTTProtocolOptions();
    }
}
