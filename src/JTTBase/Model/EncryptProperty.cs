using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 加密标识和秘钥来自于哪些属性
    /// </summary>
    public class EncryptProperty
    {
        /// <summary>
        /// 加密标识属性名称
        /// </summary>
        /// <remarks>用该属性的值来判断是否需要解密</remarks>
        public string Flag { get; set; }

        /// <summary>
        /// 秘钥属性名称
        /// </summary>
        /// <remarks>用该属性的值来获取用于解密的密钥</remarks>
        public string Key { get; set; }
    }
}
