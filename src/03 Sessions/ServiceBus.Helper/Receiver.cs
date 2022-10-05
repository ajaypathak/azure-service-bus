using Azure.Messaging.ServiceBus;

namespace ServiceBus.Helper
{
    public class Receiver : ServiceBusBaseClass
    {
        private readonly ServiceBusSessionProcessor processor;

        public Receiver(string connectionString, string queueName) : base(connectionString)
        {
            ServiceBusSessionProcessorOptions options = new ServiceBusSessionProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxAutoLockRenewalDuration = TimeSpan.FromMilliseconds(100),
                MaxConcurrentSessions = 2,
                MaxConcurrentCallsPerSession = 2
            };

            processor = ServiceBusClient.CreateSessionProcessor(queueName, options);
        }
        public async Task ReceiveTextMessage()
        {
            // add handler to process messages

            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            Console.WriteLine("\nStopping the receiver...");
            await processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");

        }

        private async Task Processor_ProcessMessageAsync(ProcessSessionMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();        
            Console.WriteLine($"Received: Body {body}, SessionId {args.Message.SessionId}");
            await args.CompleteMessageAsync(args.Message);
        }



        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
