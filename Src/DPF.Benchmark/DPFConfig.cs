using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;

namespace DPF.Benchmark
{
    public class DPFConfig : ManualConfig
    {
        public DPFConfig()
        {
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
        }
    }
}
