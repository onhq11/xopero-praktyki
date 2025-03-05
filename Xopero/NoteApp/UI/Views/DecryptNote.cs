using NoteApp.Config;
using NoteApp.Controllers;
using NoteApp.Database;
using NoteApp.Database.Models;
using NoteApp.Utils.Encryption;

namespace NoteApp.UI.Views;

public class DecryptNote
{
    public static void View(AppDbContext appDbContext, ConfigBuilder config, Note? item)
    {
        Console.Clear();
        Console.WriteLine("\n== Read note ==\n");

        if (item == null)
        {
            Console.WriteLine("There was an error while reading note");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Ui.Menu(appDbContext, config);
            return;
        }
        
        var note = NoteController.Read(appDbContext, item.Id);
        if (note == null)
        {
            Console.WriteLine("There was an error while reading note");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Ui.Menu(appDbContext, config);
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

            decryptedContent = AesEncryptor.Decrypt(note.Content, key);

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
            
            Ui.Menu(appDbContext, config);
            return;
        }
        
        ReadNote.View(appDbContext, config, item, decryptedContent);
    }
}