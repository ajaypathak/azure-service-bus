using Microsoft.Extensions.Configuration;

namespace ServiceBus.Config
{
    public class AzureAccountDetails
    {
        public string ConnectionString;
        public string QueueName;

        public AzureAccountDetails()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<AzureAccountDetails>()
            .Build();



             ConnectionString = config["connectionString"];
             QueueName = config["queueName"];


        }
    }
}