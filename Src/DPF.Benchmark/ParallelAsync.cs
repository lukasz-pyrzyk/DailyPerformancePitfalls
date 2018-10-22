using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [Config(typeof(DPFConfig))]
    public class ParallelAsync
    {
        [Params(5, 40)]
        public int N { get; set; }

        [Benchmark]
        public async Task<int> For()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                var result = await BusinessLogicAsync();
                count += result;
            }
            return count;
        }

        [Benchmark]
        public int ParallelFor()
        {
            int count = 0;
            Parallel.For(0, N, async x =>
            {
                var result = await BusinessLogicAsync();
                Interlocked.Add(ref count, result);
            });

            return count;
        }

        [Benchmark]
        public async Task<int> WhenAll()
        {
            var tasks = new Task<int>[N];
            for (int i = 0; i < N; i++)
            {
                tasks[i] = BusinessLogicAsync();
            }

            await Task.WhenAll(tasks);
            return tasks.Select(x => x.Result).Sum();
        }

        private static async Task<int> BusinessLogicAsync()
        {
            await Task.Delay(50);
            return 50;
        }
    }
}
