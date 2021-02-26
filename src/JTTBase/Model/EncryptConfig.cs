using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 加密配置
    /// </summary>
    public class EncryptConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public UInt32 M1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 IA1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 IC1 { get; set; }

        /// <summary>
        /// 目标集合
        /// <para>键 <see cref="StructureInfo.Id"/></para>
        /// </summary>
        public Dictionary<string, EncryptProperty> Targets { get; set; }
    }
}
