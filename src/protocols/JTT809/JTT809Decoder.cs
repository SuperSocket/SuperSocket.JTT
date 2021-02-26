using Flee.PublicTypes;
using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Hadnler;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTT809
{
    public class JTT809Decoder : JTTDecoder
    {
        public JTT809Decoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jtt809protocol = protocol as JTT809Protocol;
        }

        #region 公共方法

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
        readonly JTT809Protocol jtt809protocol;

        #endregion
    }
}
