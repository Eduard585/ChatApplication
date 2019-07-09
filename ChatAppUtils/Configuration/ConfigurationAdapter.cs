namespace ChatAppUtils.Configuration
{
    
    public static class ConfigurationAdapter
    {
        private static IConfigurationProvider _provider;

        public static string GetConnectionString(string key)
        {
            return _provider.GetConnectionString(key);
        }

        public static object GetSection(string name)
        {
            return _provider.GetSection(name);
        }

        public static string GetAppSetting(string key)
        {
            return _provider.GetAppSetting(key);
        }

        public static void SetUp(IConfigurationProvider provider)
        {
            _provider = provider;
        }

    }
}
