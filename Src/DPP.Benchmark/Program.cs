using BenchmarkDotNet.Running;

namespace DPP.Benchmark
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
