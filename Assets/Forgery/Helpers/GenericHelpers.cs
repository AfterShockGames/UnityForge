using System;
using System.Collections.Generic;
using System.Reflection;

internal class GenericHelpers
{
    /// <summary>
    ///     Gets all types with based on an attribute
    /// </summary>
    /// <param name="attribute">The attribute to look for</param>
    /// <param name="assembly">The assembly to look in</param>
    /// <returns>Enumerable of the found types</returns>
    public static IEnumerable<Type> GetTypesWithAttribute(Type attribute, Assembly assembly) 
    {
        if (assembly == null)
        {
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assem.GetTypes())
                {
                    if (type.GetCustomAttributes(attribute, true).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }
        else
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(attribute, true).Length > 0)
                {
                    yield return type;
                }
            }
        }
    }

    /// <summary>
    ///     Get attributes on class
    /// </summary>
    /// <param name="attribute">Attribute to look for</param>
    /// <param name="classType">Class to look in</param>
    /// <returns>An array of found attributes matching the supplied attribute</returns>
    public static Attribute[] GetClassAttributes(Type attribute, Type classType)
    {
        return Attribute.GetCustomAttributes(classType, attribute);
    }

    public static T GetAttribute<T>(Type t) where T : Attribute
    {
        T attribute = (T)Attribute.GetCustomAttribute(t, typeof(T));

        return attribute;
    }
}