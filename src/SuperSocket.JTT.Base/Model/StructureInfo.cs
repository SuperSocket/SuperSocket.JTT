using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 结构信息
    /// </summary>
    public class StructureInfo
    {
        /// <summary>
        /// 标识
        /// <para>用于排查错误</para>
        /// <para>默认值 $"{Order}-{Property}-{Explain}"</para>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否为消息头
        /// </summary>
        /// <remarks>默认 false</remarks>
        public bool IsHeader { get; set; } = false;

        /// <summary>
        /// 是否为消息体
        /// </summary>
        /// <remarks>默认 false</remarks>
        public bool IsBody { get; set; } = false;

        /// <summary>
        /// 排序值
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        /// <remarks>值为空时将不使用该属性</remarks>
        public bool IsNullable { get; set; } = false;

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        /// <remarks>转义前的长度</remarks>
        public int? Length { get; set; }

        /// <summary>
        /// 结构类型
        /// </summary>
        public StructureType StructureType { get; set; } = StructureType.normal;

        /// <summary>
        /// 数据的编码方式
        /// </summary>
        public CodeInfo Encode { get; set; }

        /// <summary>
        /// 先进行动态计算
        /// </summary>
        /// <remarks>
        /// <para>默认 true</para>
        /// <para>当同时配置了动态计算和数据映射时，决定先执行哪个操作</para>
        /// <para>true 动态计算优先于数据映射</para>
        /// <para>false 动态计算迟于数据映射</para>
        /// </remarks>
        public bool CompileFirst { get; set; } = true;

        /// <summary>
        /// 动态计算
        /// </summary>
        /// <remarks></remarks>
        public CompileConfig Compile { get; set; }

        /// <summary>
        /// 数据映射
        /// </summary>
        /// <remarks></remarks>
        public DataMappingConfig DataMapping { get; set; }

        /// <summary>
        /// 内部结构的标识
        /// <para>Internal的Key值</para>
        /// <para>决定使用哪个内部结构</para>
        /// <para>为null时默认取第一个</para>
        /// </summary>
        public InternalKeyInfo InternalKey { get; set; }

        /// <summary>
        /// 内部结构的长度信息集合
        /// <para>Key对应Internal的Key值</para>
        /// </summary>
        public Dictionary<string, InternalLengthInfo> InternalLength { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public CollectionInfo Collection { get; set; }

        /// <summary>
        /// 内部结构集合
        /// </summary>
        public Dictionary<string, List<StructureInfo>> Internal { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public AdditionalInfo Additional { get; set; }

        /// <summary>
        /// 位标识数据对应的结构体数据
        /// </summary>
        public FlagStructInfo FlagStruct { get; set; }

        /// <summary>
        /// 是否需要动态计算
        /// </summary>
        /// <returns></returns>
        public bool NeedCompile => Compile != null;

        /// <summary>
        /// 是否需要数据映射
        /// </summary>
        /// <returns></returns>
        public bool NeedDataMapping => DataMapping != null;

        /// <summary>
        /// 是否需要将位标识数组转为结构体数据
        /// </summary>
        /// <returns></returns>
        public bool NeedToFlagStruct => FlagStruct != null;

        /// <summary>
        /// 是否为数据集合
        /// </summary>
        /// <returns></returns>
        public bool IsCollection => Collection != null;
    }
}
