using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class CryptoService
    {
        private const int PBKDF2IterCount = 1000;

        private const int PBKDF2SubkeyLength = 32;

        private const int SaltSize = 16;

        internal static byte[] GenerateSaltInternal(int byteLength = 16)
        {
            byte[] array = new byte[byteLength];
            using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(array);
            return array;
        }

        public static string GenerateSalt(int byteLength = 16)
        {
            return Convert.ToBase64String(GenerateSaltInternal(byteLength));
        }

        public static string Hash(string input, string algorithm = "sha256")
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return Hash(Encoding.UTF8.GetBytes(input), algorithm);
        }

        public static string Hash(byte[] input, string algorithm = "sha256")
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            using HashAlgorithm hashAlgorithm = HashAlgorithm.Create(algorithm);
            if (hashAlgorithm != null)
            {
                return BinaryToHex(hashAlgorithm.ComputeHash(input));
            }

            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The hash algorithm '{0}' is not supported, valid values are: sha256, sha1, md5", new object[1] { algorithm }));
        }

        public static string SHA1(string input)
        {
            return Hash(input, "sha1");
        }

        public static string SHA256(string input)
        {
            return Hash(input);
        }

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] salt;
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }

            byte[] array = new byte[49];
            Buffer.BlockCopy(salt, 0, array, 1, 16);
            Buffer.BlockCopy(bytes, 0, array, 17, 32);
            return Convert.ToBase64String(array);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException("hashedPassword");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] array = Convert.FromBase64String(hashedPassword);
            if (array.Length != 49 || array[0] != 0)
            {
                return false;
            }

            byte[] array2 = new byte[16];
            Buffer.BlockCopy(array, 1, array2, 0, 16);
            byte[] array3 = new byte[32];
            Buffer.BlockCopy(array, 17, array3, 0, 32);
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array2, 1000))
            {
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }

            return ByteArraysEqual(array3, bytes);
        }

        internal static string BinaryToHex(byte[] data)
        {
            char[] array = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                byte b = (byte)(data[i] >> 4);
                array[i * 2] = (char)((b > 9) ? (b + 55) : (b + 48));
                b = (byte)(data[i] & 0xFu);
                array[i * 2 + 1] = (char)((b > 9) ? (b + 55) : (b + 48));
            }

            return new string(array);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == b)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool flag = true;
            for (int i = 0; i < a.Length; i++)
            {
                flag &= a[i] == b[i];
            }

            return flag;
        }
    }
}
