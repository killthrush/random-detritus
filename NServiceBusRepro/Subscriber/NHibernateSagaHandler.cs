using System;
using System.Collections.Generic;
using System.Threading;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Subscriber
{
    public class NHibernateSagaHandler :
        Saga<NHibernateSagaData>,
        IAmStartedByMessages<IFirstEvent>,
        IAmStartedByMessages<ISecondEvent>,
        IAmStartedByMessages<IThirdEvent>,
        IHandleMessages<CloseSagaCommand>
    {
        private const int SlowDownFactor = 20000;

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<IFirstEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
            ConfigureMapping<ISecondEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
            ConfigureMapping<IThirdEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
            ConfigureMapping<CloseSagaCommand>(s => s.OrderId).ToSaga(m => m.OrderId);
        }

        public void Handle(IFirstEvent message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(string.Format("[{0}] Received IFirstEvent for Order ID {1}", threadId, message.OrderId));
            SlowThingsDown(SlowDownFactor);

            if (!Data.ConditionTwo && !Data.ConditionThree)
            {
                Console.WriteLine(string.Format("[{0}] Created saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }
            else
            {
                Console.WriteLine(string.Format("[{0}] Using existing saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }

            Data.OrderId = message.OrderId;
            Data.ConditionOne = true;

            CreateChildSagaDataIfNeeded();
            UpdateEveryChildSagaDataRecord();

            PerformSagaCompletionCheck();
            //Bus.SendLocal(new CloseSagaCommand {OrderId = message.OrderId});
            Console.WriteLine(string.Format("[{0}] Finished Processing IFirstEvent for Order ID {1}", threadId, message.OrderId));
        }

        public void Handle(ISecondEvent message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(string.Format("[{0}] Received ISecondEvent for Order ID {1}", threadId, message.OrderId));
            SlowThingsDown(SlowDownFactor);

            if (!Data.ConditionOne && !Data.ConditionThree)
            {
                Console.WriteLine(string.Format("[{0}] Created saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }
            else
            {
                Console.WriteLine(string.Format("[{0}] Using existing saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }

            Data.OrderId = message.OrderId;
            Data.ConditionTwo = true;

            CreateChildSagaDataIfNeeded();
            UpdateEveryChildSagaDataRecord();

            PerformSagaCompletionCheck();
            //Bus.SendLocal(new CloseSagaCommand { OrderId = message.OrderId });
            Console.WriteLine(string.Format("[{0}] Finished Processing ISecondEvent for Order ID {1}", threadId, message.OrderId));
        }

        private void UpdateEveryChildSagaDataRecord()
        {
            foreach (var data in Data.RelatedData)
            {
                data.ThreadId++;
            }
        }

        public void Handle(IThirdEvent message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(string.Format("[{0}] Received IThirdEvent for Order ID {1}", threadId, message.OrderId));
            SlowThingsDown(SlowDownFactor);

            if (!Data.ConditionOne && !Data.ConditionTwo)
            {
                Console.WriteLine(string.Format("[{0}] Created saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }
            else
            {
                Console.WriteLine(string.Format("[{0}] Using existing saga with ID {1} for Order ID {2}", threadId, Data.Id, message.OrderId));
            }

            Data.OrderId = message.OrderId;
            Data.ConditionThree = true;

            CreateChildSagaDataIfNeeded();
            UpdateEveryChildSagaDataRecord();

            PerformSagaCompletionCheck();
            //Bus.SendLocal(new CloseSagaCommand { OrderId = message.OrderId });
            Console.WriteLine(string.Format("[{0}] Finished Processing IThirdEvent for Order ID {1}", threadId, message.OrderId));
        }

        /// <summary>
        /// Handles a message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        /// <remarks>
        /// This method will be called when a message arrives on the bus and should contain
        ///             the custom logic to execute when the message is received.
        /// </remarks>
        public void Handle(CloseSagaCommand message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(string.Format("[{0}] Received CloseSagaCommand for Order ID {1}", threadId, message.OrderId));
            PerformSagaCompletionCheck();
            Console.WriteLine(string.Format("[{0}] Finished Processing CloseSagaCommand for Order ID {1}", threadId, message.OrderId));
        }

        private void CreateChildSagaDataIfNeeded()
        {
            if (Data.RelatedData == null)
            {
                Data.RelatedData = new List<NHibernateRelatedSagaData>
                                       {
                                           new NHibernateRelatedSagaData
                                               {
                                                   NHibernateSagaData = Data,
                                                   ThreadId = 1
                                               },
                                           new NHibernateRelatedSagaData
                                               {
                                                   NHibernateSagaData = Data,
                                                   ThreadId = 2
                                               },
                                           new NHibernateRelatedSagaData
                                               {
                                                   NHibernateSagaData = Data,
                                                   ThreadId = 3
                                               }
                                       };
            }
        }

        private static void SlowThingsDown(int slowDownFactor)
        {
            string s = "";
            for (int i = 0; i < slowDownFactor; i++)
            {
                s += " ";
            }
        }

        private void PerformSagaCompletionCheck()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (Data.ConditionOne && Data.ConditionTwo && Data.ConditionThree)
            {
                // If we try to mark the saga as complete after adding a row, it will fail.
                MarkAsComplete();
                Console.WriteLine(string.Format("[{0}] Closing saga with ID {1}", threadId, Data.Id));
            }
        }
    }
}