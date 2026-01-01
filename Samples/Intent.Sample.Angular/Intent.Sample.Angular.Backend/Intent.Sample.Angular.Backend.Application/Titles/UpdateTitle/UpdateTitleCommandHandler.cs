using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Common.Exceptions;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles.UpdateTitle
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateTitleCommandHandler : IRequestHandler<UpdateTitleCommand>
    {
        private readonly ITitleRepository _titleRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateTitleCommandHandler(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
        {
            var title = await _titleRepository.FindByIdAsync(request.Id, cancellationToken);
            if (title is null)
            {
                throw new NotFoundException($"Could not find Title '{request.Id}'");
            }

            title.Name = request.Name;
        }
    }
}