using PaymentService.Services;

namespace PaymentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ����� CORS ������ �������� �� �� ����
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin() // ������ ��� ����
                          .AllowAnyMethod() // ������ ��� ����� (GET, POST� ���)
                          .AllowAnyHeader(); // ������ ��� ��� (headers)
                });
            });

            // ����� ����� gRPC
            builder.Services.AddGrpc();
            builder.Logging.AddConsole();

            var app = builder.Build();

            // ������� CORS ��� �� Middleware ���
            app.UseCors("AllowAll");

            // ����� ����� gRPC
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<PaymentServices>();

            // ����� ���� ������ �������� ��� API
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            // ����� �������
            app.Run();
        }
    }
}
