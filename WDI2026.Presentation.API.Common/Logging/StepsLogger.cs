namespace WDI2026.Presentation.API.Common.Logging;

public sealed class StepsLogger(ScenarioContext scenarioContext, IReqnrollOutputHelper outputHelper) : IStepsLogger
{
    public void LogError(string message) => Log($"[ERROR] {message}");

    public void LogInfo(string message) => Log($"[INFO] {message}");

    public void LogWarning(string message) => Log($"[WARNING] {message}");

    public void Log(string message, bool timeStampEnabled = true)
    {
        if (ShouldLogAdditionalMessage(message))
        {
            outputHelper.WriteLine($"{message}");
        }

        scenarioContext.TryAdd(ScenarioContextVariables.AdditionalLogging, "\nDetailed Test trace:\n");
        scenarioContext[ScenarioContextVariables.AdditionalLogging] += timeStampEnabled ? $"[{DateTime.Now}]: {message}\n" : $"{message}\n";
    }

    private static bool ShouldLogAdditionalMessage(string message) => LoggingProvider.GetEnabledAdditionalLogging() && !string.IsNullOrEmpty(message);
}

