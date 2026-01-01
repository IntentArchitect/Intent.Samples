using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles.GetTitles
{
    public class GetTitlesQuery : IRequest<List<TitleDto>>, IQuery
    {
        public GetTitlesQuery()
        {
        }
    }
}