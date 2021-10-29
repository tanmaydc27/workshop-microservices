using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> MakePayment(PaymentRequest payment);
    }
}