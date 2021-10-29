using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository :IOrderRepository
    {
        private static List<Order> _orders= new List<Order> { new Order { Id = 1, UserName = "TD", CVV = "679" } };
        public OrderRepository()
        {
        }
        public Task<Order> AddAsync(Order order)
        {
            int maxID = _orders.Select(x => x.Id).Max();
            order.Id = maxID + 1;
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<bool> DeleteAsync(int orderID)
        {
            Order order = _orders.Where(x => x.Id == orderID).FirstOrDefault();
            _orders.Remove(order);
            return Task.FromResult(true);
        }

        public Task<List<Order>> GetOrdersByUserName(string userName)
        {
            var orderList =  _orders
                                .Where(o => o.UserName == userName).ToList();
            return Task.FromResult(orderList);
        }

        Task<bool> IOrderRepository.UpdateAsync(Order order)
        {
           Order existingOrder= _orders.Where(x => x.Id == order.Id).FirstOrDefault();
           existingOrder = order;
            return Task.FromResult(true);
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return Task.FromResult(_orders.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}
