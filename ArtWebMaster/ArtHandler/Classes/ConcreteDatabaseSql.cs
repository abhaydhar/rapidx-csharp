using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;
namespace ArtHandler.Classes
{
    public class ConcreteDatabaseSql :Database
    {


        public override IDbConnection CreateConnection()
        {
            //return new OracleConnection(connectionString);
            return new SqlConnection(connectionString);
        }
        public override IDbCommand CreateCommand()
        {
            //return new OracleCommand();
            return new SqlCommand();
        }
        public override IDbConnection CreateOpenConnection()
        {
            //OracleConnection connection = (OracleConnection)CreateConnection();
            SqlConnection connection = (SqlConnection)CreateConnection();
            connection.Open();
            return connection;
        }
        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            //OracleCommand command = (OracleCommand)CreateCommand();
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = commandText;
            //command.Connection = (OracleConnection)connection;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }
        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            //OracleCommand command = (OracleCommand)CreateCommand();
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            //command.Connection = (OracleConnection)connection;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            //return new OracleParameter(parameterName, parameterValue);
            return new SqlParameter(parameterName, parameterValue);
        }
    }
}