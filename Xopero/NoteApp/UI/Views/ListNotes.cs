using NoteApp.Config;
using NoteApp.Controllers;
using NoteApp.Database;

namespace NoteApp.UI.Views;

public class ListNotes
{
    public static void View(AppDbContext appDbContext, ConfigBuilder config)
    {
        Console.Clear();
        Console.WriteLine("\n== List notes ==\n");
        
        var items = NoteController.List(appDbContext);
        if (items.Count <= 0)
        {
            Console.WriteLine("No notes found");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        foreach (var row in items)
        {
            Console.WriteLine($"[{row.Id}] {row.Title}");
        }
        
        Console.WriteLine("\n\n[0] Return");
        Console.Write($"\nChoose an option: ");

        var selected = Console.ReadLine();
        Console.Clear();
        
        switch (selected)
        {
            case "0":
                Ui.Menu(appDbContext, config);
                return;
            
            default:
                if (string.IsNullOrEmpty(selected))
                {
                    View(appDbContext, config);
                }
                
                var item = items.FirstOrDefault(item => item.Id.ToString() == selected);
                
                if (item == null)
                {
                    View(appDbContext, config);
                }
                
                DecryptNote.View(appDbContext, config, item);

                break;
        }
    }
}