using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArtHandler.DAL;
using ArtHandler.Model;
using ArtHandler.Repository;

namespace ArtHandler.Classes
{
    public class MyDatabaseHandler : DataWorker //Handles Database using Dataworker for all operations
    {
        public string GetUserDateOfBirth(string userId)
        {
            string dataOfBirth = string.Empty;
            try
            {
                // Table to store the query results               

                using (IDbConnection connection = database.CreateOpenConnection())
                {
                    Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, "MSSQL DBConnection : Success"));
                    using (IDbCommand command = database.CreateCommand("" + GenerateQuery() + "='" + userId + "'", connection))
                    {
                        Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, "DOB Query : " + GenerateUserQuery()));
                        dataOfBirth = Convert.ToString(command.ExecuteScalar());
                        Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, "DOB is : " + dataOfBirth));
                    }
                }                
            }
            catch(Exception ex)
            {
                Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, "SQL Database connectivity issue- DOB" + ex.ToString()));
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(userId,string.Empty,"SQL Database connectivity issue- DOB", ex, string.Empty);
            }
            return dataOfBirth;
        }
        public string GenerateQuery()
        {
            return "SELECT " + SingletonArtValidationDBContext.Instance.DBValidationContext.SColumnName + " FROM " + 
                SingletonArtValidationDBContext.Instance.DBValidationContext.ViewName + " WHERE " + 
                SingletonArtValidationDBContext.Instance.DBValidationContext.CColumnName + "";
        }

        //Check for non-Hexa users
        public string GetUser_LoginID(string Username)
        {
            string LoginID = string.Empty;
            try
            {
                using (IDbConnection connection = database.CreateOpenConnection())
                {
                    Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "MSSQL DBConnection : Success"));
                    using (IDbCommand command = database.CreateCommand("" + GenerateUserQuery() + "='" + Username + "'", connection))
                    {
                        Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "Mobiquity Query : " + GenerateUserQuery() + "='" + Username + "'"));
                        LoginID = Convert.ToString(command.ExecuteScalar());
                        Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "Username : "+ Username + " for UserID is : "+ LoginID));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "SQL Database connectivity issue- DOB"+ ex.ToString()));
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(Username, string.Empty, "SQL Database connectivity issue- DOB", ex, string.Empty);
            }
            return LoginID;
        }
        public string GenerateUserQuery()
        {
            return "SELECT " + SingletonArtValidationDBContext.Instance.DBValidationNonHexaContext.SColumnName + " FROM " +
            SingletonArtValidationDBContext.Instance.DBValidationNonHexaContext.ViewName + " WHERE " +
            SingletonArtValidationDBContext.Instance.DBValidationNonHexaContext.CColumnName + "";
        }
        //Check for non-Hexa users
    }
}