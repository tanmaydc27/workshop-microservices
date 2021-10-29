using Microsoft.AspNetCore.Mvc;
using System;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public IActionResult Post([FromBody] PaymentRequest request)
        {
            throw new Exception();
            //return new OkResult();
        }

        public class PaymentRequest
        {
            public double Amount { get; set; }
        }
    }
}
