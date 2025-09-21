using Microsoft.AspNetCore.Mvc;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
     

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CreateOrder")]
        public IActionResult Get()
        {
          return Ok(new Order { IdOrder = 1, Name = "Order1", NumItems = 5, Cost = 100 });
        }

        //Compensation Action to cancel an order
        [HttpPost(Name = "CancelOrder")] 
        public IActionResult CancelOrder()
        {
            return Ok(new { Message = "Order Canceled" });
        }
    }
}
