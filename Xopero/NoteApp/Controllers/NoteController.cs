using Microsoft.EntityFrameworkCore;
using NoteApp.Config;
using NoteApp.Database;
using NoteApp.Database.Models;
using NoteApp.Encryption;

namespace NoteApp.Controllers;

public class NoteController
{
    public static List<Note> List(AppDbContext appDbContext)
    {
        var items = appDbContext.Notes.ToList();
        return items;
    }
    
    public static Note? Read(AppDbContext appDbContext, int id)
    {
        var item = appDbContext.Notes.Find(id);
        return item ?? null;
    }
    
    public static bool Create(AppDbContext appDbContext, ConfigBuilder config, string title, string content, string key)
    {
        try
        {
            var encryptedData = AesEncryptor.Encrypt(content, key);

            if (config.IsDebugMode())
            {
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine($"Encryption key: {key}");
                Console.WriteLine($"Encrypted data: {encryptedData}\n");
            }

            var newNotes = new Note { Title = title, Content = encryptedData };
            appDbContext.Notes.Add(newNotes);
            appDbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            if (config.IsDebugMode())
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }

    public static bool Update(AppDbContext appDbContext, ConfigBuilder config, int id, string title,
        string content, string key)
    {
        try
        {
            var encryptedData = AesEncryptor.Encrypt(content, key);

            if (config.IsDebugMode())
            {
                Console.WriteLine($"ID: {id.ToString()}");
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Content: {content}");
                Console.WriteLine($"Encryption key: {key}");
                Console.WriteLine($"Encrypted data: {encryptedData}\n");
            }
        
            using var context = new AppDbContext(new DbContextOptions<AppDbContext>());
            var note = context.Notes.Find(id);
            if (note == null)
            {
                throw new Exception("Note not found");
            }
            
            note.Title = title;
            note.Content = encryptedData;
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            if (config.IsDebugMode())
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }

    public static bool Delete(AppDbContext appDbContext, ConfigBuilder config, int id)
    {
        try
        {
            if (config.IsDebugMode())
            {
                Console.WriteLine($"ID to remove: {id}");
            }
            
            using var context = new AppDbContext(new DbContextOptions<AppDbContext>());
            var note = context.Notes.Find(id);
            if (note == null)
            {
                throw new Exception("Note not found");
            }
            
            context.Notes.Remove(note);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            if (config.IsDebugMode())
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}