using NoteApp.Controllers;
using NoteApp.Encryption;

namespace NoteApp.UI.Views;

public class ListNotes
{
    public static void View(bool isDebugMode, string databaseConnectionString)
    {
        Console.Clear();
        Console.WriteLine("\n== List notes ==\n");
        
        var items = NoteController.ListNotes(isDebugMode, databaseConnectionString);
        if (items.Count <= 0)
        {
            Console.WriteLine("No notes found");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        foreach (var row in items)
        {
            Console.WriteLine($"[{row["id"]}] {row["title"]}");
        }
        
        Console.WriteLine("\n\n[0] Return");
        Console.Write($"\nChoose an option: ");

        var selected = Console.ReadLine();
        Console.Clear();
        
        switch (selected)
        {
            case "0":
                Ui.Menu(isDebugMode, databaseConnectionString);
                return;
            
            default:
                if (string.IsNullOrEmpty(selected))
                {
                    View(isDebugMode, databaseConnectionString);
                }
                
                var item = items.FirstOrDefault(item => item.ContainsKey("id") && item["id"].ToString() == selected);
                
                if (item == null)
                {
                    View(isDebugMode, databaseConnectionString);
                }
                
                ReadNote.View(isDebugMode, databaseConnectionString, item);

                break;
        }
    }
}