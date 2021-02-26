using Newtonsoft.Json;
using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTTBase.Hadnler
{
    /// <summary>
    /// JTT协议处理类
    /// </summary>
    public class JTTProtocolHandler : IJTTProtocolHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocol">JTT协议</param>
        public JTTProtocolHandler(IJTTProtocol protocol)
        {
            Protocol = protocol;
        }

        /// <summary>
        /// JTT协议
        /// </summary>
        protected IJTTProtocol Protocol { get; }

        /// <summary>
        /// 头标识
        /// </summary>
        ReadOnlyMemory<byte>? HeadFlag;

        /// <summary>
        /// 尾标识
        /// </summary>
        ReadOnlyMemory<byte>? EndFlag;

        /// <summary>
        /// 转义标识
        /// </summary>
        Dictionary<byte, byte[]> Escapes;

        /// <summary>
        /// 转义还原标识
        /// </summary>
        Dictionary<byte, Dictionary<byte, byte>> UnEscapes;

        /// <summary>
        /// JTT协议汉字编码
        /// </summary>
        Encoding ZHCNEncoding { get; set; }

        /// <summary>
        /// JTT协议补位字符
        /// </summary>
        byte? PaddingValue { get; set; }

        /// <summary>
        /// Crc校验对象
        /// </summary>
        CrcCcitt CrcCcitt { get; set; }

        /// <summary>
        /// 本地默认时间
        /// </summary>
        /// <remarks>1970-01-01</remarks>
        DateTime localDefaultDateTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local);

        /// <summary>
        /// 获取汉字编码
        /// </summary>
        /// <returns></returns>
        public virtual Encoding GetZHCNEncoding()
        {
            if (ZHCNEncoding == null)
            {
                //注册编码（以支持gbk）
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                ZHCNEncoding = Encoding.GetEncoding(Protocol.ZHCNEncoding);
            }
            return ZHCNEncoding;
        }

        /// <summary>
        /// 获取补位字符
        /// </summary>
        /// <returns></returns>
        public virtual byte GetPadding()
        {
            if (PaddingValue == null)
                PaddingValue = Encode(Protocol.Padding.Value, Protocol.Padding.Encode, null, false, false)[0];
            return PaddingValue.Value;
        }

        /// <summary>
        /// 获取Crc校验对象
        /// </summary>
        /// <returns></returns>
        public virtual CrcCcitt GetCrcCcitt()
        {
            if (CrcCcitt == null)
                CrcCcitt = new CrcCcitt(Protocol.CrcCcitt.Type, Protocol.CrcCcitt.InitialValue);
            return CrcCcitt;
        }

        /// <summary>
        /// 获取头标识
        /// </summary>
        /// <returns></returns>
        public virtual ReadOnlyMemory<byte> GetHeadFlagValue()
        {
            if (HeadFlag == null)
                HeadFlag = new ReadOnlyMemory<byte>(Encode(Protocol.HeadFlag.Value, Protocol.HeadFlag.Encode, null, false, false));
            return HeadFlag.Value;
        }

        /// <summary>
        /// 获取尾标识
        /// </summary>
        /// <returns></returns>
        public virtual ReadOnlyMemory<byte> GetEndFlagValue()
        {
            if (EndFlag == null)
                EndFlag = new ReadOnlyMemory<byte>(Encode(Protocol.EndFlag.Value, Protocol.EndFlag.Encode, null, false, false));
            return EndFlag.Value;
        }

        /// <summary>
        /// 获取转义标识
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<byte, byte[]> GetEscapesValue()
        {
            if (Escapes == null)
                Escapes = Protocol.Escapes.ToDictionary(
                    k => Encode(k.Target, k.Encode, null, false, false)[0],
                    v => v.Trans.Select(t => Encode(t, v.Encode, null, false, false)[0]).ToArray());
            return Escapes;
        }

        /// <summary>
        /// 获取转义还原标识
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<byte, Dictionary<byte, byte>> GetUnEscapesValue()
        {
            if (UnEscapes == null)
            {
                UnEscapes = new Dictionary<byte, Dictionary<byte, byte>>();
                var escapes = GetEscapesValue();
                foreach (var item in escapes)
                {
                    if (!UnEscapes.ContainsKey(item.Value[0]))
                        UnEscapes.Add(item.Value[0], new Dictionary<byte, byte>());

                    if (!UnEscapes[item.Value[0]].ContainsKey(item.Value[1]))
                        UnEscapes[item.Value[0]].Add(item.Value[1], item.Key);
                }
            }
            return UnEscapes;
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns></returns>
        public virtual byte[] Escape(byte[] buffer)
        {
            if (buffer?.Any() != true)
                return buffer;

            using MemoryStream ms = new MemoryStream();
            foreach (var @byte in buffer)
            {
                if (GetEscapesValue().ContainsKey(@byte))
                    ms.Write(GetEscapesValue()[@byte], 0, 2);
                else
                    ms.WriteByte(@byte);
            }
            return ms.ToArray();
        }

        /// <summary>
        /// 转义还原
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns></returns>
        public virtual byte[] UnEscape(byte[] buffer)
        {
            if (buffer?.Any() != true)
                return buffer;

            using MemoryStream ms = new MemoryStream();
            for (int i = 0; i < buffer.Length; i++)
            {
                if (i != buffer.Length - 1
                    && GetUnEscapesValue().ContainsKey(buffer[i])
                    && GetUnEscapesValue()[buffer[i]].ContainsKey(buffer[i + 1]))
                {
                    ms.WriteByte(GetUnEscapesValue()[buffer[i]][buffer[i + 1]]);
                    i++;
                }
                else
                    ms.WriteByte(buffer[i]);
            }

            return ms.ToArray();
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="info">编码信息</param>
        /// <returns></returns>
        public virtual object Decode(byte[] bytes, CodeInfo info)
        {
            switch (info.CodeType)
            {
                case CodeType.binary:
                    return new BitArray(bytes);//string.Join("", bytes.Select(o => Convert.ToString(o, 2).PadLeft(8, '0')).ToArray();
                case CodeType.boolean:
                    return BitConverter.ToBoolean(bytes);
                case CodeType.@byte:
                    return bytes[0];
                case CodeType.bytes:
                    return bytes;
                case CodeType.uint16:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadUInt16BigEndian(bytes)
                        : BinaryPrimitives.ReadUInt16LittleEndian(bytes);
                case CodeType.uint32:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadUInt32BigEndian(bytes)
                        : BinaryPrimitives.ReadUInt32LittleEndian(bytes);
                case CodeType.uint64:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadUInt64BigEndian(bytes)
                        : BinaryPrimitives.ReadUInt64LittleEndian(bytes);
                case CodeType.int16:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadInt16BigEndian(bytes)
                        : BinaryPrimitives.ReadInt16LittleEndian(bytes);
                case CodeType.int32:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadInt32BigEndian(bytes)
                        : BinaryPrimitives.ReadInt32LittleEndian(bytes);
                case CodeType.int64:
                    return Protocol.BigEndian
                        ? BinaryPrimitives.ReadInt64BigEndian(bytes)
                        : BinaryPrimitives.ReadInt64LittleEndian(bytes);
                case CodeType.byte_hex:
                    return Convert.ToByte((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.int16_hex:
                    return Convert.ToInt16((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.int32_hex:
                    return Convert.ToInt32((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.int64_hex:
                    return Convert.ToInt64((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.uint16_hex:
                    return Convert.ToUInt16((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.uint32_hex:
                    return Convert.ToUInt32((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.uint64_hex:
                    return Convert.ToUInt64((string)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    }), 16);
                case CodeType.uint64_unix:
                    return localDefaultDateTime.AddSeconds((UInt64)Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.uint64
                    }));
                case CodeType.@enum:
                    return Enum.Parse(
                        (string.IsNullOrWhiteSpace(info.Assembly) ?
                            Assembly.GetExecutingAssembly() :
                            Assembly.Load(info.Assembly))
                        .GetType(info.TypeName),
                        GetZHCNEncoding().GetString(bytes.ClearnPadding(GetPadding())));//.Replace("\0", ""));
                case CodeType.@string:
                default:
                    return GetZHCNEncoding().GetString(bytes.ClearnPadding(GetPadding()));//.Replace("\0", "");
                case CodeType.string_x_0:
                    return string.Join("", bytes.Select(o => o.ToString("x").PadLeft(2, '0')));
                case CodeType.@string_x2:
                    return bytes[0].ToString("x2");
                case CodeType.string_hex:
                    return $"0x{string.Join("", bytes.Select(o => o.ToString("x2").PadLeft(2, '0')))}";
                case CodeType.string_ascii:
                    return Encoding.ASCII.GetString(bytes);
                case CodeType.string_bcd8421:
                    return new string((char[])Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.char_bcd8421
                    }));
                case CodeType.char_bcd8421:
                    return bytes.SelectMany(o => new char[]
                    {
                        //高4位
                        (char)(48 + (o >> 4 & 0xf)),
                        //低4位
                        (char)(48 + (o & 0xf))
                    }).ToArray();
                case CodeType.datetime_bcd8421:
                    var charArray = (char[])Decode(bytes, new CodeInfo
                    {
                        CodeType = CodeType.char_bcd8421
                    });
                    return DateTime.Parse(
                        $"{DateTime.Now.Year / 100 * 100}{charArray[0]:00}" +
                        $"-{charArray[1]:00}" +
                        $"-{charArray[2]:00}" +
                        $" {charArray[3]:00}" +
                        $":{charArray[4]:00}" +
                        $":{charArray[5]:00}");
                case CodeType.date_bcd8421:
                    return ((DateTime)Decode(
                        bytes.Concat(new byte[] { 0, 0, 0 }).ToArray(),
                        new CodeInfo
                        {
                            CodeType = CodeType.datetime_bcd8421
                        })).Date;
                case CodeType.time_bcd8421:
                    return ((DateTime)Decode(
                        new byte[]
                            {
                                (byte)(DateTime.Now.Year % 100),
                                (byte)(DateTime.Now.Month),
                                (byte)(DateTime.Now.Day),
                            }.Concat(bytes).ToArray(),
                        new CodeInfo
                        {
                            CodeType = CodeType.datetime_bcd8421
                        }));
                case CodeType.data:
                    return string.Join("", bytes);
                case CodeType.data_split:
                    return string.Join(' ', bytes);
                case CodeType.json:
                    var jsonString = GetZHCNEncoding().GetString(bytes.ClearnPadding(GetPadding()));//.Replace("\0", ""));
                    if (string.IsNullOrWhiteSpace(info.TypeName))
                        return JsonConvert.DeserializeObject(jsonString);
                    else
                    {
                        var jsonType = (string.IsNullOrWhiteSpace(info.Assembly) ?
                            Assembly.GetExecutingAssembly() :
                            Assembly.Load(info.Assembly))
                        .GetType(info.TypeName);
                        return JsonConvert.DeserializeObject(jsonString, jsonType);
                    }
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="info">编码信息</param>
        /// <param name="length">总长度（不足时在右侧进行补位）</param>
        /// <param name="escape">转义</param>
        /// <param name="padding">补位</param>
        /// <returns></returns>
        public virtual byte[] Encode(object obj, CodeInfo info, int? length = null, bool escape = false, bool padding = true)
        {
            byte[] bytes;
            switch (info.CodeType)
            {
                case CodeType.binary:
                    var value_binary = (BitArray)obj;
                    var value_binary_bytes = new byte[value_binary.Length / 8];
                    value_binary.CopyTo(value_binary_bytes, 0);
                    bytes = value_binary_bytes;
                    break;
                case CodeType.boolean:
                    bytes = BitConverter.GetBytes((bool)obj);
                    break;
                case CodeType.@byte:
                    bytes = new byte[] { (byte)obj };
                    break;
                case CodeType.bytes:
                    bytes = (byte[])obj;
                    break;
                case CodeType.uint16:
                    bytes = new byte[2];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteUInt16BigEndian(new Span<byte>(bytes), (UInt16)obj);
                    else
                        BinaryPrimitives.WriteUInt16LittleEndian(new Span<byte>(bytes), (UInt16)obj);
                    break;
                case CodeType.uint32:
                    bytes = new byte[4];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteUInt32BigEndian(new Span<byte>(bytes), (UInt32)obj);
                    else
                        BinaryPrimitives.WriteUInt32LittleEndian(new Span<byte>(bytes), (UInt32)obj);
                    break;
                case CodeType.uint64:
                    bytes = new byte[8];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteUInt64BigEndian(new Span<byte>(bytes), (UInt64)obj);
                    else
                        BinaryPrimitives.WriteUInt64LittleEndian(new Span<byte>(bytes), (UInt64)obj);
                    break;
                case CodeType.int16:
                    bytes = new byte[2];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteInt16BigEndian(new Span<byte>(bytes), (Int16)obj);
                    else
                        BinaryPrimitives.WriteInt16LittleEndian(new Span<byte>(bytes), (Int16)obj);
                    break;
                case CodeType.int32:
                    bytes = new byte[4];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteInt32BigEndian(new Span<byte>(bytes), (Int32)obj);
                    else
                        BinaryPrimitives.WriteInt32LittleEndian(new Span<byte>(bytes), (Int32)obj);
                    break;
                case CodeType.int64:
                    bytes = new byte[8];
                    if (Protocol.BigEndian)
                        BinaryPrimitives.WriteInt64BigEndian(new Span<byte>(bytes), (Int64)obj);
                    else
                        BinaryPrimitives.WriteInt64LittleEndian(new Span<byte>(bytes), (Int64)obj);
                    break;
                case CodeType.byte_hex:
                    return Encode(((byte)obj).ToString("x2").PadLeft(2, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.int16_hex:
                    return Encode(((Int16)obj).ToString("x2").PadLeft(4, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.int32_hex:
                    return Encode(((Int32)obj).ToString("x2").PadLeft(8, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.int64_hex:
                    return Encode(((Int64)obj).ToString("x2").PadLeft(16, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.uint16_hex:
                    return Encode(((UInt16)obj).ToString("x2").PadLeft(4, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.uint32_hex:
                    return Encode(((UInt32)obj).ToString("x2").PadLeft(8, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.uint64_hex:
                    return Encode(((UInt64)obj).ToString("x2").PadLeft(16, '0'), new CodeInfo
                    {
                        CodeType = CodeType.string_hex
                    });
                case CodeType.uint64_unix:
                    return Encode((UInt64)(((DateTime)obj) - localDefaultDateTime).TotalSeconds, new CodeInfo
                    {
                        CodeType = CodeType.uint64
                    });
                case CodeType.@enum:
                case CodeType.@string:
                default:
                    var stringBytes = GetZHCNEncoding().GetBytes(obj.ToString());
                    bytes = stringBytes.Padding(length ?? stringBytes.Length, GetPadding());
                    break;
                case CodeType.string_x_0:
                    var string_x_0 = (string)obj;
                    var string_x_0_bytes = new byte[string_x_0.Length / 2 + string_x_0.Length % 2];
                    for (int i = string_x_0.Length - 1; i >= 0; i -= 2)
                    {
                        string_x_0_bytes[i / 2] = byte.Parse($"{(i - 1 < 0 ? 0 : string_x_0[i - 1])}{string_x_0[i]}", NumberStyles.HexNumber);
                    }
                    bytes = string_x_0_bytes;
                    break;
                case CodeType.@string_x2:
                    bytes = new byte[] { byte.Parse((string)obj, NumberStyles.HexNumber) };
                    break;
                case CodeType.string_hex:
                    var value_string_hex = ((string)obj).Replace("0x", "");
                    var string_hex_bytes = new byte[value_string_hex.Length / 2];
                    for (int i = 0; i < string_hex_bytes.Length; i++)
                    {
                        string_hex_bytes[i] = byte.Parse(value_string_hex.Substring(i * 2, 2), NumberStyles.HexNumber);
                    }
                    bytes = string_hex_bytes;
                    break;
                case CodeType.string_ascii:
                    bytes = Encoding.ASCII.GetBytes((string)obj);
                    break;
                case CodeType.string_bcd8421:
                    bytes = Encode(((string)obj).ToCharArray(), new CodeInfo
                    {
                        CodeType = CodeType.char_bcd8421
                    });
                    break;
                case CodeType.char_bcd8421:
                    var charArray = (char[])obj;
                    bytes = new byte[charArray.Length / 2];
                    for (int i = 0, j = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = (byte)(
                                //高4位
                                (charArray[j++] - 48 << 4)
                                //低4位
                                | ((charArray[j++] - 48 & 0xf))
                            );
                    }
                    break;
                case CodeType.datetime_bcd8421:
                    var datetime = (DateTime)obj;
                    bytes = Encode(
                        $"{(datetime.Year % 100):00}{datetime.Month:00}{datetime.Day:00}" +
                        $"{datetime.Hour:00}{datetime.Minute:00}{datetime.Second:00}",
                        new CodeInfo
                        {
                            CodeType = CodeType.string_bcd8421
                        });
                    break;
                case CodeType.date_bcd8421:
                    var date = (DateTime)obj;
                    bytes = Encode(
                        $"{(date.Year % 100):00}{date.Month:00}{date.Day:00}",
                        new CodeInfo
                        {
                            CodeType = CodeType.string_bcd8421
                        });
                    break;
                case CodeType.time_bcd8421:
                    var time = (DateTime)obj;
                    bytes = Encode(
                        $"{time.Hour:00}{time.Minute:00}{time.Second:00}",
                        new CodeInfo
                        {
                            CodeType = CodeType.string_bcd8421
                        });
                    break;
                case CodeType.data:
                    var value_string = ((string)obj).Length / 2 == 0 ? (string)obj : $"0{obj}";
                    bytes = new byte[value_string.Length / 2];
                    for (int i = 0; i < value_string.Length - 1; i += 2)
                    {
                        bytes[i / 2] = byte.Parse($"{value_string[i]}{value_string[i + 1]}");
                    }
                    break;
                case CodeType.data_split:
                    bytes = ((string)obj).Split(' ').Select(o => byte.Parse(o)).ToArray();
                    break;
                case CodeType.json:
                    string jsonString;
                    if (string.IsNullOrWhiteSpace(info.TypeName))
                        jsonString = JsonConvert.SerializeObject(obj);
                    else
                    {
                        var jsonType = (string.IsNullOrWhiteSpace(info.Assembly) ?
                                Assembly.GetExecutingAssembly() :
                                Assembly.Load(info.Assembly))
                            .GetType(info.TypeName);
                        jsonString = JsonConvert.SerializeObject(obj, jsonType, null);
                    }
                    var jsonBytes = GetZHCNEncoding().GetBytes(jsonString);
                    bytes = jsonBytes.Padding(length ?? jsonBytes.Length, GetPadding());
                    break;
            }

            var result = padding ? bytes.Padding(length ?? bytes.Length, GetPadding()) : bytes;

            return escape ? Escape(result) : result;
        }

        /// <summary>
        /// 检查Crc校验码
        /// </summary>
        /// <param name="buffer">数据头+数据体+校验码</param>
        /// <param name="data">数据头+数据体</param>
        /// <param name="crc_code">校验码</param>
        /// <param name="crc_code_compute">计算后的值</param>
        /// <returns>是否一致</returns>
        public virtual bool CheckCrcCode(byte[] buffer, out byte[] data, out byte[] crc_code, out byte[] crc_code_compute)
        {
            crc_code = Protocol.CrcCcitt.ValueLength > 0 ?
                buffer.Take(Protocol.CrcCcitt.ValueLength).ToArray() :
                buffer.Skip(buffer.Length + Protocol.CrcCcitt.ValueLength).ToArray();

            data = buffer.Skip(Protocol.CrcCcitt.IgnoreLength?.Front ?? 0)
                        .Take(buffer.Length - (Protocol.CrcCcitt.IgnoreLength?.Posterior ?? 0) - Math.Abs(Protocol.CrcCcitt.ValueLength))
                        .ToArray();

            if (Protocol.CrcCcitt.Check)
                crc_code_compute = GetCrcCcitt().ComputeChecksumBytes(data);
            else
                crc_code_compute = crc_code;

            return crc_code.SequenceEqual(crc_code_compute);
        }

        /// <summary>
        /// 计算Crc校验码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual void ComputeCrcCode(IJTTPackageInfo info)
        {
            var crc = ComputeCrcValue(info.Buffer.ToArray());

            info.Crc_Code = new ReadOnlyMemory<byte>(crc);
        }

        /// <summary>
        /// 计算Crc校验码(未转义)
        /// </summary>
        /// <param name="buffer">流数据</param>
        /// <returns>crc校验值</returns>
        public virtual byte[] ComputeCrcValue(byte[] buffer)
        {
            return GetCrcCcitt().ComputeChecksumBytes(
                buffer
                .Skip(Protocol.CrcCcitt.IgnoreLength?.Front ?? 0)
                .Take(buffer.Length - Protocol.CrcCcitt.IgnoreLength?.Posterior ?? 0)
                .ToArray());
        }
    }
}
