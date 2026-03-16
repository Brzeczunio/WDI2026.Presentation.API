namespace WDI2026.Presentation.API.Common.Configuration.Providers;

public static class LoggingProvider
{
    public static bool GetEnabledAdditionalLogging()
    {
        var scenarioAdditionalLoggingEnabled = SystemEnvironment.GetEnvironmentVariable(EnvironmentVariables.ScenarioAdditionalLoggingEnabled);

        if (string.IsNullOrEmpty(scenarioAdditionalLoggingEnabled))
        {
            return TestConfiguration.Settings.TestSettings.ScenarioAdditionalLoggingEnabled;
        }

        return bool.TryParse(scenarioAdditionalLoggingEnabled, out var isScenarioAdditionalLoggingEnabled) && isScenarioAdditionalLoggingEnabled;
    }
}

