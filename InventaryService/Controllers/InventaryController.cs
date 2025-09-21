using Microsoft.AspNetCore.Mvc;

namespace InventaryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventaryController : ControllerBase
    {
      

        private readonly ILogger<InventaryController> _logger;

        public InventaryController(ILogger<InventaryController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "ReserveInventary")]
        public IActionResult ReserveInventary()
        {
            return Ok(new { Message = "Inventary Reserved" });
        }

        //Compensation Action
        [HttpPost( Name = "CleanInventory")]
    }
}
