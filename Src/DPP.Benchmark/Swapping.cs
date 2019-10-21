using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DPP.Benchmark
{
    [Config(typeof(DPFConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class Swapping
    {
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public void TempVariable(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void AddAndSubtract(ref int x, ref int y)
        {
            x = x + y;
            y = x - y;
            x = x - y;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void XOR(ref int x, ref int y)
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
