using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RepoDbVsEF.Domain
{
    public class SimpleStringCipher
    {
        public static SimpleStringCipher Instance { get; } = new SimpleStringCipher();

        /// <summary>
        /// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        /// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        /// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        /// </summary>
        private readonly byte[] _initVectorBytes;

        /// <summary>
        /// Default password to encrypt/decrypt texts.
        /// </summary>
        public const string DefaultPassPhrase = "gsKnGZ041HLL4IM8";

        /// <summary>
        /// This constant is used to determine the keysize of the encryption algorithm.
        /// </summary>
        public const int Keysize = 256;

        public SimpleStringCipher()
        {
            _initVectorBytes = Encoding.ASCII.GetBytes("jkE49230Tf093b42");
        }

        public string Encrypt(string plainText, string passPhrase = DefaultPassPhrase)
        {
            if (plainText == null)
            {
                return null;
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(Keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, _initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public string Decrypt(string cipherText, string passPhrase = DefaultPassPhrase)
        {
            if (cipherText == null)
            {
                return null;
            }

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(Keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, _initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The SHA1 hash string is impossible to transform back to its original string.
        /// </summary>
        public string SHA1(string value)
        {
            var shaSHA1 = System.Security.Cryptography.SHA1.Create();
            var inputEncodingBytes = Encoding.UTF8.GetBytes(value);
            var hashString = shaSHA1.ComputeHash(inputEncodingBytes);

            var stringBuilder = new StringBuilder();
            for (var x = 0; x < hashString.Length; x++)
            {
                stringBuilder.Append(hashString[x].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
