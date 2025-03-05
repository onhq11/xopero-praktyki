using Microsoft.Extensions.Configuration;

namespace NoteApp.Config.Builders;

public class DatabaseConnection
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    public DatabaseConnection(IConfigurationRoot appSettings)
    {
        appSettings.GetSection("DatabaseConnection").Bind(this);
    }
    
    public new string ToString()
    {
        return
            $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
}