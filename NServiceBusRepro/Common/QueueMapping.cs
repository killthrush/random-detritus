using System;

namespace Common
{
    public class QueueMapping : Tuple<string, string>
    {
        public QueueMapping(string assemblyName, string queueName)
            : base(assemblyName, queueName)
        {
        }
    }
}