using System;
using BenchmarkDotNet.Running;

namespace DFS.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<_1_Swap_Benchmark>();
        }
    }
}
