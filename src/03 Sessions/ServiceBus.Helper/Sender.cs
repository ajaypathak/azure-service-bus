using Azure.Messaging.ServiceBus;

namespace ServiceBus.Helper
{
    public class Sender : ServiceBusBaseClass
    {
        public readonly ServiceBusSender serviceBusSender;

        public Sender(string connectionString, string queueName) : base(connectionString)
        {
            serviceBusSender = ServiceBusClient.CreateSender(queueName);

        }

        public async Task SendTextMessage(string text)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int number = rnd.Next(1, 220000);
                int v = number % 22;
                string body = text + Delimiter + i + Delimiter + v;
                var message = new ServiceBusMessage(body);
             
                message.SessionId = v.ToString();
                await serviceBusSender.SendMessageAsync(message);
                Console.WriteLine($"Message {body}  published to the queue.");

            }


        }
    }
}
