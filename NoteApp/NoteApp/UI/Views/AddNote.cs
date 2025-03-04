using NoteApp.Controllers;

namespace NoteApp.UI.Views;

public class AddNote
{
    public static void View(bool isDebugMode, string databaseConnectionString)
    {
        Console.Clear();
        Console.WriteLine("\n== Add note ==\n");
        
        Console.Write("Title: ");
        var title = Console.ReadLine();
        if (string.IsNullOrEmpty(title)) title = "(no title)";
        
        Console.Write("Content: ");
        var content = Console.ReadLine();
        if (string.IsNullOrEmpty(content)) content = "(no content)";
        
        Console.WriteLine("\nNow you have to put your encryption key.");
        Console.WriteLine("If you lose it, you won't be able to read your note.");

        string? key = null;

        while (string.IsNullOrEmpty(key))
        {
            Console.Write("Encryption key: ");
            key = Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("\n== Encrypting note ==\n");

        var success = NoteController.CreateNote(isDebugMode, databaseConnectionString, title, content, key);
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