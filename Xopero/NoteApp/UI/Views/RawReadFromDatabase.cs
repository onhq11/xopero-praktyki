using NoteApp.Controllers;
using NoteApp.Database;

namespace NoteApp.UI.Views;

public class RawReadFromDatabase
{
    public static void View(AppDbContext appDbContext)
    {
        Console.Clear();
        Console.WriteLine("\n== Raw Read From Database ==\n");
        
        var items = NoteController.List(appDbContext);

        if (items.Count <= 0)
        {
            Console.WriteLine("\nNo notes found");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        foreach (var row in items)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"id: {row.Id}");
            Console.WriteLine($"title: {row.Title}");
            Console.WriteLine($"content: {row.Content}");
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}