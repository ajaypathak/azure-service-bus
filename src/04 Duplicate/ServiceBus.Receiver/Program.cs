// See https://aka.ms/new-console-template for more information
using ServiceBus.Config;
using ServiceBus.Helper;
AzureAccountDetails azureAccountDetails = new AzureAccountDetails();


Receiver receiver = new Receiver(azureAccountDetails.ConnectionString, azureAccountDetails.QueueName);

await receiver.ReceiveTextMessage();
