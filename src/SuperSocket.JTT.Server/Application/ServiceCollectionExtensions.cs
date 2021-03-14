using SuperSocket.JTT.Server.Application;
using SuperSocket.JTT.Server.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.JTT.Server.Gen;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加JTT服务
        /// <para>单例模式</para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddJTT(
            this IServiceCollection services,
            Action<JTTGenOptions> setupAction = null)
        {
            //注册自定义配置程序，将高级配置（<WeChatGenOptions）应用于低级配置（WeChatServiceOptions）。
            services.AddTransient<IConfigureOptions<JTTProtocolOptions>, ConfigureJTTProtocolOptions>();

            //注册生成器和依赖
            services.AddTransient(s => s.GetRequiredService<IOptions<JTTGenOptions>>().Value);

            services.AddSingleton<IJTTProvider, JTTGenerator>();

            if (setupAction != null) services.ConfigureJTT(setupAction);

            return services;
        }

        /// <summary>
        /// 配置JTT服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        public static void ConfigureJTT(
            this IServiceCollection services,
            Action<JTTGenOptions> setupAction)
        {
            services.Configure(setupAction);
        }
    }
}
