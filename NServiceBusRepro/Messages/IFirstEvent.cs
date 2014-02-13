using System;

namespace Messages
{
    public interface IFirstEvent
    {
        Guid OrderId { get; set; }
    }
}
