using System.IO;
using Microsoft.Extensions.Configuration;

namespace EverfitExam.Helpers;

public class ConfigurationHelper
{
    public static IConfiguration Config;

    public static void Initialize()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();;
    }

    public static string GetConfigurationByKey(IConfiguration config, string key)
    {
        var value = config[key];
        if (!string.IsNullOrEmpty(value)) return value;
        var message = $"Attribute [{key}] has not been set in AppSettings.";
        throw new InvalidDataException(message);
    }
}