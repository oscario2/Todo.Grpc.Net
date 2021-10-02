using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Todo.Grpc.FireBase.Extensions
{
    public static class EnumDisplayAttribute
    {
        /// <summary>
        /// extended from https://stackoverflow.com/a/25109341
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static DisplayAttribute? GetDisplayAttributes(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
        }
    }
}