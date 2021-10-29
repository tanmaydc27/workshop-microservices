using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Model
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
    }
}
