using System.Security.Cryptography;

namespace TestFramework.Helpers
{
    public static class EncryptionUtil
    {
        private const int KeySize = 256;

        public static byte[] GetOrCreateKey(string keyFilePath)
        {
            if (File.Exists(keyFilePath))
                return File.ReadAllBytes(keyFilePath);

            using Aes aes = Aes.Create();
            aes.KeySize = KeySize;
            aes.GenerateKey();
            File.WriteAllBytes(keyFilePath, aes.Key);
            return aes.Key;
        }

        public static string EncryptString(string plainText, string keyFilePath)
        {
            byte[] key = GetOrCreateKey(keyFilePath);

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using ICryptoTransform encryptor = aes.CreateEncryptor();
            using MemoryStream memoryStream = new();
            memoryStream.Write(aes.IV, 0, aes.IV.Length);

            using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(plainText);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string DecryptString(string cipherText, string keyFilePath)
        {
            byte[] key = GetOrCreateKey(keyFilePath);
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor();
            using MemoryStream memoryStream = new(cipher);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
