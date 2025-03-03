using Npgsql;

namespace NoteApp;

public class DatabaseConnection
{
    private String _connectionString;

    public DatabaseConnection(String connectionString)
    {
        _connectionString = connectionString;
    }

    public async void Init()
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
        Console.WriteLine(_connectionString);
    }
}