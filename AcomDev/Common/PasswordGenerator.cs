using System;
using System.Security.Cryptography;

namespace AcomDev.Common
{
    public class PasswordGenerator
    {
        private const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";

        public static string GenerateRandomPassword(int length)
        {
            if (length <= 0) throw new ArgumentException("Password length must be greater than 0.");

            char[] password = new char[length];
            byte[] randomBytes = new byte[length];

            RandomNumberGenerator.Fill(randomBytes); // Fills array with secure random values

            for (int i = 0; i < password.Length; i++)
            {
                password[i] = ValidChars[randomBytes[i] % ValidChars.Length];
            }

            return new string(password);
        }
    }

}
