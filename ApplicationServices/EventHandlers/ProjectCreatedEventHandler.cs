using System.Threading.Tasks;

using MediatR;

using NLog;

using MyServiceEvents = Contracts.Events;

namespace ApplicationServices.EventHandlers
{
    public class ProjectCreatedEventHandler : IAsyncNotificationHandler<MyServiceEvents.ProjectCreatedEvent>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public Task Handle(MyServiceEvents.ProjectCreatedEvent notification)
        {
            Logger.Debug("Handle ProjectCreatedEvent, EventId: {0}", notification.EventId);
            return Task.CompletedTask;
        }
    }
}