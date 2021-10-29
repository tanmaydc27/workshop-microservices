using Inventory.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private static List<Product> _product = PopulateProduts();


        public Task<Product> Get(int productId)
        {
            var product = _product.Where(x => x.ProductID == productId).FirstOrDefault();
            return Task.FromResult(product);
        }

        public Task<bool> Update(int productId, int quantity)
        {
            var existingProd=_product.Where(x => x.ProductID == productId).FirstOrDefault();
            existingProd.AvailableQuantity = existingProd.AvailableQuantity - quantity;
            return Task.FromResult(true);
        }

        private static List<Product> PopulateProduts()
        {
            var products=new List<Product> { 
                new Product { ProductID = 1, ProductName = "Product 1", AvailableQuantity=100,  TotalQuantity=100},
                new Product { ProductID = 2, ProductName = "Product 2", AvailableQuantity=100,  TotalQuantity=100},
                new Product { ProductID = 3, ProductName = "Product 3", AvailableQuantity=100,  TotalQuantity=100},
                new Product { ProductID = 4, ProductName = "Product 4", AvailableQuantity=100,  TotalQuantity=100},
                new Product { ProductID = 5, ProductName = "Product 5", AvailableQuantity=100,  TotalQuantity=100}
            };
            return products;
        }
    }
}
