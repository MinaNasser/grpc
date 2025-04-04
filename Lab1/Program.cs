namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // ежгщи езогогй Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpcClient<InventoryService.Protos.Inventory.InventoryClient>(options =>
            {
                options.Address = new Uri("https://localhost:7055");  
            });

            builder.Services.AddGrpcClient<PaymentServiceGrpc.Protos.Payment.PaymentClient>(options =>
            {
                options.Address = new Uri("https://localhost:7153");  
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
