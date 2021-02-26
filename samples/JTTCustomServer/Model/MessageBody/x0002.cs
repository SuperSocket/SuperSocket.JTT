using JTTCustomServer.Model.Flag;
using SuperSocket.JTTBase.Interface;
using System;
using System.Collections;

namespace JTTCustomServer.Model.MessageBody
{
    /// <summary>
    /// x0002
    /// </summary>
    /// <remarks>
    /// <para>21字节</para>
    /// </remarks>
    public class x0002 : IJTTMessageBody
    {
        /// <summary>
        /// 报警
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public BitArray Alarm { get; set; }

        /// <summary>
        /// 报警标志位
        /// </summary>
        /// <remarks>标志位定义</remarks>
        public AlarmFlag Alarm_Flag { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public BitArray State { get; set; }

        /// <summary>
        /// 状态标志位
        /// </summary>
        /// <remarks>标志位定义</remarks>
        public StateFlag State_Flag { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public UInt32 Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public UInt32 Longitude { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// </remarks>
        public UInt16 Speed { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// </remarks>
        public byte Direction { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        /// <remarks>
        /// <para>6字节</para>
        /// <para>YY-MM-DD-hh-mm-ss</para>
        /// </remarks>
        public string DateTime { get; set; }
    }
}
