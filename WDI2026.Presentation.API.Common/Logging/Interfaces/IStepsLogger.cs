namespace WDI2026.Presentation.API.Common.Logging.Interfaces;

public interface IStepsLogger
{
    void Log(string message, bool timeStampEnabled = true);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}

