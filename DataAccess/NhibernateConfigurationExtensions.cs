using System;

using DomainModel.Common;

using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Mapping;

namespace DataAccess
{
    public static class NhibernateConfigurationExtensions
    {
        public static void AddDddListeners(this Configuration configuration)
        {
            foreach (PersistentClass clazz in configuration.ClassMappings)
            {
                if ((typeof(IAggregateRoot).IsAssignableFrom(clazz.MappedClass) == false)
                    && (typeof(IAggregateRootProvider).IsAssignableFrom(clazz.MappedClass) == false))
                {
                    throw new InvalidOperationException("DDD Violation " + clazz.MappedClass);
                }
            }
            
            /*
                        configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[]
                            {
                                new ForceRootAggregateUpdateListener()
                            };
            */

            configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[]
                {
                    new ForceRootAggregateUpdateListener()
                };
        }
    }
}