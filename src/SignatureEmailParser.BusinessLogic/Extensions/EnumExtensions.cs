using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SignatureEmailParser.BusinessLogic.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes is null || !attributes.Any() ? source.ToString() : attributes[0].Description;
        }
    }
}
