using NoteApp.Controllers;
using NoteApp.Encryption;

namespace NoteApp.UI.Views;

public class DecryptNote
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
                failedAttempts++;
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

        var id = (int)item["id"];
        var title = item["title"].ToString() ?? "(no title)";
        var content = decryptedContent;
        
        ReadNote.View(isDebugMode, databaseConnectionString, id, title, content);
    }
}