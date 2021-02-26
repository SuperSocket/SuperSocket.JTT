using SuperSocket.JTTBase.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT808.Flag
{
    /// <summary>
    /// 状态位定义
    /// </summary>
    /// <remarks>JTT808-2019表24</remarks>
    public struct StateFlag
    {
        /// <summary>
        /// <para>true ACC开</para>
        /// <para>false ACC关</para>
        /// </summary>
        [FlagIndex(0)]
        public bool ACC { get; set; }

        /// <summary>
        /// <para>true 定位</para>
        /// <para>false 未定位</para>
        /// </summary>
        [FlagIndex(1)]
        public bool Location { get; set; }

        /// <summary>
        /// <para>true 南纬</para>
        /// <para>false 北纬</para>
        /// </summary>
        [FlagIndex(2)]
        public bool Latitude { get; set; }

        /// <summary>
        /// <para>true 西经</para>
        /// <para>false 东经</para>
        /// </summary>
        [FlagIndex(3)]
        public bool Longitude { get; set; }

        /// <summary>
        /// <para>true 停运状态</para>
        /// <para>false 运营状态</para>
        /// </summary>
        [FlagIndex(4)]
        public bool Operate { get; set; }

        /// <summary>
        /// <para>true 经纬度已经保密插件加密</para>
        /// <para>false 经纬度未经保密插件加密</para>
        /// </summary>
        [FlagIndex(5)]
        public bool Encryption { get; set; }

        /// <summary>
        /// <para>true 紧急刹车系统采集的前撞预警</para>
        /// </summary>
        [FlagIndex(6)]
        public bool ForwardCollisionWarning { get; set; }

        /// <summary>
        /// <para>true 车道偏移预警</para>
        /// </summary>
        [FlagIndex(7)]
        public bool LaneDepartureWarning { get; set; }

        /// <summary>
        /// <para>true,true 满载</para>
        /// <para>true,false 保留</para>
        /// <para>false,true 半载</para>
        /// <para>false,false 空车</para>
        /// <para>可表示客车的空载状态，</para>
        /// <para>重车及货车的空载、满载状态，</para>
        /// <para>该状态可由人工输入或传感器获取</para>
        /// </summary>
        [FlagIndex(8, 9)]
        public bool[] Loading { get; set; }

        /// <summary>
        /// <para>true 车辆油路断开</para>
        /// <para>false 车辆油路正常</para>
        /// </summary>
        [FlagIndex(10)]
        public bool OilCircuitDisconnected { get; set; }

        /// <summary>
        /// <para>true 车辆电路断开</para>
        /// <para>false 车辆电路正常</para>
        /// </summary>
        [FlagIndex(11)]
        public bool CircuitDisconnected { get; set; }

        /// <summary>
        /// <para>true 车门加锁</para>
        /// <para>false 车门解锁</para>
        /// </summary>
        [FlagIndex(12)]
        public bool DoorLocked { get; set; }

        /// <summary>
        /// <para>true 门1开</para>
        /// <para>false 门1关</para>
        /// <para>前门</para>
        /// </summary>
        [FlagIndex(13)]
        public bool Door1 { get; set; }

        /// <summary>
        /// <para>true 门2开</para>
        /// <para>false 门2关</para>
        /// <para>中门</para>
        /// </summary>
        [FlagIndex(14)]
        public bool Door2 { get; set; }

        /// <summary>
        /// <para>true 门3开</para>
        /// <para>false 门3关</para>
        /// <para>后门</para>
        /// </summary>
        [FlagIndex(15)]
        public bool Door3 { get; set; }

        /// <summary>
        /// <para>true 门4开</para>
        /// <para>false 门4关</para>
        /// <para>驾驶席门</para>
        /// </summary>
        [FlagIndex(16)]
        public bool Door4 { get; set; }

        /// <summary>
        /// <para>true 门5开</para>
        /// <para>false 门5关</para>
        /// <para>自定义</para>
        /// </summary>
        [FlagIndex(17)]
        public bool Door5 { get; set; }

        /// <summary>
        /// <para>true 使用GPS卫星进行定位</para>
        /// <para>false 未使用GPS卫星进行定位</para>
        /// </summary>
        [FlagIndex(18)]
        public bool GPS { get; set; }

        /// <summary>
        /// <para>true 使用北斗卫星进行定位</para>
        /// <para>false 未使用北斗卫星进行定位</para>
        /// </summary>
        [FlagIndex(19)]
        public bool BDS { get; set; }

        /// <summary>
        /// <para>true 使用GLONASS卫星进行定位</para>
        /// <para>false 未使用GLONASS卫星进行定位</para>
        /// </summary>
        [FlagIndex(20)]
        public bool GLONASS { get; set; }

        /// <summary>
        /// <para>true 使用Galileo卫星进行定位</para>
        /// <para>false 未使用Galileo卫星进行定位</para>
        /// </summary>
        [FlagIndex(21)]
        public bool Galileo { get; set; }

        /// <summary>
        /// <para>true 车辆处于行驶状态</para>
        /// <para>false 车辆处于停止状态</para>
        /// </summary>
        [FlagIndex(22)]
        public bool Travel { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        [FlagIndex(23, 31, BeginToEnd = true)]
        public bool[] Retain { get; set; }
    }
}
