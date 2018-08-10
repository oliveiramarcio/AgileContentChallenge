using AgileContentChallenge.DecReprSenior;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace AgileContentChallenge.Test
{
    [TestClass]
    public class NSimblingsSolutionTest
    {
        private readonly RandomGenerator randomGenerator;

        public NSimblingsSolutionTest()
        {
            this.randomGenerator = new RandomGenerator();
        }

        [TestMethod]
        [Theory]
        [InlineData(-1)]
        public void When_N_LessThanZero_ExpectArgumentOutOfRangeException(int n)
        {
            Action solutionMethod = () => NSimblingsSolution.Solution(n);
            solutionMethod.Should().Throw<ArgumentOutOfRangeException>().WithMessage("n");
        }
    }
}