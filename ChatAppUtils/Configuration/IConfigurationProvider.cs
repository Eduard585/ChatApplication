namespace ChatAppUtils.Configuration
{
    public interface IConfigurationProvider
    {
        string GetConnectionString(string key);
        string GetDefaultConnection();
        string GetAppSetting(string key);
        object GetSection(string key);
    }
}
