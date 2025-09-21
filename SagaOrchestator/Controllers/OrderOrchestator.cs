using Microsoft.AspNetCore.Mvc;

namespace SagaOrchestator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderOrchestator : ControllerBase
    {

        private readonly Stack<Func<Task>> _compensations = new();
        private readonly ILogger<OrderOrchestator> _logger;

        public OrderOrchestator(ILogger<OrderOrchestator> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "executeOrder")]
        public async Task<IActionResult> ExecuteOrder()
        {
            try
            {
                var pedidoId = await CreateOrderAsync();
                _compensations.Push(() => CancelOrderAsync(pedidoId));

                var inventarioOK = await ReserveInventary(pedidoId);
                if (!inventarioOK) throw new Exception("Error al reservar inventario");
                _compensations.Push(() => CleanInventory(pedidoId));

                var pagoOK = await MakePayment(pedidoId);
                if (!pagoOK) throw new Exception("Error al procesar pago");
                _compensations.Push(() => RefundPayment(pedidoId));

                return Ok( "Process completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Saga fallida: {ex.Message}");
                while (_compensations.Count > 0)
                {
                    var compensar = _compensations.Pop();
                    await compensar();
                }
                return StatusCode(500, "Error procesing order, all actions where reverted");
            }
        }

        // Simulación de servicios
        private Task<string> CreateOrderAsync()
        {
            //Call service  OrderService.Controller.CreateOrder
            return Task.FromResult("PEDIDO123");
        }

        private Task CancelOrderAsync(string pedidoId)
        {
            //Call service  OrderService.Controller.CancelOrder
            return Task.CompletedTask;
        }

        private Task<bool> ReserveInventary(string pedidoId)
        {
            //Call service  OrderService.Controller.CreateOrder
            return Task.FromResult(true);
        }

        private Task CleanInventory(string pedidoId)
        {
            Console.WriteLine($"Inventario liberado para pedido {pedidoId}.");
            return Task.CompletedTask;
        }

        private Task<bool> MakePayment(string pedidoId)
        {
            Console.WriteLine("Pago procesado.");
            return Task.FromResult(true);
        }

        private Task RefundPayment(string pedidoId)
        {
            Console.WriteLine($"Pago reembolsado para pedido {pedidoId}.");
            return Task.CompletedTask;
        }
    }
}
