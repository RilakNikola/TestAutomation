using TestFramework.Driver;

namespace TestFramework.Config;

public class TestSettings
{
    public BrowserType BrowserType { get; set; }
    public Uri? ApplicationUrl { get; set; }
    public float? TimeoutInterval { get; set; }
    public AccessCredentials AccessCredentials { get; set; }
    public Uri? ApiBaseUrl { get; set; }
}

public class AccessCredentials
{
    public required string Username { get; set; }
    public string? EncryptedPassword { get; set; }
    public string? KeyFilePath { get; set; }
    public string? Pass { get; set; }
}