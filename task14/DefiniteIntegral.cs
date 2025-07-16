using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        if (threadsNumber < 1)
            throw new ArgumentException("Количество потоков должно быть по крайней мере равно 1", nameof(threadsNumber));
        if (step <= 0)
            throw new ArgumentException("Шаг интегрирования должен быть положительным", nameof(step));
        if (b <= a)
            throw new ArgumentException("Верхний предел интегрирования должен быть больше нижнего");
        
        double result = 0;

        Barrier barrier = new Barrier(threadsNumber + 1);

        double segmentLength = (b - a) / threadsNumber;
        
        Thread[] threads = new Thread[threadsNumber];
        
        for (int i = 0; i < threadsNumber; i++)
        {
            double localStart = a + i * segmentLength;
            double localEnd;
            if (i == threadsNumber - 1)
                localEnd = b;
            else
                localEnd = localStart + segmentLength;
            threads[i] = new Thread(() =>
            {
                double partialSum = 0;
                double x = localStart;
                while (x < localEnd)
                {
                    double xNext = Math.Min(x + step, localEnd);
                    partialSum += (function(x) + function(xNext)) * (xNext - x) / 2;
                    x = xNext;
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

                barrier.SignalAndWait();
            });
            threads[i].Start();
        }
        barrier.SignalAndWait();
        return result;
    }
}
