using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    {
        IEnumerable<string> methods = _type.GetMethods().Select(m => m.Name);
        return methods;
    }

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        IEnumerable<string> methodparams = _type.GetMethods().Where(m => m.Name == methodname).SelectMany(m => m.GetParameters()).Select(mp => mp.Name ?? string.Empty);
        return methodparams;
    }

    public IEnumerable<string> GetAllFields()
    {
        IEnumerable<string> fields = _type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Select(f => f.Name ?? string.Empty);
        return fields;
    }

    public IEnumerable<string> GetProperties()
    {
        IEnumerable<string> properties = _type.GetProperties().Select(p => p.Name);
        return properties;
    }

    public bool HasAttribute<T>() where T : Attribute
    {
        return Attribute.IsDefined(_type, typeof(T));
    }
}
