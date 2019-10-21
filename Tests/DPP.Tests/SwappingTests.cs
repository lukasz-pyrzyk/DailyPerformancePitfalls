using System.Collections.Generic;
using DPP.Benchmark;
using FluentAssertions;
using Xunit;

namespace DPP.Tests
{
    public class SwappingTests
    {
        private readonly Swapping _benchmark = new Swapping();

        [Theory]
        [MemberData(nameof(Data))]
        public void TempVariable(int x, int y, int expectedX, int expectedY)
        {
            _benchmark.TempVariable(ref x, ref y);
            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void AddOrSubtract(int x, int y, int expectedX, int expectedY)
        {
            _benchmark.AddAndSubtract(ref x, ref y);
            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void XOR(int x, int y, int expectedX, int expectedY)
        {
            _benchmark.XOR(ref x, ref y);
            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] {5, 10, 10, 5};
        }
    }
}
