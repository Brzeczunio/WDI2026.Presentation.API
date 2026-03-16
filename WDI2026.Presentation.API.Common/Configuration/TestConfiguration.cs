namespace WDI2026.Presentation.API.Common.Configuration;

public static class TestConfiguration
{
    public static AppSettings Settings => AppSettings.Value;

    private static readonly Lazy<AppSettings> AppSettings = new Lazy<AppSettings>(SettingsProvider.GetSettings<AppSettings>());
}

