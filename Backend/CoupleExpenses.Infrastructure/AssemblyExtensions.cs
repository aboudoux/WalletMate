using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoupleExpenses.Infrastructure
{
    public static class AssemblyExtensions 
    {
        public static IEnumerable<Type> GetAllConcreteTypeThatImplementInterface<TInterface>(this Assembly assembly)
            => GetAllConcreteTypeThatImplementInterface(assembly, typeof(TInterface));

        public static IEnumerable<Type> GetAllConcreteTypeThatImplementInterface(this Assembly assembly, Type @interface) 
        {
            Type[] allConcreteTypes;
            try {
                allConcreteTypes = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex) {
                allConcreteTypes = ex.Types.Where(a => a != null).ToArray();
            }

            return allConcreteTypes.Where(t => t.GetInterfaces().Any(i => i.Name == @interface.Name) && (t.IsClass || IsStruct(t)) && !t.IsAbstract).Distinct();

            bool IsStruct(Type type) => type.IsValueType && !type.IsPrimitive && !type.Namespace.StartsWith("System") &&!type.IsEnum;
        }
    }
}