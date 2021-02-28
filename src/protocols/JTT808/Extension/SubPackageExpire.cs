using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT808.Extension
{
    /// <summary>
    /// 分包数据过期策略
    /// </summary>
    public enum SubPackageExpire
    {
        /// <summary>
        /// 超时即过期
        /// </summary>
        timeout,
        /// <summary>
        /// 存储空间达到阈值
        /// </summary>
        threshold
    }
}
