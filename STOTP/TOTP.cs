using STOTP.Extensions;
using System;
using System.Security.Cryptography;

// Simple TOTP
namespace STOTP
{
    // https://tools.ietf.org/html/rfc6238
    public class TOTP
    {
        private static DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        public static int DefaultTimeInterval = 30; // interval to update (default is 30 seconds)
        public static int DefaultLength = 6;
        private readonly int _hotpValueLength;
        private readonly HMACSHA1 _hmac;

        public TOTP(string key, int length = 0, int timeInterval = 0)
        {
            // assuming default hash
            _hmac = new HMACSHA1(key.Base32StringToByteArray());
            _hotpValueLength = length > 0 ? length : DefaultLength;
            Window = timeInterval > 0 ? timeInterval : DefaultTimeInterval;
        }

        /// <summary>
        /// Seconds between updates
        /// </summary>
        public int Window { get; }

        /// <summary>
        /// Current OTP based on the key and time
        /// </summary>
        public string Otp
        {
            get
            {
                // https://tools.ietf.org/html/rfc6238
                // TOTP: https://en.wikipedia.org/wiki/Time-based_One-time_Password_algorithm 

                // HMAC -this is the default underlying security algorithm
                // 20 bytes (160 bits) to work with
                var cT = (long)(DateTimeOffset.Now - UnixEpoch).TotalSeconds / (long)Window;
                var data = BitConverter.GetBytes(cT);
                // Bytes need to be reversed since windows and .net use little endian
                // and big endian is assumed for the algorithm
                Array.Reverse(data);
                var hash = _hmac.ComputeHash(data);

                // HOTP: https://en.wikipedia.org/wiki/HMAC-based_One-time_Password_algorithm#Definition
                // HOTP value = HOTP(K, C) mod 10^d
                // HOTP(K, C) = truncate(HMACH(K, C))
                // truncate(MAC) = extract31(MAC, MAC[(19 × 8) + 4:(19 × 8) + 7] × 8)
                // extract31(MAC, i) = MAC[i + 1:i + (4 × 8) − 1]
                /* Fancy notation aside this is the important bit:
                    "Truncation first takes the 4 least significant bits of the MAC and 
                    uses them as an offset, i.
                    That index i is used to select 31 bits from MAC, starting at bit i + 1."
                */
                var offset = hash[hash.Length - 1] & 0x0F;  // 4 least significant bits
                var otp = (hash[offset] & 0x7f) << 24       // otp build from the offset
                       | (hash[offset + 1] & 0xff) << 16
                       | (hash[offset + 2] & 0xff) << 8
                       | (hash[offset + 3] & 0xff) % 1000000;

                var truncatedValue = ((int)otp % (int)Math.Pow(10, _hotpValueLength));
                // Return as a friendly readable string padded with 0s
                return truncatedValue.ToString().PadLeft(_hotpValueLength, '0');
            }
        }

        /// <summary>
        /// Seconds left before a new Window period
        /// </summary>
        public int SecondsRemaining
        {
            get
            {
                return (int)(Window - (long)(DateTimeOffset.Now - UnixEpoch).TotalSeconds % (long)Window);
            }
        }
    }
}
