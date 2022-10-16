using Azure.Messaging.ServiceBus.Administration;

namespace TopicsAndSubscriptions.Helper
{
    public class QueueManager
    {
        ServiceBusAdministrationClient client;
        public QueueManager(string connectionString)
        {
            client = new ServiceBusAdministrationClient(connectionString);

        }
        public async Task<QueueProperties> CreateQueueAsync(string queueName)
        {
            if (await client.QueueExistsAsync(queueName) == true)
            {
                await client.DeleteQueueAsync(queueName);
            }
            var queueOptions = new CreateQueueOptions(queueName)
            {
                AutoDeleteOnIdle = TimeSpan.FromDays(7),
                DefaultMessageTimeToLive = TimeSpan.FromDays(2),
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(1),               
                EnableBatchedOperations = true,
                EnablePartitioning = false,
                MaxSizeInMegabytes = 2048,
                RequiresDuplicateDetection = true,
                UserMetadata = "some metadata"
            };

            queueOptions.AuthorizationRules.Add(new SharedAccessAuthorizationRule(
                   "allClaims",
                   new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen }));

            var createdQueue = await client.CreateQueueAsync(queueOptions);
            return createdQueue;
        }
    }
}
