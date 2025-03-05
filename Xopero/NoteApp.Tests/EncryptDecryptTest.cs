using NoteApp.Utils.Encryption;

namespace NoteApp.Tests;

public class EncryptDecryptTest
{
    [Fact]
    public void AesEncryptor_EncryptDecrypt()
    {
        const string key = "12345678901234567890123456789012";
        const string text = "Hello, World!";
        var encrypted = AesEncryptor.Encrypt(text, key);
        var decrypted = AesEncryptor.Decrypt(encrypted, key);
        
        Assert.Equal(text, decrypted);
    }
}