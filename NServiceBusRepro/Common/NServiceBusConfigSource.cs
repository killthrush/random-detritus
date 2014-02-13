using System;
using System.Collections.Generic;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Common
{
    public class NServiceBusConfigSource : IConfigurationSource
    {
        public static readonly Func<Type, bool> MessageLocator = t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus");
        public static readonly Func<Type, bool> EventLocator = t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus") && t.Name.EndsWith("Event");
        public static readonly Func<Type, bool> CommandLocator = t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus") && t.Name.EndsWith("Command");

        private readonly string _connectionString;
        private readonly string _databaseSchema;
        private readonly string _errorQueueName;
        private readonly string _subscriptionQueueName;
        private readonly int _numberOfFirstLevelRetries;
        private readonly int _numberOfWorkerThreads;
        private readonly int _numberOfSecondLevelRetries;
        private readonly TimeSpan _secondLevelRetryInterval;
        private readonly IEnumerable<QueueMapping> _queueMappings;

        public NServiceBusConfigSource(NServiceBusConfigPackage configPackage)
        {
            if (configPackage == null)
            {
                throw new ArgumentNullException("configPackage");
            }

            _connectionString = configPackage.DatabaseConnectionString;
            _databaseSchema = configPackage.DatabaseSchemaName;
            _errorQueueName = configPackage.ErrorQueueName;
            _subscriptionQueueName = configPackage.SubscriptionQueueName;
            _numberOfFirstLevelRetries = configPackage.NumberOfFirstLevelRetries;
            _numberOfWorkerThreads = configPackage.NumberOfWorkerThreads;
            _queueMappings = configPackage.MessageMappings;
            _numberOfSecondLevelRetries = configPackage.NumberOfSecondLevelRetries;
            _secondLevelRetryInterval = configPackage.SecondLevelRetryInterval;
        }

        public T GetConfiguration<T>() where T : class, new()
        {
            if (typeof(T) == typeof(MsmqSubscriptionStorageConfig))
            {
                return GetMsmqSubscriptionStorageConfig() as T;
            }

            if (typeof(T) == typeof(DBSubscriptionStorageConfig))
            {
                return new DBSubscriptionStorageConfig() as T;
            }

            if (typeof(T) == typeof(TransportConfig))
            {
                return GetTransportConfig() as T;
            }

            if (typeof(T) == typeof(UnicastBusConfig))
            {
                return GetUnicastBusConfig() as T;
            }

            if (typeof(T) == typeof(NHibernateSagaPersisterConfig))
            {
                return GetNHibernateSagaPersisterConfig() as T;
            }

            if (typeof(T) == typeof(TimeoutPersisterConfig))
            {
                return GetTimeoutPersisterConfig() as T;
            }

            if (typeof(T) == typeof(SecondLevelRetriesConfig))
            {
                return GetSecondLevelRetriesConfig() as T;
            }

            if (typeof(T) == typeof(MessageForwardingInCaseOfFaultConfig))
            {
                return GetMessageForwardingInCaseOfFaultConfig() as T;
            }

            return null;
        }

        private SecondLevelRetriesConfig GetSecondLevelRetriesConfig()
        {
            return new SecondLevelRetriesConfig
            {
                NumberOfRetries = _numberOfSecondLevelRetries,
                TimeIncrease = _secondLevelRetryInterval
            };
        }

        private TimeoutPersisterConfig GetTimeoutPersisterConfig()
        {
            var properties = new ProgrammableNHibernatePropertyCollection();
            properties.Load(_connectionString, _databaseSchema);
            return new TimeoutPersisterConfig
            {
                NHibernateProperties = properties,
                UpdateSchema = true
            };
        }

        private NHibernateSagaPersisterConfig GetNHibernateSagaPersisterConfig()
        {
            var properties = new ProgrammableNHibernatePropertyCollection();
            properties.Load(_connectionString, _databaseSchema);
            return new NHibernateSagaPersisterConfig
            {
                NHibernateProperties = properties,
                UpdateSchema = true
            };
        }

        private MsmqSubscriptionStorageConfig GetMsmqSubscriptionStorageConfig()
        {
            return new MsmqSubscriptionStorageConfig
            {
                Queue = _subscriptionQueueName
            };
        }

        private MessageForwardingInCaseOfFaultConfig GetMessageForwardingInCaseOfFaultConfig()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = _errorQueueName
            };
        }

        private UnicastBusConfig GetUnicastBusConfig()
        {
            var mappings = new MessageEndpointMappingCollection();
            foreach (var mapping in _queueMappings)
            {
                mappings.Add(new MessageEndpointMapping { Messages = mapping.Item1, Endpoint = mapping.Item2 });
            }

            return new UnicastBusConfig
            {
                DistributorControlAddress = "",
                DistributorDataAddress = "",
                ForwardReceivedMessagesTo = "",
                MessageEndpointMappings = mappings
            };
        }

        private TransportConfig GetTransportConfig()
        {
            return new TransportConfig
            {
                MaxRetries = _numberOfFirstLevelRetries,
                MaximumConcurrencyLevel = _numberOfWorkerThreads
            };
        }

        private class ProgrammableNHibernatePropertyCollection : NHibernatePropertyCollection
        {
            public void Load(string connectionString, string schema)
            {
                AddKeyValue("connection.provider", @"NHibernate.Connection.DriverConnectionProvider");
                AddKeyValue("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
                AddKeyValue("connection.connection_string", connectionString);
                AddKeyValue("dialect", "NHibernate.Dialect.MsSql2005Dialect");
                AddKeyValue("default_schema", schema);
            }

            private void AddKeyValue(string key, string value)
            {
                var element = (NHibernateProperty)CreateNewElement();
                element.Key = key;
                element.Value = value;
                BaseAdd(element);
            }
        }
    }
}