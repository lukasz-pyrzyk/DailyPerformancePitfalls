using DPF.Benchmark;
using FluentAssertions;
using Xunit;

namespace DPF.Tests
{
    public class StructParsingTests
    {
        private readonly StructParsing _benchmark = new StructParsing();

        public StructParsingTests()
        {
            _benchmark.Setup();
        }

        [Fact]
        public void Reflection()
        {
            var header = _benchmark.Reflection();
            Assert(header);
        }

        [Fact]
        public void Manual()
        {
            var header = _benchmark.Manual();
            Assert(header);
        }

        [Fact]
        public void ManualWithUnsafe()
        {
            var header = _benchmark.ManualWithUnsafe();
            Assert(header);
        }

        [Fact]
        public void PtrToStructure_GCHandle()
        {
            var header = _benchmark.PtrToStructure_GCHandle();
            Assert(header);
        }

        [Fact]
        public void PtrToStructure_Fixed()
        {
            var header = _benchmark.PtrToStructure_Fixed();
            Assert(header);
        }

        [Fact]
        public void Pointer()
        {
            var header = _benchmark.Pointer();
            Assert(header);
        }

        private void Assert(StructParsing.TcpHeader header)
        {
            header.Source.Should().Be(_benchmark.Header.Source);
            header.Destination.Should().Be(_benchmark.Header.Destination);
            header.SourcePort.Should().Be(_benchmark.Header.SourcePort);
            header.DestinationPort.Should().Be(_benchmark.Header.DestinationPort);
            header.Flags.Should().Be(_benchmark.Header.Flags);
            header.Checksum.Should().Be(_benchmark.Header.Checksum);
        }
    }
}
