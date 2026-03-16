namespace WDI2026.Presentation.API.Common.Logging;

internal static class ApiClientLogging
{
    internal static async Task<T> ExecuteWithLogging<T>(
        Func<Task<T>> apiCallFunc,
        string requestUrl,
        Method httpMethod,
        IStepsLogger stepsLogger) where T : RestResponse
    {
        var stopwatch = new Stopwatch();

        try
        {
            stepsLogger.Log($"Sending {httpMethod} request to {requestUrl}");

            Stopwatch.StartNew();
            var response = await apiCallFunc();
            stopwatch.Stop();

            response.LogRequestAndResponse(stopwatch.ElapsedMilliseconds, stepsLogger);
            return response;
        }
        catch (HttpRequestException ex)
        {
            stopwatch.Stop();
            stepsLogger.LogError($"HTTP request failed to {requestUrl}. Duration: {stopwatch.ElapsedMilliseconds}ms. Exception message: {ex.Message}");
            throw;
        }
        catch (TaskCanceledException ex)
        {
            stopwatch.Stop();
            stepsLogger.LogError($"Timeout when calling {requestUrl}. Duration: {stopwatch.ElapsedMilliseconds}ms. Exception message: {ex.Message}");
            throw new TimeoutException($"Request to {requestUrl} timed out after {stopwatch.ElapsedMilliseconds}ms", ex);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            stepsLogger.LogError($"Unexpected error during API call to {requestUrl}. Duration: {stopwatch.ElapsedMilliseconds}ms. Exception message: {ex.Message}");
            throw;
        }
    }
}

