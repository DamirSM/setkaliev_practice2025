using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double SolveSingle(double a, double b, Func<double, double> function, double step)
    {
        double sum = 0;
        for (double x = a; x < b; x += step)
        {
            double x2 = Math.Min(x + step, b);
            sum += (function(x) + function(x2)) * (x2 - x) / 2;
        }
        return sum;
    }

    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        if (threadsNumber < 1)
            throw new ArgumentException("Количество потоков должно быть по крайней мере равно 1", nameof(threadsNumber));
        if (step <= 0)
            throw new ArgumentException("Шаг интегрирования должен быть положительным", nameof(step));
        if (b <= a)
            throw new ArgumentException("Верхний предел интегрирования должен быть больше нижнего");

        double result = 0;
        
        double segmentLength = (b - a) / threadsNumber;
        double[] threads = new double[threadsNumber];
        ParallelOptions options = new ParallelOptions();
        Parallel.For(
            0,
            threadsNumber,
            options,
            i =>
            {
                double localStart = a + i * segmentLength;
                double localEnd;
                if (i == threadsNumber - 1)
                    localEnd = b;
                else
                    localEnd = localStart + segmentLength;
                
                double partialSum = 0;
                double x = localStart;
                while (x < localEnd)
                {
                    double x2 = Math.Min(x + step, localEnd);
                    partialSum += (function(x) + function(x2)) * (x2 - x) / 2;
                    x = x2;
                }
                
                double initial, computed;
                do
                {
                    initial = result;
                    computed = initial + partialSum;
                }
                while (Interlocked.CompareExchange(
                    ref result,
                    computed,
                    initial) != initial);
            }
        );
        return result;
    }
}
