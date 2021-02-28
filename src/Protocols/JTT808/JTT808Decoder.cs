using Flee.PublicTypes;
using SuperSocket.JTT.JTT808.Const;
using SuperSocket.JTT.JTT808.Internal;
using SuperSocket.JTT.JTTBase.Extension;
using SuperSocket.JTT.JTTBase.Hadnler;
using SuperSocket.JTT.JTTBase.Interface;
using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SuperSocket.JTT.JTT808
{
    public class JTT808Decoder : JTTDecoder
    {
        public JTT808Decoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt808protocol = protocol as JTT808Protocol;
            subPackages = new ConcurrentDictionary<string, byte[][]>();
            checkSubPackagesExpire = new Timer(CheckSubPackagesExpire, null, checkSubPackagesExpirePeriod, checkSubPackagesExpirePeriod);
            subPackagesKeys = new ConcurrentQueue<string>();
            subPackagesTimeout = new ConcurrentDictionary<string, DateTime>();
        }

        #region 公共方法

        public override void Analysis(IJTTPackageInfo packageInfo, ReadOnlySpan<byte> bytes, ref int offset)
        {
            foreach (var structure in protocol.Structures)
            {
                AnalysisStructure(packageInfo, packageInfo, structure, bytes, ref offset);

                if (!structure.IsHeader)
                    continue;

                var jtt808packageInfo = packageInfo as JTT808PackageInfo;

                //获取消息体属性结构数据
                jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo = new MsgBodyProperty(jtt808packageInfo.JTT808MessageHeader.MsgBodyProperty);

                if (!jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo.SubPackage)
                    continue;

                var bodyBuffer = bytes.Slice(offset, jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo.Length)
                     .ToArray();

                //处理分包
                if (!IsSubPackagesComplete(jtt808packageInfo.JTT808MessageHeader, bodyBuffer, out byte[] completeBodyBuffer))
                {
                    jtt808packageInfo.SubPackagesComplete = false;
                    return;
                }

                jtt808packageInfo.SubPackagesComplete = true;

                //替换为完整的消息体数据
                bytes = new ReadOnlySpan<byte>(
                    bytes.Slice(0, offset).ToArray()
                    .Concat(completeBodyBuffer)
                    .Concat(bytes[(bytes.Length - offset - jtt808packageInfo.JTT808MessageHeader.MsgBodyPropertyInfo.Length)..].ToArray())
                    .ToArray());
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <param name="buffer">数据</param>
        /// <param name="structure">结构</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public override void Decrypt(IJTTPackageInfo packageInfo, ReadOnlySpan<byte> buffer, StructureInfo structure, int offset)
        {
            try
            {
                if (protocol.Encrypt.Targets?.ContainsKey(structure.Id) != true)
                    return;

                var encryptProperty = protocol.Encrypt.Targets[structure.Id];
                var flag = (bool)packageInfo.GetPropertyValue(encryptProperty.Flag.Split('.'));
                if (!flag)
                    return;

                var length = buffer.Length - offset - protocol.Structures.Where(o => o.Order > structure.Order).Sum(o => o.Length.Value);
                var key = (UInt32)packageInfo.GetPropertyValue(encryptProperty.Key.Split('.'));
                using MemoryStream ms_encrypt = new MemoryStream();
                var writer = new BinaryWriter(ms_encrypt);
                using (MemoryStream ms = new MemoryStream(buffer.ToArray()))
                {
                    var reader = new BinaryReader(ms);

                    //获取加密的数据
                    ms.Seek(offset, SeekOrigin.Begin);

                    //解密
                    while (ms.Position < length)
                    {
                        var Char = reader.ReadChar();

                        //将传输的数据与伪随机码按字节进行异或运算
                        key = protocol.Encrypt.IA1 * (key % protocol.Encrypt.M1) + protocol.Encrypt.IC1;
                        Char ^= (Char)((key << 20) & 0xff);

                        //写入解密的数据
                        writer.Write(Char);
                    }
                }
                buffer = ms_encrypt.ToArray();
            }
            catch (Exception ex)
            {
                throw new JTTException($"解密时发生错误, structureId: {structure.Id}.", ex);
            }
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// JTT协议
        /// </summary>
        readonly JTT808Protocol jtt808protocol;

        /// <summary>
        /// 分包数据
        /// </summary>
        readonly ConcurrentDictionary<string, byte[][]> subPackages;

        /// <summary>
        /// 定时检查器
        /// </summary>
        readonly Timer checkSubPackagesExpire;

        /// <summary>
        /// 定时检查时间间隔
        /// </summary>
        readonly TimeSpan checkSubPackagesExpirePeriod = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 当前分包数据量
        /// </summary>
        /// <remarks>单位 Byte</remarks>
        long currentSubPackagesBytes = 0;

        /// <summary>
        /// 分包数据标识队列
        /// </summary>
        readonly ConcurrentQueue<string> subPackagesKeys;

        /// <summary>
        /// 分包数据过期时间
        /// </summary>
        readonly ConcurrentDictionary<string, DateTime> subPackagesTimeout;

        /// <summary>
        /// 检查分包数据是否已过期
        /// </summary>
        void CheckSubPackagesExpire(object state)
        {
            _ = checkSubPackagesExpire.Change(Timeout.Infinite, Timeout.Infinite);

            switch (jtt808protocol.SubPackageExpire)
            {
                case Extension.SubPackageExpire.timeout:
                    var keys = subPackagesTimeout.Keys.ToArray();
                    foreach (var key in keys)
                    {
                        if (subPackagesTimeout[key] <= DateTime.Now)
                        {
                            subPackagesTimeout.TryRemove(key, out _);
                            subPackages.TryRemove(key, out byte[][] bodyBuffer);
                            for (int i = 0; i < bodyBuffer.Length; i++)
                            {
                                currentSubPackagesBytes -= bodyBuffer[i].Length;
                            }
                        }
                    }
                    break;
                case Extension.SubPackageExpire.threshold:
                default:
                    if (currentSubPackagesBytes >= jtt808protocol.SubPackageThresholdBytes)
                    {
                        while (subPackagesKeys.TryDequeue(out string key))
                        {
                            subPackages.TryRemove(key, out byte[][] bodyBuffer);
                            for (int i = 0; i < bodyBuffer.Length; i++)
                            {
                                currentSubPackagesBytes -= bodyBuffer[i].Length;
                            }

                            if (currentSubPackagesBytes < jtt808protocol.SubPackageThresholdBytes)
                                break;
                        }
                    }
                    break;
            }

            checkSubPackagesExpire.Change(checkSubPackagesExpirePeriod, checkSubPackagesExpirePeriod);
        }

        /// <summary>
        /// 是否已接收全部的分包数据
        /// </summary>
        /// <param name="JTT808MessageHeader">消息头</param>
        /// <param name="bodyBuffer">消息体流数据</param>
        /// <param name="completeBodyBuffer">完整的消息体流数据</param>
        bool IsSubPackagesComplete(JTT808MessageHeader JTT808MessageHeader, byte[] bodyBuffer, out byte[] completeBodyBuffer)
        {
            var key = $"TEL: {JTT808MessageHeader.Tel}, MSGID: {JTT808MessageHeader.MsgID}, Total:{JTT808MessageHeader.SubPackage.Total}";

            subPackages.AddOrUpdate(
               key,
               key =>
               {
                   var value = new byte[JTT808MessageHeader.SubPackage.Total][];
                   value[JTT808MessageHeader.SubPackage.Order] = bodyBuffer;
                   return value;
               },
               (key, oldValue) =>
               {
                   oldValue[JTT808MessageHeader.SubPackage.Order] = bodyBuffer;
                   return oldValue;
               });

            if (subPackages[key].Length == JTT808MessageHeader.SubPackage.Total)
            {
                completeBodyBuffer = subPackages[key].SelectMany(o => o).ToArray();

                subPackagesTimeout.TryRemove(key, out _);
                currentSubPackagesBytes -= completeBodyBuffer.Length;

                return true;
            }
            else
            {
                switch (jtt808protocol.SubPackageExpire)
                {
                    case Extension.SubPackageExpire.timeout:
                        var timeout = DateTime.Now.AddMilliseconds(jtt808protocol.SubPackageTimeout.Milliseconds);
                        subPackagesTimeout.AddOrUpdate(
                            key,
                            key => timeout,
                            (key, oldValue) => timeout);
                        break;
                    case Extension.SubPackageExpire.threshold:
                    default:
                        subPackagesKeys.Enqueue(key);
                        break;
                }

                currentSubPackagesBytes += bodyBuffer.Length;

                completeBodyBuffer = null;
                return false;
            }
        }

        #endregion
    }
}
