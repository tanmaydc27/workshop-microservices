using Discount.Api.Entities;
using Discount.Api.Protos;
using Discount.Api.Repositories.Interfaces;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository=repository;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = new Coupon { Amount = request.Coupon.Amount, Description = request.Coupon.Description, ProductName = request.Coupon.ProductName };
            await _repository.CreateDiscount(coupon);
            request.Coupon.Id = coupon.Id;
            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted=await _repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };

            return response;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }

            var couponModel = new CouponModel { Id = coupon.Id, ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description };
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new Coupon
            {
                Id = request.Coupon.Id,
                Description = request.Coupon.Description,
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount
            };

            await _repository.UpdateDiscount(coupon);

            return request.Coupon;
        }
    }
}
