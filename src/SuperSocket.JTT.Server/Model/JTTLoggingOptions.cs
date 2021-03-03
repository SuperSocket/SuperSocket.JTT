using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Server.Model
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class JTTLoggingOptions
    {
        /// <summary>
        /// 日志构造器
        /// </summary>
        public ILoggerProvider Provider { get; set; }

        /// <summary>
        /// 添加控制台日志
        /// </summary>
        public bool AddConsole { get; set; }

        /// <summary>
        /// 添加调试日志
        /// </summary>
        public bool AddDebug { get; set; }
    }
}
