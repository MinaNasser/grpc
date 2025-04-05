using PaymentService.Services;

namespace PaymentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ≈÷«›… CORS ··”„«Õ »«·ÿ·»«  „‰ √Ì ‰ÿ«ﬁ
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin() // «·”„«Õ »√Ì ‰ÿ«ﬁ
                          .AllowAnyMethod() // «·”„«Õ »√Ì ÿ—Ìﬁ… (GET, POST° ≈·Œ)
                          .AllowAnyHeader(); // «·”„«Õ »√Ì —√” (headers)
                });
            });

            // ≈÷«›… Œœ„«  gRPC
            builder.Services.AddGrpc();
            builder.Logging.AddConsole();

            var app = builder.Build();

            // «” Œœ«„ CORS ﬁ»· √Ì Middleware ¬Œ—
            app.UseCors("AllowAll");

            //  ﬂÊÌ‰ Œœ„«  gRPC
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<PaymentServices>();

            //  ﬂÊÌ‰ ‰ﬁÿ… «·Ê’Ê· «·—∆Ì”Ì… ··‹ API
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            //  ‘€Ì· «· ÿ»Ìﬁ
            app.Run();
        }
    }
}
