using Order.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private static List<OrderDetail> _orderDetail = new List<OrderDetail> { new OrderDetail { OrderId = 1, Name = "Order :" + Guid.NewGuid(), ProductId = 3, Quantity = 10, User = "TD" ,Status="Order Cancelled"} };
        public Task<int> Create(OrderDetail orderDetail)
        {
            int maxID=_orderDetail.Select(x => x.OrderId).Max();
            orderDetail.OrderId = maxID + 1;
            orderDetail.Name = "Order :" + Guid.NewGuid();
            orderDetail.Status = "Order Pending";
            _orderDetail.Add(orderDetail);
            return Task.FromResult(orderDetail.OrderId);
        }

        public bool Delete(int orderId)
        {
           var order= _orderDetail.Where(x => x.OrderId == orderId).FirstOrDefault();
           return _orderDetail.Remove(order);
        }

        public bool Update(int orderId,string status)
        {
            var order = _orderDetail.Where(x => x.OrderId == orderId).FirstOrDefault();
            order.Status = status;
            return true;
            //return _orderDetail.Remove(order);
        }

        public List<OrderDetail> Get()
        {
            return _orderDetail;
        }
    }
}
