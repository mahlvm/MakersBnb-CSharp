namespace MakersBnB.Models;
using Microsoft.EntityFrameworkCore;

public class MakersBnBDbContext : DbContext
{
    
    internal DbSet<Space>? Spaces { get; set; }
    internal DbSet<User>? Users { get; set; }
    public DbSet<Reservation>? Reservations { get; set; }

    
    internal string? DbPath { get; }


    internal string dbName = "makersbnb_aspdotnet_dev";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Spaces)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

          
            modelBuilder.Entity<Space>()
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Space)
                .HasForeignKey(r => r.SpaceId);

          
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }

    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
        @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
}
