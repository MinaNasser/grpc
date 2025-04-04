using Lab1.Models;

namespace Lab1.Repositories
{
    public class ProductRepository
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 15000, Stock = 10 },
            new Product { Id = 2, Name = "Smartphone", Price = 8000, Stock = 20 },
            new Product { Id = 3, Name = "Headphones", Price = 1200, Stock = 30 }
        };

        public List<Product> GetAll() => _products;
        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
        public void Add(Product product)
        {
            _products.Add(product);
        }
    }
}