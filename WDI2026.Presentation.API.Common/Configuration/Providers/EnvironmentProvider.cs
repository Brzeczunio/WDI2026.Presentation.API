namespace WDI2026.Presentation.API.Common.Configuration.Providers;

public static class EnvironmentProvider
{
    public static Environment GetEnvironment()
    {
        var environment = SystemEnvironment.GetEnvironmentVariable(EnvironmentVariables.Environment);

        return Enum.TryParse(environment, true, out Environment env) ? env : Environment.Dev;
    }
}

