using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Extension
{
    /// <summary>
    /// 反射功能拓展方法
    /// </summary>
    public static class ReflectionExtension
    {
        static BindingFlags bindingFlags { get; }
             = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;

        /// <summary>
        /// 是否存在指定属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldWithMultiLevel">
        /// 多层级字段
        /// <para>例如: ["FieldA", "FieldB", "FieldC"] => ModelA.FieldA.Field.B.FieldC</para>
        /// </param>
        /// <returns></returns>
        public static bool ContainsProperty(this object obj, IEnumerable<string> fieldWithMultiLevel)
        {
            try
            {
                return obj.GetProperty(fieldWithMultiLevel) != null;
            }
            catch (Exception ex)
            {
                throw new JTTException($"检查属性值错误: obj : [{obj?.GetType().FullName}] fieldWithMultiLevel : [{string.Join(".", fieldWithMultiLevel)}]", ex);
            }
        }

        /// <summary>
        /// 获取指定属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldWithMultiLevel">
        /// 多层级字段
        /// <para>例如: ["FieldA", "FieldB", "FieldC"] => ModelA.FieldA.Field.B.FieldC</para>
        /// </param>
        /// <param name="returnNull">为找到时返回null</param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(this object obj, IEnumerable<string> fieldWithMultiLevel, bool returnNull = false)
        {
            try
            {
                var property = obj.GetType()
                                .GetProperty(fieldWithMultiLevel.First(), bindingFlags);

                if (property == null && returnNull)
                    return null;

                if (fieldWithMultiLevel.Count() > 1)
                    return property.GetProperty(fieldWithMultiLevel.Skip(1));

                return property;
            }
            catch (Exception ex)
            {
                throw new JTTException($"获取属性值错误: obj : [{obj?.GetType().FullName}] fieldWithMultiLevel : [{string.Join(".", fieldWithMultiLevel)}]", ex);
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="fieldWithMultiLevel">
        /// 多层级字段
        /// <para>例如: ["FieldA", "FieldB", "FieldC"] => ModelA.FieldA.Field.B.FieldC</para>
        /// </param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this object obj, IEnumerable<string> fieldWithMultiLevel)
        {
            try
            {
                var value = obj.GetType()
                                .GetProperty(fieldWithMultiLevel.First())
                                .GetValue(obj);

                if (fieldWithMultiLevel.Count() > 1)
                    return value.GetPropertyValue(fieldWithMultiLevel.Skip(1));

                return value;
            }
            catch (Exception ex)
            {
                throw new JTTException($"获取属性值错误: obj : [{obj?.GetType().FullName}] fieldWithMultiLevel : [{string.Join(".", fieldWithMultiLevel)}]", ex);
            }
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj">实例</param>
        /// <param name="fieldWithMultiLevel">
        /// 多层级字段
        /// <para>例如: ["FieldA", "FieldB", "FieldC"] => ModelA.FieldA.Field.B.FieldC</para>
        /// </param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetValueToProperty(this object obj, IEnumerable<string> fieldWithMultiLevel, object value)
        {
            try
            {
                var property = obj.GetType()
                                .GetProperty(fieldWithMultiLevel.First());

                if (fieldWithMultiLevel.Count() > 1)
                    property.GetValue(obj)
                        .SetValueToProperty(fieldWithMultiLevel.Skip(1), value);
                else
                    property.SetValue(obj, value);
            }
            catch (Exception ex)
            {
                throw new JTTException($"设置属性值错误: obj : [{obj?.GetType().FullName}] fieldWithMultiLevel : [{string.Join(".", fieldWithMultiLevel)}]", ex);
            }
        }
    }
}
