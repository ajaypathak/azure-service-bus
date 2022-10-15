namespace ServiceBus.Model
{
    public class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Id { get; }
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
        public override string ToString()
        {
            return $"Product:{ProductName}\tPrice:{Price}";
        }
    }
}