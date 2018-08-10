using AgileContentChallenge.NewCDNiTaas;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AgileContentChallenge.Test.NewCDNiTaas
{
    public class AgoraLogLineTest
    {
        private readonly RandomGenerator randomGenerator;
        private readonly HttpMethod httpMethod;
        private readonly HttpStatusCode statusCode;
        private readonly string uriPath;
        private readonly int timeTaken;
        private readonly int responseSize;
        private readonly string cacheStatus;
        private AgoraLogLine sut;

        public AgoraLogLineTest()
        {
            this.randomGenerator = new RandomGenerator();
            this.httpMethod = Builder<HttpMethod>.CreateNew().WithFactory(() => new HttpMethod("GET")).Build();
            this.statusCode = Builder<HttpStatusCode>.CreateNew().Build();
            this.uriPath = this.randomGenerator.NextString(5, 5);
            this.timeTaken = this.randomGenerator.Next(1, 10);
            this.responseSize = this.randomGenerator.Next(20, 30);
            this.cacheStatus = this.randomGenerator.NextString(6, 6);
        }

        public class Constructor : AgoraLogLineTest
        {
            [Fact]
            public void Given_ConstructorWithInvalidParameters_ExpectException()
            {
                Action invalidLogLineConstructor = () => new AgoraLogLine(null, statusCode, string.Empty, -1, -1, string.Empty);
                invalidLogLineConstructor.Should().Throw<ArgumentNullException>();

                invalidLogLineConstructor = () => new AgoraLogLine(this.httpMethod, this.statusCode, string.Empty, -1, -1, string.Empty);
                invalidLogLineConstructor.Should().Throw<ArgumentNullException>();

                invalidLogLineConstructor = () => new AgoraLogLine(this.httpMethod, this.statusCode, this.uriPath, -1, -1, string.Empty);
                invalidLogLineConstructor.Should().Throw<ArgumentOutOfRangeException>();

                invalidLogLineConstructor = () => new AgoraLogLine(this.httpMethod, this.statusCode, this.uriPath, this.timeTaken, -1, string.Empty);
                invalidLogLineConstructor.Should().Throw<ArgumentOutOfRangeException>();

                invalidLogLineConstructor = () => new AgoraLogLine(this.httpMethod, this.statusCode, this.uriPath, this.timeTaken, this.responseSize, string.Empty);
                invalidLogLineConstructor.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void Given_ConstructorWithValidParameters_ExpectValidObject()
            {
                this.sut = new AgoraLogLine(this.httpMethod, this.statusCode, this.uriPath, this.timeTaken, this.responseSize, this.cacheStatus);
                this.sut.Should().NotBeNull();
                this.sut.HttpMethod.Should().BeSameAs(this.httpMethod);
                this.sut.StatusCode.Should().Be(this.statusCode);
                this.sut.UriPath.Should().BeSameAs(this.uriPath);
                this.sut.TimeTaken.Should().Be(this.timeTaken);
                this.sut.ResponseSize.Should().Be(this.responseSize);
                this.sut.CacheStatus.Should().BeSameAs(this.cacheStatus);
            }
        }
    }
}