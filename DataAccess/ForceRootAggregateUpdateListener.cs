using DomainModel.Common;

using NHibernate;
using NHibernate.Event;

namespace DataAccess
{
    public class ForceRootAggregateUpdateListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent insertEvent)
        {
            var rootFinder = insertEvent.Entity as IAggregateRootProvider;
            if (rootFinder == null)
            {
                return false;
            }

            insertEvent.Session.Lock(rootFinder.AggregateRoot, LockMode.Force);

            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent updateEvent)
        {
            var rootFinder = updateEvent.Entity as IAggregateRootProvider;
            if (rootFinder == null)
            {
                return false;
            }

            updateEvent.Session.Lock(rootFinder.AggregateRoot, LockMode.Force);

            return false;
        }
    }
}