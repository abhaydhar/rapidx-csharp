using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ArtHandler.Repository;

namespace ArtHandler.Classes
{
    public sealed class DatabaseFactorySectionHandler : ConfigurationSection
    {

        [ConfigurationProperty("Name")]
        public string Name
        {
            get { return (string)base["Name"]; }
        }
        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)base["ConnectionStringName"]; }
        }
        public string ConnectionString
        {
            get
            {
                try
                {
                    return Utility.Encryptor.Decrypt(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString, Constants.PASSPHARSE);
                }
                catch (Exception excep)
                {
                    throw new Exception("Connection string " + ConnectionStringName + " was not found in web.config. " + excep.Message);
                }
            }
        }
    }

}
