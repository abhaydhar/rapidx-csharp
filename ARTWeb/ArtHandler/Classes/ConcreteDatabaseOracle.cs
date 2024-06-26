using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
//using MySql.Data.MySqlClient;
namespace ArtHandler.Classes
{
    public class ConcreteDatabaseOracle : Database
    {


        public override IDbConnection CreateConnection()
        {
            return new OracleConnection(connectionString);
            //return new MySqlConnection(connectionString);
        }
        public override IDbCommand CreateCommand()
        {
            return new OracleCommand();
            //return new MySqlCommand();
        }
        public override IDbConnection CreateOpenConnection()
        {
            OracleConnection connection = (OracleConnection)CreateConnection();
            //MySqlConnection connection = (MySqlConnection)CreateConnection();
            connection.Open();
            return connection;
        }
        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            OracleCommand command = (OracleCommand)CreateCommand();
            //MySqlCommand command = (MySqlCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (OracleConnection)connection;
            //command.Connection = (MySqlConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }
        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            OracleCommand command = (OracleCommand)CreateCommand();
            //MySqlCommand command = (MySqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (OracleConnection)connection;
            //command.Connection = (MySqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new OracleParameter(parameterName, parameterValue);
            //return new MySqlParameter(parameterName, parameterValue);
        }
    }
}