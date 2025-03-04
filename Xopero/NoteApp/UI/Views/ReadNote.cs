using NoteApp.Controllers;
using NoteApp.Encryption;

namespace NoteApp.UI.Views;

public class ReadNote
{
    public static void View(bool isDebugMode, string databaseConnectionString, Dictionary<string, object>? item)
    {
        Console.Clear();
        Console.WriteLine("\n== Read note ==\n");

        if (item == null)
        {
            Console.WriteLine("There was an error while reading note");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Ui.Menu(isDebugMode, databaseConnectionString);
            return;
        }
        
        var note = NoteController.ReadNote(isDebugMode, databaseConnectionString, (int)item["id"]);
        if (note == null)
        {
            Console.WriteLine("There was an error while reading note");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Ui.Menu(isDebugMode, databaseConnectionString);
            return;
        }

        int failedAttempts = 0;
        string decryptedContent = "";
        while (string.IsNullOrEmpty(decryptedContent) && failedAttempts < 5)
        {
            string key = "";
            while (string.IsNullOrEmpty(key))
            {
                Console.Write("Insert your encryption key: ");
                key = Console.ReadLine();
            }

            decryptedContent = AesEncryptor.Decrypt(note["content"].ToString(), key);

            if (string.IsNullOrEmpty(decryptedContent))
            {
                Console.WriteLine("Invalid key\n");
                failedAttempts = failedAttempts + 1;
            }
        }

        if (failedAttempts >= 5)
        {
            Console.WriteLine("\nYou have reached the maximum number of attempts\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Ui.Menu(isDebugMode, databaseConnectionString);
            return;
        }
        
        Console.Clear();
        Console.WriteLine("\n== Read note ==\n");
        
        Console.WriteLine($"ID: {note["id"]}");
        Console.WriteLine($"Title: {note["title"]}");
        Console.WriteLine($"Content: {decryptedContent}");
        
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
                break;
            
            case "2":
                DeleteNote.View(isDebugMode, databaseConnectionString, (int)item["id"]);
                break;
            
            default:
                View(isDebugMode, databaseConnectionString, item);
                return;
        }
    }
}