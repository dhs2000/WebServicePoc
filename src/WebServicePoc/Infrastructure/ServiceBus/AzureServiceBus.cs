using System;
using System.Threading.Tasks;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public class AzureServiceBus : IBus
    {
        private readonly string connectionString;

        private readonly string topicName;

        private readonly Lazy<Task<TopicClient>> topicClient;

        private readonly IBrokeredMessageFactory brokeredMessageFactory;

        public AzureServiceBus(string connectionString, string topicName, IBrokeredMessageFactory brokeredMessageFactory)
        {
            if (brokeredMessageFactory == null)
            {
                throw new ArgumentNullException(nameof(brokeredMessageFactory));
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(topicName));
            }

            this.connectionString = connectionString;
            this.topicName = topicName;
            this.brokeredMessageFactory = brokeredMessageFactory;

            this.topicClient = new Lazy<Task<TopicClient>>(() => CreateTopicClientAsync(this.connectionString, this.topicName));
        }

        private static async Task<TopicClient> CreateTopicClientAsync(string connectionString, string topicName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            bool topicExist = await namespaceManager.TopicExistsAsync(topicName);
            if (!topicExist)
            {
                var topicDescription = new TopicDescription(topicName);
                await namespaceManager.CreateTopicAsync(topicDescription);
            }

            TopicDescription description = await namespaceManager.GetTopicAsync(topicName);
            TopicClient topicClient = TopicClient.CreateFromConnectionString(connectionString, description.Path);
            return topicClient;
        }

        public Task PublishAsync(params object[] events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            return this.PublishInternalAsync(events);
        }

        private async Task PublishInternalAsync(object[] events)
        {
            TopicClient client = await this.topicClient.Value;
            foreach (object @event in events)
            {
                BrokeredMessage brokeredMessage = this.brokeredMessageFactory.CreateMessage(@event);

                await client.SendAsync(brokeredMessage);
            }
        }
    }
}