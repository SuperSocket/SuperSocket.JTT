using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 初始CRC值
    /// </summary>
    public enum InitialCrcValue
    {
        Zeros = 0,
        NonZero1 = 0xffff,
        NonZero2 = 0x1D0F
    }
}
