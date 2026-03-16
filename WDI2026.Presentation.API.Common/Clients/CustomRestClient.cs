namespace WDI2026.Presentation.API.Common.Clients;

public sealed class CustomRestClient : BaseRestClient, IRestClient
{
    public CustomRestClient(Uri baseUri, IStepsLogger stepsLogger) : base(baseUri, stepsLogger) { }

    public CustomRestClient(Uri baseUri, RestClientOptions restClientOptions, IStepsLogger stepsLogger) : base(baseUri, restClientOptions, stepsLogger) { }

    public override async Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        var response = await base.ExecuteAsync(request, cancellationToken);
        return response;
    }

    public override async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        var response = await base.ExecuteAsync<T>(request, cancellationToken);
        return response;
    }

    public override async Task<RestResponse> ExecuteGetAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecuteGetAsync(request, cancellationToken);
    }

    public override async Task<RestResponse<T>> ExecuteGetAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecuteGetAsync<T>(request, cancellationToken);
    }

    public override async Task<RestResponse> ExecutePostAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecutePostAsync(request, cancellationToken);
    }

    public override async Task<RestResponse<T>> ExecutePostAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecutePostAsync<T>(request, cancellationToken);
    }
    public override async Task<RestResponse> ExecutePutAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecutePutAsync(request, cancellationToken);
    }

    public override async Task<RestResponse<T>> ExecutePutAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecutePutAsync<T>(request, cancellationToken);
    }

    public override async Task<RestResponse> ExecuteDeleteAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecuteDeleteAsync(request, cancellationToken);
    }

    public override async Task<RestResponse<T>> ExecuteDeleteAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await base.ExecuteDeleteAsync<T>(request, cancellationToken);
    }
}

