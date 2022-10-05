// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;

string connectionString = "Endpoint=sb://ajaydemo.servicebus.windows.net/;SharedAccessKeyName=sessionsend;SharedAccessKey=BrA3wDkUu9VuRtl9Zo9lAnbHmnMDzIOGKRV4Dgtbfy4=;EntityPath=sessionqueue";
string queueName = "sessionqueue";
Sender sender= new Sender(connectionString,queueName);

await sender.SendTextMessage("Message");
Console.ReadLine();
