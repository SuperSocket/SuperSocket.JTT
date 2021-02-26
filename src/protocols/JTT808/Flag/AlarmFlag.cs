using SuperSocket.JTTBase.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT808.Flag
{
    /// <summary>
    /// 报警预警标志位定义
    /// </summary>
    /// <remarks>JTT808-2019表25</remarks>
    public struct AlarmFlag
    {
        /// <summary>
        /// <para>true 紧急报警，触动报警开关后触发</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(0)]
        public bool Emergency { get; set; }

        /// <summary>
        /// <para>true 超速报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(1)]
        public bool Speeding { get; set; }

        /// <summary>
        /// <para>true 疲劳驾驶报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(2)]
        public bool Fatigue { get; set; }

        /// <summary>
        /// <para>true 危险驾驶行为报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(3)]
        public bool Dangerous { get; set; }

        /// <summary>
        /// <para>true GNSS模块发生故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(4)]
        public bool GNSSOffline { get; set; }

        /// <summary>
        /// <para>true GNSS天线未接或被剪断报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(5)]
        public bool GNSSAntennaDisconnected { get; set; }

        /// <summary>
        /// <para>true GNSS天线短路报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(6)]
        public bool GNSSAntennaShortCircuit { get; set; }

        /// <summary>
        /// <para>true 终端主电源欠压报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(7)]
        public bool UVP { get; set; }

        /// <summary>
        /// <para>true 终端主电源掉电报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(8)]
        public bool PowerOff { get; set; }

        /// <summary>
        /// <para>true 终端LCD或显示器故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(9)]
        public bool LCD { get; set; }

        /// <summary>
        /// <para>true TTS模块故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(10)]
        public bool TTS { get; set; }

        /// <summary>
        /// <para>true 摄像头故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(11)]
        public bool Camera { get; set; }

        /// <summary>
        /// <para>true 道路运输证IC卡模块故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(12)]
        public bool ICCardOffline { get; set; }

        /// <summary>
        /// <para>true 超速预警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(13)]
        public bool PreSpeeding { get; set; }

        /// <summary>
        /// <para>true 疲劳驾驶预警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(14)]
        public bool PreFatigue { get; set; }

        /// <summary>
        /// <para>true 违规行驶报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(15)]
        public bool Violation { get; set; }

        /// <summary>
        /// <para>true 胎压预警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(16)]
        public bool PreTirePressure { get; set; }

        /// <summary>
        /// <para>true 右转盲区异常报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(17)]
        public bool RightBlindArea { get; set; }

        /// <summary>
        /// <para>true 当天累计驾驶超时报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(18)]
        public bool AccumulatedDay { get; set; }

        /// <summary>
        /// <para>true 超时停车报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(19)]
        public bool OvertimeParking { get; set; }

        /// <summary>
        /// <para>true 进出区域报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(20)]
        public bool AccessArea { get; set; }

        /// <summary>
        /// <para>true 进出路线报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(21)]
        public bool AccessRoutes { get; set; }

        /// <summary>
        /// <para>true 路段行驶时间不足/过长报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(22)]
        public bool RoadTravelTime { get; set; }

        /// <summary>
        /// <para>true 路线偏离报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(23)]
        public bool OffCourse { get; set; }

        /// <summary>
        /// <para>true 车辆VSS故障</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(24)]
        public bool VSS { get; set; }

        /// <summary>
        /// <para>true 车辆油量异常报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(25)]
        public bool OilQuantity { get; set; }

        /// <summary>
        /// <para>true 车辆被盗报警（通过车辆防盗器）</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(26)]
        public bool BeStolen { get; set; }

        /// <summary>
        /// <para>true 车辆非法点火报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(27)]
        public bool IllegalIgnition { get; set; }

        /// <summary>
        /// <para>true 车辆非法位移报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(28)]
        public bool IllegalMoving { get; set; }

        /// <summary>
        /// <para>true 碰撞侧翻报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(29)]
        public bool ImpactRollover { get; set; }

        /// <summary>
        /// <para>true 侧翻预警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(30)]
        public bool PreRollover { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        [FlagIndex(31)]
        public bool Retain { get; set; }
    }
}
