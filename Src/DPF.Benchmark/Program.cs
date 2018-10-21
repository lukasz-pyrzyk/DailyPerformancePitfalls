using BenchmarkDotNet.Running;

namespace DPF.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromTypes(new[]
            {
                typeof(Swap_Benchmark),
                typeof(CountingEvenNumbers),
            });
        }
    }
}
