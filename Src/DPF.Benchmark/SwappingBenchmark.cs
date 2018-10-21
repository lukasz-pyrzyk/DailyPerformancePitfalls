using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [Config(typeof(DFSConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class SwappingBenchmark
    {
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void SwapXOR(ref int x, ref int y)
        {
            x = x ^ y;
            y = x ^ y;
            x = x ^ y;
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { 0, 1 };
        }
    }
}
