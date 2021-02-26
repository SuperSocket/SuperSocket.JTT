using SuperSocket.JTTBase.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTTBase.Extension
{
    /// <summary>
    /// 位数组拓展方法
    /// </summary>
    public static class BitArrayExtension
    {
        static readonly Type flagIndexAttribute = typeof(FlagIndexAttribute);

        /// <summary>
        /// 获取位标识数组
        /// </summary>
        /// <param name="flagStruct">结构体数据</param>
        /// <param name="type">结构体类型</param>
        /// <returns></returns>
        public static BitArray GetBitArray(this object flagStruct, Type type = null)
        {
            if (type == null)
                type = flagStruct.GetType();

            var bits = type.GetProperties()
                  .Where(p => p.IsDefined(flagIndexAttribute, false))
                  .Select(p =>
                  {
                      var flagIndex = p.GetCustomAttribute<FlagIndexAttribute>();

                      var value = p.GetValue(flagStruct);

                      bool fill = false;
                      bool[] bits_;

                      if (flagIndex.BeginToEnd)
                      {
                          var value_ = (bool[])value;
                          var length = flagIndex.Index[1] - flagIndex.Index[0] + 1;
                          if (value_ == null || value_.Length != length)
                          {
                              bits_ = new bool[length];
                              fill = true;
                          }
                          else
                              bits_ = value_;
                      }
                      else
                      {
                          if (flagIndex.Index == null)
                              bits_ = Array.Empty<bool>();
                          else if (flagIndex.Index.Length > 1)
                          {
                              var value_ = (bool[])value;
                              if (flagIndex.Index.Length != value_.Length)
                                  bits_ = new bool[flagIndex.Index.Length];
                              else
                                  bits_ = value_;
                          }
                          else
                              bits_ = new bool[] { (bool)value };
                      }

                      if (fill)
                          Array.Fill(bits_, flagIndex.Default);

                      return new
                      {
                          Index = flagIndex.Index?.First() ?? 0,
                          Bits = bits_
                      };
                  })
                  .OrderBy(p => p.Index)
                  .SelectMany(p => p.Bits)
                  .ToArray();

            return new BitArray(bits);
        }

        /// <summary>
        /// 获取位标识数组
        /// </summary>
        /// <typeparam name="T">结构体类型</typeparam>
        /// <param name="flagStruct">结构体数据</param>
        /// <returns></returns>
        public static BitArray GetBitArray<T>(this T flagStruct) where T : struct
        {
            return flagStruct.GetBitArray(typeof(T));
        }

        /// <summary>
        /// 获取位标识结构体数据
        /// </summary>
        /// <param name="bitArray">位标识数组</param>
        /// <param name="type">结构体类型</param>
        /// <returns></returns>
        public static object GetFlagStruct(this BitArray bitArray, Type type)
        {
            var flagStruct = type.Assembly.CreateInstance(type.FullName);

            var properties = type.GetProperties()
                   .Where(p => p.IsDefined(flagIndexAttribute, false));

            foreach (var property in properties)
            {
                var flagIndex = property.GetCustomAttribute<FlagIndexAttribute>();

                bool[] value;

                if (flagIndex.BeginToEnd)
                {
                    value = new bool[flagIndex.Index[1] - flagIndex.Index[0] + 1];
                }
                else if (flagIndex.Index.Length > 1)
                {
                    value = new bool[flagIndex.Index.Length];
                }
                else
                {
                    property.SetValue(flagStruct, bitArray[flagIndex.Index[0]]);
                    continue;
                }

                var length = flagIndex.Index.Last();

                for (int i = flagIndex.Index[0]; i <= length; i++)
                {
                    value[i - flagIndex.Index[0]] = bitArray[i];
                }

                property.SetValue(flagStruct, value);
            }

            return flagStruct;
        }

        /// <summary>
        /// 获取位标识结构体数据
        /// </summary>
        /// <typeparam name="T">结构体类型</typeparam>
        /// <param name="bitArray">位标识数组</param>
        /// <returns></returns>
        public static T GetFlagStruct<T>(this BitArray bitArray) where T : struct
        {
            return (T)bitArray.GetFlagStruct(typeof(T));
        }
    }
}
