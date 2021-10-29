using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class TopicExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            int count = 0;
            while (true)
            {
                var messgae = new { Name = "Producer", Message = $"Hello Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messgae));
                if (count % 2 == 0)
                {
                    channel.BasicPublish("demo-topic-exchange", "account.init", null, body);
                }
                else
                {
                    channel.BasicPublish("demo-topic-exchange", "update.init", null, body);
                }
                Thread.Sleep(1000);
                count++;
            }

        }
    }
}
