using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SuperSocket.JTT.JTT1078.Extension
{
    /// <summary>
    /// 拓展工具类
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 获取音视频请求Url
        /// </summary>
        /// <param name="serverIP">音视频流服务器IP</param>
        /// <param name="serverPort">音视频流服务器端口号</param>
        /// <param name="vehicleNo">
        /// <para>车牌号码</para>
        /// <para>应采用UTF-8编码，并统一转化为 IETF RFC 2854 中的 application/x-www-form-URLencoded MIME 格式</para>
        /// </param>
        /// <param name="vehicleColor">
        /// <para>车牌颜色</para>
        /// <para>按照JTT415-2006中5.4.12的规定</para>
        /// </param>
        /// <param name="channelID">
        /// <para>逻辑通道号</para>
        /// <para>按照JTT1076-2016中表2, 0表示所有通道</para>
        /// </param>
        /// <param name="avitemType">音视频标志<see cref="Const.AvitemType"/></param>
        /// <param name="authorizeCode">
        /// <para>时效口令</para>
        /// <para>由企业平台服务器生成，归属地区政府平台客户端的时效口令与跨域地区政府平台的时效口令不同。</para>
        /// <para>时效口令应仅由英文字母（含大小写）及阿拉伯数字构成，长度为64个ASCII字符，应每24h更新一次。</para>
        /// </param>
        /// <param name="gnssData">
        /// <para>位置标识</para>
        /// <para>车辆5min之内的任一时刻的卫星定位时间和经纬度，用于跨域地区政府平台访问时的验证，归属地区政府平台客户端访问时可为空。</para>
        /// <para>ASCII字符表示，格式为：YYYYMMDD-HHMMSS-NXX.XXXXXX-EXXX.XXXXXX</para>
        /// </param>
        /// <returns></returns>
        public static string GetRealVideoUrl(string serverIP, UInt16 serverPort, string vehicleNo, byte vehicleColor, byte channelID, byte avitemType, byte[] authorizeCode, byte[] gnssData = null)
        {
            return $"http://{serverIP}:{serverPort}/{HttpUtility.UrlEncode(vehicleNo, Encoding.UTF8)}.{vehicleColor}.{channelID}.{avitemType}.{Encoding.ASCII.GetString(authorizeCode)}{(gnssData == null ? "" : $".{Encoding.ASCII.GetString(gnssData)}")}";
        }
    }
}
