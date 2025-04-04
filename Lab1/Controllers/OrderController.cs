using Grpc.Net.Client;
using InventoryService.Protos;
using Lab1.Models;
using Lab1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        private readonly Inventory.InventoryClient _inventoryClient;  

        public OrderController(Inventory.InventoryClient inventoryClient)
        {
            _productRepository = new ProductRepository();
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
    }
}
