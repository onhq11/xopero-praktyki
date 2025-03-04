using Microsoft.Extensions.Configuration;

namespace NoteApp.Config;

public class AppSettingsFileReader
{
    private readonly IConfigurationRoot _appSettings = new ConfigurationBuilder().Build();
    
    public AppSettingsFileReader()
    {
        if (!File.Exists("appsettings.json"))
        {
            Console.WriteLine("Appsettings.json not found");
            return;
        }
        
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _appSettings = builder.Build();
    }
    
    public IConfigurationRoot GetAppSettings()
    {
        return _appSettings;
    }
}