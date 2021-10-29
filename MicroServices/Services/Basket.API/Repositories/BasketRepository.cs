using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private static IList<ShoppingCart> _shoppingCarts=new List<ShoppingCart> { new ShoppingCart { UserName = "TD", Items = new List<ShoppingCartItem> { new ShoppingCartItem { Color = "Red", Price = 150000, ProductId = "GalaxySamsung", ProductName = "GalaxySamsung", Quantity = 2 } } } };
        public BasketRepository()
        {
          
        }
        public Task DeleteBasket(string userName)
        {
            ShoppingCart cart = _shoppingCarts.Where(x => x.UserName == userName).FirstOrDefault();
            return Task.FromResult(_shoppingCarts.Remove(cart));
        }

        public Task<ShoppingCart> GetBasket(string userName)
        {
            return Task.FromResult(_shoppingCarts.Where(x => x.UserName == userName).FirstOrDefault());
        }

        public Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            ShoppingCart existingCart = _shoppingCarts.Where(x => x.UserName == basket.UserName).FirstOrDefault();
            existingCart.Items = basket.Items;
            return Task.FromResult(existingCart);
        }
    }
}
