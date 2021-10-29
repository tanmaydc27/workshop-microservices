using Discount.Api.Entities;
using Discount.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private static IList<Coupon> _coupons= new List<Coupon> { new Coupon { Id = 1, ProductName = "GalaxySamsung", Amount = 20000, Description = "Coupon valid for GalaxySamsung" } };

        public DiscountRepository()
        {
        }
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            int maxID = _coupons.Select(x => x.Id).Max();
            coupon.Id = maxID + 1;
            _coupons.Add(coupon);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            Coupon coupon = _coupons.Where(x => x.ProductName == productName).FirstOrDefault();
            return Task.FromResult(_coupons.Remove(coupon));
        }

        public Task<Coupon> GetDiscount(string productName)
        {
            return Task.FromResult(_coupons.Where(x => x.ProductName == productName).FirstOrDefault());
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            Coupon existingCoupon = _coupons.Where(x => x.Id == coupon.Id).FirstOrDefault();
            existingCoupon.ProductName = coupon.ProductName;
            existingCoupon.Amount = coupon.Amount;
            existingCoupon.Description = coupon.Description;

            return Task.FromResult(true);
        }
    }
}
