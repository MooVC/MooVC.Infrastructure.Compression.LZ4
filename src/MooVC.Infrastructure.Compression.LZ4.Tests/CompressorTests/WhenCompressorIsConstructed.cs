namespace MooVC.Infrastructure.Compression.LZ4.CompressorTests
{
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using K4os.Compression.LZ4;
    using K4os.Compression.LZ4.Streams;
    using Xunit;

    public sealed class WhenCompressorIsConstructed
    {
        [Fact]
        public void GivenNoSettingsThenTheDefaultSettingsAreUsed()
        {
            var compressor = new Compressor();
            var decoder = new LZ4DecoderSettings();
            var encoder = new LZ4EncoderSettings();

            AssertEqual(compressor.Decoder, decoder);
            AssertEqual(compressor.Encoder, encoder);
        }

        [Fact]
        public void GivenSettingsThenTheSettingsAreApplied()
        {
            var decoder = new LZ4DecoderSettings
            {
                ExtraMemory = 1024,
            };

            var encoder = new LZ4EncoderSettings
            {
                BlockSize = 131072,
                ChainBlocks = false,
                CompressionLevel = LZ4Level.L12_MAX,
                ContentLength = 2048,
                ExtraMemory = 4096,
            };

            var compressor = new Compressor(
                decoder: decoder,
                encoder: encoder);

            AssertEqual(compressor.Decoder, decoder);
            AssertEqual(compressor.Encoder, encoder);
        }

        private static void AssertEqual(LZ4DecoderSettings actual, LZ4DecoderSettings expected)
        {
            Assert.Equal(expected.ExtraMemory, actual.ExtraMemory);
        }

        private static void AssertEqual(LZ4EncoderSettings actual, LZ4EncoderSettings expected)
        {
            Assert.Equal(expected.ContentLength, actual.ContentLength);
            Assert.Equal(expected.ChainBlocks, actual.ChainBlocks);
            Assert.Equal(expected.BlockSize, actual.BlockSize);
            Assert.Equal(expected.ContentChecksum, actual.ContentChecksum);
            Assert.Equal(expected.BlockChecksum, actual.BlockChecksum);
            Assert.Equal(expected.Dictionary, actual.Dictionary);
            Assert.Equal(expected.CompressionLevel, actual.CompressionLevel);
            Assert.Equal(expected.ExtraMemory, actual.ExtraMemory);
        }
    }
}