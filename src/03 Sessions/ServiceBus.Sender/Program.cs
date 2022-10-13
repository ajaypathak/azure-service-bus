// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;

Sender sender = new Sender(AzureAccountDetails.connectionString, AzureAccountDetails.queueName);

await sender.SendTextMessage("Message");
Console.ReadLine();
