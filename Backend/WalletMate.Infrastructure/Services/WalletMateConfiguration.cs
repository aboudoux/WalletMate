using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WalletMate.Domain.Common;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.Services
{
    [Serializable]
    public class WalletMateConfiguration
    {
        private static readonly byte[] Key = Encoding.ASCII.GetBytes("55C0A73E3F5D460CB1A7B322EF21CC7F");
        private static readonly byte[] Iv = Encoding.ASCII.GetBytes("2C5503C419E94885");

        public User FirstPair { get; set; }
        public User SecondPair { get; set; }
        public User Operator { get; set; }

        public bool AnyNameMissing() 
            => FirstPair.Username.IsEmpty() || SecondPair.Username.IsEmpty() || Operator.Username.IsEmpty();

        public bool AnyPasswordMissing()
            => FirstPair.Password.IsEmpty() || SecondPair.Password.IsEmpty() || Operator.Password.IsEmpty();

        public bool EncodePlainTextPasswords()
        {
            var isPlaintextPassword = false;

            if (IsPlainText(FirstPair.Password))
                FirstPair.Password = Encode(FirstPair.Password);
            if (IsPlainText(SecondPair.Password))
                SecondPair.Password = Encode(SecondPair.Password);
            if (IsPlainText(Operator.Password))
                Operator.Password = Encode(Operator.Password);

            return isPlaintextPassword;

            bool IsPlainText(string password)
                => !password.StartsWith("secret:");

            string Encode(string password)
            {
                isPlaintextPassword = true;
                return "secret:" + Convert.ToBase64String(EncryptStringToBytes_Aes(GetSha1(password), Key, Iv));
            }
        }

        public IReadOnlyList<User> GetUsersWithDecryptedPassword()
        {
            return new List<User>
            {
                GetDecodedUser(FirstPair),
                GetDecodedUser(SecondPair),
                GetDecodedUser(Operator),
            };

            User GetDecodedUser(User encodedUser)
            {
                var decodedUser = new User(encodedUser);
                if(!encodedUser.Password.StartsWith("secret:"))
                    throw new Exception("Le mot de passe n'est pas un secret");

                decodedUser.Password = DecryptStringFromBytes_Aes(Convert.FromBase64String(encodedUser.Password.Remove(0, 7)), Key, Iv);
                return decodedUser;
            }
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static string GetSha1(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(bytes);
                return hashBytes.Aggregate(string.Empty, (seed, item) => seed + item.ToString("x2"));
            }
        }
    }
}