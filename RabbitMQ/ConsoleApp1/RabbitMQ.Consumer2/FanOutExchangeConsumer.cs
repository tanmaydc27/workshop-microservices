using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer2
{
    public static class FanOutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("demo-fanout-queue-consumer2", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-fanout-queue-consumer2", "demo-fanout-exchange",string.Empty, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-fanout-queue-consumer2", true, consumer);
            Console.WriteLine("Consumer2 Started");
            Console.ReadLine();
        }
    }
}
