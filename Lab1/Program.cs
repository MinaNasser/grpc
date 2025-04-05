using Microsoft.AspNetCore.Server.Kestrel.Core;
using Order;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ≈÷«›… Œœ„«  «·Ã·”« 
            builder.Services.AddControllers();

            // ≈÷«›… ≈⁄œ«œ«  Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ≈÷«›… gRPC Clients
            builder.Services.AddGrpcClient<InventoryService.Protos.Inventory.InventoryClient>(options =>
            {
                options.Address = new Uri("https://localhost:7055");
            });

            builder.Services.AddGrpcClient<PaymentServiceGrpc.Protos.Payment.PaymentClient>(options =>
            {
                options.Address = new Uri("https://localhost:7153");
            });

            // ≈÷«›… CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(7160, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });
            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    options.ListenLocalhost(7161, listenOptions =>
            //    {
            //        listenOptions.Protocols = HttpProtocols.Http2;
            //    });
            //});
            var app = builder.Build();

            //  ﬂÊÌ‰ «·‹ HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ≈÷«›… CORS ≈·Ï «· ÿ»Ìﬁ
            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseGrpcWeb();  //  √ﬂœ „‰ √‰ Â–Â «·”ÿ— »⁄œ UseRouting()

            //  „ﬂÌ‰ gRPC Services
            app.MapGrpcService<OrderGrpcService>().EnableGrpcWeb();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
