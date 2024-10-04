using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestFramework.Config;

public class ConfigReader
{
    public static TestSettings ReadConfig()
    {
        var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/appsettings.json");
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        return DeserializeConfig<Settings>(configFile, jsonSerializerOptions).TestSettings;
    }

    public static T DeserializeConfig<T>(string json, JsonSerializerOptions jsonSerializerOptions)
    {
        return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
    }

    public static string SerializeConfig<T>(T config, JsonSerializerOptions jsonSerializerOptions)
    {
        return JsonSerializer.Serialize(config, jsonSerializerOptions);
    }

    public class Settings
    {
        public TestSettings? TestSettings { get; set; }
    }
}