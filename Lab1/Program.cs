namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ����� ����� �������
            builder.Services.AddControllers();

            // ����� ������� Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ����� gRPC Clients
            builder.Services.AddGrpcClient<InventoryService.Protos.Inventory.InventoryClient>(options =>
            {
                options.Address = new Uri("https://localhost:7055");
            });

            builder.Services.AddGrpcClient<PaymentServiceGrpc.Protos.Payment.PaymentClient>(options =>
            {
                options.Address = new Uri("https://localhost:7153");
            });

            // ����� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // ����� ��� HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ����� CORS ��� �������
            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
