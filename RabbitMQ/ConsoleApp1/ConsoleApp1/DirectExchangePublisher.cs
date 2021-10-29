using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange",ExchangeType.Direct);
            int count = 0;
            while (true)
            {
                var messgae = new { Name = "Producer", Message = $"Hello Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messgae));
                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
                Thread.Sleep(1000);
                count++;
            }

        }
    }
}
