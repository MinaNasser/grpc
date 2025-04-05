// Services/OrderGrpcService.cs

using Grpc.Core;
using Order;
using InventoryService.Protos;
using Lab1.Repositories;
using System.Reflection;
using Lab1.Models;

public class OrderGrpcService : OrderService.OrderServiceBase
{
    private readonly ProductRepository _productRepository = new();
    private readonly OrderRepository _orderRepository = new();
    private readonly Inventory.InventoryClient _inventoryClient;

    public OrderGrpcService(Inventory.InventoryClient inventoryClient)
    {
        _inventoryClient = inventoryClient;
    }

    public override async Task<OrderResponse> CreateOrder(OrderRequest request, ServerCallContext context)
    {
        double totalAmount = 0;

        foreach (var item in request.Items)
        {
            var stockResponse = await _inventoryClient.CheckStockAsync(new StockRequest
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });

            if (!stockResponse.IsAvailable)
            {
                return new OrderResponse
                {
                    Message = $"Product with ID {item.ProductId} is out of stock.",
                    Success = false
                };
            }

            var product = _productRepository.GetById(item.ProductId);
            if (product == null)
            {
                return new OrderResponse
                {
                    Message = $"Product with ID {item.ProductId} does not exist.",
                    Success = false
                };
            }

            totalAmount += product.Price * item.Quantity;

            var updated = _productRepository.UpdateStock(item.ProductId, item.Quantity);
            if (!updated)
            {
                return new OrderResponse
                {
                    Message = $"Failed to update stock for product ID {item.ProductId}",
                    Success = false
                };
            }
        }

        var order = new Orderent
        {
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            CustomerPhone = request.CustomerPhone,
            PaymentMethod = request.PaymentMethod,
            TotalAmount = totalAmount,
            CreatedAt = DateTime.UtcNow,
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        _orderRepository.Add(order);

        return new OrderResponse
        {
            Message = "Order Created Successfully",
            Success = true,
            TotalAmount = totalAmount
        };
    }

}
