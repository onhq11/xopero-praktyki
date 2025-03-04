using System.Security.Cryptography;
using System.Text;

namespace NoteApp.Encryption;

public class AesEncryptor
{
    public static string Encrypt(string text, string key)
    {
        var aes = Aes.Create();
        aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        aes.GenerateIV();
        
        var encryptor = aes.CreateEncryptor();
        var encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(text), 0, text.Length);
        return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string? encryptedText, string key)
    {
        if (string.IsNullOrEmpty(encryptedText)) return "";
        
        var parts = encryptedText.Split(':');
        
        var aes = Aes.Create();
        aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        aes.IV = Convert.FromBase64String(parts[0]);
       
        var decryptor = aes.CreateDecryptor();
        byte[] decrypted;

        try
        {
            decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(parts[1]), 0,
                Convert.FromBase64String(parts[1]).Length);
        }
        catch (Exception ex)
        {
            return "";
        }
        
        return Encoding.UTF8.GetString(decrypted);
    }
}