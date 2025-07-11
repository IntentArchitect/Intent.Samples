using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AspNetCoreCleanArchitecture.Sample.Application.Buyers
{
    public class BuyerStatisticsDto
    {
        public BuyerStatisticsDto()
        {
            BuyerName = null!;
            BuyerSurname = null!;
        }

        public Guid BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public int NoOfOrders { get; set; }
        public decimal AverageCartValue { get; set; }

        public static BuyerStatisticsDto Create(
            Guid buyerId,
            string buyerName,
            string buyerSurname,
            int noOfOrders,
            decimal averageCartValue)
        {
            return new BuyerStatisticsDto
            {
                BuyerId = buyerId,
                BuyerName = buyerName,
                BuyerSurname = buyerSurname,
                NoOfOrders = noOfOrders,
                AverageCartValue = averageCartValue
            };
        }
    }
}