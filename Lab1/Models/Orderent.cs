namespace Lab1.Models
{
    public class Orderent
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTime CreatedAt { get; set; }
        public double TotalAmount { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
