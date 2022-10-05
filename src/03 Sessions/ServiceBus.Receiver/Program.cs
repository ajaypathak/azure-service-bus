// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;

Console.WriteLine("Hello, World!");
string connectionString = "Endpoint=sb://ajaydemo.servicebus.windows.net/;SharedAccessKeyName=sessionlisten;SharedAccessKey=fY5QRbp9iLCCkU2n5jiJ/E+PZ3QFm+7h0JkTHX5Rr1o=;EntityPath=sessionqueue";
string queueName = "sessionqueue";
Receiver receiver = new Receiver(connectionString, queueName);

await receiver.ReceiveTextMessage();
