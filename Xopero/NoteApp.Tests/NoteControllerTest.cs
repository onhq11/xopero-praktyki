using Microsoft.EntityFrameworkCore;
using NoteApp.Controllers;
using NoteApp.Database;
using NoteApp.Database.Models;
using NoteApp.Utils.Encryption;

namespace NoteApp.Tests;

public class NoteControllerTest
{
    private AppDbContext CreateDbContext()
    {
        const string encryptionKey = "UltraSecureEncryptionKey";
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Notes.Add(new Note { Id = 1, Title = "Test Note 1", Content = AesEncryptor.Encrypt("Test Content 1", encryptionKey) });
        context.Notes.Add(new Note { Id = 2, Title = "Test Note 2", Content = AesEncryptor.Encrypt("Test Content 2", encryptionKey) });
        context.Notes.Add(new Note { Id = 3, Title = "Test Note 3", Content = AesEncryptor.Encrypt("Test Content 3", encryptionKey) });
        context.SaveChanges();

        return context;
    }
    
    [Fact]
    public void Note_List()
    {
        using var context = CreateDbContext();
        var notes = NoteController.List(context);
        Assert.Equal(3, notes.Count);
    }
    
    [Fact]
    public void Note_Read()
    {
        using var context = CreateDbContext();
        var note = NoteController.Read(context, 1);
        Assert.NotNull(note);
    }
    
    [Fact]
    public void Note_Create()
    {
        using var context = CreateDbContext();
        
        var testNote = new { Title = "Test Note 4", Content = "Test Content 4" };
        const string testKey = "UltraSecureEncryptionKey";
        
        var success = NoteController.Create(context, null, testNote.Title, testNote.Content, testKey);
        if (success)
        {
            var notes = NoteController.List(context);
            var noteCreated = notes.Count == 4;

            var note = NoteController.Read(context, 4);
            var noteDataMatches = note != null && (note.Title == testNote.Title && AesEncryptor.Decrypt(note.Content, testKey) == testNote.Content);
            
            Assert.True(noteCreated && noteDataMatches, "Note wasn't created or data doesn't match");
        }
        
        Assert.True(success, "Cannot create note");
    }

    [Fact]
    public void Note_Update()
    {
        using var context = CreateDbContext();
        
        var note = NoteController.Read(context, 1);
        if (note == null)
        {
            Assert.Fail("Note with ID 1 not found");
            return;
        }
        
        var testNote = new { Title = "New Test Note 1", Content = "New Test Content 1" };
        const string testKey = "NewUltraSecureEncryptionKey";
        
        var success = NoteController.Update(context, null, note.Id, testNote.Title, testNote.Content, testKey);
        if (success)
        {
            var newNote = NoteController.Read(context, 1);
            var noteDataMatches = newNote != null && (newNote.Title == testNote.Title && AesEncryptor.Decrypt(newNote.Content, testKey) == testNote.Content);
            
            Assert.True(noteDataMatches, "Data doesn't match with updated note");
        }
        
        Assert.True(success, "Cannot update note");
    }

    [Fact]
    public void Note_Delete()
    {
        using var context = CreateDbContext();
        
        var success = NoteController.Delete(context, null, 1);
        if (success)
        {
            var notes = NoteController.List(context);
            Assert.Equal(2, notes.Count);
        }
        
        Assert.True(success, "Cannot delete note");
    }
}
