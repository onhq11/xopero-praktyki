using NoteApp.Config;
using NoteApp.Database;
using NoteApp.UI.Views;

namespace NoteApp.UI;

public class Ui
{
    public static void Menu(AppDbContext appDbContext, ConfigBuilder config)
    {
        Console.Clear();
        Console.WriteLine("\n== NoteApp ==\n");
        Console.WriteLine("[1] Add note");
        Console.WriteLine("[2] List notes");

        if (config.IsDebugMode())
        {
            Console.WriteLine("[3] Raw read from database");
        }
        
        Console.WriteLine("\n\n[9] Exit");
        Console.Write($"\nChoose an option: ");

        var selected = Console.ReadLine();
        Console.Clear();
        
        switch (selected)
        {
            case "1": 
                AddNote.View(appDbContext, config);
                break;
            
            case "2":
                ListNotes.View(appDbContext, config);
                break;
            
            case "3":
                if (!config.IsDebugMode()) break;
                
                RawReadFromDatabase.View(appDbContext);
                break;
            
            case "9":
                Exit.View();
                return;
        }

        Menu(appDbContext, config);
    }
}