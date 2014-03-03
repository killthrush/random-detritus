using System;
using System.Collections.Generic;
using NServiceBus.Saga;
using NServiceBus.SagaPersisters.NHibernate.AutoPersistence.Attributes;

namespace Subscriber
{
    public class NHibernateSagaData : IContainSagaData
    {
        [RowVersion]
        public virtual byte[] Version { get; set; }

        public virtual Guid Id { get; set; }

        public virtual string Originator { get; set; }

        public virtual string OriginalMessageId { get; set; }

        [Unique]
        public virtual Guid OrderId { get; set; }

        public virtual bool ConditionOne { get; set; }

        public virtual bool ConditionTwo { get; set; }

        public virtual bool ConditionThree { get; set; }

        public virtual IList<NHibernateRelatedSagaData> RelatedData { get; set; }
    }
}
