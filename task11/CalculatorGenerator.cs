using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11;

public interface ICalculator
{
    int Add(int a, int b);
    int Minus(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public class CalculatorGenerator
{
    const string code = @"
    using task11;
    public class Calculator : ICalculator
    {
        public int Add(int a, int b) => a + b;
        public int Minus(int a, int b) => a - b;
        public int Mul(int a, int b) => a * b;
        public int Div(int a, int b) => a / b;
    }";
    public static ICalculator CreateCalculator()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var refs = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create("CalculatorAssembly", new[] {syntaxTree}, refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        var calcType = assembly.GetType("Calculator")!;

        var obj = Activator.CreateInstance(calcType)!;

        return (ICalculator) obj;
    }
}
