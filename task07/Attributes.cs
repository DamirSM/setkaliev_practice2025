using System;
using System.Reflection;

namespace task07;

[AttributeUsage(AttributeTargets.All)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }

    public DisplayNameAttribute(string DisplayName)
    {
        this.DisplayName = DisplayName;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    public int Major { get; }
    public int Minor { get; }

    public VersionAttribute(string Version)
    {
        string[] VersionFormat = Version.Split('.');
        Major = Convert.ToInt32(VersionFormat[0]);
        Minor = Convert.ToInt32(VersionFormat[1]);
    }
}

[DisplayNameAttribute("Пример класса")]
[VersionAttribute("1.0")]
public class SampleClass
{
    [DisplayNameAttribute("Тестовый метод")]
    public void TestMethod() {}

    [DisplayNameAttribute("Числовое свойство")]
    public int Number { get; set; }

    public string String;

    public SampleClass(int Number, string String)
    {
        this.Number = Number;
        this.String = String;
    }
}

public class SampleClass2
{
    public void TestMethod2() {}

    public string String2 { get; set; }

    public SampleClass2(string String2)
    {
        this.String2 = String2;
    }
}

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayAttr = type.GetCustomAttribute<DisplayNameAttribute>();
        string displayName = displayAttr?.DisplayName ?? string.Empty;
        Console.WriteLine("Display Name - {0}", displayName);

        var versionAttr = type.GetCustomAttribute<VersionAttribute>();
        string major = versionAttr?.Major.ToString() ?? string.Empty;
        string minor = versionAttr?.Minor.ToString() ?? string.Empty;
        Console.WriteLine("Version - {0}.{1}", major, minor);

        Console.WriteLine("Methods:");
        foreach (var method in type.GetMethods())
        {
            var methodAttr = method.GetCustomAttribute<DisplayNameAttribute>();
            string methodDisplay = methodAttr?.DisplayName ?? string.Empty;
            Console.WriteLine("{0}: Display Name - {1}", method.Name, methodDisplay);
        }

        Console.WriteLine("Properties:");
        foreach (var prop in type.GetProperties())
        {
            var propAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
            string propDisplay = propAttr?.DisplayName ?? string.Empty;
            Console.WriteLine("{0}: Display Name - {1}", prop.Name, propDisplay);
        }
    }
}
