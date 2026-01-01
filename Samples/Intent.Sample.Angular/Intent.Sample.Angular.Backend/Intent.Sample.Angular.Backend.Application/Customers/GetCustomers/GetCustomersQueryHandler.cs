using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Pagination;
using Intent.Sample.Angular.Backend.Domain.Entities;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;
using static System.Linq.Dynamic.Core.DynamicQueryableExtensions;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers.GetCustomers
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedResult<CustomerSummaryDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<PagedResult<CustomerSummaryDto>> Handle(
            GetCustomersQuery request,
            CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.FindAllAsync(
                request.PageNo,
                request.PageSize,
                query =>
                {
                    if (request.IsActive.HasValue)
                    {
                        query = query.Where(x => x.IsActive == request.IsActive.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    {
                        var term = request.SearchTerm!.Trim().ToLower();
                        query = query.Where(x =>
                            x.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                            x.Surname.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
                            x.Email.Contains(term, StringComparison.CurrentCultureIgnoreCase));
                    }

                    return query.OrderBy(request.OrderBy ?? "Id");
                },
                cancellationToken);

            return customers.MapToPagedResult(x => x.MapToCustomerSummaryDto(_mapper));
        }
    }
}