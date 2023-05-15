namespace SignatureEmailParser.BusinessLogic.Helpers.Interfaces
{
    public interface IEncryptionHelper
    {
        bool IsBase64Encoded(string value);
        string Decrypt(string value, string encryptionKey = null, string encryptionSalt = null);
        string Encrypt(string value, string encryptionKey = null, string encryptionSalt = null);
    }
}
