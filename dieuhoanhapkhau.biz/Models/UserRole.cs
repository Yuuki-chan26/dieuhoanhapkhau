using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dieuhoanhapkhau.biz.Models
{
    public enum UserRole : short
    {
        /// <summary>
        /// Represents a role of a registered user.
        /// </summary>
        Users = 1,
        /// <summary>
        /// Represents a role of a member that have been promoted by an admin or moderator.
        /// </summary>
        Managers = 2,
        /// <summary>
        /// Forum moderator
        /// </summary>
        Moderator = 3,
        /// <summary>
        /// Admin role
        /// </summary>
        Admin = 10
    }


    public class ListUserRole
    {
        public static IEnumerable<Enum> GetListValues(Enum UserRole)
        {
            List<Enum> enumerations = new List<Enum>();
            foreach (FieldInfo fieldInfo in UserRole.GetType().GetFields(
                  BindingFlags.Static | BindingFlags.Public))
            {
                enumerations.Add((Enum)fieldInfo.GetValue(UserRole));
            }
            return enumerations;
        }

        public static List<T> GetListEnumValues<T>() where T : new()
        {
            var valueType = new T();
            return typeof(T).GetFields()
                .Select(fieldInfo => (T)fieldInfo.GetValue(valueType))
                .Distinct()
                .ToList();
        }

        public static List<String> GetListStringEnumNames<T>()
        {
            return typeof(T).GetFields()
                .Select(info => info.Name)
                .Distinct()
                .ToList();
        }
    }

    public static class Enum<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public static IEnumerable<T> GetValues()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
