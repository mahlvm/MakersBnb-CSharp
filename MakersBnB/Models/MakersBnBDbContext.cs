namespace MakersBnB.Models;
using Microsoft.EntityFrameworkCore;

public class MakersBnBDbContext : DbContext
{
    // Representa a tabela Spaces no banco de dados
    internal DbSet<Space>? Spaces { get; set; }
    internal DbSet<User>? Users { get; set; }

    // Caminho para o banco de dados
    internal string? DbPath { get; }

     // Nome do banco de dados
    internal string dbName = "makersbnb_aspdotnet_dev";
    
    // Configuração do banco de dados
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
        @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
}