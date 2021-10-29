using Discount.Api.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    //Wrapper for get discount method
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel>GetDiscount(string productName)
        {
            return await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
        }

    }
}
