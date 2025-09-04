using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using eShop.Web.Client.Common;
using eShop.Web.Client.Contracts.Catalog.Services.CatalogItems;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.WebUtilities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.HttpClient", Version = "2.0")]

namespace eShop.Web.Client.Implementations
{
    public class CatalogServiceHttpClient : ICatalogService
    {
        private const string JSON_MEDIA_TYPE = "application/json";
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public CatalogServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<CatalogBrandDto>> CatalogBrandsAsync(CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/catelog/catalogbrands";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

            using (var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, httpRequest, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    return (await JsonSerializer.DeserializeAsync<List<CatalogBrandDto>>(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false))!;
                }
            }
        }

        public async Task<List<CatalogTypeDto>> CatalogTypesAsync(CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/catelog/catalogtypes";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

            using (var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, httpRequest, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    return (await JsonSerializer.DeserializeAsync<List<CatalogTypeDto>>(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false))!;
                }
            }
        }

        public async Task<CatalogItemDto> ItemByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/catelog/items/{id}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

            using (var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, httpRequest, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    return (await JsonSerializer.DeserializeAsync<CatalogItemDto>(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false))!;
                }
            }
        }

        public async Task<PagedResult<CatalogItemDto>> ItemsByTypeIdAndBrandIdAsync(
            int catalogTypeId,
            int? catalogBrandId,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/catelog/items/type/{catalogTypeId}/brand/{catalogBrandId}";

            var queryParams = new Dictionary<string, string?>();
            queryParams.Add("pageSize", pageSize.ToString());
            queryParams.Add("pageIndex", pageIndex.ToString());
            relativeUri = QueryHelpers.AddQueryString(relativeUri, queryParams);
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

            using (var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, httpRequest, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    return (await JsonSerializer.DeserializeAsync<PagedResult<CatalogItemDto>>(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false))!;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Class cleanup goes here
        }
    }
}