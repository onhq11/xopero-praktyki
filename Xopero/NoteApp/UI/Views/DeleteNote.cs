using NoteApp.Config;
using NoteApp.Controllers;
using NoteApp.Database;
using NoteApp.Database.Models;

namespace NoteApp.UI.Views;

public class DeleteNote
{
    public static void View(AppDbContext appDbContext, ConfigBuilder config, Note item)
    {
        Console.Clear();
        Console.WriteLine("\n== Deleting note ==\n");
        
        var success = NoteController.Delete(appDbContext, config, item.Id);
        if (!success)
        {
            Console.WriteLine("\nFailed to remove note.\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("\nRemoved note successfully!\n");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}