using SuperSocket.JTT.JTTBase.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTTCustomServer.Model.Flag
{
    /// <summary>
    /// 报警标志位定义
    /// </summary>
    public struct AlarmFlag
    {
        /// <summary>
        /// <para>true 紧急报警，触动报警开关后触发</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(0)]
        public bool Emergency { get; set; }

        /// <summary>
        /// <para>true 预警</para>
        /// <para>标志维持至报警条件解除或预警转化为报警事件</para>
        /// </summary>
        [FlagIndex(1)]
        public bool Early { get; set; }

        /// <summary>
        /// <para>true 卫星定位模块发生故障</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(2)]
        public bool GPSOffline { get; set; }

        /// <summary>
        /// <para>true 卫星定位天线未接或被剪断报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(3)]
        public bool GPSAntennaDisconnected { get; set; }

        /// <summary>
        /// <para>true 卫星定位天线短路报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(4)]
        public bool GPSAntennaShortCircuit { get; set; }

        /// <summary>
        /// <para>true 终端主电源欠压报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(5)]
        public bool UVP { get; set; }

        /// <summary>
        /// <para>true 终端主电源掉电报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(6)]
        public bool PowerOff { get; set; }

        /// <summary>
        /// <para>true 液晶(LCD)显示终端故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(7)]
        public bool LCDTerminal { get; set; }

        /// <summary>
        /// <para>true 语音合成(TTS)模块故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(8)]
        public bool TTS { get; set; }

        /// <summary>
        /// <para>true 摄像头故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(9)]
        public bool Camera { get; set; }

        /// <summary>
        /// <para>true 计价器故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(10)]
        public bool Taximeter { get; set; }

        /// <summary>
        /// <para>true 服务评价器故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(11)]
        public bool ServiceEvaluator { get; set; }

        /// <summary>
        /// <para>true LED广告屏故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(12)]
        public bool LEDAd { get; set; }

        /// <summary>
        /// <para>true 液晶(LCD)显示屏故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(13)]
        public bool LCD { get; set; }

        /// <summary>
        /// <para>true 安全访问模块故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(14)]
        public bool SecureAccess { get; set; }

        /// <summary>顶灯广告屏故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(15)]
        public bool LEDTopLight { get; set; }

        /// <summary>
        /// <para>true 超速报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(16)]
        public bool Speeding { get; set; }

        /// <summary>
        /// <para>true 连续驾驶超时/疲劳驾驶报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(17)]
        public bool Fatigue { get; set; }

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
        /// <para>true 进出区域/路线报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(20)]
        public bool AccessArea { get; set; }

        /// <summary>
        /// <para>true 路段行驶时间不足/过长报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(21)]
        public bool RoadTravelTime { get; set; }

        /// <summary>
        /// <para>true 禁行路段行驶报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(22)]
        public bool ForbiddenRoute { get; set; }

        /// <summary>
        /// <para>true 车速传感器故障报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(23)]
        public bool SpeedSensor { get; set; }

        /// <summary>
        /// <para>true 车辆非法点火报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(24)]
        public bool IllegalIgnition { get; set; }

        /// <summary>
        /// <para>true 车辆非法位移报警</para>
        /// <para>收到应答后清零</para>
        /// </summary>
        [FlagIndex(25)]
        public bool IllegalMoving { get; set; }

        /// <summary>
        /// <para>true 智能终端存储异常报警</para>
        /// <para>标志维持至报警条件解除</para>
        /// </summary>
        [FlagIndex(26)]
        public bool Storage { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        [FlagIndex(27, 31, BeginToEnd = true)]
        public bool[] Retain { get; set; }
    }
}
