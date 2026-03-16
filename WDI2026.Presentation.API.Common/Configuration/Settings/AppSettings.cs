using WDI2026.Presentation.API.Common.Configuration.Settings.Clients;

namespace WDI2026.Presentation.API.Common.Configuration.Settings;

public sealed record AppSettings : ISettings
{
    public TestSettings TestSettings { get; init; }

    public ShopApiClientSettings ShopApiClientSettings { get; init; }
}

