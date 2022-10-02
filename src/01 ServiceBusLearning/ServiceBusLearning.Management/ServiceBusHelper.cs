
using Microsoft.Azure.ServiceBus.Management;

using System.Text;

namespace ServiceBusLearning.Management
{
    public class ServiceBusHelper
    {
        private readonly ManagementClient _serviceBusClient;
        private readonly string _queuesFilesName = @"c:\temp\queues.txt";
        private readonly string _topicsFilesName = @"c:\temp\topics.txt";

        private readonly List<string> _queues = new List<string>();
        private readonly Dictionary<string, string> _topics = new Dictionary<string, string>();

        public ServiceBusHelper(string connectionString)
        {
            _serviceBusClient = new ManagementClient(connectionString);
        }


        #region Create Queue, Topic and Subscriptions

        /// <summary>
        /// Driver program for creating Queue , Topic and Subscription
        /// </summary>

        /// <summary>
        /// Creates a new queue in the service namespace with the given name
        /// </summary>
        /// <param name="queueName">Queue Name</param>
        /// <returns></returns>
        public async Task CreateQueue(string queueName)
        {
            var exists = await _serviceBusClient.QueueExistsAsync(queueName);
            if (!exists)
            {
                await _serviceBusClient.CreateQueueAsync(queueName);
            }
        }
        public async Task Create()
        {
            string random = DateTime.UtcNow.ToFileTimeUtc().ToString();

            string queueName = "SampleQueue" + random;
            await CreateQueue(queueName);
            _queues.Add(queueName);

            string topicName = "SampleTopic" + random;
            await CreateTopic(topicName);


            string subscriptionName = "SampleSubscription" + random;

            await ListQueues();
            await CreateSubscription(topicName, subscriptionName);
            _topics.Add(topicName, subscriptionName);

        }

        /// <summary>
        /// Creates a new Topic in the service namespace with the given name
        /// </summary>
        /// <param name="topicName">Topic Name</param>
        /// <returns></returns>
        public async Task CreateTopic(string topicName)
        {
            var exists = await _serviceBusClient.TopicExistsAsync(topicName);
            if (!exists)
            {
                await _serviceBusClient.CreateTopicAsync(topicName);
            }
        }
        /// <summary>
        /// CCreates a new subscription within a topic in the service namespace with the given name.
        /// </summary>
        /// <param name="topicName">Topic Name</param>
        /// <param name="subscriptionName">Subscription Name</param>
        /// <returns></returns>
        public async Task CreateSubscription(string topicName, string subscriptionName)
        {
            var exists = await _serviceBusClient.SubscriptionExistsAsync(topicName, subscriptionName);
            if (!exists)
            {
                var subscriptionDescription = await _serviceBusClient.CreateSubscriptionAsync(topicName, subscriptionName);

            }


        }

        #endregion

        #region Show Information

        /// <summary>
        /// Driver Program for Showing Queue, Topic and Subscription Details
        /// </summary>
        /// <returns></returns>
        public async Task ShowNamespace()
        {
            await ListQueues();
            await ListTopics();
        }

        /// <summary>
        /// List all Topics present in a namespace
        /// </summary>
        /// <returns></returns>
        public async Task ListTopics()
        {
            var topics = await _serviceBusClient.GetTopicsAsync();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var topic in topics)
            {
                stringBuilder.Append($"\n");
                stringBuilder.Append($"\n Topic description for {topic.Path}");
                stringBuilder.Append($"    \nPath:                                   {topic.Path}");
                stringBuilder.Append($"    \nMaxSizeInMB:                            {topic.MaxSizeInMB}");
                stringBuilder.Append($"    \nRequiresDuplicateDetection:             {topic.RequiresDuplicateDetection}");
                stringBuilder.Append($"    \nDuplicateDetectionHistoryTimeWindow:    {topic.DuplicateDetectionHistoryTimeWindow}");
                stringBuilder.Append($"    \nDefaultMessageTimeToLive:               {topic.DefaultMessageTimeToLive}");
                stringBuilder.Append($"    \nEnableBatchedOperations:                {topic.EnableBatchedOperations}");
                stringBuilder.Append($"    \nMaxSizeInMegabytes:                     {topic.MaxSizeInMB}");
                stringBuilder.Append($"    \nStatus:                                 {topic.Status}");
                stringBuilder.Append($"    \nAutoDeleteOnIdle:                       {topic.AutoDeleteOnIdle}");
                stringBuilder.Append($"    \nEnablePartitioning:                     {topic.EnablePartitioning}");
                await ListSubscriptions(topic.Path, stringBuilder);

            }
            File.WriteAllText(_topicsFilesName, stringBuilder.ToString());
        }

        /// <summary>
        /// List all subscriptions present in a given topic
        /// </summary>
        /// <param name="topicName">Topic Name</param>
        /// <param name="stringBuilder">Stringbuilder object for appending details</param>
        /// <returns></returns>
        public async Task ListSubscriptions(string topicName, StringBuilder stringBuilder)
        {
            var subscriptions = await _serviceBusClient.GetSubscriptionsAsync(topicName);

            foreach (var subscription in subscriptions)
            {
                stringBuilder.Append($"\n");
                stringBuilder.Append($"\n\t Subscription description for {subscription.SubscriptionName}");
                stringBuilder.Append($"    \n\t\tDefaultMessageTimeToLive:               {subscription.DefaultMessageTimeToLive}");
                stringBuilder.Append($"    \n\t\tEnableBatchedOperations:                {subscription.EnableBatchedOperations}");
                stringBuilder.Append($"    \n\t\tStatus:                                 {subscription.Status}");
                stringBuilder.Append($"    \n\t\tAutoDeleteOnIdle:                       {subscription.AutoDeleteOnIdle}");

            }

        }

        /// <summary>
        /// List all Queues present in a namespace
        /// </summary>
        /// <returns></returns>
        public async Task ListQueues()
        {
            var quesues = await _serviceBusClient.GetQueuesAsync();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var queue in quesues)
            {
                stringBuilder.Append($"\n");
                stringBuilder.Append($"\nQueue description for {queue.Path}");
                stringBuilder.Append($"    \nPath:                                   {queue.Path}");
                stringBuilder.Append($"    \nMaxSizeInMB:                            {queue.MaxSizeInMB}");
                stringBuilder.Append($"    \nRequiresSession:                        {queue.RequiresSession}");
                stringBuilder.Append($"    \nRequiresDuplicateDetection:             {queue.RequiresDuplicateDetection}");
                stringBuilder.Append($"    \nDuplicateDetectionHistoryTimeWindow:    {queue.DuplicateDetectionHistoryTimeWindow}");
                stringBuilder.Append($"    \nLockDuration:                           {queue.LockDuration}");
                stringBuilder.Append($"    \nDefaultMessageTimeToLive:               {queue.DefaultMessageTimeToLive}");
                stringBuilder.Append($"    \nEnableDeadLetteringOnMessageExpiration: {queue.EnableDeadLetteringOnMessageExpiration}");
                stringBuilder.Append($"    \nEnableBatchedOperations:                {queue.EnableBatchedOperations}");
                stringBuilder.Append($"    \nMaxSizeInMegabytes:                     {queue.MaxSizeInMB}");
                stringBuilder.Append($"    \nMaxDeliveryCount:                       {queue.MaxDeliveryCount}");
                stringBuilder.Append($"    \nStatus:                                 {queue.Status}");
            }
            File.WriteAllText(_queuesFilesName, stringBuilder.ToString());
        }
        #endregion

        #region Delete Queue, Topic and Subscriptions

        /// <summary>
        /// Driver function for deleting Queue, Topic and Subscriptions
        /// </summary>
        /// <returns></returns>
        public async Task Delete()
        {
            foreach (var queue in _queues)
            {
                await DeleteQueue(queue);
            }

            foreach (var keyValuePair in _topics)
            {
                await DeleteSubscription(keyValuePair.Key, keyValuePair.Value);
            }
            foreach (var keyValuePair in _topics)
            {
                await DeleteTopic(keyValuePair.Key);
            }

        }
        /// <summary>
        /// Delete a given queue if it is present in namespace
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task DeleteQueue(string queueName)
        {
            var exists = await _serviceBusClient.QueueExistsAsync(queueName);
            if (exists)
            {
                await _serviceBusClient.DeleteQueueAsync(queueName);
            }


        }

        /// <summary>
        /// Delete a given topic if it is present in namespace
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        public async Task DeleteTopic(string topicName)
        {
            var exists = await _serviceBusClient.TopicExistsAsync(topicName);
            if (exists)
            {
                await _serviceBusClient.DeleteTopicAsync(topicName);
            }

        }

        /// <summary>
        /// Delete a given subscription if it is present in namespace
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="subscriptionName"></param>
        /// <returns></returns>
        public async Task DeleteSubscription(string topicName, string subscriptionName)
        {
            var exists = await _serviceBusClient.SubscriptionExistsAsync(topicName, subscriptionName);
            if (exists)
            {
                await _serviceBusClient.DeleteSubscriptionAsync(topicName, subscriptionName);
            }

        }
        #endregion

    }
}
