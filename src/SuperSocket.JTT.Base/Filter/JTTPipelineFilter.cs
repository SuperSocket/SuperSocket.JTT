using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.JTT.Base.Extension;
using System.Buffers;
using SuperSocket.JTT.Base.Interface;

namespace SuperSocket.JTT.Base.Filter
{
    /// <summary>
    /// JTT协议流数据拦截器
    /// </summary>
    public class JTTPipelineFilter : PipelineFilterBase<IJTTPackageInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocol">JTT协议</param>
        public JTTPipelineFilter(IJTTProtocol protocol)
        {
            var handler = protocol.GetHandler();

            _beginMark = handler.GetHeadFlagValue();
            _endMark = handler.GetEndFlagValue();
            Decoder = protocol.Decoder;
        }

        readonly ReadOnlyMemory<byte> _beginMark;

        readonly ReadOnlyMemory<byte> _endMark;

        bool _foundBeginMark;

        public override IJTTPackageInfo Filter(ref SequenceReader<byte> reader)
        {
            if (!_foundBeginMark)
            {
                var beginMark = _beginMark.Span;

                tryAdvance:
                if (!reader.TryAdvanceTo(beginMark[0]))
                    return null;

                if (beginMark.Length > 1)
                    if (!reader.IsNext(beginMark[1..], advancePast: true))
                        goto tryAdvance;

                _foundBeginMark = true;
            }

            var endMark = _endMark.Span;

            if (!reader.TryReadTo(out ReadOnlySequence<byte> buffer, endMark, advancePastDelimiter: false))
            {
                return null;
            }

            reader.Advance(endMark.Length);
            return DecodePackage(ref buffer);
        }

        public override void Reset()
        {
            _foundBeginMark = false;
        }

        protected override IJTTPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return Decoder.Decode(ref buffer, Context);
        }
    }
}
