using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI.Models;

namespace EventRegistrationAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<Registration>().HasIndex(r => new { r.UserName, r.EventId }).IsUnique();
    }
}