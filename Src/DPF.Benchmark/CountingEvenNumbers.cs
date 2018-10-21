using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace DPF.Benchmark
{
    [Config(typeof(DPFConfig))]
    [HardwareCounters(HardwareCounter.BranchMispredictions)]
    [DisassemblyDiagnoser(printAsm: true)]
    public class CountingEvenNumbers
    {
        public int[] Array = Enumerable.Range(0, 10 * 1000 * 1000).ToArray();

        [Benchmark(Baseline = true)]
        public int For()
        {
            int n = 0;
            for (var i = 0; i < Array.Length; i++)
            {
                if (IsEven(Array[i])) n++;
            }

            return n;
        }

        [Benchmark]
        public int ParallelForEach()
        {
            int count = 0;
            Parallel.ForEach(Array, x =>
            {
                if (IsEven(x)) Interlocked.Increment(ref count);
            });
            return count;
        }

        [Benchmark]
        public int PartitionerCreate()
        {
            int count = 0;

            var partitioner = Partitioner.Create(0, Array.Length);
            Parallel.ForEach(partitioner, (range, state) =>
            {
                int localStore = 0;
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    if (IsEven(Array[i])) localStore++;
                }
                Interlocked.Add(ref count, localStore);
            });

            return count;
        }

        [Benchmark]
        public int PLINQ_AsParallel()
        {
            return Array.AsParallel().Count(IsEven);
        }

        private static bool IsEven(int x) => x % 2 == 0;
    }
}
