using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Entities;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.CreateTitle
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, Guid>
    {
        private readonly ITitleRepository _titleRepository;

        [IntentManaged(Mode.Merge)]
        public CreateTitleCommandHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Guid> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
        {
            var title = new Title
            {
                Name = request.Name
            };

            _titleRepository.Add(title);
            await _titleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return title.Id;
        }
    }
}