using NoteApp.Config;
using NoteApp.Controllers;
using NoteApp.Database;
using NoteApp.Database.Models;

namespace NoteApp.UI.Views;

public class EditNote
{
    public static void View(AppDbContext appDbContext, ConfigBuilder config, Note item, string decryptedContent)
    {
        Console.Clear();
        Console.WriteLine("\n== Edit note ==\n");
        
        Console.WriteLine($"Previous title: {item.Title}");
        Console.Write("Title (leave blank to not change): ");
        var newTitle = Console.ReadLine();
        if (string.IsNullOrEmpty(newTitle)) newTitle = item.Title;
        
        Console.WriteLine($"\nPrevious content: {decryptedContent}");
        Console.Write("Content: ");
        var newContent = Console.ReadLine();
        if (string.IsNullOrEmpty(newContent)) newContent = decryptedContent;
        
        Console.WriteLine("\nNow you have to put your encryption key (you can type same as previously)");
        Console.WriteLine("If you lose it, you won't be able to read your note.");

        string? key = null;

        while (string.IsNullOrEmpty(key))
        {
            Console.Write("Encryption key: ");
            key = Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("\n== Encrypting note ==\n");

        var success = NoteController.Update(appDbContext, config, item.Id, newTitle, newContent, key);
        if (!success)
        {
            Console.WriteLine("\nFailed to edit note.\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("\nNote edited successfully!\n");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}