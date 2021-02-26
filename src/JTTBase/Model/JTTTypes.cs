using SuperSocket.JTTBase.Filter;
using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{

    public static class JTTTypes
    {
        public static Type Protocol = typeof(IJTTProtocol);

        public static Type PipelineFilter = typeof(JTTPipelineFilter);

        public static Type PackageInfo = typeof(IJTTPackageInfo);

        public static Type MessageHeader = typeof(IJTTMessageHeader);

        public static Type Encoder = typeof(IJTTEncoder);

        public static Type Decoder = typeof(IJTTDecoder);
    }
}
