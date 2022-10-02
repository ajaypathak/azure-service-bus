using Microsoft.Azure.ServiceBus;

using System.Text;

namespace ServiceBus.Helper
{
    public class Sender : ServiceBusBaseClass
    {

        public Sender()
        {
            _serviceBusClient = new QueueClient(SenderConnectionString, QueueName);


        }
        public async Task SendTextMessage(string text)
        {
            var message = new Message(Encoding.UTF8.GetBytes(text));
            await _serviceBusClient.SendAsync(message);

        }


    }
}