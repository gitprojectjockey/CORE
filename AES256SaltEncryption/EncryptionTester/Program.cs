using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTester
{
    class Program
    {
        static void Main(string[] args)
        {
           var encryptionPassword = ConfigurationManager.AppSettings["encryptionPassword"];

            var encryptedText = EncryptText("This is the text to encrypt", encryptionPassword);
            var decryptedText = DecryptText(encryptedText, encryptionPassword);

            var randomEncryptedText = RandomSaltTextEncryption("This is the text to encrypt", encryptionPassword);
            var randomDecryptedText = RandomSaltTextDecryption(randomEncryptedText, encryptionPassword);
        }

        private static string EncryptText(string textToEncrypt, string encryptionPassKey)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassKey);

            // Get salt for random encryption
            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + textToEncrypt.Length];

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = new AES256SaltEncryptionLibrary.AESEncryption().Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        private static string RandomSaltTextDecryption(string textToDecrypt, string encryptionPassKey)
        {
            // Hash the password with SHA256
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            // Get bytes from base64 encoded bytes
            byte[] textBytes = Convert.FromBase64String(textToDecrypt);

            byte[] bytesDecrypted = new AES256SaltEncryptionLibrary.AESEncryption().Decrypt(textBytes, passwordBytes);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] resultBytes = new byte[bytesDecrypted.Length - saltLength];

            for (int i = 0; i < resultBytes.Length; i++)
                resultBytes[i] = bytesDecrypted[i + saltLength];

            string result = Encoding.UTF8.GetString(resultBytes);
            return result;
        }

        private static string RandomSaltTextEncryption(string textToEncrypt, string encryptionPassKey)
        {
            // Hash the password with SHA256
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(textToEncrypt);

            // Get salt for random encryption
            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + bytesToBeEncrypted.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < textToEncrypt.Length; i++)
                baEncrypted[i + baSalt.Length] = bytesToBeEncrypted[i];

            byte[] bytesEncrypted = new AES256SaltEncryptionLibrary.AESEncryption().Encrypt(baEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        

        public static string DecryptText(string textToDecrypt, string encryptionPassKey)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(textToDecrypt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = new AES256SaltEncryptionLibrary.AESEncryption().Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        private static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        private static int GetSaltLength()
        {
            return 8;
        }
    }
}
