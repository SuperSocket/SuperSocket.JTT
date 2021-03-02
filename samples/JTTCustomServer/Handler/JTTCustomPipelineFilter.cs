using JTTCustomServer.Model.Config;
using Microservice.Library.Container;
using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Interface;
using System.Buffers;

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

#pragma warning disable IDE0052 // 删除未读的私有成员
        readonly SystemConfig Config = AutofacHelper.GetService<SystemConfig>();
#pragma warning restore IDE0052 // 删除未读的私有成员

        protected override IJTTPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return base.DecodePackage(ref buffer);
        }
    }
}
