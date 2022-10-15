// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

using ServiceBus.Config;
using ServiceBus.Helper;


AzureAccountDetails azureAccountDetails = new AzureAccountDetails();

ProductReader productReader = new ProductReader();
var products = productReader.GetProducts();
int counter = 0;
int position = 0;
Random random = new Random(DateTime.Now.Millisecond);
Sender sender = new Sender(azureAccountDetails.ConnectionString, azureAccountDetails.QueueName);
while (counter < 10)
{
    await sender.SendMessageAsync(products[position]);
    position = random.Next(0, products.Count);
    counter++;
    Thread.Sleep(100);
}