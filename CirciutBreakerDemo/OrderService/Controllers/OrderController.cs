using Microsoft.AspNetCore.Mvc;
using OrderService.Services;
using Polly.CircuitBreaker;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public OrderController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDto order)
        {
            try
            {
                var response = await _paymentService.MakePayment(new PaymentRequest { Amount = 20 });
                if (response.Status == "Success")
                    return new OkResult();
                else
                    return new OkObjectResult("Unable to make payment");
            }
            catch(BrokenCircuitException)
            {
                return new StatusCodeResult(500);
            }
        }
    }

    public class OrderDto
    {
        public string Name { get; set; }
    }
}
