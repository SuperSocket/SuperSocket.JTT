using SuperSocket.JTT.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Extension
{
    /// <summary>
    /// Crc校验
    /// </summary>
    public class CrcCcitt
    {
        /// <summary>
        /// 多项式
        /// </summary>
        const ushort polynominal = 0x1021;

        /// <summary>
        /// 查询表
        /// </summary>
        ushort[] table = new ushort[256];

        /// <summary>
        /// 初始值
        /// </summary>
        ushort initialValue = 0;

        /// <summary>
        /// 初始值
        /// </summary>
        public InitialCrcValue initialCrcValue;

        /// <summary>
        /// 类型（位数）
        /// </summary>
        public CrcType crcType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">位数</param>
        /// <param name="initialValue">初始值</param>
        public CrcCcitt(CrcType type, InitialCrcValue initialValue = InitialCrcValue.Zeros)
        {
            crcType = type;
            if (crcType == CrcType.CRC_16)
            {
                initialCrcValue = initialValue;
                this.initialValue = (ushort)initialValue;
                ushort temp, a;
                for (int i = 0; i < table.Length; i++)
                {
                    temp = 0;
                    a = (ushort)(i << 8);
                    for (int j = 0; j < 8; j++)
                    {
                        if (((temp ^ a) & 0x8000) != 0)
                            temp = (ushort)((temp << 1) ^ polynominal);
                        else
                            temp <<= 1;
                        a <<= 1;
                    }
                    table[i] = temp;
                }
            }
        }

        /// <summary>
        /// 计算分片数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public ushort ComputeChecksum_16(byte[] bytes)
        {
            ushort crc = this.initialValue;
            for (int i = 0; i < bytes.Length; i++)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xff & bytes[i]))]);
            }
            return crc;
        }

        /// <summary>
        /// 计算分片数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte ComputeChecksum_8(byte[] bytes)
        {
            byte crc = (byte)(bytes[0] ^ bytes[1]);
            for (int i = 2; i < bytes.Length; i++)
            {
                crc ^= bytes[i];
            }
            return crc;
        }

        /// <summary>
        /// 计算分片数量
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public object ComputeChecksum(byte[] bytes)
        {
            switch (crcType)
            {
                case CrcType.CRC_8:
                    return ComputeChecksum_8(bytes);
                case CrcType.CRC_16:
                    return ComputeChecksum_16(bytes);
                default:
                    throw new JTTException($"计算分片数失败, 不支持的 CrcLength值: {crcType}.");
            }
        }

        /// <summary>
        /// 计算分片数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            byte[] crc;
            switch (crcType)
            {
                case CrcType.CRC_8:
                    byte crc_B8 = ComputeChecksum_8(bytes);
                    crc = new byte[] { crc_B8 };
                    break;
                case CrcType.CRC_16:
                    ushort crc_B16 = ComputeChecksum_16(bytes);
                    crc = new byte[] { (byte)(crc_B16 >> 8), (byte)(crc_B16 & 0x00ff) };
                    break;
                default:
                    throw new JTTException($"计算分片数失败, 不支持的 CrcLength值: {crcType}.");
            }
            return crc;
        }
    }
}
