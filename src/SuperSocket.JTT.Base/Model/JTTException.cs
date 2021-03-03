using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// JTT异常
    /// </summary>
    public class JTTException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="ex">原始异常</param>
        public JTTException(string message, Exception ex = null)
            : base(message, ex)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <param name="ex">原始异常</param>
        public JTTException(string title, string message, Exception ex = null)
            : base($"{title} : {message}", ex)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="buffer">流数据</param>
        /// <param name="ex">原始异常</param>
        public JTTException(string message, byte[] buffer, Exception ex = null)
            : base(message, ex)
        {
            Buffer = buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <param name="buffer">流数据</param>
        /// <param name="ex">原始异常</param>
        public JTTException(string title, string message, byte[] buffer, Exception ex = null)
            : base($"{title} : {message}", ex)
        {
            Buffer = buffer;
        }

        /// <summary>
        /// 流数据
        /// </summary>
        public byte[] Buffer { get; }
    }
}
