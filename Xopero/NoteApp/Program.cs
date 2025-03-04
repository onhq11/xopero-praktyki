using NoteApp.Config;
using NoteApp.UI;
using NoteApp.Utils.Database;

namespace NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        var appSettingsFileReader = new AppSettingsFileReader();
        var appSettings = appSettingsFileReader.GetAppSettings();
        
        var config = new ConfigBuilder(appSettings);

        var isDebugMode = config.IsDebugMode();
        var databaseConnectionString = config.GetDatabaseConnectionString();

        Ui.Menu(isDebugMode, databaseConnectionString);
    }
}