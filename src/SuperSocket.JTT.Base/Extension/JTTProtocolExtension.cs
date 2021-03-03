using Microservice.Library.Configuration;
using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using SuperSocket.ProtoBase;
using System.Collections.Generic;
using System.Linq;

namespace SuperSocket.JTT.Base.Extension
{
    /// <summary>
    /// JTT协议拓展方法
    /// </summary>
    public static class JTTProtocolExtension
    {
        #region 私有成员

        /// <summary>
        /// 获取协议
        /// </summary>
        /// <typeparam name="TJTTProtocol">JTT协议类型</typeparam>
        /// <param name="configFilePath">Json配置文件路径</param>
        /// <param name="section">板块</param>
        /// <returns></returns>
        static TJTTProtocol GetProtocol<TJTTProtocol>(string configFilePath, string section) where TJTTProtocol : IJTTProtocol
        {
            var protocol = new ConfigHelper(configFilePath).GetModel<TJTTProtocol>(section);
            if (protocol.Structures?.Any() == true)
                protocol.Structures = OrderBy(protocol.Structures);
            return protocol;
        }

        /// <summary>
        /// 排序并赋值初始Id
        /// </summary>
        /// <param name="structures"></param>
        static List<StructureInfo> OrderBy(List<StructureInfo> structures)
        {
            return structures.OrderBy(o => o.Order)
                .Select(
                    o =>
                    {
                        if (string.IsNullOrWhiteSpace(o.Id) && o.StructureType != StructureType.empty)
                            o.Id = o.GetDefaultStructureId();

                        if (o.Internal?.Any() == true)
                            o.Internal = o.Internal
                                .ToDictionary(
                                    ik => ik.Key,
                                    iv => OrderBy(iv.Value));

                        return o;
                    })
                .ToList();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取协议
        /// </summary>
        /// <typeparam name="TJTTProtocol">JTT协议类型</typeparam>
        /// <param name="configFilePath">Json配置文件路径</param>
        /// <param name="version">版本</param>
        /// <returns></returns>
        public static TJTTProtocol GetProtocol<TJTTProtocol>(string configFilePath, JTTVersion version) where TJTTProtocol : IJTTProtocol
        {
            var protocol = GetProtocol<TJTTProtocol>(configFilePath, version.ToString());
            return protocol;
        }

        /// <summary>
        /// 获取自定义协议
        /// </summary>
        /// <typeparam name="TJTTProtocol">JTT协议类型</typeparam>
        /// <param name="configFilePath">Json配置文件路径</param>
        /// <param name="section">板块</param>
        /// <returns></returns>
        public static TJTTProtocol GetCustomProtocol<TJTTProtocol>(string configFilePath, string section) where TJTTProtocol : IJTTProtocol
        {
            var protocol = GetProtocol<TJTTProtocol>(configFilePath, section);
            return protocol;
        }

        /// <summary>
        /// 获取JTT协议流数据拦截器
        /// </summary>
        /// <param name="protocol">JTT协议</param>
        /// <returns></returns>
        public static IPipelineFilter<IJTTPackageInfo> GetFilter(this IJTTProtocol protocol)
        {
            return protocol.JTTPipelineFilter ?? new JTTPipelineFilter(protocol);
        }

        /// <summary>
        /// 获取JTT协议处理类
        /// </summary>
        /// <param name="protocol">JTT协议</param>
        /// <returns></returns>
        public static IJTTProtocolHandler GetHandler(this IJTTProtocol protocol)
        {
            return protocol.Handler ?? new JTTProtocolHandler(protocol);
        }

        #endregion
    }
}
