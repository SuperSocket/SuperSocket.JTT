using JTTCustomServer.Model.Config;
using Library.Container;
using SuperSocket.JTTBase.Filter;
using SuperSocket.JTTBase.Interface;
using System;
using System.Buffers;
using System.Linq;

namespace JTTCustomServer.Handler
{
    /// <summary>
    /// 自定义JTT协议流数据拦截器
    /// <!--可以重写或新增方法-->
    /// </summary>
    public class JTTCustomPipelineFilter : JTTPipelineFilter
    {
        public JTTCustomPipelineFilter(IJTTProtocol protocol)
            : base(protocol)
        {

        }

        readonly SystemConfig Config = AutofacHelper.GetService<SystemConfig>();

        protected override IJTTPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return base.DecodePackage(ref buffer);
        }
    }
}
