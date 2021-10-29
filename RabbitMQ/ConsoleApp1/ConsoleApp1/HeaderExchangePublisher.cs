using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            int count = 0;
            while (true)
            {
                var messgae = new { Name = "Producer", Message = $"Hello Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messgae));
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };
                channel.BasicPublish("demo-header-exchange", string.Empty, properties, body);
                Thread.Sleep(1000);
                count++;
            }

        }
    }
}
