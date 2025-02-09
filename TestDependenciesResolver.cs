using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EverfitExam.Drivers;
using System.IO;
using EverfitExam.APIs;
using EverfitExam.PageObjects;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace EverfitExam;

public static class TestDependenciesResolver
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        IConfiguration configuration = _configurationBuilder();

        services.AddSingleton(configuration);

        services.AddScoped<BrowserFactory>();

        services.AddScoped<MealPlanApiHelpers>();

        services.AddScoped<LoginPage>();
        services.AddScoped<HomePage>();
        services.AddScoped<MealPlanTemplatesPage>();

        return services;
    }

    private static IConfiguration _configurationBuilder()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }
}