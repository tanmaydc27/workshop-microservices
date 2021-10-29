using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByUserName(string userName);
        Task<bool> DeleteAsync(int orderID);
        Task<bool> UpdateAsync(Order order);

        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
    }
}
