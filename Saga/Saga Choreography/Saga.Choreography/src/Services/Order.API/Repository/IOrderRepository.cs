using Order.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Repository
{
    public interface IOrderRepository
    {
        List<OrderDetail> Get();
        Task<int> Create(OrderDetail orderDetail);
        bool Delete(int orderId);

        bool Update(int orderId, string status);
    }
}
