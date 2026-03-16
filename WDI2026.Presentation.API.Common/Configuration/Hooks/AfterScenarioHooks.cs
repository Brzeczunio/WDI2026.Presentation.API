namespace WDI2026.Presentation.API.Common.Configuration.Hooks;

[Binding]
public sealed class AfterScenarioHooks
{
    [AfterScenario]
    public static void LogTraceOnFailure(ScenarioContext scenarioContext)
    {
        if (scenarioContext is { TestError: not null } && !LoggingProvider.GetEnabledAdditionalLogging())
        {
            TestContext.Out.Write(scenarioContext[ScenarioContextVariables.AdditionalLogging].ToString());
            TestContext.Out.Write($"[{DateTime.Now:G}]: {scenarioContext.TestError}\n");
        }
    }
}

