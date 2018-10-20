using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DPF.Tests
{
    public class SwapTests
    {
        [Theory]
        [MemberData(nameof(Samples))]
        public void Swaps(int x, int y, int expectedX, int expectedY)
        {
            _1_Swap.Swap(ref x, ref y);

            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        [Theory]
        [MemberData(nameof(Samples))]
        public void SwapXOR(int x, int y, int expectedX, int expectedY)
        {
            _1_Swap.SwapXOR(ref x, ref y);

            x.Should().Be(expectedX);
            y.Should().Be(expectedY);
        }

        public static IEnumerable<object[]> Samples()
        {
            yield return new object[] {0, 0, 0, 0};
            yield return new object[] {0, 1, 1, 0};
            yield return new object[] {1, 0, 0, 1};
            yield return new object[] {-50, 50, 50, -50};
        }
    }
}
