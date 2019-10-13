using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DPF.Benchmark
{
    [Config(typeof(DPFConfig))]
    [DisassemblyDiagnoser(printAsm: true)]
    public class Inlining
    {
        public class Calculator
        {
            public double Divide(double a, double b) => a / b;

            public double DivideVirtual(double a, double b) => a / b;

            public double DivideWithException(double a, double b)
            {
                if (Math.Abs(b) < 0.001) throw new ArgumentException("B cannot be 0");
                return a / b;
            }

            public double DivideWithExceptionHelper(double a, double b)
            {
                if (Math.Abs(b) < 0.001) ThrowDivideByZeroException();
                return a / b;
            }

            private void ThrowDivideByZeroException()
            {
                throw new ArgumentException("B cannot be 0");
            }
        }

        private readonly Calculator _calc = new Calculator();

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public double Divide(double a, double b)
        {
            return _calc.Divide(a, b);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public double DivideVirtual(double a, double b)
        {
            return _calc.DivideVirtual(a, b);
        }


        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public double DivideWithException(double a, double b)
        {
            return _calc.DivideWithException(a, b);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public double DivideWithExceptionHelper(double a, double b)
        {
            return _calc.DivideWithExceptionHelper(a, b);
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { 0, 1 };
        }
    }
}
