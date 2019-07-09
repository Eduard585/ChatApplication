using System.Configuration;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = ChatAppUtils.Configuration.IConfigurationProvider;

namespace ChatAppUtils.Configuration
{
    public class ChatAppConfiguration:IConfigurationProvider
    {
        private readonly IConfigurationRoot _root;
        private readonly System.Configuration.Configuration _configuration;


        public ChatAppConfiguration(IConfigurationRoot root,string config)
        {
            _root = root;
            var configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = config
            };
            //_configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

        }
        public string GetConnectionString(string key)
        {
            return _root.GetConnectionString(key);
        }

        public string GetDefaultConnection()
        {
            return _root.GetConnectionString("DefaultConnection");
        }

        public string GetAppSetting(string key)
        {
            throw new System.NotImplementedException();
        }

        public object GetSection(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
