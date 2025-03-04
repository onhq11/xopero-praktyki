using NoteApp.Controllers;
using NoteApp.UI.Views;

namespace NoteApp.UI;

public class Ui
{
    public static void Menu(bool isDebugMode, string databaseConnectionString)
    {
        Console.Clear();
        Console.WriteLine("\n== NoteApp ==\n");
        Console.WriteLine("[1] Add note");
        Console.WriteLine("[2] List notes");

        if (isDebugMode)
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
                AddNote.View(isDebugMode, databaseConnectionString);
                break;
            
            case "2":
                ListNotes.View(isDebugMode, databaseConnectionString);
                break;
            
            case "3":
                if (!isDebugMode) break;
                
                RawReadFromDatabase.View(isDebugMode, databaseConnectionString);
                break;
            
            case "9":
                Exit.View();
                return;
        }

        Menu(isDebugMode, databaseConnectionString);
    }
}