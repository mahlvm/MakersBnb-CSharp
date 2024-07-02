namespace MakersBnB.Models;
using Microsoft.EntityFrameworkCore;

public class MakersBnBDbContext : DbContext
{
    // Representa a tabela Spaces, User e Reservation no banco de dados
    internal DbSet<Space>? Spaces { get; set; }
    internal DbSet<User>? Users { get; set; }
    public DbSet<Reservation>? Reservations { get; set; }

    // Caminho para o banco de dados
    internal string? DbPath { get; }

     // Nome do banco de dados
    internal string dbName = "makersbnb_aspdotnet_dev";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura o relacionamento um-para-muitos entre User e Space
            modelBuilder.Entity<User>()
                .HasMany(u => u.Spaces)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            // Configura o relacionamento um-para-muitos entre Space e Reservation
            modelBuilder.Entity<Space>()
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Space)
                .HasForeignKey(r => r.SpaceId);

            // Configura o relacionamento um-para-muitos entre User e Reservation
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }

    
    
    // Configuração do banco de dados
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
        @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
}