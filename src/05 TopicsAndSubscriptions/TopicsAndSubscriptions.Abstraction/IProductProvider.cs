using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Abstraction
{
    public interface IProductProvider
    {
        List<Product> GetProducts();
    }
}