namespace WDI2026.Presentation.API.Common.Clients.Interfaces;

public interface IRestClient
{
    Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse> ExecuteGetAsync(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse<T>> ExecuteGetAsync<T>(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse> ExecutePostAsync(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse<T>> ExecutePostAsync<T>(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse> ExecutePutAsync(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse<T>> ExecutePutAsync<T>(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse> ExecuteDeleteAsync(RestRequest request, CancellationToken cancellationToken = default);
    Task<RestResponse<T>> ExecuteDeleteAsync<T>(RestRequest request, CancellationToken cancellationToken = default);
}

