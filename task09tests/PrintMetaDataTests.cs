using task07;
using task09;

namespace task09tests;

public class PrintMetaDataTests
{
    [Fact]
    public void PrintMetaData_PrintsCorrectMetadata_ForSeveralClasses()
    {
        string dllPath = typeof(SampleClass).Assembly.Location;

        var output = new StringWriter();
        Console.SetOut(output);
        PrintMetaData.Main(new[] {dllPath});
        var outputString = output.ToString();

        var sections = outputString.Split("Class Name - ").Select(s => "Class Name - " + s).ToList();

        var firstSection = sections.First(s => s.Contains("Class Name - SampleClass")).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var secondSection = sections.First(s => s.Contains("Class Name - SampleClass2")).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        Assert.Contains("Class Name - SampleClass", firstSection);
        Assert.Contains("Attribute - DisplayNameAttribute", firstSection);
        Assert.Contains("DisplayName - Пример класса", firstSection);
        Assert.Contains("Attribute - VersionAttribute", firstSection);
        Assert.Contains("Major - 1", firstSection);
        Assert.Contains("Minor - 0", firstSection);

        Assert.Contains("Methods:", firstSection);
        Assert.Contains("Method - TestMethod", firstSection);
        Assert.Contains("Attribute - DisplayNameAttribute", firstSection);
        Assert.Contains("DisplayName - Тестовый метод", firstSection);

        Assert.Contains("Properties:", firstSection);
        Assert.Contains("Property - Number", firstSection);
        Assert.Contains("Attribute - DisplayNameAttribute", firstSection);
        Assert.Contains("DisplayName - Числовое свойство", firstSection);

        Assert.Contains("Constructors:", firstSection);
        Assert.Contains("SampleClass(Int32 Number, String String)", firstSection);

        Assert.Contains("Class Name - SampleClass2", secondSection);
        Assert.DoesNotContain("Attribute - DisplayNameAttribute", secondSection);
        Assert.DoesNotContain("Attribute - VersionAttribute", secondSection);
        Assert.Contains("Methods:", secondSection);
        Assert.Contains("Method - TestMethod2", secondSection);
        Assert.DoesNotContain("Attribute - DisplayNameAttribute", secondSection);
        Assert.Contains("Properties:", secondSection);
        Assert.Contains("Property - String2", secondSection);
        Assert.DoesNotContain("Attribute - DisplayNameAttribute", secondSection);
        Assert.Contains("Constructors:", secondSection);
        Assert.Contains("SampleClass2(String String2)", secondSection);
    }
}
