using Intent.RoslynWeaver.Attributes;
using MediatR;
using MudBlazor.Sample.Application.Common.Interfaces;
using MudBlazor.Sample.Application.Common.Pagination;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace MudBlazor.Sample.Application.Products.GetProducts
{
    public class GetProductsQuery : IRequest<PagedResult<ProductDto>>, IQuery
    {
        public GetProductsQuery(int pageNo, int pageSize, string? orderBy, string? searchText)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            OrderBy = orderBy;
            SearchText = searchText;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SearchText { get; set; }
    }
}