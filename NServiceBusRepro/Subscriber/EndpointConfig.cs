using System;
using System.Transactions;
using Common;
using Messages;
using NLog.Targets;
using NServiceBus;
using NServiceBus.Features;
using StructureMap;

namespace Subscriber
{
	public class EndpointConfig : IConfigureThisEndpoint, IEmptyRole, IWantCustomInitialization
	{
		public void Init()
		{
			var container = new Container();
			NServiceBusConfigPackage configPackage = CreateNServiceBusConfigurationPackage();
			var configSource = new NServiceBusConfigSource(configPackage);

			Configure.Features
				.Disable<Audit>()
				.Enable<Sagas>()
				.Enable<AutoSubscribe>()
				.Enable<XmlSerialization>()
				.Enable<SecondLevelRetries>()
				.Enable<TimeoutManager>()
				.Enable<MsmqTransport>();

			Configure.Transactions
				.Enable()
				.Advanced(x => x.EnableDistributedTransactions())
				.Advanced(x => x.IsolationLevel(IsolationLevel.ReadCommitted));

			Configure.With()
				.License(NServiceBusResources.NServiceBusLicense)
				.DefineEndpointName(() => configPackage.InputQueueName)
				.CustomConfigurationSource(configSource)
				.StructureMapBuilder(container)
				.NLog(new NullTarget())
				.DefiningMessagesAs(NServiceBusConfigSource.MessageLocator)
				.DefiningEventsAs(NServiceBusConfigSource.EventLocator)
				.DefiningCommandsAs(NServiceBusConfigSource.CommandLocator)
				.UseTransport<Msmq>(() => "deadLetter=true;journal=true;useTransactionalQueues=true;cacheSendConnection=true")
				.MsmqSubscriptionStorage()
				.UseNHibernateSagaPersister()
				.UseNHibernateTimeoutPersister()
				.PurgeOnStartup(false)
				.UnicastBus()
				.ImpersonateSender(true)
				.LoadMessageHandlers()
				.CreateBus()
				.Start();
		}

		private static NServiceBusConfigPackage CreateNServiceBusConfigurationPackage()
		{
			var configPackage = new NServiceBusConfigPackage();
			configPackage.InputQueueName = "NServiceBusRepro.SubscriberInputQueue";
			configPackage.ErrorQueueName = "NServiceBusRepro.SubscriberErrorQueue";
			configPackage.SubscriptionQueueName = "NServiceBusRepro.SubscriberSubscriptionQueue";
			configPackage.DatabaseConnectionString = @"server=localhost\SQLEXPRESS;database=NServiceBusRepro;Trusted_Connection=True;";
			configPackage.DatabaseSchemaName = "dbo";
			configPackage.NumberOfFirstLevelRetries = 2;
			configPackage.NumberOfSecondLevelRetries = 0;
			configPackage.SecondLevelRetryInterval = new TimeSpan(0, 0, 10);
			configPackage.MessageMappings = new[]
												{
													new QueueMapping(typeof (IFirstEvent).AssemblyQualifiedName, "NServiceBusRepro.PublisherInputQueue"),
													new QueueMapping(typeof (ISecondEvent).AssemblyQualifiedName, "NServiceBusRepro.PublisherInputQueue"),
													new QueueMapping(typeof (IThirdEvent).AssemblyQualifiedName, "NServiceBusRepro.PublisherInputQueue")
												};
			configPackage.NumberOfWorkerThreads = 8;  // Change this to increase worker threads
			
			return configPackage;
		}
	}
}
