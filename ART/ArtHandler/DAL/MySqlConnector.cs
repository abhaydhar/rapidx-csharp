using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArtHandler
{
    public class MySqlConnector
    {
        public static string data = ConfigurationManager.ConnectionStrings["Dbconnection"].ToString();

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection Connection = new MySqlConnection(Utility.Encryptor.Decrypt(data, Constants.PASSPHARSE));
            if (Connection.State == System.Data.ConnectionState.Closed || Connection.State == System.Data.ConnectionState.Broken)
                Connection.Open();
            return Connection;
        }
    }
}
