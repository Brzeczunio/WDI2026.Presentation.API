namespace WDI2026.Presentation.API.Common.Clients.Extensions;

internal static class RestSharpResponseExtensions
{
    internal static void LogRequestAndResponse<T>(this T response, long elapsedMiliseconds, IStepsLogger stepsLogger) where T : RestResponse
    {
        stepsLogger.Log(response.PrepareRequestParametersLog());
        stepsLogger.Log($"Received succcessful response from {response.ResponseUri} in {elapsedMiliseconds}ms.");
        stepsLogger.Log($"Status Code: {response.StatusCode}");
        stepsLogger.Log(string.IsNullOrEmpty(response.Content) ? "Response body: <no body>" : $"Response body:\n {response.Content}");
        stepsLogger.Log(string.Empty, false);
    }

    private static string PrepareRequestParametersLog<T>(this T response) where T : RestResponse
    {
        var requestParametersLog = string.Join(", ", response.Request.Parameters.Select(parameter => 
        { 
            var name = string.IsNullOrEmpty(parameter.Name) ? "Request body" : parameter.Name;
            var value = parameter.Value;
            return $"{name}: {value}";
        }));

        return $"Request parameters:\n {requestParametersLog}";
    }
}

