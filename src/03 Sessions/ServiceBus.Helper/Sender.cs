using Azure.Messaging.ServiceBus;

using System.Diagnostics;

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
                string body = text + Delimiter +
                    " Process Id : " + Process.GetCurrentProcess().Id + Delimiter +
                    " TimeStamp " + DateTime.Now.ToString("yyyy-mm-dd-HH-m-ss-ff") + Delimiter +
                    " Session Id : " + v;
                var message = new ServiceBusMessage(body)
                {
                    SessionId = v.ToString()
                };
                await serviceBusSender.SendMessageAsync(message);
                Console.WriteLine($"Message {body}  published to the queue.");

            }


        }
    }
}
