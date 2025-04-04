using Lab1.Models;

namespace Lab1.Repositories
{
    public class OrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return _orders;
        }
    }
}
