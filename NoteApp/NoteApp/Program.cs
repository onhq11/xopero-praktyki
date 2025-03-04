using Microsoft.Extensions.Configuration;
using NoteApp.Config;
using NoteApp.Encryption;

namespace NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        var appSettingsFileReader = new AppSettingsFileReader();
        var appSettings = appSettingsFileReader.GetAppSettings();
        
        var config = new ConfigBuilder(appSettings);

        var isDebugMode = config.IsDebugMode();
        var databaseConnection = new PostgresDatabaseDriver(config.GetDatabaseConnectionString());

        UI.Ui.Menu(isDebugMode);
        
        var encrypted = AesEncryptor.Encrypt("aaa", "aaa");
        Console.WriteLine(encrypted);
        Console.WriteLine("Type key: ");
        var key = Console.ReadLine();
        var decrypted = AesEncryptor.Decrypt(key, "aaa");
        Console.WriteLine(decrypted);
        databaseConnection.Init();
    }
}