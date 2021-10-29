using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var factory = new ConnectionFactory { Uri=new Uri("amqp://guest:guest@localhost:5672") };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            FanOutExchangePublisher.Publish(channel);
        }
    }
}

