using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Services
{
    public class ProductService : IProductProvider
    {
        

        public List<Product> GetProducts()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "files", "products.json");
            List<Product> products = new List<Product>();
            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath);
                products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(text);
            }
            return products;
        }
    }
}