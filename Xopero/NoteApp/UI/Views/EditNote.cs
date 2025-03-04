using NoteApp.Controllers;

namespace NoteApp.UI.Views;

public class EditNote
{
    public static void View(bool isDebugMode, string databaseConnectionString, int id, string title, string content)
    {
        Console.Clear();
        Console.WriteLine("\n== Edit note ==\n");
        
        Console.WriteLine($"Previous title: {title}");
        Console.Write("Title (leave blank to not change): ");
        var newTitle = Console.ReadLine();
        if (string.IsNullOrEmpty(newTitle)) newTitle = title;
        
        Console.WriteLine($"\nPrevious content: {content}");
        Console.Write("Content: ");
        var newContent = Console.ReadLine();
        if (string.IsNullOrEmpty(newContent)) newContent = content;
        
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

        var success = NoteController.UpdateNote(isDebugMode, databaseConnectionString, id, newTitle, newContent, key);
        if (!success)
        {
            Console.WriteLine("\nFailed to add note.\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("\nNote added successfully!\n");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}