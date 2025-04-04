using Grpc.Core;
using InventoryService.Protos;
using Microsoft.Extensions.Logging;  

namespace InventoryService.Services
{
    public class InventoryService : Inventory.InventoryBase
    {
        private readonly Dictionary<int, int> _stock = new()
        {
            { 1, 10 },
            { 2, 20 },
            { 3, 30 }
        };

        private readonly ILogger<InventoryService> _logger; 

        public InventoryService(ILogger<InventoryService> logger)
        {
            _logger = logger;
        }

        public override Task<StockResponse> CheckStock(StockRequest request, ServerCallContext context)
        {
            // تسجيل بداية العملية
            _logger.LogInformation("Checking stock for ProductId: {ProductId} and Quantity: {Quantity}", request.ProductId, request.Quantity);

            bool available = _stock.TryGetValue(request.ProductId, out int availableQuantity)
                             && availableQuantity >= request.Quantity;

            if (available)
            {
                
                _logger.LogInformation("Product {ProductId} is available with {AvailableQuantity} in stock.", request.ProductId, availableQuantity);
            }
            else
            {
                
                _logger.LogWarning("Product {ProductId} is out of stock or requested quantity exceeds available stock.", request.ProductId);
            }

            return Task.FromResult(new StockResponse
            {
                IsAvailable = available,
                AvailableQuantity = available ? availableQuantity : 0
            });
        }
    }
}
