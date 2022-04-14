using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Munchkin.Core.Contracts
{
    public abstract record Enumeration(int Code, string Name)
        : IComparable
    {
        public static IReadOnlyCollection<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetTypeInfo().GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly);

            List<T> enumerations = new();

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;
                if (locatedValue != null)
                {
                    enumerations.Add(locatedValue);
                }
            }

            return enumerations;
        }

        public static T Parse<T>(string name) where T : Enumeration, new()
        {
            return GetAll<T>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            Enumeration enumeration = obj as Enumeration;
            return Code.CompareTo(enumeration?.Code);
        }
    }
}
