namespace MooVC.Infrastructure.Compression.LZ4.CompressorTests
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class WhenBytesAreCompressed
    {
        [Fact]
        public async Task GivenBytesThenTheResultMatchesAsync()
        {
            byte[] expected = new byte[32768];
            var random = RandomNumberGenerator.Create();

            random.GetNonZeroBytes(expected);

            var compressor = new Compressor();
            IEnumerable<byte> compressed = await compressor.CompressAsync(expected);

            Assert.NotEqual(expected, compressed);

            IEnumerable<byte> decompressed = await compressor.DecompressAsync(compressed);

            Assert.Equal(expected, decompressed);
        }
    }
}