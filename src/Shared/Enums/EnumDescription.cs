using System;
using System.ComponentModel;
using System.Reflection;

namespace BlazorHero.CleanArchitecture.Shared.Enums
{
    public static class EnumDescription
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
