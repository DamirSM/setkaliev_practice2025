using task11;

namespace task11tests;

public class CalculatorGeneratorTests
{
    ICalculator _calc = CalculatorGenerator.CreateCalculator();
    
    [Fact]
    public void Add_WorksCorrectly()
    {
        Assert.Equal(7, _calc.Add(3, 4));
        Assert.Equal(-1, _calc.Add(3, -4));
    }

    [Fact]
    public void Minus_WorksCorrectly()
    {
        Assert.Equal(-1, _calc.Minus(3, 4));
        Assert.Equal(7, _calc.Minus(3, -4));
    }

    [Fact]
    public void Mul_WorksCorrectly()
    {
        Assert.Equal(12, _calc.Mul(3, 4));
        Assert.Equal(-12, _calc.Mul(3, -4));
    }

    [Fact]
    public void Div_WorksCorrectly()
    {
        Assert.Equal(2, _calc.Div(8, 4));
    }

    [Fact]
    public void Div_DivideByZero_ThrowsException()
    {
        Assert.Throws<System.DivideByZeroException>(() => _calc.Div(1, 0));
    }
}
