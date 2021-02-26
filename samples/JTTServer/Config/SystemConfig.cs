﻿using Library.Configuration.Annotations;
using Library.Models;
using SuperSocket.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTTServer.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 日志存储类型
        /// </summary>
        public LoggerType LoggerType { get; set; }

        /// <summary>
        /// 接入码
        /// </summary>
        public UInt32[] GnsscenterID { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 监听IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 监听端口号
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 日志备份数量
        /// </summary>
        public int ServerBackLog { get; set; }

        /// <summary>
        /// 启用转发
        /// </summary>
        public bool EnableForward { get; set; }

        /// <summary>
        /// JTT协议版本
        /// </summary>
        public JTTVersion JTTVersion { get; set; }

        /// <summary>
        /// 协议版本号标识
        /// </summary>
        /// <remarks>
        /// <para>3字节</para>
        /// <para>上下级平台之间采用的标准协议版本编号</para>
        /// <para>0x01 0x02 0x0F 表示的版本号是 V1.2.15，依此类推</para>
        /// </remarks>
        public byte[] JTT809VersionFlag { get; set; }

        /// <summary>
        /// JTT协议Json配置文件路径
        /// </summary>
        public string JTTConfigFilePath { get; set; }

        /// <summary>
        /// 独立配置结构集合
        /// </summary>
        /// <remarks>避免把所有配置全放置在一个Json文件中</remarks>
        [JsonConfig("jsonconfig/jtt809-protocol-structures.json", "Structures")]
        public List<StructureInfo> Structures { get; set; }

        /// <summary>
        /// 独立配置数据映射集合
        /// </summary>
        /// <remarks>避免把所有配置全放置在一个Json文件中</remarks>
        [JsonConfig("jsonconfig/jtt809-protocol-datamappings.json", "DataMappings")]
        public Dictionary<string, List<MatcheInfo>> DataMappings { get; set; }

        /// <summary>
        /// 独立配置内部实体映射集合
        /// </summary>
        /// <remarks>避免把所有配置全放置在一个Json文件中</remarks>
        [JsonConfig("jsonconfig/jtt809-protocol-internalentitysmappings.json", "InternalEntitysMappings")]
        public Dictionary<string, InternalEntitysMappingInfo> InternalEntitysMappings { get; set; }
    }
}