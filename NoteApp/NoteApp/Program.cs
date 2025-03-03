using Microsoft.Extensions.Configuration;

namespace NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        if (!File.Exists("appsettings.json"))
        {
            Console.WriteLine("Appsettings.json not found");
            return;
        }
        
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var appSettings = builder.Build();
        
        var databaseConnectionSettings = new ConnectionStringBuilder();
        appSettings.GetSection("DatabaseConnection").Bind(databaseConnectionSettings);
        
        var connectionString = databaseConnectionSettings.ToString();
        var databaseConnection = new DatabaseConnection(connectionString);

        databaseConnection.Init();
    }
}