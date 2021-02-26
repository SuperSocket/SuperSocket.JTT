using SuperSocket.JTTBase.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Flag
{
    /// <summary>
    /// 视频报警标志位定义
    /// </summary>
    /// <remarks>JTT1078-2016表14</remarks>
    public struct VideoAlarmFlag
    {
        /// <summary>
        /// <para>true 视频信号丢失报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(0)]
        public bool SignalLoss { get; set; }

        /// <summary>
        /// <para>true 视频信号遮挡报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(1)]
        public bool SignalOcclusion { get; set; }

        /// <summary>
        /// <para>true 存储单元故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(2)]
        public bool StorageFailure { get; set; }

        /// <summary>
        /// <para>true 其他视频设备故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(3)]
        public bool OtherFaults { get; set; }

        /// <summary>
        /// <para>true 客车超员报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(4)]
        public bool Overman { get; set; }

        /// <summary>
        /// <para>true 异常驾驶行为报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(5)]
        public bool AbnormalDriving { get; set; }

        /// <summary>
        /// <para>true 特殊报警录像达到存储阈值报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(6)]
        public bool SpecialAlarmVideoThreshold { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        [FlagIndex(7, 31, BeginToEnd = true)]
        public bool[] Retain { get; set; }
    }
}
