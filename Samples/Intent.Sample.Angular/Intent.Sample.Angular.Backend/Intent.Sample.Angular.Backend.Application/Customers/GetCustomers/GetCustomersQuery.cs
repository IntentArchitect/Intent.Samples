using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using Intent.Sample.Angular.Backend.Application.Common.Pagination;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers.GetCustomers
{
    public class GetCustomersQuery : IRequest<PagedResult<CustomerSummaryDto>>, IQuery
    {
        public GetCustomersQuery(int pageNo, int pageSize, string? searchTerm, string? orderBy, bool? isActive)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            IsActive = isActive;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }
        public bool? IsActive { get; set; }
    }
}