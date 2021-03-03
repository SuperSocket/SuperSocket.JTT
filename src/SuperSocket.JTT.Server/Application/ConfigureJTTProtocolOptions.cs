using SuperSocket.JTT.Server.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Server.Application
{
    /// <summary>
    /// 
    /// </summary>
    internal class ConfigureJTTProtocolOptions : IConfigureOptions<JTTProtocolOptions>
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly JTTGenOptions GenOptions;

        public ConfigureJTTProtocolOptions(
            IServiceProvider serviceProvider,
            IOptions<JTTGenOptions> jTTGenOptionsAccessor)
        {
            ServiceProvider = serviceProvider;
            GenOptions = jTTGenOptionsAccessor.Value;
        }

        public void Configure(JTTProtocolOptions options)
        {
            DeepCopy(GenOptions.ProtocolOptions, options);
        }

        private void DeepCopy(JTTProtocolOptions source, JTTProtocolOptions target)
        {
            target = JsonConvert.DeserializeObject<JTTProtocolOptions>(JsonConvert.SerializeObject(source));
        }
    }
}
