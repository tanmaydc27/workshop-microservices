using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Event
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
