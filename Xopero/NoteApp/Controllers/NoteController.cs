using NoteApp.Encryption;
using NoteApp.UI;
using NoteApp.Utils.Database;
using Npgsql;

namespace NoteApp.Controllers;

public class NoteController
{
    public static List<Dictionary<string, object>> ListRawNotesData(bool isDebugMode, string databaseConnectionString)
    {
        var databaseDriver = new PostgresDatabaseDriver(isDebugMode, databaseConnectionString);
        var items = databaseDriver.List("notes");

        return items;
    }
    
    public static List<Dictionary<string, object>> ListNotes(bool isDebugMode, string databaseConnectionString)
    {
        var items = new List<Dictionary<string, object>>();

        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var query = "SELECT id, title FROM notes";
            using var cmd = new NpgsqlCommand(query, connection);
            items = PostgresDatabaseDriver.ExecuteReader(cmd.ExecuteReader());
        }
        catch (Exception ex)
        {
            if (isDebugMode)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return items;
    }
    
    public static Dictionary<string, object>? ReadNote(bool isDebugMode, string databaseConnectionString, int id)
    {
        var databaseDriver = new PostgresDatabaseDriver(isDebugMode, databaseConnectionString);
        var item = databaseDriver.Read("notes", id);

        return item;
    }
    
    public static bool CreateNote(bool isDebugMode, string databaseConnectionString, string title, string content, string key)
    {
        var encryptedData = AesEncryptor.Encrypt(content, key);

        if (isDebugMode)
        {
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Content: {content}");
            Console.WriteLine($"Encryption key: {key}");
            Console.WriteLine($"Encrypted data: {encryptedData}\n");
        }

        var databaseDriver = new PostgresDatabaseDriver(isDebugMode, databaseConnectionString);
        var dataToInsert = new Dictionary<string, object>
        {
            {"title", title},
            {"content", encryptedData}
        };

        return databaseDriver.Insert("notes", dataToInsert);
    }

    public static bool DeleteNote(bool isDebugMode, string databaseConnectionString, int id)
    {
        if (isDebugMode)
        {
            Console.WriteLine($"ID to remove: {id}");
        }

        var databaseDriver = new PostgresDatabaseDriver(isDebugMode, databaseConnectionString);
        return databaseDriver.Delete("notes", id);
    }
}