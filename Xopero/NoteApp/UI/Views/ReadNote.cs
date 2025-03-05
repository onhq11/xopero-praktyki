using NoteApp.Config;
using NoteApp.Database;
using NoteApp.Database.Models;

namespace NoteApp.UI.Views;

public class ReadNote
{
    public static void View(AppDbContext appDbContext, ConfigBuilder config, Note item, string decryptedContent)
    {
        Console.Clear();
        Console.WriteLine("\n== Read note ==\n");
        
        Console.WriteLine($"ID: {item.Id.ToString()}");
        Console.WriteLine($"Title: {item.Title}");
        Console.WriteLine($"Content: {decryptedContent}");
        
        Console.WriteLine("\n[1] Edit");
        Console.WriteLine("[2] Delete");
        Console.WriteLine("\n\n[0] Return");
        
        Console.Write($"\nChoose an option: ");
        var selected = Console.ReadLine();

        switch (selected)
        {
            case "0":
                ListNotes.View(appDbContext, config);
                break;
            
            case "1":
                EditNote.View(appDbContext, config, item, decryptedContent);
                break;
            
            case "2":
                DeleteNote.View(appDbContext, config, item);
                break;
            
            default:
                View(appDbContext, config, item, decryptedContent);
                return;
        }
    }
}