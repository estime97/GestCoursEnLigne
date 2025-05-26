using System;
using System.ComponentModel;
using System.Reflection;

namespace BlazorHero.CleanArchitecture.Shared.Enums
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T GetValue<T>(string description) where T : Enum
        {
            var enumType = typeof(T);
            foreach (var field in enumType.GetFields())
            {
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attribute != null && attribute.Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException($"Aucune valeur d'énumération ne correspond à la description '{description}'.");
        }
    }
}
