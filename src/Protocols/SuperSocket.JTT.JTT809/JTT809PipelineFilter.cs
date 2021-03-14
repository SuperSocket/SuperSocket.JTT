using SuperSocket.JTT.Base.Filter;
using System;

namespace SuperSocket.JTT.JTT809
{
    /// <summary>
    /// JTT809协议流数据拦截器
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTT809PipelineFilter : JTTPipelineFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginMark">帧头</param>
        /// <param name="endMark">帧尾</param>
        public JTT809PipelineFilter(ReadOnlyMemory<byte> beginMark, ReadOnlyMemory<byte> endMark)
            : base(beginMark, endMark)
        {

        }
    }
}
