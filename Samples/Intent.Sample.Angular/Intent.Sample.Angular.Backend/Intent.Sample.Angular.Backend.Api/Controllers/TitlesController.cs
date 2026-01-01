using System.Net.Mime;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Api.Controllers.ResponseTypes;
using Intent.Sample.Angular.Backend.Application.Titles;
using Intent.Sample.Angular.Backend.Application.Titles.CreateTitle;
using Intent.Sample.Angular.Backend.Application.Titles.GetTitleById;
using Intent.Sample.Angular.Backend.Application.Titles.GetTitles;
using Intent.Sample.Angular.Backend.Application.Titles.UpdateTitle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Api.Controllers
{
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly ISender _mediator;

        public TitlesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// </summary>
        /// <response code="201">Successfully created.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPost("api/titles")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateTitle(
            [FromBody] CreateTitleCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetTitleById), new { id = result }, new JsonResponse<Guid>(result));
        }

        /// <summary>
        /// </summary>
        /// <response code="204">Successfully updated.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">One or more entities could not be found with the provided parameters.</response>
        [HttpPut("api/titles/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTitle(
            [FromRoute] Guid id,
            [FromBody] UpdateTitleCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.Id == Guid.Empty)
            {
                command.Id = id;
            }

            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified TitleDto.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">No TitleDto could be found with the provided parameters.</response>
        [HttpGet("api/titles/{id}")]
        [ProducesResponseType(typeof(TitleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TitleDto>> GetTitleById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetTitleByIdQuery(id: id), cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;TitleDto&gt;.</response>
        [HttpGet("api/titles")]
        [ProducesResponseType(typeof(List<TitleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TitleDto>>> GetTitles(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetTitlesQuery(), cancellationToken);
            return Ok(result);
        }
    }
}