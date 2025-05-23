using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
class Subscriber
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqps://xzhfkjrz:Kq0gWWMiMN5xR9m1Rmz1kE89Go8ALf4h@porpoise.rmq.cloudamqp.com/xzhfkjrz")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        string queueName = "minha-fila";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Mensagem recebida: " + message);
        };
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("Aguardando mensagens. Pressione [enter] para sair.");
        Console.ReadLine();
    }
}