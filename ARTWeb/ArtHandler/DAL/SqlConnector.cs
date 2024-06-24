using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.DAL
{
    public static class SqlConnector
    {
        public static string data = ConfigurationManager.ConnectionStrings["SqlDBConnection"].ToString();

        public static SqlConnection OpenConnection()
        {
            SqlConnection Connection = new SqlConnection(Utility.Encryptor.Decrypt(data, Constants.PASSPHARSE));
            if (Connection.State == System.Data.ConnectionState.Closed || Connection.State == System.Data.ConnectionState.Broken)
                Connection.Open();
            return Connection;           
        }
    }
}
