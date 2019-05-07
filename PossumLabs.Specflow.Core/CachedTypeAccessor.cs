using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PossumLabs.Specflow.Core
{
    public static class CachedTypeAccessor
    {
        static CachedTypeAccessor()
        {
            Properties = new ConcurrentDictionary<Type, PropertyInfo[]>();
            Fields = new ConcurrentDictionary<Type, FieldInfo[]>();
            Methods = new ConcurrentDictionary<Type, MethodInfo[]>();
            Constructors = new ConcurrentDictionary<Type, ConstructorInfo[]>();
        }

        private static ConcurrentDictionary<Type, PropertyInfo[]> Properties { get; }
        public static PropertyInfo[] CachedGetProperties(this Type t)
            =>Properties.GetOrAdd(t, t.GetProperties(BindingFlags.Public | BindingFlags.Instance));

        private static ConcurrentDictionary<Type, FieldInfo[]> Fields { get; }
        public static FieldInfo[] CachedGetFields(this Type t)
            => Fields.GetOrAdd(t, t.GetFields(BindingFlags.Public | BindingFlags.Instance));

        private static ConcurrentDictionary<Type, MethodInfo[]> Methods { get; }
        public static MethodInfo[] CachedGetMethods(this Type t)
            => Methods.GetOrAdd(t, t.GetMethods());

        private static ConcurrentDictionary<Type, ConstructorInfo[]> Constructors { get; }
        public static ConstructorInfo[] CachedGetConstructors(this Type t)
            => Constructors.GetOrAdd(t, t.GetConstructors(BindingFlags.Public | BindingFlags.Instance));
    }
}
