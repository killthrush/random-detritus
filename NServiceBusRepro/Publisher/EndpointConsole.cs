using System;
using System.Threading;
using Messages;
using NServiceBus;

namespace Publisher
{
    public class EndpointConsole : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Console.WriteLine("Press '<Enter>' to publish a triple of events that should cause concurrent saga operations.");

            while ((Console.ReadLine()) != null)
            {
                Guid orderId = Guid.NewGuid();
                var firstEventMessage = Bus.CreateInstance<IFirstEvent>();
                firstEventMessage.OrderId = orderId;
                var secondEventMessage = Bus.CreateInstance<ISecondEvent>();
                secondEventMessage.OrderId = orderId;
                var thirdEventMessage = Bus.CreateInstance<IThirdEvent>();
                thirdEventMessage.OrderId = orderId;

                int threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(string.Format("[{0}] Published events for Order ID {1}", threadId, orderId));
                Bus.Publish(firstEventMessage);
                Bus.Publish(secondEventMessage);
                Bus.Publish(thirdEventMessage);
            }
        }

        public void Start()
        {
            Run();
        }

        public void Stop()
        {

        }
    }
}