using BenchmarkDotNet.Running;

namespace DPF.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromTypes(new[]
            {
                typeof(Swapping),
                typeof(Inlining),
                typeof(FibonacciCalc),
                typeof(StringFormatting),
            }).Run(args);
        }
    }
}
