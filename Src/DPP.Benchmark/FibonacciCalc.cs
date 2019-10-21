using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace DPP.Benchmark
{
    [Config(typeof(DPFConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class FibonacciCalc
    {
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveTPLAll(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            var a = Task.Run(() => RecursiveTPLAll(n - 2));
            var b = Task.Run(() => RecursiveTPLAll(n - 1));
            Task.WaitAll(a, b);
            return a.Result + b.Result;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveTPLStart(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            var a = Task.Run(() => RecursiveTPLStartImplementation(n - 2));
            var b = Task.Run(() => RecursiveTPLStartImplementation(n - 1));
            Task.WaitAll(a, b);
            return a.Result + b.Result;
        }

        private ulong RecursiveTPLStartImplementation(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveMemo(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            var results = new ulong[n];
            results[0] = 1;
            results[1] = 1;
            return RecursiveMemo(n, results);
        }

        private static ulong RecursiveMemo(ulong n, ulong[] results)
        {
            var current = n - 1;
            var previous = current - 1;
            var beforePrevious = previous - 1;

            if (results[beforePrevious] == 0)
            {
                results[beforePrevious] = RecursiveMemo(previous, results);
            }

            if (results[previous] == 0)
            {
                results[previous] = RecursiveMemo(current, results);
            }

            results[current] = results[beforePrevious] + results[previous];
            return results[n - 1];
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            ulong a = 1, b = 1;
            for (ulong i = 2; i < n; i++)
            {
                ulong temp = a + b;
                a = b;
                b = temp;
            }

            return b;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
