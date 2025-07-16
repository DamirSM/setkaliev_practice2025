using task14;

namespace task14tests;

public class DefiniteIntegralTests
{
    private static readonly Func<double, double> X = x => x;
    private static readonly Func<double, double> SIN = x => Math.Sin(x);

    [Fact]
    public void Solve_IntegralOfX_FromMinus1To1_Equals0()
    {
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
    }

    [Fact]
    public void SolveSingle_IntegralOfX_FromMinus1To1_Equals0()
    {
        Assert.Equal(0, DefiniteIntegral.SolveSingle(-1, 1, X, 1e-4), 1e-4);
    }

    [Fact]
    public void Solve_IntegralOfSin_FromMinus1To1_Equals0()
    {
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
    }

    [Fact]
    public void SolveSingle_IntegralOfSin_FromMinus1To1_Equals0()
    {
        Assert.Equal(0, DefiniteIntegral.SolveSingle(-1, 1, SIN, 1e-5), 1e-4);
    }

    [Fact]
    public void Solve_IntegralOfX_From0To5_Equals12Point5()
    {
        Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
    }

    [Fact]
    public void SolveSingle_IntegralOfX_From0To5_Equals12Point5()
    {
        Assert.Equal(12.5, DefiniteIntegral.SolveSingle(0, 5, X, 1e-6), 1e-5);
    }
}
