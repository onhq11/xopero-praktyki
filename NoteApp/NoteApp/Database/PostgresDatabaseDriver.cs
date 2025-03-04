using Npgsql;

namespace NoteApp;

public class PostgresDatabaseDriver(string connectionString)
{
    public async void Init()
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        Console.WriteLine(connectionString);
    }
}