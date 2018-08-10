using AgileContentChallenge.NewCDNiTaas;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AgileContentChallenge.Test.NewCDNiTaas
{
    public class AgoraLogTest
    {
        private readonly RandomGenerator randomGenerator;
        private readonly IList<AgoraLogLine> LogLines;
        private AgoraLog sut;

        public AgoraLogTest()
        {
            this.randomGenerator = new RandomGenerator();

            this.LogLines = Builder<AgoraLogLine>.CreateListOfSize(3).All().WithFactory(() => new AgoraLogLine(
                Builder<HttpMethod>.CreateNew().WithFactory(() => new HttpMethod("GET")).Build(),
                Builder<HttpStatusCode>.CreateNew().Build(),
                this.randomGenerator.NextString(5, 5),
                this.randomGenerator.Next(1, 10),
                this.randomGenerator.Next(20, 30),
                this.randomGenerator.NextString(6, 6))).Build();
        }

        public class Constructor : AgoraLogTest
        {
            [Fact]
            public void Given_ConstructorWithNullOrEmptyLine_ExpectArgumentNullException()
            {
                Action nullLineConstructor = () => new AgoraLog(null);
                nullLineConstructor.Should().Throw<ArgumentNullException>();

                Action emptyLineConstructor = () => new AgoraLog(new List<AgoraLogLine>());
                emptyLineConstructor.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void Given_ConstructorWithValidLines_ExpectValidObject()
            {
                this.sut = new AgoraLog(this.LogLines);
                this.sut.Should().NotBeNull();
                this.sut.Version.Should().BeSameAs("1.0");
                this.sut.Provider.Should().BeSameAs("\"MINHA CDN\"");
                this.sut.Lines.Should().BeSameAs(this.LogLines);
            }
        }

        public class ToStringMethod : AgoraLogTest
        {
            [Fact]
            public void Given_ValidObject_ExpectValidReturn()
            {
                this.sut = new AgoraLog(this.LogLines);
                this.sut.ToString().Should().NotBeNull();
            }
        }
    }
}