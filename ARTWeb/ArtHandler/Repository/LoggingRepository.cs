using ArtHandler.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public static class LoggingRepository
    {
        /// <summary>
        /// To insert the change password log
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool InsertChangePasswordLog(string userId, string status,string sessionId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTCHANGEPASSWORDLOG, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("changedatetime", DateTime.Now));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("changestatus", status));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("source_origin", Constants.Source_Orgin));

                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public static bool InsertUserActivityLog(string userId, string sessionId, string userActivity, string stepno, string activityStatus, DateTime? startDatetime)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTUSERACTIVITYLOG, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", userActivity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("step_no", stepno));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("activity_status",activityStatus));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("start_datetime", startDatetime));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("is_internet", Utility.IsInternetRequest() ? "Yes" : "No"));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("source_origin", Constants.Source_Orgin));

                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public static bool UpdateUserActivityLog(string userId, string sessionId, string userActivity, string stepno, string activityStatus, DateTime? endDatetime)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_UPDATEUSERACTIVITYLOG, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", userActivity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("step_no", stepno));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("activity_status", activityStatus));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("end_datetime", endDatetime));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("is_internet", Utility.IsInternetRequest() ? "Yes" : "No"));

                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public static void ITSMTraceLog(string itsmProvider, string request, string response, string ticketId,string method)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlCommand logExep = new MySqlCommand(Constants.SP_INSERTARTITSMTRACE, con);
                    logExep.CommandType = System.Data.CommandType.StoredProcedure;
                    logExep.Parameters.Add(new MySqlParameter("itsm_provider", itsmProvider));
                    logExep.Parameters.Add(new MySqlParameter("_request", request));
                    logExep.Parameters.Add(new MySqlParameter("_response", response));
                    logExep.Parameters.Add(new MySqlParameter("ticket_id", ticketId));
                    logExep.Parameters.Add(new MySqlParameter("_method", method));
                    logExep.Parameters.Add(new MySqlParameter("source_origin", Constants.Source_Orgin));

                    logExep.ExecuteNonQuery();
                }
            }
            catch (MySqlException exep)
            {

            }
        }
    }
}
