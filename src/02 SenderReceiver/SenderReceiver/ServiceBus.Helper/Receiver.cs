using Microsoft.Azure.ServiceBus;

using System.Collections;
using System.Text;

namespace ServiceBus.Helper
{
    public class Receiver : ServiceBusBaseClass
    {

        public Receiver()
        {
            _serviceBusClient = new QueueClient(ListenConnectionString, QueueName);


        }
        public async Task ReceiveTextMessage()
        {
            var options = new MessageHandlerOptions(ErrorHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 10,
                MaxAutoRenewDuration = TimeSpan.FromSeconds(30)
            };

            

            _serviceBusClient.RegisterMessageHandler(handleMessage, options);



            Console.ReadLine();
        }

        private async Task handleMessage(Message message, CancellationToken cancellationToken)
        {
            string result = System.Text.Encoding.UTF8.GetString(message.Body);
            WriteLine(result, ConsoleColor.DarkCyan);

            await _serviceBusClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ErrorHandler(ExceptionReceivedEventArgs arg)
        {
            await Task.CompletedTask;
        }


    }
}
