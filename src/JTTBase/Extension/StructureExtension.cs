using SuperSocket.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Extension
{
    /// <summary>
    /// JTT结构拓展方法
    /// </summary>
    public static class StructureExtension
    {
        /// <summary>
        /// 获取默认的结构标识
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static string GetDefaultStructureId(this StructureInfo structure)
        {
            return $"{structure.Order}-{structure.Property}-{structure.Explain}";
        }
    }
}
