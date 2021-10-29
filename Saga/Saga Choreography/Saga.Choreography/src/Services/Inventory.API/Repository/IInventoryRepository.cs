using Inventory.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repository
{
    public interface IInventoryRepository
    {
        Task <bool>Update(int productId, int quantity);
        Task <Product> Get(int productId);

    }
}
