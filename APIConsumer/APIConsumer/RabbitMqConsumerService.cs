using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace APIConsumer;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqConsumerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://xzhfkjrz:Kq0gWWMiMN5xR9m1Rmz1kE89Go8ALf4h@porpoise.rmq.cloudamqp.com/xzhfkjrz")
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        string queueName = "minha-fila";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Aqui você pode processar a mensagem, salvar no banco, etc.
            Console.WriteLine($"Mensagem recebida: {message}");
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}