using EventBus.Event;
using EventBus.RabbitMQ;
using Inventory.API.Repository;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.API
{
    public class OrderCreatedListener : IHostedService
    {
        private readonly IPublisher publisher;
        private readonly ISubscriber subscriber;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderCreatedListener(IPublisher publisher, ISubscriber subscriber, IInventoryRepository inventoryRepository)
        {
            this.publisher = publisher;
            this.subscriber = subscriber;
            this._inventoryRepository = inventoryRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(Subscribe);
            return Task.CompletedTask;
        }

        private bool Subscribe(string message, IDictionary<string, object> header)
        {
            var response = JsonConvert.DeserializeObject<OrderCreatedEvent>(message);
            try
            {
                var product =  _inventoryRepository.Get(response.ProductId).GetAwaiter().GetResult();
                if (product.AvailableQuantity < response.Quantity)
                {
                    publisher.Publish(JsonConvert.SerializeObject(
                        new OrderUpdateEvent { OrderID = response.OrderId ,IsSuccess=false}
                        ), "inventory.response", null);
                }
                else
                {
                    _inventoryRepository.Update(response.ProductId, response.Quantity).GetAwaiter().GetResult();
                    publisher.Publish(JsonConvert.SerializeObject(
                    new OrderUpdateEvent { OrderID = response.OrderId, IsSuccess = true }
                    ), "inventory.response", null);
                }


            }
            catch (Exception)
            {
                publisher.Publish(JsonConvert.SerializeObject(
                    new OrderUpdateEvent { OrderID = response.OrderId,IsSuccess=false }
                    ), "inventory.response", null);
            }

            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    

    
}
