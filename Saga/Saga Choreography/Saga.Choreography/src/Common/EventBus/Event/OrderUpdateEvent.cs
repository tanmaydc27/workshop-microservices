using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Event
{
    public class OrderUpdateEvent
    {
        public int OrderID { get; set; }

        public bool IsSuccess { get; set; }
    }
}
