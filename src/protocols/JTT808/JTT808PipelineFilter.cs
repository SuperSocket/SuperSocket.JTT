using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using SuperSocket.JTTBase.Filter;
using SuperSocket.JTTBase.Interface;

namespace SuperSocket.JTT808
{
    /// <summary>
    /// JTT808协议流数据拦截器
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTT808PipelineFilter : JTTPipelineFilter
    {
        public JTT808PipelineFilter(IJTTProtocol protocol)
            : base(protocol)
        {

        }
    }
}
