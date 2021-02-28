using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Extension
{
    /// <summary>
    /// 字节数组拓展方法
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// 补位
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="length">总长度</param>
        /// <param name="padding">配位字符</param>
        /// <param name="right">右侧</param>
        /// <returns></returns>
        public static byte[] Padding(this byte[] bytes, int length, byte padding = 0x00, bool right = true)
        {
            if (length == 0 || bytes?.Length == length)
                return bytes;

            var result = new byte[length];

            for (int i = 0; i < result.Length; i++)
            {
                if ((right && i >= (bytes?.Length ?? 0)) || (!right && i < length - (bytes?.Length ?? 0)))
                    result[i] = padding;
                else
                    result[i] = bytes[right ? i : (i - length + (bytes?.Length ?? 0))];
            }

            return result;
        }

        /// <summary>
        /// 清理补位
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="padding">补位字符</param>
        /// <param name="right">右侧</param>
        /// <returns></returns>
        public static byte[] ClearnPadding(this byte[] bytes, byte padding = 0x00, bool right = true)
        {
            if (bytes.Length == 0)
                return bytes;

            for (int i = right ? bytes.Length - 1 : 0; right ? i >= 0 : i < bytes.Length; i -= right ? 1 : -1)
            {
                if (!bytes[i].Equals(padding))
                    return (right ? bytes.Take(++i) : bytes.TakeLast(bytes.Length - i)).ToArray();
            }

            return Array.Empty<byte>();
        }

        /// <summary>
        /// 转为16进制byte数组
        /// </summary>
        /// <param name="x2String">十六进制字符串</param>
        /// <returns></returns>
        public static byte Get0xByte(this string x2String)
        {
            return (byte)Int32.Parse($@"{x2String[2]}{x2String[3]}", NumberStyles.HexNumber);
        }

        /// <summary>
        /// 转为16进制byte数组
        /// </summary>
        /// <param name="x2String">十六进制字符串</param>
        /// <returns></returns>
        public static byte[] Get0xBytes(this string x2String)
        {
            return new byte[] { x2String.Get0xByte() };
        }

        /// <summary>
        /// 转为16进制byte数组
        /// </summary>
        /// <param name="x2String">十六进制字符串</param>
        /// <returns></returns>
        public static byte[] Get0xBytes(this string[] x2String)
        {
            return x2String.Select(o => o.Get0xByte()).ToArray();
        }

        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="x2Byte">十六进制byte</param>
        /// <returns></returns>
        public static string Get0xString(this byte x2Byte)
        {
            return $"{x2Byte:x2}";
        }

        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="x2Bytes">十六进制byte</param>
        /// <returns></returns>
        public static string[] Get0xString(this byte[] x2Bytes)
        {
            return x2Bytes.Select(o => o.Get0xString()).ToArray();
        }

        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="x2Byte">十六进制byte</param>
        /// <returns></returns>
        public static string GetHexString(this byte x2Byte)
        {
            return $"0x{x2Byte:x2}";
        }

        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="x2Byte">十六进制byte</param>
        /// <returns></returns>
        public static string[] GetHexString(this byte[] x2Byte)
        {
            return x2Byte.Select(o => GetHexString(o)).ToArray();
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="String0x"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string String0x)
        {
            String0x = String0x.Replace(" ", "");
            byte[] buf = new byte[String0x.Length / 2];
            ReadOnlySpan<char> readOnlySpan = String0x.AsSpan();
            for (int i = 0; i < String0x.Length; i++)
            {
                if (i % 2 == 0)
                {
                    buf[i / 2] = Convert.ToByte(readOnlySpan.Slice(i, 2).ToString(), 16);
                }
            }
            return buf;
        }
    }
}
