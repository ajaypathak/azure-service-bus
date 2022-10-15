using ServiceBus.Model;

namespace ServiceBus.Abstraction
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}