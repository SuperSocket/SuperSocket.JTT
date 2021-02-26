using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Interface
{
    /// <summary>
    /// JTT协议处理类
    /// </summary>
    public interface IJTTProtocolHandler
    {
        /// <summary>
        /// 获取汉字编码
        /// </summary>
        /// <returns></returns>
        Encoding GetZHCNEncoding();

        /// <summary>
        /// 获取补位字符
        /// </summary>
        /// <returns></returns>
        byte GetPadding();

        /// <summary>
        /// 获取Crc校验对象
        /// </summary>
        /// <returns></returns>
        CrcCcitt GetCrcCcitt();

        /// <summary>
        /// 获取头标识
        /// </summary>
        /// <returns></returns>
        ReadOnlyMemory<byte> GetHeadFlagValue();

        /// <summary>
        /// 获取尾标识
        /// </summary>
        /// <returns></returns>
        ReadOnlyMemory<byte> GetEndFlagValue();

        /// <summary>
        /// 获取转义标识
        /// </summary>
        /// <returns></returns>
        Dictionary<byte, byte[]> GetEscapesValue();

        /// <summary>
        /// 获取转义还原标识
        /// </summary>
        /// <returns></returns>
        Dictionary<byte, Dictionary<byte, byte>> GetUnEscapesValue();

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns></returns>
        byte[] Escape(byte[] buffer);

        /// <summary>
        /// 转义还原
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns></returns>
        byte[] UnEscape(byte[] buffer);

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="info">编码信息</param>
        /// <returns></returns>
        object Decode(byte[] bytes, CodeInfo info);

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="info">编码信息</param>
        /// <param name="length">总长度（不足时在右侧进行补位）</param>
        /// <param name="escape">转义</param>
        /// <param name="padding">补位</param>
        /// <returns></returns>
        byte[] Encode(object obj, CodeInfo info, int? length = null, bool escape = false, bool padding = true);

        /// <summary>
        /// 检查Crc校验码
        /// </summary>
        /// <param name="buffer">数据头+数据体+校验码</param>
        /// <param name="data">数据头+数据体</param>
        /// <param name="crc_code">校验码(已转义)</param>
        /// <param name="crc_code_compute">计算后的值(已转义)</param>
        /// <returns>是否一致</returns>
        bool CheckCrcCode(byte[] buffer, out byte[] data, out byte[] crc_code, out byte[] crc_code_compute);

        /// <summary>
        /// 计算Crc校验码(已转义)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        void ComputeCrcCode(IJTTPackageInfo info);

        /// <summary>
        /// 计算Crc校验码(未转义)
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns>crc校验值</returns>
        byte[] ComputeCrcValue(byte[] buffer);
    }
}
