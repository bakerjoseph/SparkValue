using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Services
{
    public class SecurityService
    {
        private const int SaltSize = 24;
        private const int HashSize = 24;
        private const int Iterations = 30000;

        /// <summary>
        /// Salts and Hashs a given string using PBKDF2
        /// </summary>
        /// <param name="password">The password string to be hashed</param>
        /// <returns>A tuple with the salt and hashed input string</returns>
        public (string salt, string hashedPassword) ProtectPassword(string password)
        {
            byte[] salt = SaltGenerator();

            byte[] hash = HashGenerator(password, salt);

            return (Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        private string ProtectPassword(string password, string salt)
        {
            byte[] hash = HashGenerator(password, Convert.FromBase64String(salt));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Test a given password to see if it matches a hashed password with salt
        /// </summary>
        /// <param name="password">Input password to be tested</param>
        /// <param name="salt">Salt from the saved hashed password</param>
        /// <param name="hashedPassword">Saved hashed password to be tested against</param>
        /// <returns>The result of the equality test between the two passwords</returns>
        public bool PasswordMatch(string password, string salt, string hashedPassword)
        {
            string incommingHashedPassword = ProtectPassword(password, salt);

            return hashedPassword.Equals(incommingHashedPassword);
        }

        private byte[] HashGenerator(string password, byte[] salt)
        {
            // Hashes using Password-Based Key Derivation Function 2 (PBKDF2)
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            return pbkdf2.GetBytes(HashSize);
        }

        private byte[] SaltGenerator()
        {
            // Generates the salt from a cryptographic random number generator
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            return salt;
        }
    }
}
