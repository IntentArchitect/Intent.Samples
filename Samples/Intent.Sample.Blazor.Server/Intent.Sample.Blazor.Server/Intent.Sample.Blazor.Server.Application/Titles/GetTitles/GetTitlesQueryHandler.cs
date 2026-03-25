using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.GetTitles
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetTitlesQueryHandler : IRequestHandler<GetTitlesQuery, List<TitleDto>>
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetTitlesQueryHandler(ITitleRepository titleRepository, IMapper mapper)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<TitleDto>> Handle(GetTitlesQuery request, CancellationToken cancellationToken)
        {
            var titles = await _titleRepository.FindAllAsync(cancellationToken);
            return titles.MapToTitleDtoList(_mapper);
        }
    }
}