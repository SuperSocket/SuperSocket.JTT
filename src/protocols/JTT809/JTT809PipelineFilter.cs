using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using SuperSocket.JTT.JTTBase.Filter;
using SuperSocket.JTT.JTTBase.Interface;

namespace SuperSocket.JTT.JTT809
{
    /// <summary>
    /// JTT809协议流数据拦截器
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTT809PipelineFilter : JTTPipelineFilter
    {
        public JTT809PipelineFilter(IJTTProtocol protocol)
            : base(protocol)
        {

        }
    }
}
