using System.Text;
using Npgsql;

namespace NoteApp.Utils.Database;

public class PostgresDatabaseDriver(bool isDebugMode, string databaseConnectionString)
{
    public List<Dictionary<string, object>> List(string tableName)
    {
        var items = new List<Dictionary<string, object>>();

        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var query = $"SELECT * FROM {tableName}";
            using var cmd = new NpgsqlCommand(query, connection);
            items = ExecuteReader(cmd.ExecuteReader());
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
    
    public Dictionary<string, object>? Read(string tableName, int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var query = $"SELECT * FROM {tableName} WHERE id = @id";
            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
            var items = ExecuteReader(cmd.ExecuteReader());

            if (items.Count <= 0)
            {
                return null;
            }

            return items[0];
        }
        catch (Exception ex)
        {
            if (isDebugMode)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return null;
    }
    
    public bool Insert(string tableName, Dictionary<string, object> columns)
    {
        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var columnNames = new StringBuilder();
            var columnValues = new StringBuilder();
            var parameters = new List<NpgsqlParameter>();

            foreach (var column in columns)
            {
                if (columnNames.Length > 0)
                {
                    columnNames.Append(", ");
                    columnValues.Append(", ");
                }

                columnNames.Append(column.Key);
                columnValues.Append($"@{column.Key}");
                parameters.Add(new NpgsqlParameter(column.Key, column.Value));
            }

            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({columnValues})";
            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters.ToArray());
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            if (isDebugMode)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
    
    public bool Update(string tableName, int id, Dictionary<string, object> columns)
    {
        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var setClause = new StringBuilder();
            var parameters = new List<NpgsqlParameter>();

            foreach (var column in columns)
            {
                if (setClause.Length > 0)
                {
                    setClause.Append(", ");
                }

                setClause.Append($"{column.Key} = @{column.Key}");
                parameters.Add(new NpgsqlParameter(column.Key, column.Value));
            }

            var query = $"UPDATE {tableName} SET {setClause} WHERE id = @id";
            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddRange(parameters.ToArray());
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            if (isDebugMode)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
    
    public bool Delete(string tableName, int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(databaseConnectionString);
            connection.Open();

            var query = $"DELETE FROM {tableName} WHERE id = @id";
            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            if (isDebugMode)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
    
    public static List<Dictionary<string, object>> ExecuteReader(NpgsqlDataReader reader)
    {
        var items = new List<Dictionary<string, object>>();
        
        while (reader.Read())
        {
            var row = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }
                
            items.Add(row);
        }

        return items;
    }
}