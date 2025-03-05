using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NoteApp.Config;
using NoteApp.Controllers;
using NoteApp.Database;
using NoteApp.UI;

namespace NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        var appSettingsFileReader = new AppSettingsFileReader();
        var appSettings = appSettingsFileReader.GetAppSettings();
        
        var config = new ConfigBuilder(appSettings);
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging((logging) =>
            {
                logging.SetMinimumLevel(LogLevel.Warning);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddDbContext<AppDbContext>((options) =>
                {
                    options.UseNpgsql(config.GetDatabaseConnectionString());
                });
            })
            .Build();
        
        host.Start();

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var appDbContext = services.GetRequiredService<AppDbContext>();
        Ui.Menu(appDbContext, config);
    }
}