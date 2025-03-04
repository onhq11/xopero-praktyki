using Microsoft.Extensions.Configuration;
using NoteApp.Config.Builders;

namespace NoteApp.Config;

public class ConfigBuilder(IConfigurationRoot appSettings)
{
    private readonly DatabaseConnection _databaseConnection = new DatabaseConnection(appSettings);
    private readonly DebugMode _debugMode = new DebugMode(appSettings);

    public string GetDatabaseConnectionString()
    {
        return _databaseConnection.ToString();
    }
    
    public bool IsDebugMode()
    {
        return _debugMode.IsDebugMode();
    }
}