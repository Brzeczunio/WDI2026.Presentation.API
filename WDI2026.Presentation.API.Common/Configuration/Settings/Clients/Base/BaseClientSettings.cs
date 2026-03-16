namespace WDI2026.Presentation.API.Common.Configuration.Settings.Clients.Base;

public abstract record BaseClientSettings
{
    public Uri Url { get; init; }
}

