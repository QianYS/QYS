using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace QYS.Service.Tool
{
    /// <summary>
    /// 定义枚举扩展类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举变量值的 Description 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param>
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetDescription(this object obj, bool isTop = false)
        {
            if (obj == null)
                return string.Empty;

            var enumType = obj.GetType();
            DescriptionAttribute dna;
            if (isTop)
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
            else
            {
                var fi = enumType.GetField(Enum.GetName(enumType, obj));
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            }
            if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                return dna.Description;

            return obj.ToString();
        }
        /// <summary>
        /// 获取枚举变量值的 ExtendProperty 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <returns>如果包含 ExtendProperty 属性，则返回 ExtendProperty 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetExtendProperty(this object obj)
        {
            if (obj == null)
                return string.Empty;

            var enumType = obj.GetType();

            var fi = enumType.GetField(Enum.GetName(enumType, obj));
            var dna = (ExtendPropertyAttribute)Attribute.GetCustomAttribute(
                fi, typeof(ExtendPropertyAttribute));

            if (dna != null && !string.IsNullOrEmpty(dna.ExtendProperty))
                return dna.ExtendProperty;

            return obj.ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ExtendPropertyAttribute : Attribute
    {
        public readonly string ExtendProperty;

        public ExtendPropertyAttribute(string extendProperty)
        {
            ExtendProperty = extendProperty;
        }
    }
}
