using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 附件数据信息
    /// </summary>
    public class AdditionalInfo
    {
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 解码信息
        /// </summary>
        public CodeInfo Decode { get; set; }

        /// <summary>
        /// 编码信息
        /// </summary>
        public CodeInfo Encode { get; set; }

        /// <summary>
        /// 结构信息
        /// </summary>
        public Dictionary<string, StructureInfo> Structures { get; set; }
    }
}
