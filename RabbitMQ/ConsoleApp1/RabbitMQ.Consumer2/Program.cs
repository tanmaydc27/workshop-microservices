using RabbitMQ.Client;
using System;

namespace RabbitMQ.Consumer2
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            FanOutExchangeConsumer.Consume(channel);
        }
    }
}
