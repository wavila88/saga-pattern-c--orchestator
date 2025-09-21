using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        Random rnd = new Random();

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "MakePayment")]
        public IActionResult Payment()
        {
            IActionResult result;
            int numError=rnd.Next(0, 3);

            if (numError == 3)
            {
                result = StatusCode(500, "Internal server Error");
            }
            else
            {
                result = Ok(new { Message = "Payment Made" });
            }

            return result;
        }

        //Compensation Action refund payment
        [HttpPost(Name = "RefundPayment")]           
        public IActionResult RefundPayment()
        {
            return Ok(new { Message = "Payment Refunded" });
        }
    }
}
