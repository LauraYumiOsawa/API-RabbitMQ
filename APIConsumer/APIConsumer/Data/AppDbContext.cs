using Microsoft.EntityFrameworkCore;
using APIConsumer.Models;

namespace APIConsumer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Message> Mensagens { get; set; }
}