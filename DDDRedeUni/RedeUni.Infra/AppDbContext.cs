using RedeUni.Domain;
using Microsoft.EntityFrameworkCore;

namespace RedeUni.Infra;

public class AppDbContext : DbContext
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=universidade;User Id=sa;Password=MinhaSenha;TrustServerCertificate=False;");
        }
    }
}