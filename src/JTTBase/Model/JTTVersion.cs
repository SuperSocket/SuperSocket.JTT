using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 协议版本
    /// </summary>
    /// <remarks> http://jtst.mot.gov.cn/search/std </remarks>
    public enum JTTVersion
    {
        /// <summary>
        /// JTT808
        /// </summary>
        /// <remarks> https://github.com/SuperSocket/SuperSocket.JTT/blob/master/specs/JTT808-2019.PDF </remarks>
        JTT808,

        /// <summary>
        /// JTT809
        /// </summary>
        /// <remarks> https://github.com/SuperSocket/SuperSocket.JTT/blob/master/specs/JTT809-2019.PDF </remarks>
        JTT809,

        /// <summary>
        /// JTT1078
        /// </summary>
        /// <remarks> https://github.com/SuperSocket/SuperSocket.JTT/blob/master/specs/JTT1078-2016.pdf </remarks>
        JTT1078,
        /// <summary>
        /// 自定义
        /// </summary>
        JTTCustom
    }
}
