using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.RabbitMQ
{
    public interface IPublisher : IDisposable
    {
        void Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, string timeToLive = null);
    }
}
