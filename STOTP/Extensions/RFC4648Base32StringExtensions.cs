namespace STOTP.Extensions
{
    /// <summary>
    /// RFC4648: https://tools.ietf.org/html/rfc4648 - The Base16, Base32, and Base64 Data Encodings
    /// </summary>
    public static class RFC4648Base32StringExtensions
    {
        private static int BITS_PER_ALPHABET_CHAR = 5;
        private static int BITS_PER_BYTE = 8;
        private static int ALPHABET_BYTE_MASK = 0x1F;
        // Value is index in the string (0-31)
        private static string BASE32_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        /// <summary>
        /// Implementation of converting a Base32 encoded string per RFC4648 to
        /// an array of bytes.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Base32StringToByteArray(this string input)
        {
            // Remove any padding characters from the input
            input = input.ToUpper().TrimEnd(new char[] { '=' });

            // calculate bytes needed and create output array, this is 
            // integer math, so we'll round up to hold anything not
            // evenly divisible (no padding in this direction)
            var result = new byte[input.Length * BITS_PER_ALPHABET_CHAR / BITS_PER_BYTE];

            // Each char in the alphabet is 5bits, these chars are 
            // converted to a string of bits that are then grouped
            // by bytes.  This byte grouping is what we return.  

            // This is the basic repeating pattern, every 5 Bytes
            // we repeat the same pattern for offsets
            // 00000 11111 00000 11111 00000 11111 00000 11111 00000 11111 00000 11111
            // 00000111 11000001 11110000 01111100 00011111 00000111 11...
            // |  0        1         2         3      4   | |   5     6...
            // 0123456701234567 short (int16)
            //       0123401234
            // So now, just loop through the input string converting the 
            // BASE32 alphabet to 5 bit values (which are stored in bytes at a time
            // since that is the smallest type size) and pack them into 8 bit
            // (byte) aligned data

            var buffer = 0;         // temp storage
            var bitsShifted = 0;    // tracks bits as we shift
            var resultIndex = 0;    // index into output array of bytes
            for (var i = 0; i < input.Length; i++)
            {
                // Read the next char and shift it into the buffer
                // Masking off the new bits
                var v = BASE32_ALPHABET.IndexOf(input[i]);
                buffer <<= BITS_PER_ALPHABET_CHAR;
                buffer |= v & ALPHABET_BYTE_MASK;

                // If we've shifted enough bits (8) then add it to the
                // result, and update the tracking variables
                bitsShifted += BITS_PER_ALPHABET_CHAR;
                if (bitsShifted >= BITS_PER_BYTE)
                {
                    result[resultIndex++] = (byte)(buffer >> (bitsShifted - BITS_PER_BYTE));
                    bitsShifted -= BITS_PER_BYTE;
                }
            }
            return result;
        }
    }
}
