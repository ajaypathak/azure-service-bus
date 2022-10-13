using Azure.Messaging.ServiceBus;

namespace ServiceBus.Helper
{
    public class Receiver : ServiceBusBaseClass
    {
        private readonly ServiceBusProcessor serviceBusProcessor;

        public Receiver(string connectionString, string queueName) : base(connectionString)
        {
            serviceBusProcessor = ServiceBusClient.CreateProcessor(queueName);
        }
        public async Task ReceiveTextMessage()
        {
            serviceBusProcessor.ProcessMessageAsync += ServiceBusProcessor_ProcessMessageAsync;
            serviceBusProcessor.ProcessErrorAsync += ServiceBusProcessor_ProcessErrorAsync;

            // start processing 
            await serviceBusProcessor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            Console.WriteLine("\nStopping the receiver...");
            await serviceBusProcessor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");

            Console.ReadLine();
        }

        private Task ServiceBusProcessor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task ServiceBusProcessor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            string result = System.Text.Encoding.UTF8.GetString(arg.Message.Body);
            WriteLine(result, ConsoleColor.DarkCyan);

            await arg.CompleteMessageAsync(arg.Message);
        }




    }
}
