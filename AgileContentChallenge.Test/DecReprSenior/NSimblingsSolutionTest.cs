using AgileContentChallenge.DecReprSenior;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AgileContentChallenge.Test.DecReprSenior
{
    public class IntegerSimblingTest
    {
        public int N { get; set; }
        public int Simbling { get; set; }

        public IntegerSimblingTest(int n, int simbling)
        {
            this.N = n;
            this.Simbling = simbling;
        }
    }

    public class NSimblingsSolutionTest
    {
        public NSimblingsSolutionTest()
        { }

        public class SolutionMethod : NSimblingsSolutionTest
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(-123)]
            [InlineData(-535)]
            [InlineData(-392)]
            public void When_IntegerIsLessThanZero_ExpectArgumentOutOfRangeException(int n)
            {
                Action solutionMethod = () => NSimblingsSolution.Solution(n);
                solutionMethod.Should().Throw<ArgumentOutOfRangeException>();
            }

            [Theory]
            [InlineData(100000001)]
            [InlineData(int.MaxValue)]
            public void When_IntegerIsGreaterThanOneHundredMillion_ExpectMinusOne(int n)
            {
                int solution = NSimblingsSolution.Solution(n);
                solution.Should().Be(-1);
            }

            [Fact]
            public void Given_ValidInteger_ExpectCorrectSimbling()
            {
                IList<IntegerSimblingTest> simblingKeyValueList = new List<IntegerSimblingTest>()
                {
                    new IntegerSimblingTest(0, 0),
                    new IntegerSimblingTest(535, 553),
                    new IntegerSimblingTest(123, 321),
                    new IntegerSimblingTest(392, 932),
                    new IntegerSimblingTest(10000000, 10000000)
                };

                foreach (var simblingKeyValue in simblingKeyValueList)
                {
                    int solution = NSimblingsSolution.Solution(simblingKeyValue.N);
                    solution.Should().Be(simblingKeyValue.Simbling);
                }
            }
        }
    }
}