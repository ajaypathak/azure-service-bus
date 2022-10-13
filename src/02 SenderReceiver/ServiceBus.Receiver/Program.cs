// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;


Receiver receiver = new Receiver(AzureAccountDetails.connectionString, AzureAccountDetails.queueName);

await receiver.ReceiveTextMessage();
