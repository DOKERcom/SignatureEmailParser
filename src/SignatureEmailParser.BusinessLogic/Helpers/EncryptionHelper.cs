using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public bool IsBase64Encoded(string value)
        {
            try
            {
                byte[] converted = Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Decrypt(string value, string encryptionKey, string encryptionSalt)
        {
            if (!IsBase64Encoded(value))
                return null;

            if (string.IsNullOrEmpty(encryptionKey) && string.IsNullOrEmpty(encryptionSalt))
            {
                encryptionKey = SettingConstant.ENCRYPTION_KEY;
                encryptionSalt = SettingConstant.ENCRYPTION_SALT;
            }

            byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, Encoding.ASCII.GetBytes(encryptionSalt));
                encryptor.Key = pdb.GetBytes(SettingConstant.ENCRYPTION_KEY_COUNT_BYTES);
                encryptor.IV = pdb.GetBytes(SettingConstant.ENCRYPTION_VECTOR_COUNT_BYTES);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(cipherBytes, default(int), cipherBytes.Length);
                        cryptoStream.Close();
                    }
                    value = Encoding.Unicode.GetString(memoryStream.ToArray());
                }
            }
            return value;
        }

        public string Encrypt(string value, string encryptionKey, string encryptionSalt)
        {
            if (string.IsNullOrEmpty(encryptionKey) && string.IsNullOrEmpty(encryptionSalt))
            {
                encryptionKey = SettingConstant.ENCRYPTION_KEY;
                encryptionSalt = SettingConstant.ENCRYPTION_SALT;
            }

            byte[] clearBytes = Encoding.Unicode.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, Encoding.ASCII.GetBytes(encryptionSalt));
                encryptor.Key = pdb.GetBytes(SettingConstant.ENCRYPTION_KEY_COUNT_BYTES);
                encryptor.IV = pdb.GetBytes(SettingConstant.ENCRYPTION_VECTOR_COUNT_BYTES);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(clearBytes, default(int), clearBytes.Length);
                        cryptoStream.Close();
                    }
                    value = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return value;
        }
    }
}
