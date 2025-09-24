using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    public class BasketCheckoutDto
    {
        public BasketCheckoutDto()
        {
            City = null!;
            Street = null!;
            State = null!;
            Country = null!;
            ZipCode = null!;
            CardNumber = null!;
            CardHolderName = null!;
            CardSecurityNumber = null!;
            Buyer = null!;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardType { get; set; }
        public string Buyer { get; set; }
        public Guid RequestId { get; set; }

        public static BasketCheckoutDto Create(
            string city,
            string street,
            string state,
            string country,
            string zipCode,
            string cardNumber,
            string cardHolderName,
            DateTime cardExpiration,
            string cardSecurityNumber,
            int cardType,
            string buyer,
            Guid requestId)
        {
            return new BasketCheckoutDto
            {
                City = city,
                Street = street,
                State = state,
                Country = country,
                ZipCode = zipCode,
                CardNumber = cardNumber,
                CardHolderName = cardHolderName,
                CardExpiration = cardExpiration,
                CardSecurityNumber = cardSecurityNumber,
                CardType = cardType,
                Buyer = buyer,
                RequestId = requestId
            };
        }
    }
}