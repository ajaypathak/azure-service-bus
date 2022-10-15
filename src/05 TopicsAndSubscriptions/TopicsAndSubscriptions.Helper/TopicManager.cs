using Azure.Messaging.ServiceBus.Administration;

using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Helper
{
    public class TopicManager
    {
        ServiceBusAdministrationClient client;
        string defaultSubscriptionName = "allsubscriptions";
        private readonly IIndianStateProvider _indianStateProvider;

        public TopicManager(string connectionString, IIndianStateProvider indianStateProvider)
        {
            client = new ServiceBusAdministrationClient(connectionString);
            _indianStateProvider = indianStateProvider;
        }
        public async Task<TopicProperties> CreateTopicAsync(string topicName)
        {
            if (await client.TopicExistsAsync(topicName) == true)
            {
                await client.DeleteTopicAsync(topicName);
            }

            var topicOptions = new CreateTopicOptions(topicName)
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

            topicOptions.AuthorizationRules.Add(new SharedAccessAuthorizationRule(
                   "allClaims",
                   new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen }));

            TopicProperties createdTopic = await client.CreateTopicAsync(topicOptions);
            return createdTopic;

        }
        public async Task<List<SubscriptionProperties>> CreateSubscriptions(string topicName)
        {
            List<SubscriptionProperties> subscriptions = new List<SubscriptionProperties>();

            CreateSubscriptionOptions subscriptionOptions = getSubscriptionOptions(topicName, defaultSubscriptionName);
            SubscriptionProperties createdSubscription;

            if (await client.SubscriptionExistsAsync(topicName, subscriptionOptions.SubscriptionName) == true)
            {
                await client.DeleteSubscriptionAsync(topicName, subscriptionOptions.SubscriptionName);
            }

            createdSubscription = await client.CreateSubscriptionAsync(subscriptionOptions);
            subscriptions.Add(createdSubscription);

            List<IndianState> states = _indianStateProvider.GetAllIndianStates();

            foreach (var item in states)
            {

                subscriptionOptions = getSubscriptionOptions(topicName, item.StateCode + "-Subscription");
                if (await client.SubscriptionExistsAsync(topicName, subscriptionOptions.SubscriptionName) == true)
                {
                    await client.DeleteSubscriptionAsync(topicName, subscriptionOptions.SubscriptionName);
                }
                CreateRuleOptions createRuleOptions = new CreateRuleOptions();
                createRuleOptions.Name = item.StateCode + "-Rule";
                createRuleOptions.Filter = new SqlRuleFilter($"State='{item.StateCode}'");

                createdSubscription = await client.CreateSubscriptionAsync(subscriptionOptions, createRuleOptions);
                subscriptions.Add(createdSubscription);
            }

            return subscriptions;
        }

        private static CreateSubscriptionOptions getSubscriptionOptions(string topicName, string subscritpionName)
        {
            return new CreateSubscriptionOptions(topicName, subscritpionName)
            {
                AutoDeleteOnIdle = TimeSpan.FromDays(7),
                DefaultMessageTimeToLive = TimeSpan.FromDays(2),
                EnableBatchedOperations = true,
                UserMetadata = "some metada"
            };
        }
    }
}