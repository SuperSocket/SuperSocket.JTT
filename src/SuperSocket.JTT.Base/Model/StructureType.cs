using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 结构类型
    /// </summary>
    public enum StructureType
    {
        /// <summary>
        /// 普通
        /// </summary>
        normal,
        /// <summary>
        /// 内部结构
        /// </summary>
        @internal,
        /// <summary>
        /// 附加信息
        /// </summary>
        additional,
        /// <summary>
        /// 空
        /// </summary>
        /// <remarks>不进行任何处理</remarks>
        empty
    }
}
