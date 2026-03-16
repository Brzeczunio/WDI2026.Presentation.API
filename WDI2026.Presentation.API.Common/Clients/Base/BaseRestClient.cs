namespace WDI2026.Presentation.API.Common.Clients.Base;

public abstract class BaseRestClient : IRestClient
{
    private const int ClientTimeoutSeconds = 60;

    private readonly RestClient _restClient;
    private readonly IStepsLogger _stepsLogger;

    protected BaseRestClient(Uri baseUri, IStepsLogger stepsLogger)
    {
        var restClientOptions = new RestClientOptions
        {
            RemoteCertificateValidationCallback = (_, _, _, _) => true,
            Timeout = TimeSpan.FromSeconds(ClientTimeoutSeconds)
        };

        _restClient = new RestClient(restClientOptions);
        _stepsLogger = stepsLogger ?? throw new ArgumentNullException(nameof(stepsLogger), "StepsLogger cannot be null.");
    }

    protected BaseRestClient(Uri baseUri, RestClientOptions restClientOptions, IStepsLogger stepsLogger)
    {
        if (restClientOptions == null)
        {
            throw new ArgumentNullException(nameof(restClientOptions), "RestClientOptions cannot be null.");
        }

        restClientOptions.BaseUrl = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        restClientOptions.RemoteCertificateValidationCallback = (_, _, _, _) => true;
        restClientOptions.Timeout = TimeSpan.FromSeconds(ClientTimeoutSeconds);

        _restClient = new RestClient(restClientOptions);
        _stepsLogger = stepsLogger ?? throw new ArgumentNullException(nameof(stepsLogger), "StepsLogger cannot be null.");
    }

    public virtual async Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteAsync(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteAsync<T>(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse> ExecuteGetAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteGetAsync(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse<T>> ExecuteGetAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteGetAsync<T>(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse> ExecutePostAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecutePostAsync(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse<T>> ExecutePostAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecutePostAsync<T>(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse> ExecutePutAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecutePutAsync(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse<T>> ExecutePutAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecutePutAsync<T>(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse> ExecuteDeleteAsync(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteDeleteAsync(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    public virtual async Task<RestResponse<T>> ExecuteDeleteAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        return await ApiClientLogging.ExecuteWithLogging(
            () => _restClient.ExecuteDeleteAsync<T>(request, cancellationToken),
            PrepareFullRequestUrl(request),
            request.Method,
            _stepsLogger);
    }

    private string PrepareFullRequestUrl(RestRequest request) => _restClient.BuildUri(request).ToString();
}

