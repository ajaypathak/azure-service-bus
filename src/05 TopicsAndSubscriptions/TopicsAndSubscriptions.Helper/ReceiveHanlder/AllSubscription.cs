using Azure.Messaging.ServiceBus;

using Serilog;

using System.Text;

using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Helper.ReceiveHanlder
{
    public class ProcessSubscriptions : ServiceBusBaseClass
    {
        private readonly ServiceBusSender serviceBusSender;

        private readonly ServiceBusProcessor processor;
        private readonly string _subscriptionName;
        public ProcessSubscriptions(string connectionString, string topicName, string subscriptionName) : base(connectionString)
        {
            processor = ServiceBusClient.CreateProcessor(topicName, subscriptionName);
            _subscriptionName = subscriptionName;
            serviceBusSender = ServiceBusClient.CreateSender(connectionString);

        }
        public async Task ReceiveMessagesAsyns()
        {

            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
            // start processing 
            await processor.StartProcessingAsync();
            Log.Information($"Subscription Processor started for {_subscriptionName} subscription");
            //Console.WriteLine("Wait for a minute and then press any key to end the processing");
            //Console.ReadKey();

            // stop processing 
            //Console.WriteLine("\nStopping the receiver...");
            //  await processor.StopProcessingAsync();
            //Console.WriteLine("Stopped receiving messages");

            //Console.ReadLine();
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {


            Console.WriteLine(arg.Exception.ToString());
            processor.StopProcessingAsync().GetAwaiter();
            Log.Information($"Subscription Processor Stopped for {_subscriptionName} subscription");
            return Task.CompletedTask;
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {

            string result = Encoding.UTF8.GetString(arg.Message.Body);
            var order = System.Text.Json.JsonSerializer.Deserialize<Order>(result);
            var properties = arg.Message.ApplicationProperties;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in properties)
            {
                stringBuilder.Append("Key:").Append(item.Key).Append("\t").Append("Value:").Append(item.Value).Append("\n");

            }
            Log.Information($"Processed Message for {order.State} having {stringBuilder.ToString()} Application Properties");
            WriteLine(result, ConsoleColor.DarkCyan);

            await arg.CompleteMessageAsync(arg.Message);



        }
    }
}
