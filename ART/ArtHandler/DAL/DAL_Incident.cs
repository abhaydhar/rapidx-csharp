using ArtHandler.Model;
using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ArtHandler.DAL
{
    public class DAL_Incident
    {
        public string GetIncidentNo(string userId, string activity)
        {
            try
            {
                string result = string.Empty;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETINCIDENTNUM, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    result = Convert.ToString(adapter.SelectCommand.ExecuteScalar());
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return string.Empty;
            }
        }

        public string GetSysNo(string userId, string activity)
        {
            try
            {
                string result = string.Empty;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.Sp_GETSYSNUM, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    result = Convert.ToString(adapter.SelectCommand.ExecuteScalar());
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return string.Empty;
            }
        }
    }
}
