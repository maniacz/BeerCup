using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Encryption
{
    public static class Encryption
    {
        public static string HashPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");

            return hashed;
        }

        public static byte[] GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Generated salt: {Convert.ToBase64String(salt)}");
            return salt;
        }

        public static bool IsValidPassword(string storedPassword, string storedSalt, string enteredPassword)
        {
            var salt = Convert.FromBase64String(storedSalt);
            return string.Equals(storedPassword, HashPassword(enteredPassword, salt));
        }
    }
}
