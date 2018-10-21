using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [Config(typeof(DFSConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class Swap_Benchmark
    {
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public void Swap(ref int x, ref int y)
        {
            DPF.Swap.SwapWithTemp(ref x, ref y);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void SwapXOR(ref int x, ref int y)
        {
            DPF.Swap.SwapXOR(ref x, ref y);
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { 0, 1 };
        }
    }
}
