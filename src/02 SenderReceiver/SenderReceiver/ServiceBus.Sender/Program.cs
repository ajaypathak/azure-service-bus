// See https://aka.ms/new-console-template for more information
using ServiceBus.Helper;

Console.WriteLine("Hello, World!");
Sender Sender = new Sender();
for (int i = 0; i < 100; i++)
{
    await Sender.SendTextMessage("Hello World" + i);

}
