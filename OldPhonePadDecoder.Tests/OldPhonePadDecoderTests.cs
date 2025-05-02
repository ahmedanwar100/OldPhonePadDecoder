using Decoder = OldPhonePadDecoder.Core.OldPhonePadDecoder;

namespace OldPhonePadDecoder.Tests
{
    /// <summary>
    /// Unit tests for the OldPhonePad decoding logic.
    /// Tests include standard decoding, backspaces, wrap-around, pauses, and invalid inputs.
    /// </summary>
    public class OldPhonePadDecoderTests
    {
        [Theory]
        [InlineData("33#", "E")]
        [InlineData("2 22 222#", "ABC")]
        [InlineData("4433555 555666096667775553#", "HELLO WORLD")]
        [InlineData("8 88777444666*664#", "TURING")]
        [InlineData("66*6#", "M")] // for backspace test
        [InlineData("2222#", "A")] // for wrap-around test
        [InlineData("*#", "")] // for empty after deletion
        [InlineData("999337777#", "YES")]
        public void OldPhonePad_ReturnsExpectedOutput(string input, string expected)
        {
            //Act
            var result = Decoder.OldPhonePad(input);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OldPhonePad_InvalidDigit_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Decoder.OldPhonePad("544221#"));
        }

        [Fact]
        public void OldPhonePad_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Decoder.OldPhonePad(null));
        }

        [Fact]
        public void OldPhonePad_EmptyInput_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Decoder.OldPhonePad(""));
        }

        [Fact]
        public void OldPhonePad_WhitespaceInput_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Decoder.OldPhonePad(" "));
        }
    }
}