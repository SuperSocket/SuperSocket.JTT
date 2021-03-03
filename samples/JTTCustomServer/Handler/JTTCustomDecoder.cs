using JTTCustomServer.Model;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using System;

namespace JTTCustomServer.Handler
{
    /// <summary>
    /// 自定义JTT协议解码器
    /// </summary>
    public class JTTCustomDecoder : JTTDecoder
    {
        public JTTCustomDecoder(IJTTProtocol protocol)
            : base(protocol)
        {
            jttCustomprotocol = protocol as JTTCustomProtocol;
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
                //无需解密
                return;
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
#pragma warning disable IDE0052 // 删除未读的私有成员
        readonly JTTCustomProtocol jttCustomprotocol;
#pragma warning restore IDE0052 // 删除未读的私有成员

        #endregion
    }
}
