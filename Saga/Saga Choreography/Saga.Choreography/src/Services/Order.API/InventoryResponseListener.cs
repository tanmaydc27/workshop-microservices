
using EventBus.Event;
using EventBus.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Order.API.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API
{
    public class InventoryResponseListener : IHostedService
    {
        private readonly ISubscriber subscriber;
        private readonly IOrderRepository _orderRepository;

        public InventoryResponseListener(ISubscriber subscriber, IOrderRepository orderRepository)
        {
            this.subscriber = subscriber;
            this._orderRepository = orderRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(Subscribe);
            return Task.CompletedTask;
        }

        private bool Subscribe(string message, IDictionary<string, object> header)
        {
            var response = JsonConvert.DeserializeObject<OrderUpdateEvent>(message);
            if (response.IsSuccess)
            {
                return _orderRepository.Update(response.OrderID,"Order Submitted");
            }
            else
            {
                return _orderRepository.Update(response.OrderID, "Order Cancelled");
            }
        } 

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
