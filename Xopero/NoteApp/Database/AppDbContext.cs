using Microsoft.EntityFrameworkCore;
using NoteApp.Database.Models;

namespace NoteApp.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }
}