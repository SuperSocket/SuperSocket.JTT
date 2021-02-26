using SuperSocket.JTTBase.Interface;

namespace JTTCustomServer.Model.MessageBody
{
    /// <summary>
    /// x0003
    /// </summary>
    public class x0003 : IJTTMessageBody
    {
        /// <summary>
        /// x0002
        /// </summary>
        public x0002 x0002 { get; set; }

        /// <summary>
        /// 单位代码
        /// </summary>
        /// <remarks>
        /// <para>16字节</para>
        /// </remarks>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 司机代码
        /// </summary>
        /// <remarks>
        /// <para>19字节</para>
        /// </remarks>
        public string DriverCode { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        /// <remarks>
        /// <para>6字节</para>
        /// </remarks>
        public string PlateNumber { get; set; }
    }
}
