using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Ordering.Application.Orders
{
    public class CardTypeDto
    {
        public CardTypeDto()
        {
            Name = null!;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public static CardTypeDto Create(int id, string name)
        {
            return new CardTypeDto
            {
                Id = id,
                Name = name
            };
        }
    }
}