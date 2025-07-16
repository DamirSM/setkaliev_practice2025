using System.Diagnostics;
using System.Threading;
using ScottPlot;
using task14;

namespace OptimizationAnalysis;

class Program
{
    static void Main()
    {
        double a = -100;
        double b = 100;
        Func<double, double> function = Math.Sin;
        double reference = 0;
        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        int iterations = 5;

        DefiniteIntegral.Solve(a, b, function, 1e-7, 2);
        
        var stepResults = new List<(double step, double result, double avgTime)>();
        Console.WriteLine("Определение оптимального шага:");
        foreach (var step in steps)
        {
            double result = 0;
            double totalTime = 0;
            for (int i = 0; i < iterations; i++)
            {
                var watch = Stopwatch.StartNew();
                result += Math.Abs(reference - DefiniteIntegral.Solve(a, b, function, step, 2));
                watch.Stop();
                totalTime += watch.Elapsed.TotalMilliseconds;
            }
            double avgTime = totalTime / iterations;
            result /= iterations;
            stepResults.Add((step, result, avgTime));
            Console.WriteLine($"Шаг: {step}, Погрешность: {result}, Среднее время: {avgTime} мс");
        }
        var optimalStep = stepResults.Where(x => x.result <= 1e-4).OrderBy(x => x.avgTime).First().step;
        Console.WriteLine($"Оптимальный шаг: {optimalStep}");

        int maxThreads = Environment.ProcessorCount;
        Console.WriteLine("\nИзмерение производительности при разном числе потоков:");
        var threadResults = new List<(int threads, double avgTime)>();
        for (int threads = 1; threads <= maxThreads; threads++)
        {
            double totalTime = 0;
            for (int i = 0; i < iterations; i++)
            {
                var watch = Stopwatch.StartNew();
                DefiniteIntegral.Solve(a, b, function, optimalStep, threads);
                watch.Stop();
                totalTime += watch.Elapsed.TotalMilliseconds;
            }
            double avgTime = totalTime / iterations;
            threadResults.Add((threads, avgTime));
            Console.WriteLine($"Потоков: {threads}, Среднее время: {avgTime} мс");
        }
        
        double singleTotalTime = 0;
        for (int i = 0; i < iterations; i++)
        {
            var watch = Stopwatch.StartNew();
            double result = DefiniteIntegral.SolveSingle(a, b, function, optimalStep);
            watch.Stop();
            singleTotalTime += watch.Elapsed.TotalMilliseconds;
        }
        double singleAvgTime = singleTotalTime / iterations;

        Console.WriteLine($"\nОднопоточная версия: {singleAvgTime} мс");

        var optimalThreadResult = threadResults.OrderBy(x => x.avgTime).First();
        int optimalThreads = optimalThreadResult.threads;
        double optimalTime = optimalThreadResult.avgTime;
        double improvement = (singleAvgTime - optimalTime) / singleAvgTime * 100;
        Console.WriteLine($"\nОптимальное число потоков: {optimalThreads}, Время: {optimalTime} мс");
        Console.WriteLine($"Улучшение: {improvement}%");

        var plot = new ScottPlot.Plot();
        var threadValues = threadResults.Select(x => x.threads).ToArray();
        var times = threadResults.Select(x => x.avgTime).ToArray();
        plot.Add.Scatter(times, threadValues);
        plot.Title("Зависимость времени выполнения от числа потоков");
        plot.XLabel("Время вычисления функции Solve, мс");
        plot.YLabel("Количество потоков");
        plot.SavePng("performance.png", 800, 600);

        using (StreamWriter sw = new StreamWriter("results.txt"))
        {
            sw.WriteLine($"Оптимальный шаг: {optimalStep}");
            sw.WriteLine($"Оптимальное число потоков: {optimalThreads}");
            sw.WriteLine($"Время работы однопоточной версии: {singleAvgTime} мс");
            sw.WriteLine($"Время работы лучшей многопоточной версии: {optimalTime} мс");
            sw.WriteLine($"Разница в процентах: {improvement}%");
        }
    }
}
