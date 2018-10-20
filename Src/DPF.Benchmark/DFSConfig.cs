using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;

namespace DPF.Benchmark
{
    public class DFSConfig : ManualConfig
    {
        public DFSConfig()
        {
            Add(MarkdownExporter.GitHub);
        }
    }
}
