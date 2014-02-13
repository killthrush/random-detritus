using System;

namespace Subscriber
{
    public class NHibernateRelatedSagaData
    {
        public virtual NHibernateSagaData NHibernateSagaData { get; set; }
        public virtual Guid Id { get; set; }
        public virtual int ThreadId { get; set; }
    }
}