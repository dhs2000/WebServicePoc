using System;
using System.Collections.Generic;
using System.Diagnostics;

using DomainModel.Common;

using NHibernate;
using NHibernate.Type;

namespace DataAccess
{
    public class DomainEventsIntercepter : EmptyInterceptor
    {
        private readonly List<IEvent> events = new List<IEvent>();

        private readonly IEventsPublisher eventsPublisher;

        public DomainEventsIntercepter(IEventsPublisher eventsPublisher)
        {
            if (eventsPublisher == null)
            {
                throw new ArgumentNullException(nameof(eventsPublisher));
            }

            this.eventsPublisher = eventsPublisher;
        }

        public override void AfterTransactionCompletion(ITransaction tx)
        {
            Debug.Assert(!tx.IsActive, "Transaction should be inactive.");
            if (tx.WasCommitted)
            {
                this.eventsPublisher.Publish(this.events);
            }

            this.events.Clear();

            base.AfterTransactionCompletion(tx);
        }

        public override int[] FindDirty(
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types)
        {
            this.HandleEntityEvents(entity);

            return base.FindDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            this.HandleEntityEvents(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }

        private void HandleEntityEvents(object entity)
        {
            var eventsProvider = entity as IEventsProvider;
            if (eventsProvider == null)
            {
                return;
            }

            this.events.AddRange(eventsProvider.Events);
            eventsProvider.ResetEvents();
        }
    }
}