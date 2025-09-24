using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using eShop.Catalog.Api.Controllers.ResponseTypes;
using eShop.Catalog.Application.CatalogItems;
using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Application.Common.Pagination;
using eShop.Catalog.Application.Common.Validation;
using eShop.Catalog.Application.Interfaces;
using eShop.Catalog.Domain.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace eShop.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/catelog")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _appService;
        private readonly IValidationService _validationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CatalogController(ICatalogService appService,
            IValidationService validationService,
            IUnitOfWork unitOfWork,
            IEventBus eventBus)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified PagedResult&lt;CatalogItemDto&gt;.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpGet("items")]
        [ProducesResponseType(typeof(PagedResult<CatalogItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CatalogItemDto>>> Items(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = default(PagedResult<CatalogItemDto>);
            result = await _appService.Items(pageIndex, pageSize, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified CatalogItemDto.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">No CatalogItemDto could be found with the provided parameters.</response>
        [HttpGet("items/{id}")]
        [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogItemDto>> ItemById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var result = default(CatalogItemDto);
            result = await _appService.ItemById(id, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified PagedResult&lt;CatalogItemDto&gt;.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpGet("items/withname/{name}")]
        [ProducesResponseType(typeof(PagedResult<CatalogItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CatalogItemDto>>> ItemsWithName(
            [FromRoute] string name,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            var result = default(PagedResult<CatalogItemDto>);
            result = await _appService.ItemsWithName(name, pageSize, pageIndex, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified PagedResult&lt;CatalogItemDto&gt;.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">No PagedResult&lt;CatalogItemDto&gt; could be found with the provided parameters.</response>
        [HttpGet("items/type/{catalogTypeId}/brand/{catalogBrandId?}")]
        [ProducesResponseType(typeof(PagedResult<CatalogItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CatalogItemDto>>> ItemsByTypeIdAndBrandId(
            [FromRoute] int catalogTypeId,
            [FromRoute] int? catalogBrandId,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            var result = default(PagedResult<CatalogItemDto>);
            result = await _appService.ItemsByTypeIdAndBrandId(catalogTypeId, catalogBrandId, pageSize, pageIndex, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified PagedResult&lt;CatalogItemDto&gt;.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">No PagedResult&lt;CatalogItemDto&gt; could be found with the provided parameters.</response>
        [HttpGet("items/type/all/brand/{catalogBrandId?}")]
        [ProducesResponseType(typeof(PagedResult<CatalogItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CatalogItemDto>>> ItemsByBrandId(
            [FromRoute] int? catalogBrandId,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            var result = default(PagedResult<CatalogItemDto>);
            result = await _appService.ItemsByBrandId(catalogBrandId, pageSize, pageIndex, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;CatalogTypeDto&gt;.</response>
        [HttpGet("catalogtypes")]
        [ProducesResponseType(typeof(List<CatalogTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CatalogTypeDto>>> CatalogTypes(CancellationToken cancellationToken = default)
        {
            var result = default(List<CatalogTypeDto>);
            result = await _appService.CatalogTypes(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;CatalogBrandDto&gt;.</response>
        [HttpGet("catalogbrands")]
        [ProducesResponseType(typeof(List<CatalogBrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CatalogBrandDto>>> CatalogBrands(CancellationToken cancellationToken = default)
        {
            var result = default(List<CatalogBrandDto>);
            result = await _appService.CatalogBrands(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="204">Successfully updated.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">One or more entities could not be found with the provided parameters.</response>
        [HttpPut("items/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProduct(
            [FromBody] CatalogItemUpdateDto dto,
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            await _validationService.Handle(dto, cancellationToken);

            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _appService.UpdateProduct(dto, id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                transaction.Complete();
            }
            await _eventBus.FlushAllAsync(cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <response code="201">Successfully created.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPost("items")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<int>>> CreateProduct(
            [FromBody] CatalogItemCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            await _validationService.Handle(dto, cancellationToken);
            var result = default(int);

            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _appService.CreateProduct(dto, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                transaction.Complete();
            }
            await _eventBus.FlushAllAsync(cancellationToken);
            return CreatedAtAction(nameof(ItemById), new { id = result }, new JsonResponse<int>(result));
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Successfully deleted.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">One or more entities could not be found with the provided parameters.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _appService.DeleteProduct(id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                transaction.Complete();
            }
            await _eventBus.FlushAllAsync(cancellationToken);
            return Ok();
        }
    }
}