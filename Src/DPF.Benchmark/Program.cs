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
                typeof(CountingEvenNumbers),
                typeof(StructParsing),
            }).Run(args);
        }
    }
}
