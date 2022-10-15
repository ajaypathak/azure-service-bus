using Azure.Messaging.ServiceBus;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Helper
{
    public class TopicSender:ServiceBusBaseClass
    {
        public readonly ServiceBusSender serviceBusSender;
        public TopicSender(string topicName,string connectionString):base(connectionString)
        {
            serviceBusSender = ServiceBusClient.CreateSender(topicName);

        }
        public async Task SendMessageAsync(Order order)
        {
            if (order == null)
            {
                return;
            }

            string body = JsonSerializer.Serialize<Order>(order);
            var message = new ServiceBusMessage(body);
            message.ApplicationProperties.Add("State", order.State);


            await serviceBusSender.SendMessageAsync(message);
            Console.WriteLine($"Message {order}  published to the queue.");
        }
    }
}
