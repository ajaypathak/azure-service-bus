

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
            SenderConnectionString = "Endpoint=sb://ajaydemo.servicebus.windows.net/;SharedAccessKeyName=Sender;" +
                "SharedAccessKey=PWSfqny83QpriBLn/om7V0Go0qgpZdgr92lcc2QYWyg=";
            ListenConnectionString = "Endpoint=sb://ajaydemo.servicebus.windows.net/;SharedAccessKeyName=Listen;" +
                "SharedAccessKey=FqsVHRAVjYFisuO+4mlDWsAsuMR6sr2HnI4gjccK1lk=";
            QueueName = "SenderReceiver";
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