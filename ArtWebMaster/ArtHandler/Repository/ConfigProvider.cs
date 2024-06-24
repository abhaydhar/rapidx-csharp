using ArtHandler.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
