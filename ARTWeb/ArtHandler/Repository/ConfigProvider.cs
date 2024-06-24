using ArtHandler.Interface;
using System.Configuration;

namespace ArtHandler.Repository
{
    public class ConfigProvider : IConfigProvider
    {
        public string GetConfigValue(string key)
        {
            return ConfigurationSettings.AppSettings[key].ToString();
        }
    }
}
