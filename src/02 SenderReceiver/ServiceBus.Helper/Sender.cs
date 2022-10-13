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

        public async Task SendTextMessageAsync(string text)
        {
            for (int i = 0; i < 100; i++)
            {

                string body = text + i + Delimiter +
                    " Process Id : " + Process.GetCurrentProcess().Id + Delimiter +
                    " TimeStamp " + DateTime.Now.ToString("yyyy-mm-dd-HH-m-ss-ff") + Delimiter;
                var message = new ServiceBusMessage(body);

                await serviceBusSender.SendMessageAsync(message);
                Console.WriteLine($"Message {body}  published to the queue.");

            }


        }

        public async Task SendTextMessageAsBatchAsync(string text)
        {
            List<ServiceBusMessage> messages = new List<ServiceBusMessage>();
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    string body = text + j + Delimiter + i + Delimiter +
                        " Process Id : " + Process.GetCurrentProcess().Id + Delimiter +
                        " TimeStamp " + DateTime.Now.ToString("yyyy-mm-dd-HH-m-ss-ff") + Delimiter;
                    var message = new ServiceBusMessage(body);
                    messages.Add(message);

                }
                Console.WriteLine($"A Total {messages.Count}, messages published to {AzureAccountDetails.queueName}");
                await serviceBusSender.SendMessagesAsync(messages);
            }



        }
    }
}