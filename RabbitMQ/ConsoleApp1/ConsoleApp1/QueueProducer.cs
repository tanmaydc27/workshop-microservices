using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            int count = 0;
            while (true)
            {
                var messgae = new { Name = "Producer", Message = $"Hello Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messgae));
                channel.BasicPublish("", "demo-queue", null, body);
                Thread.Sleep(1000);
                count++;
            }

        }
    }
}
