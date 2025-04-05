using Grpc.Net.Client;
using InventoryService.Protos;
using Lab1.Models;
using Lab1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lab1.Models;
namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly Inventory.InventoryClient _inventoryClient;

        public OrderController(Inventory.InventoryClient inventoryClient)
        {
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _inventoryClient = inventoryClient;
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var stockRequest = new StockRequest
            {
                ProductId = product.Id,
                Quantity = 1
            };

            var stockResponse = await _inventoryClient.CheckStockAsync(stockRequest);
            if (!stockResponse.IsAvailable)
            {
                return BadRequest(new { Message = "Product is out of stock!" });
            }

            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Orderent order)
        {
            double totalAmount = 0;
            foreach (var item in order.Items)
            {
                var stockRequest = new StockRequest
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var stockResponse = await _inventoryClient.CheckStockAsync(stockRequest);
                if (!stockResponse.IsAvailable)
                {
                    return BadRequest(new { Message = $"Product with ID {item.ProductId} is out of stock!" });
                }

                var product = _productRepository.GetById(item.ProductId);
                if (product != null)
                {
                    totalAmount += product.Price * item.Quantity;

                    var isUpdated = _productRepository.UpdateStock(item.ProductId, item.Quantity);
                    if (!isUpdated)
                    {
                        return BadRequest(new { Message = $"Failed to update stock for product {item.ProductId}" });
                    }
                }
            }

            order.TotalAmount = totalAmount;
            order.CreatedAt = DateTime.UtcNow;

            _orderRepository.Add(order);

            return CreatedAtAction(nameof(GetProducts), new { id = order.Id }, order);
        }

        //public async Task<IActionResult> CreateOrder([FromBody] Order order)
        //{
        //    double totalAmount = 0;
        //    foreach (var item in order.Items)
        //    {
        //        var stockRequest = new StockRequest
        //        {
        //            ProductId = item.ProductId,
        //            Quantity = item.Quantity
        //        };

        //        var stockResponse = await _inventoryClient.CheckStockAsync(stockRequest);
        //        if (!stockResponse.IsAvailable)
        //        {
        //            return BadRequest(new { Message = $"Product with ID {item.ProductId} is out of stock!" });
        //        }
        //        var product = _productRepository.GetById(item.ProductId);
        //        if (product != null)
        //        {
        //            totalAmount += product.Price * item.Quantity;

        //            var isUpdated = _productRepository.UpdateStock(item.ProductId, item.Quantity);
        //            if (!isUpdated)
        //            {
        //                return BadRequest(new { Message = $"Failed to update stock for product {item.ProductId}" });
        //            }
        //        }
        //    }
        //    order.TotalAmount = totalAmount;
        //    order.CreatedAt = DateTime.UtcNow;

        //    _orderRepository.Add(order);

        //    return CreatedAtAction(nameof(GetProducts), new { id = order.Id }, order);
        //}
    }
}
