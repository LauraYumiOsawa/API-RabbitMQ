using Microsoft.EntityFrameworkCore;
using APIConsumer.Data;

namespace APIConsumer;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=APIConsumer;Trusted_Connection=True;TrustServerCertificate=True;"));

        builder.Services.AddControllers();
        builder.Services.AddHostedService<RabbitMqConsumerService>();

        var app = builder.Build();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        app.Run();
    }
}