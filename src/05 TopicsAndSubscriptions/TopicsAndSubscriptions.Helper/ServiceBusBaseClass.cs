using Azure.Messaging.ServiceBus;

namespace TopicsAndSubscriptions.Helper
{
    public abstract class ServiceBusBaseClass
    {
        public readonly string TopicName;
        public readonly ServiceBusClient ServiceBusClient;
        
        protected string Delimiter = "-";

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