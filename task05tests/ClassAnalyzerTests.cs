using Xunit;
using Moq;
using task05;

namespace task05tests;

public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }

    public void MethodParams(int Number) {}
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetMethodParams_WithoutParams_ReturnsEmptyMethodParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodparams = analyzer.GetMethodParams("Method");

        Assert.Empty(methodparams);
    }

    [Fact]
    public void GetMethodParams_WithParams_ReturnsCorrectMethodParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodparams = analyzer.GetMethodParams("MethodParams");

        Assert.Contains("Number", methodparams);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void HasAttribute_ReturnsTrueForAttributedClass()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        Assert.True(analyzer.HasAttribute<SerializableAttribute>());
    }

    [Fact]
    public void HasAttribute_ReturnsFalseForNonAttributedClass()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        Assert.False(analyzer.HasAttribute<SerializableAttribute>());
    }
}
