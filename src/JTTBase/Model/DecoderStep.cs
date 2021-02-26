using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 解码步骤
    /// </summary>
    public static class DecoderStep
    {
        /// <summary>
        /// 无
        /// </summary>
        public const byte None = 0;

        /// <summary>
        /// 分析数据
        /// </summary>
        public const byte AnalysisBuffer = 1;

        /// <summary>
        /// 分析结构
        /// </summary>
        public const byte AnalysisStructure = 2;

        /// <summary>
        /// 结束
        /// </summary>
        public const byte Done = 3;
    }
}
