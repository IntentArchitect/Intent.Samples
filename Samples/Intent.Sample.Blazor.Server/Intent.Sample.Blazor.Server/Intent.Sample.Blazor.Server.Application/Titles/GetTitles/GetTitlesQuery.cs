using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.GetTitles
{
    public class GetTitlesQuery : IRequest<List<TitleDto>>, IQuery
    {
        public GetTitlesQuery()
        {
        }
    }
}