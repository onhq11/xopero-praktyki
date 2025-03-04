namespace NoteApp.UI;

public class Ui
{
    public static void Menu(bool isDebugMode)
    {
        Console.WriteLine("[1] Add note");
        Console.WriteLine("[2] List notes");

        if (isDebugMode)
        {
            Console.WriteLine("\n\n== Developer options ==");
            Console.WriteLine("[3] Raw read from database");
        }
        
        Console.WriteLine("\n\n\n\n[9] Exit");
        Console.Write($"\nChoose an option: ");

        var selected = Console.ReadLine();
        Console.Clear();
        
        switch (selected)
        {
            case "1":
                AddNote();
                break;
            
            case "2":
                ListNotes();
                break;
            
            case "3":
                if (!isDebugMode)
                {
                    InvalidOption();
                }
                
                RawReadFromDatabase();
                break;
            
            case "9":
                Exit();
                break;
            
            default:
                InvalidOption();
                break;
        }
    }

    private static void AddNote()
    {
        
    }

    private static void ListNotes()
    {
        
    }

    private static void RawReadFromDatabase()
    {
        
    }

    private static void Exit()
    {
        Environment.Exit(0);
    }

    private static void InvalidOption()
    {
        Console.WriteLine(" == INVALID OPTION == \n\n");
    }
}