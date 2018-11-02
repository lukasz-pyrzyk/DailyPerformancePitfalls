using System.Threading.Tasks;
using DPF.Benchmark;
using FluentAssertions;
using Xunit;

namespace DPF.Tests
{
    public class ParallelAsyncTests
    {
        private readonly ParallelAsync _benchmark = new ParallelAsync() { N = 5 };

        [Fact]
        public async Task For()
        {
            var result = await _benchmark.For();

            result.Should().Be(_benchmark.N * 50);
        }

        [Fact(Skip = "Expected to be failed, since Parallel is not created to work with async")]
        public void Parallel()
        {
            var result = _benchmark.ParallelFor();

            result.Should().Be(_benchmark.N * 50);
        }

        [Fact]
        public async Task WhenAll()
        {
            var result = await _benchmark.WhenAll();

            result.Should().Be(_benchmark.N * 50);
        }
    }
}
