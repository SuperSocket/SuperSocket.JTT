using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 编码类型
    /// </summary>
    public static class CodeType
    {
        /// <summary>
        /// BitArray
        /// </summary>
        public const string binary = "binary";
        /// <summary>
        /// BitConverter.ToBoolean(bytes);
        /// </summary>
        public const string boolean = "boolean";
        public const string @byte = "byte";
        public const string bytes = "bytes";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt16
        /// 编码 BinaryPrimitives.WriteUInt16
        /// </summary>
        public const string uint16 = "uint16";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt32
        /// 编码 BinaryPrimitives.WriteUInt32
        /// </summary>
        public const string uint32 = "uint32";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt64
        /// 编码 BinaryPrimitives.WriteUInt64
        /// </summary>
        public const string uint64 = "uint64";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt16
        /// 编码 BinaryPrimitives.WriteInt16
        /// </summary>
        public const string int16 = "int16";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt32
        /// 编码 BinaryPrimitives.WriteInt32
        /// </summary>
        public const string int32 = "int32";
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt64
        /// 编码 BinaryPrimitives.WriteInt64
        /// </summary>
        public const string int64 = "int64";
        /// <summary>
        /// Convert.ToUInt16("0x", 16)
        /// </summary>
        public const string uint16_hex = "uint16_hex";
        /// <summary>
        /// Convert.ToUInt32("0x", 16)
        /// </summary>
        public const string uint32_hex = "uint32_hex";
        /// <summary>
        /// Convert.ToUInt64("0x", 16)
        /// </summary>
        public const string uint64_hex = "uint64_hex";
        /// <summary>
        /// 时间戳
        /// 转为C# DateTime
        /// </summary>
        public const string uint64_unix = "uint64_unix";
        /// <summary>
        /// Convert.ToByte("0x", 16)
        /// </summary>
        public const string byte_hex = "byte_hex";
        /// <summary>
        /// Convert.ToInt16("0x", 16)
        /// </summary>
        public const string int16_hex = "int16_hex";
        /// <summary>
        /// Convert.ToInt32("0x", 16)
        /// </summary>
        public const string int32_hex = "int32_hex";
        /// <summary>
        /// Convert.ToInt64("0x", 16)
        /// </summary>
        public const string int64_hex = "int64_hex";
        /// <summary>
        /// Enum.Parse(type, Encoding.GetEncoding("gbk").GetString(data).Replace("\0", ""))
        /// </summary>
        public const string @enum = "@enum";
        /// <summary>
        /// Encoding.GetEncoding("gbk").GetString(data).Replace("\0", "");
        /// </summary>
        public const string @string = "@string";
        /// <summary>
        /// string.Join('\0', data.ToList().ForEach(o => o.ToString("x").PadLeft(2, '0')));
        /// </summary>
        public const string string_x_0 = "string_x_0";
        /// <summary>
        /// data.ToString("x2");
        /// </summary>
        public const string string_x2 = "string_x2";
        /// <summary>
        /// $"0x{data.ToString("x2")}";
        /// </summary>
        public const string string_hex = "string_hex";
        /// <summary>
        /// 
        /// </summary>
        public const string string_ascii = "string_ascii";
        /// <summary>
        /// 
        /// </summary>
        public const string string_bcd8421 = "string_bcd8421";
        /// <summary>
        /// 
        /// </summary>
        public const string char_bcd8421 = "char_bcd8421";
        /// <summary>
        /// yy-MM-dd HH:mm:ss
        /// </summary>
        public const string datetime_bcd8421 = "datetime_bcd8421";
        /// <summary>
        /// yy-MM-dd
        /// </summary>
        public const string date_bcd8421 = "date_bcd8421";
        /// <summary>
        /// HH:MM
        /// </summary>
        public const string time_bcd8421 = "time_bcd8421";
        /// <summary>
        /// string.Join(' ', data);
        /// </summary>
        public const string data = "data";
        /// <summary>
        /// 使用空格分隔
        /// </summary>
        public const string data_split = "data_split";
        /// <summary>
        /// Encoding.GetEncoding(jT.Encoding).GetBytes(value.ToJson())
        /// </summary>
        public const string json = "json";
    }
}
