using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 编码类型
    /// </summary>
    public enum CodeType
    {
        /// <summary>
        /// BitArray
        /// </summary>
        binary,
        /// <summary>
        /// BitConverter.ToBoolean(bytes);
        /// </summary>
        boolean,
        @byte,
        bytes,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt16
        /// 编码 BinaryPrimitives.WriteUInt16
        /// </summary>
        uint16,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt32
        /// 编码 BinaryPrimitives.WriteUInt32
        /// </summary>
        uint32,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadUInt64
        /// 编码 BinaryPrimitives.WriteUInt64
        /// </summary>
        uint64,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt16
        /// 编码 BinaryPrimitives.WriteInt16
        /// </summary>
        int16,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt32
        /// 编码 BinaryPrimitives.WriteInt32
        /// </summary>
        int32,
        /// <summary>
        /// 解码 BinaryPrimitives.ReadInt64
        /// 编码 BinaryPrimitives.WriteInt64
        /// </summary>
        int64,
        /// <summary>
        /// Convert.ToUInt16("0x", 16)
        /// </summary>
        uint16_hex,
        /// <summary>
        /// Convert.ToUInt32("0x", 16)
        /// </summary>
        uint32_hex,
        /// <summary>
        /// Convert.ToUInt64("0x", 16)
        /// </summary>
        uint64_hex,
        /// <summary>
        /// 时间戳
        /// 转为C# DateTime
        /// </summary>
        uint64_unix,
        /// <summary>
        /// Convert.ToByte("0x", 16)
        /// </summary>
        byte_hex,
        /// <summary>
        /// Convert.ToInt16("0x", 16)
        /// </summary>
        int16_hex,
        /// <summary>
        /// Convert.ToInt32("0x", 16)
        /// </summary>
        int32_hex,
        /// <summary>
        /// Convert.ToInt64("0x", 16)
        /// </summary>
        int64_hex,
        /// <summary>
        /// Enum.Parse(type, Encoding.GetEncoding("gbk").GetString(data).Replace("\0", ""))
        /// </summary>
        @enum,
        /// <summary>
        /// Encoding.GetEncoding("gbk").GetString(data).Replace("\0", "");
        /// </summary>
        @string,
        /// <summary>
        /// string.Join('\0', data.ToList().ForEach(o => o.ToString("x").PadLeft(2, '0')));
        /// </summary>
        string_x_0,
        /// <summary>
        /// data.ToString("x2");
        /// </summary>
        string_x2,
        /// <summary>
        /// $"0x{data.ToString("x2")}";
        /// </summary>
        string_hex,
        /// <summary>
        /// 
        /// </summary>
        string_ascii,
        /// <summary>
        /// 
        /// </summary>
        string_bcd8421,
        /// <summary>
        /// 
        /// </summary>
        char_bcd8421,
        /// <summary>
        /// yy-MM-dd HH:mm:ss
        /// </summary>
        datetime_bcd8421,
        /// <summary>
        /// yy-MM-dd
        /// </summary>
        date_bcd8421,
        /// <summary>
        /// HH:MM
        /// </summary>
        time_bcd8421,
        /// <summary>
        /// string.Join(' ', data);
        /// </summary>
        data,
        /// <summary>
        /// 使用空格分隔
        /// </summary>
        data_split,
        /// <summary>
        /// Encoding.GetEncoding(jT.Encoding).GetBytes(value.ToJson())
        /// </summary>
        json
    }
}
