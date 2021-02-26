using SuperSocket.JTTBase.Interface;
using System;

namespace JTTCustomServer.Model.MessageBody
{
    /// <summary>
    /// x0001
    /// </summary>
    /// <remarks>
    /// <para>6字节</para>
    /// </remarks>
    public class x0001 : IJTTMessageBody
    {
        /// <summary>
        /// 数据A
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// </remarks>
        public UInt16 A { get; set; }

        /// <summary>
        /// 数据B
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public UInt32 B { get; set; }
    }
}
