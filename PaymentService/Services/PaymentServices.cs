using Grpc.Core;
using Microsoft.Extensions.Logging;
using PaymentServiceGrpc.Protos;
using static PaymentServiceGrpc.Protos.Payment;

namespace PaymentService.Services
{
    public class PaymentService : PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override Task<PaymentResponse> ProcessPayment(PaymentRequest request, ServerCallContext context)
        {
            if (request.Amount <= 0)
            {
                _logger.LogWarning("Invalid payment amount: {Amount}", request.Amount);
                return Task.FromResult(new PaymentResponse
                {
                    Success = false,
                    Message = "Invalid amount"
                });
            }

            _logger.LogInformation("Payment processed successfully for Order ID: {OrderId} with amount: {Amount}", request.OrderId, request.Amount);
            return Task.FromResult(new PaymentResponse
            {
                Success = true,
                Message = "Payment processed successfully"
            });
        }
    }
}
