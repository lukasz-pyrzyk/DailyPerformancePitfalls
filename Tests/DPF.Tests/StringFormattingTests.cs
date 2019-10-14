using DPF.Benchmark;
using FluentAssertions;
using Xunit;

namespace DPF.Tests
{
    public class StringFormattingTests
    {
        [Fact]
        public void ResultsAreEqual()
        {
            var bench = new StringFormatting();
            var a = bench.Default();
            var b = bench.StringBuilder();
            var c = bench.StringBuilderWithInitializedSize();
            var d = bench.StringBuilderWithInitializedSizeAndFormat();
            var e = bench.CachedStringBuilderWithFormat();

            b.Length.Should().Be(a.Length);
            c.Length.Should().Be(a.Length);
            d.Length.Should().Be(a.Length);
            e.Length.Should().Be(a.Length);
        }
    }
}
