using ServiceBusLearning.Management;

using System;

Console.WriteLine("Hello World!");
ServiceBusHelper ServiceBusHelper = new ServiceBusHelper(getConnectionString());
await ServiceBusHelper.Create();
await ServiceBusHelper.ShowNamespace();
await ServiceBusHelper.Delete();

static string getConnectionString()
{
    return @"<Add Connection String >";
}