using Lab1.Models;

namespace Lab1.Repositories
{
    public class OrderRepository
    {
        private readonly List<Orderent> _orders = new List<Orderent>();

        public void Add(Orderent order)
        {
            _orders.Add(order);
        }

        public List<Orderent> GetAll()
        {
            return _orders;
        }
    }
}
