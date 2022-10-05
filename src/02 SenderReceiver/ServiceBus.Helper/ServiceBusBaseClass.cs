

using Microsoft.Azure.ServiceBus;

namespace ServiceBus.Helper
{
    public abstract class ServiceBusBaseClass
    {
        public readonly string SenderConnectionString;
        public readonly string ListenConnectionString;

        public readonly string QueueName;
        protected QueueClient _serviceBusClient;

        public ServiceBusBaseClass()
        {
            SenderConnectionString = "";
            ListenConnectionString = "";
            QueueName = "";
        }
        public virtual string MessageId()
        {
            var messageId = Guid.NewGuid().ToString();
            return messageId;
        }
        protected void WriteLine(string text, ConsoleColor color)
        {
            var tempColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = tempColor;
        }

    }
}