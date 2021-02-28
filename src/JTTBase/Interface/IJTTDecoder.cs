using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Interface
{
    /// <summary>
    /// JTT协议解码器
    /// </summary>
    /// <remarks>
    /// <para>解码发生异常 <see cref="ApplicationException"/>时,</para>
    /// <para>应当设置 <see cref="IJTTPackageInfo.Success"/>为false,</para>
    /// <para>并写入异常 <see cref="IJTTPackageInfo.Exception"/>,</para>
    /// <para>最后调用 <see cref="IPackageDecoder{TPackageInfo}.Decode(ref ReadOnlySequence{byte}, object)"/>方法输出消息包.</para>
    /// </remarks>
    public interface IJTTDecoder : IPackageDecoder<IJTTPackageInfo>
    {

    }
}
