using Microsoft.Extensions.Configuration;
using NoteApp.Config.Builders;

namespace NoteApp.Config;

public class ConfigBuilder(IConfigurationRoot appSettings)
{
    private DatabaseConnection _databaseConnection = new DatabaseConnection(appSettings);
    private DebugMode _debugMode = new DebugMode(appSettings);

    private void Init()
    {
        _databaseConnection = new DatabaseConnection(appSettings);
        _debugMode = new DebugMode(appSettings);
    }

    public string GetDatabaseConnectionString()
    {
        return _databaseConnection.ToString();
    }
    
    public bool IsDebugMode()
    {
        return _debugMode.IsDebugMode();
    }
}