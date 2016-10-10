using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using NLog;

namespace WebServicePoc.Infrastructure
{
    public class RequestTypeProvider : IRequestTypeProvider
    {
        private const string CommandSufix = "Command";

        private const string RequestSufix = "Request";

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<string, Type> typeMappings =
            new ConcurrentDictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        public RequestTypeProvider(Type[] types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            if (types.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            }

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
            }
        }
    }
}