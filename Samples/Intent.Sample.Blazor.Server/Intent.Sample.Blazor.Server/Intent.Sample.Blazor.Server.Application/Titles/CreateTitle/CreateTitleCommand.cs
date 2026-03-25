using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.CreateTitle
{
    public class CreateTitleCommand : IRequest<Guid>, ICommand
    {
        public CreateTitleCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}