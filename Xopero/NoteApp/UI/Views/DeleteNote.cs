using NoteApp.Controllers;

namespace NoteApp.UI.Views;

public class DeleteNote
{
    public static void View(bool isDebugMode, string databaseConnectionString, int id)
    {
        Console.Clear();
        Console.WriteLine("\n== Deleting note ==\n");
        
        var success = NoteController.DeleteNote(isDebugMode, databaseConnectionString, id);
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