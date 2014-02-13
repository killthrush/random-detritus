using System;
using System.Collections.Generic;
using System.Threading;
using Messages;
using NServiceBus.Saga;

namespace Subscriber
{
    public class NHibernateSagaHandler :
        Saga<NHibernateSagaData>,
        IAmStartedByMessages<IFirstEvent>,
        IAmStartedByMessages<ISecondEvent>,
        IAmStartedByMessages<IThirdEvent>
    {
        private const int SlowDownFactor = 20000;

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<IFirstEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
            ConfigureMapping<ISecondEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
            ConfigureMapping<IThirdEvent>(s => s.OrderId).ToSaga(m => m.OrderId);
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
            if (Data.RelatedData == null)
            {
                Data.RelatedData = new List<NHibernateRelatedSagaData>();
            }
            Data.RelatedData.Add(new NHibernateRelatedSagaData
                            {
                                NHibernateSagaData = Data,
                                ThreadId = threadId
                            });
            PerformSagaCompletionCheck();
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
            if (Data.RelatedData == null)
            {
                Data.RelatedData = new List<NHibernateRelatedSagaData>();
            }
            Data.RelatedData.Add(new NHibernateRelatedSagaData
                                     {
                                         NHibernateSagaData = Data,
                                         ThreadId = threadId
                                     });
            PerformSagaCompletionCheck();
            Console.WriteLine(string.Format("[{0}] Finished Processing ISecondEvent for Order ID {1}", threadId, message.OrderId));
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
            if (Data.RelatedData == null)
            {
                Data.RelatedData = new List<NHibernateRelatedSagaData>();
            }
            Data.RelatedData.Add(new NHibernateRelatedSagaData
            {
                NHibernateSagaData = Data,
                ThreadId = threadId
            });
            PerformSagaCompletionCheck();
            Console.WriteLine(string.Format("[{0}] Finished Processing IThirdEvent for Order ID {1}", threadId, message.OrderId));
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