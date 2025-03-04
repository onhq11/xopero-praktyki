using NoteApp.Controllers;

namespace NoteApp.UI.Views;

public class RawReadFromDatabase
{
    public static void View(bool isDebugMode, string databaseConnectionString)
    {
        Console.Clear();
        Console.WriteLine("\n== Raw Read From Database ==\n");
        
        var items = NoteController.ListRawNotesData(isDebugMode, databaseConnectionString);

        if (items.Count <= 0)
        {
            Console.WriteLine("\nNo notes found");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        foreach (var row in items)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"id: {row["id"]}");
            Console.WriteLine($"title: {row["title"]}");
            Console.WriteLine($"content: {row["content"]}");
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}