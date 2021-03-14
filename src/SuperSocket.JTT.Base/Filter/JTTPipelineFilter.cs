using SuperSocket.JTT.Base.Interface;
using SuperSocket.ProtoBase;
using System;

namespace SuperSocket.JTT.Base.Filter
{
    /// <summary>
    /// JTT协议流数据拦截器
    /// </summary>
    public class JTTPipelineFilter : BeginEndMarkPipelineFilter<IJTTPackageInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginMark">帧头</param>
        /// <param name="endMark">帧尾</param>
        public JTTPipelineFilter(ReadOnlyMemory<byte> beginMark, ReadOnlyMemory<byte> endMark)
            : base(beginMark, endMark)
        {

        }
    }
}
