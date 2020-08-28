using System;
using System.ComponentModel;
using System.Linq;
using Common.Enum;

namespace Common.Extension
{
    public static class EnumExtension
    {
        public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            var type = EnumValue.GetType();
            var members = type.GetMember(EnumValue.ToString());
            var attributes = members.FirstOrDefault()?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)(attributes ?? throw new InvalidOperationException()).FirstOrDefault())?.Description;
            return description;

        }
    }
}