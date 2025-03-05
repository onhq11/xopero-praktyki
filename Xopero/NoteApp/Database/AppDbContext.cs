using Microsoft.EntityFrameworkCore;
using NoteApp.Database.Models;

namespace NoteApp.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Note> Notes { get; set; }
}