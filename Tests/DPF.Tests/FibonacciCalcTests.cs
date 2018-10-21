using System.Collections.Generic;
using DPF.Benchmark;
using FluentAssertions;
using Xunit;

namespace DPF.Tests
{
    public class FibonacciCalcTests
    {
        private readonly FibonacciCalc _calc = new FibonacciCalc();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Iterative(ulong n, ulong expected)
        {
            _calc.Iterative(n).Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Recursive(ulong n, ulong expected)
        {
            _calc.Recursive(n).Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Memo(ulong n, ulong expected)
        {
            _calc.RecursiveMemo(n).Should().Be(expected);
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] {1, 1};
            yield return new object[] {2, 1};
            yield return new object[] {15, 610};
            yield return new object[] {20, 6765};
            yield return new object[] {35, 9227465};
        }
    }
}
