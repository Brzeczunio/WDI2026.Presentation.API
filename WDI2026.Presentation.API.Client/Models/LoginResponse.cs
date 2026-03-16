namespace WDI2026.Presentation.API.Client.Models;

public record LoginResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
