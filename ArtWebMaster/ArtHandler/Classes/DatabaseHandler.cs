using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArtHandler.Model;

namespace ArtHandler.Classes
{
    public class MyDatabaseHandler : DataWorker //Handles Database using Dataworker for all operations
    {
        public string GetUserDateOfBirth(string userId)
        {
            // Table to store the query results
            string dataOfBirth = string.Empty;

            using (IDbConnection connection = database.CreateOpenConnection())
            {
                using (IDbCommand command = database.CreateCommand("" + GenerateQuery() + "='" + userId + "'", connection))
                {
                    dataOfBirth = Convert.ToString(command.ExecuteScalar());
                }
            }
            return dataOfBirth;
        }
        public string GenerateQuery()
        {
            return "SELECT " + SingletonArtValidationDBContext.Instance.DBValidationContext.SColumnName + " FROM " + 
                SingletonArtValidationDBContext.Instance.DBValidationContext.ViewName + " WHERE " + 
                SingletonArtValidationDBContext.Instance.DBValidationContext.CColumnName + "";
        }
    }
}