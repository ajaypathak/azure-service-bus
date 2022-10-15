using Azure.Messaging.ServiceBus;

using System.Text.Json;

namespace ServiceBus.Helper
{
    public class Sender : ServiceBusBaseClass
    {
        public readonly ServiceBusSender serviceBusSender;

        public Sender(string connectionString, string queueName) : base(connectionString)
        {
            serviceBusSender = ServiceBusClient.CreateSender(queueName);

        }

        public async Task SendMessageAsync(Product product)
        {
            if (product == null)
            {
                return;
            }

            string body = JsonSerializer.Serialize<Product>(product);
            var message = new ServiceBusMessage(body)
            {
                MessageId = product.Id
            };

            await serviceBusSender.SendMessageAsync(message);
            Console.WriteLine($"Message {product}  published to the queue.");
        }


    }
}