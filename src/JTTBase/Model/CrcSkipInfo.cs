using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// Crc校验时忽略部分的长度
    /// </summary>
    public class CrcIgnoreLength
    {
        /// <summary>
        /// 前段
        /// </summary>
        public int Front { get; set; }

        /// <summary>
        /// 后段
        /// </summary>
        public int Posterior { get; set; }
    }
}
