using NoteApp.Controllers;
using NoteApp.Encryption;

namespace NoteApp.UI.Views;

public class ReadNote
{
    public static void View(bool isDebugMode, string databaseConnectionString, int id, string title, string content)
    {
        Console.Clear();
        Console.WriteLine("\n== Read note ==\n");
        
        Console.WriteLine($"ID: {id.ToString()}");
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Content: {content}");
        
        Console.WriteLine("\n[1] Edit");
        Console.WriteLine("[2] Delete");
        Console.WriteLine("\n\n[0] Return");
        
        Console.Write($"\nChoose an option: ");
        var selected = Console.ReadLine();

        switch (selected)
        {
            case "0":
                ListNotes.View(isDebugMode, databaseConnectionString);
                break;
            
            case "1":
                EditNote.View(isDebugMode, databaseConnectionString, id, title, content);
                break;
            
            case "2":
                DeleteNote.View(isDebugMode, databaseConnectionString, id);
                break;
            
            default:
                View(isDebugMode, databaseConnectionString, id, title, content);
                return;
        }
    }
}