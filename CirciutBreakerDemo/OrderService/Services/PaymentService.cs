using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentResponse> MakePayment(PaymentRequest payment)
        {
            // Some logic
            // Call to payment service
            var content = new StringContent(JsonSerializer.Serialize(payment), System.Text.Encoding.Default, "application/json");
            var response = await _httpClient.PostAsync("payments", content);

            PaymentResponse result = null;
            if (response.IsSuccessStatusCode)
                result = PaymentResponse.CreateSuccessResponse();
            else
                result = PaymentResponse.CreateFailedResponse();

            return result;
        }
    }

    public class PaymentResponse
    {
        public string Status { get; } = "Success";

        private PaymentResponse() { }

        private PaymentResponse(string status)
        {
            Status = status;
        }

        internal static PaymentResponse CreateSuccessResponse()
        {
            return new PaymentResponse();
        }

        internal static PaymentResponse CreateFailedResponse()
        {
            return new PaymentResponse("Failed");
        }
    }

    public class PaymentRequest
    {
        public double Amount { get; set; }
    }
}
