using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
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
