// See https://aka.ms/new-console-template for more information
using ServiceBus.Config;
using ServiceBus.Helper;
using ServiceBus.Services;

AzureAccountDetails azureAccountDetails = new AzureAccountDetails();

ProductService productService = new ProductService();
var products = productService.GetProducts();
int counter = 0;
int position = 0;
Random random = new Random(DateTime.Now.Millisecond);
Sender sender = new Sender(azureAccountDetails.ConnectionString, azureAccountDetails.QueueName);
while (counter < 10)
{
    //Cause random message generation
    if (random.NextDouble() > .5)
    {
        position++;
    }
    await sender.SendMessageAsync(products[position]);
    Console.WriteLine($"Sent Product at {position}: {products[position]} ");
    counter++;
    Thread.Sleep(100);
}
Console.WriteLine("{0} total products sent reads.", counter);
Console.ReadLine();