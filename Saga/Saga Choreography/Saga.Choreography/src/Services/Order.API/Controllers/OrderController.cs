using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Event;
using EventBus.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Order.API.Model;
using Order.API.Repository;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublisher publisher;


        public OrderController(IOrderRepository orderRepository, IPublisher publisher)
        {
            this._orderRepository = orderRepository;
            this.publisher = publisher;
        }

        [HttpGet]
        public IEnumerable<OrderDetail> Get()
        {
            return _orderRepository.Get();
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] OrderDetail orderDetail)
        {
            var id = await _orderRepository.Create(orderDetail);
            //Publish Event

            publisher.Publish(JsonConvert.SerializeObject(new OrderCreatedEvent
            {
                OrderId = id,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }), "order.created", null);

            //
            return Ok(new { Id=id,Status="Order Pending"});
        }

    }
}
