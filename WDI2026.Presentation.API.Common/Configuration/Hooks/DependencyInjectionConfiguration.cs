namespace WDI2026.Presentation.API.Common.Configuration.Hooks;

[Binding]
public sealed class DependencyInjectionConfiguration
{
    [BeforeScenario(Order = int.MinValue)]
    public static void RegisterDepedencies(IObjectContainer container)
    {
        container.RegisterTypeAs<StepsLogger, IStepsLogger>();
    }
}

