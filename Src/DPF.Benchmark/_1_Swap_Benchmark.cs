using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [Config(typeof(DFSConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class _1_Swap_Benchmark
    {
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public void Swap(ref int x, ref int y)
        {
            _1_Swap.Swap(ref x, ref y);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void SwapXOR(ref int x, ref int y)
        {
            _1_Swap.SwapXOR(ref x, ref y);
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { 0, 1 };
        }
    }
}
