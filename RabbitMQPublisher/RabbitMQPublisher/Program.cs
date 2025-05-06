using RabbitMQ.Client;
using System;
using System.Text;
class Publisher
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://yaziixeo:IOzBI0HPToX4fgXX0Ng7Z_CHqIH6vWR_@jaragua.lmq.cloudamqp.com/yaziixeo")
        };
        using var connection = factory.CreateConnection(); //
        using var channel = connection.CreateModel();
        string queueName = "minha-fila";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        string message = "Olá, Asdrubal!";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        Console.WriteLine("Mensagem enviada: " + message);
    }
}