using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NLog;

namespace Infrastructure.Messages
{
    public class MessageTypeRepository : IMessageTypeRepository
    {
        private const string CommandSufix = "Command";

        private const string RequestSufix = "Request";

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMessageTypesFinder messageTypesFinder;

        private readonly ConcurrentDictionary<string, Type> typeMappings =
            new ConcurrentDictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        public MessageTypeRepository(IMessageTypesFinder messageTypesFinder, params Assembly[] assemblies)
        {
            if (messageTypesFinder == null)
            {
                throw new ArgumentNullException(nameof(messageTypesFinder));
            }

            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            this.messageTypesFinder = messageTypesFinder;

            Type[] types =
                assemblies.SelectMany(
                    i =>
                        this.messageTypesFinder.GetCommandTypes(i)
                            .Union(this.messageTypesFinder.GetEventTypes(i))
                            .Union(this.messageTypesFinder.GetQueryTypes(i))).ToArray();

            this.InitMappings(types);
        }

        public Type GetType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Type type;
            if (!this.typeMappings.TryGetValue(name, out type))
            {
                throw new ArgumentException($"Invalid request name '{name}'.", nameof(name));
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Found request type - '{0}'", type.FullName);
            }

            return type;
        }

        private void InitMappings(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                string key;
                if (type.Name.EndsWith(CommandSufix, StringComparison.InvariantCultureIgnoreCase))
                {
                    key = type.Name.Substring(0, type.Name.Length - CommandSufix.Length);
                }
                else if (type.Name.EndsWith(RequestSufix, StringComparison.InvariantCultureIgnoreCase))
                {
                    key = type.Name.Substring(0, type.Name.Length - RequestSufix.Length);
                }
                else
                {
                    key = type.Name;
                }

                Logger.Debug("Mapping request type '{0}' -> '{1}'", type.FullName, key);

                if (!this.typeMappings.TryAdd(key, type))
                {
                    throw new InvalidOperationException(
                        $"Can't map type '{type.FullName}' to the key '{key}'. Duplicate request name.");
                }

                if (!this.typeMappings.TryAdd(type.FullName, type))
                {
                    throw new InvalidOperationException(
                        $"Can't map type '{type.FullName}' to the key '{type.FullName}'. Duplicate request name.");
                }

                if (key == type.Name)
                {
                    continue;
                }

                if (!this.typeMappings.TryAdd(type.Name, type))
                {
                    throw new InvalidOperationException(
                        $"Can't map type '{type.FullName}' to the key '{type.Name}'. Duplicate request name.");
                }
            }
        }
    }
}