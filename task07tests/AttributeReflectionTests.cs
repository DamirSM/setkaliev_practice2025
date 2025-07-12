using Xunit;
using System.Reflection;
using task07;

namespace task07tests;

public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = method?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = prop?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }

    [Fact]
    public void PrintTypeInfo_WritesCorrectInfo_ForSampleClass()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        ReflectionHelper.PrintTypeInfo(typeof(SampleClass));
        var outputList = output.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        Assert.Contains("Display Name - Пример класса", outputList);
        Assert.Contains("Version - 1.0", outputList);
        Assert.Contains("Methods:", outputList);
        Assert.Contains("TestMethod: Display Name - Тестовый метод", outputList);
        Assert.Contains("Properties:", outputList);
        Assert.Contains("Number: Display Name - Числовое свойство", outputList);
    }
}
