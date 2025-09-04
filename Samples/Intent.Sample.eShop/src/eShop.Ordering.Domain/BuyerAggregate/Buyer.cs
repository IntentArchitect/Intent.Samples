using System.Collections.Generic;
using System.Linq;
using eShop.Ordering.Domain.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Ordering.Domain.BuyerAggregate
{
    public class Buyer : IHasDomainEvent
    {

        public Buyer(string buyerIdentifier, string name)
        {
            BuyerIdentifier = buyerIdentifier;
            Name = name;
        }

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        protected Buyer()
        {
            BuyerIdentifier = null!;
            Name = null!;
        }

        public int Id { get; private set; }

        public string BuyerIdentifier { get; private set; }

        public string Name { get; private set; }

        public List<DomainEvent> DomainEvents { get; set; } = [];


    }
}