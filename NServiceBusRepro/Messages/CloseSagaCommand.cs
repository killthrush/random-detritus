using System;

namespace Messages
{
    public class CloseSagaCommand
    {
        public Guid OrderId { get; set; }
    }
}
