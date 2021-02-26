using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Annotations
{
    /// <summary>
    /// 位标识序号
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FlagIndexAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">序号（从0开始）</param>
        public FlagIndexAttribute(params int[] index)
        {
            Index = index;
        }

        /// <summary>
        /// 序号（从0开始）
        /// </summary>
        public int[] Index { get; set; }

        /// <summary>
        /// 开始序号到结束序号
        /// </summary>
        /// <remarks>
        /// <para>默认 false</para>
        /// <para>Index[0] 开始序号</para>
        /// <para>Index[1] 结束序号</para>
        /// <para>用于保留位</para>
        /// </remarks>
        public bool BeginToEnd { get; set; } = false;

        /// <summary>
        /// 属性值为空时的默认值
        /// </summary>
        /// <remarks>默认 false</remarks>
        public bool Default { get; set; } = false;
    }
}
