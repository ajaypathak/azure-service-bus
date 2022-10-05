using Azure.Messaging.ServiceBus;

namespace ServiceBus.Helper
{
    public abstract class ServiceBusBaseClass
    {

        public readonly string QueueName;
        public readonly ServiceBusClient ServiceBusClient;

        public ServiceBusBaseClass(string connectionString)
        {
            ServiceBusClient = new ServiceBusClient(connectionString);
           
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