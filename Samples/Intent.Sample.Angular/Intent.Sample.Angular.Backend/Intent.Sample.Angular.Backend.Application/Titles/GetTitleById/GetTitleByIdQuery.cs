using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles.GetTitleById
{
    public class GetTitleByIdQuery : IRequest<TitleDto>, IQuery
    {
        public GetTitleByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}