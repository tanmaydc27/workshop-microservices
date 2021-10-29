using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.RabbitMQ
{
    public interface IConnectionProvider : IDisposable
    {
        IConnection GetConnection();
    }
}
