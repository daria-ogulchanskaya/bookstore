using System;
using System.Security.Cryptography;

namespace Bookstore.Core.Providers
{
    public class PasswordGenerator
    {
        private const int BITS_DEFAULT = 6;
        private const int BYTE_COMPLEMENT = 7;
        private const int BYTE_DEFAULT = 8;

        public string GeneratePassword(int length)
        {
            using var rng = new RNGCryptoServiceProvider();

            var bitCount = length * BITS_DEFAULT;
            var byteCount = (bitCount + BYTE_COMPLEMENT) / BYTE_DEFAULT;

            var bytes = new byte[byteCount];

            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
