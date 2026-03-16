namespace WDI2026.Presentation.API.Common.Configuration.Providers;

public static class SettingsProvider
{
    public static T GetSettings<T>() where T : ISettings, new()
    {
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile($"appSettings.{EnvironmentProvider.GetEnvironment()}.json")
            .Build();

        var model = new T();
        configurationRoot.Bind(model);

        return model;
    }
}

