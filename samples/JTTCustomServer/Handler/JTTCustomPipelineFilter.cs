using JTTCustomServer.Model.Config;
using Microservice.Library.Container;
using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Interface;
using System;
using System.Buffers;

namespace JTTCustomServer.Handler
{
    /// <summary>
    /// 自定义JTT协议流数据拦截器
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTTCustomPipelineFilter : JTTPipelineFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginMark">帧头</param>
        /// <param name="endMark">帧尾</param>
        public JTTCustomPipelineFilter(ReadOnlyMemory<byte> beginMark, ReadOnlyMemory<byte> endMark)
            : base(beginMark, endMark)
        {

        }
    }
}
