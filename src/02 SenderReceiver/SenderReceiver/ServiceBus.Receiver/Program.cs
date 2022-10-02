// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;

Console.WriteLine("Hello, World!");
Receiver receiver = new Receiver();
await receiver.ReceiveTextMessage();