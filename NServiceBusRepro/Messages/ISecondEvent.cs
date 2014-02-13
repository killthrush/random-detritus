using System;

namespace Messages
{
    public interface ISecondEvent
    {
        Guid OrderId { get; set; }
    }
}
