using System;
using System.Threading.Tasks;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

using NLog;

namespace Infrastructure.ServiceBus
{
    public class AzureServiceBus : IBus
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IBrokeredMessageFactory brokeredMessageFactory;

        private readonly string connectionString;

        private readonly Lazy<Task<TopicClient>> topicClient;

        private readonly string topicName;

        public AzureServiceBus(
            string connectionString,
            string topicName,
            IBrokeredMessageFactory brokeredMessageFactory)
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

            this.topicClient =
                new Lazy<Task<TopicClient>>(() => CreateTopicClientAsync(this.connectionString, this.topicName));
        }

        public Task PublishAsync(params object[] events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            return this.PublishInternalAsync(events);
        }

        private static async Task<TopicClient> CreateTopicClientAsync(string connectionString, string topicName)
        {
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
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

        private async Task PublishInternalAsync(object[] events)
        {
            try
            {
                TopicClient client = await this.topicClient.Value;
                foreach (object @event in events)
                {
                    BrokeredMessage brokeredMessage = this.brokeredMessageFactory.CreateMessage(@event);

                    await client.SendAsync(brokeredMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error during publishing events. {0}", ex.Message);
                throw;
            }
        }
    }
}