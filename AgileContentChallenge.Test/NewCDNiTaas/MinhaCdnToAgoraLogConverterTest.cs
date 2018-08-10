using AgileContentChallenge.NewCDNiTaas;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace AgileContentChallenge.Test.NewCDNiTaas
{
    public class MinhaCdnToAgoraLogConverterTest
    {
        private readonly RandomGenerator randomGenerator;
        private readonly string validUrl;
        private readonly string inexistentFileUrl;
        public readonly string invalidFileUrl;
        private readonly string outputFile;

        public MinhaCdnToAgoraLogConverterTest()
        {
            this.randomGenerator = new RandomGenerator();
            this.validUrl = @"https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            this.inexistentFileUrl = @"https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-02.txt";
            this.invalidFileUrl = @"https://www.w3.org/TR/PNG/iso_8859-1.txt";
            this.outputFile = @"./output/minhaCdn1.txt";
        }

        public class ConvertLogMethod : MinhaCdnToAgoraLogConverterTest
        {
            [Fact]
            public void Given_InvalidParameters_ExpectException()
            {
                Action convertLogMethod = () => MinhaCdnToAgoraLogConverter.ConvertLog(string.Empty, string.Empty);
                convertLogMethod.Should().Throw<ArgumentNullException>();

                convertLogMethod = () => MinhaCdnToAgoraLogConverter.ConvertLog(this.randomGenerator.NextString(20, 20), string.Empty);
                convertLogMethod.Should().Throw<UriFormatException>();

                convertLogMethod = () => MinhaCdnToAgoraLogConverter.ConvertLog(this.validUrl, string.Empty);
                convertLogMethod.Should().Throw<ArgumentNullException>();

                convertLogMethod = () => MinhaCdnToAgoraLogConverter.ConvertLog(this.inexistentFileUrl, this.outputFile);
                convertLogMethod.Should().Throw<Exception>();

                convertLogMethod = () => MinhaCdnToAgoraLogConverter.ConvertLog(this.invalidFileUrl, this.outputFile);
                convertLogMethod.Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Given_ValidParameters_ExpectValidObject()
            {
                AgoraLog agoraLog = MinhaCdnToAgoraLogConverter.ConvertLog(this.validUrl, this.outputFile);
                agoraLog.Should().NotBeNull();
                agoraLog.Lines.Should().NotBeNull();
                agoraLog.Lines.Count().Should().BeGreaterThan(0);
                agoraLog.ToString().Should().NotBeNull();
                Directory.Exists(Path.GetDirectoryName(this.outputFile)).Should().Be(true);
            }
        }
    }
}