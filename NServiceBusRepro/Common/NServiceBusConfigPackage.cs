using System;
using System.Collections.Generic;
using System.Reflection;

namespace Common
{
    public class NServiceBusConfigPackage
    {
        public string DatabaseConnectionString { get; set; }
        public string DatabaseSchemaName { get; set; }
        public string InputQueueName { get; set; }
        public string ErrorQueueName { get; set; }
        public string SubscriptionQueueName { get; set; }
        public int NumberOfFirstLevelRetries { get; set; }
        public int NumberOfSecondLevelRetries { get; set; }
        public TimeSpan SecondLevelRetryInterval { get; set; }
        public int NumberOfWorkerThreads { get; set; }
        public IEnumerable<QueueMapping> MessageMappings { get; set; }
        public IEnumerable<Assembly> AssembliesToIgnoreInScan { get; set; }
    }
}
