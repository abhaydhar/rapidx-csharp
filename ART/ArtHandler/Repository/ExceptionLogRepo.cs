using ArtHandler.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public static class Log
    {
        public static bool WriteFile(string content)
        {
            try
            {
                DirectoryInfo ds = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName);
                string fileNameFormat = DateTime.Now.ToString("yyyy-MM-dd");

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName);
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt"))
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt").Close();
                else
                {
                    File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt", FileAttributes.Normal);

                }
                // Write single line to new file.
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt", true))
                {
                    writer.WriteLine(content);
                    writer.WriteLine("----------- END OF CONTENT -----------------");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static MySqlConnection OpenConnection(MySqlConnection Connection)
        {
            if (Connection.State == System.Data.ConnectionState.Closed || Connection.State == System.Data.ConnectionState.Broken)
                Connection.Open();
            return Connection;
        }
        public static void LogException(CustomException ex)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlCommand logExep = new MySqlCommand("Sp_ExceptionInsert", con);
                    logExep.CommandType = System.Data.CommandType.StoredProcedure;
                    logExep.Parameters.Add(new MySqlParameter("UserId", ex.UserId));
                    logExep.Parameters.Add(new MySqlParameter("ExceptionMsg", ex.ExceptionMessage));
                    logExep.Parameters.Add(new MySqlParameter("StackTraceMsg", ex.StackTrace));
                    logExep.Parameters.Add(new MySqlParameter("Method_Name", ex.MethodName));

                    logExep.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }
        public static void LogITSM(string userId, string userActivity, string itsmProvider, string incidentId, string sys_id)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlCommand logExep = new MySqlCommand(Constants.SP_INSERTARTITSMLOG, con);
                    logExep.CommandType = System.Data.CommandType.StoredProcedure;
                    logExep.Parameters.Add(new MySqlParameter("user_id", userId));
                    logExep.Parameters.Add(new MySqlParameter("user_activity", userActivity));
                    logExep.Parameters.Add(new MySqlParameter("itsm_provider", itsmProvider));
                    logExep.Parameters.Add(new MySqlParameter("incident_id", incidentId));
                    logExep.Parameters.Add(new MySqlParameter("sys_id", sys_id));
                    logExep.Parameters.Add(new MySqlParameter("source_origin", Constants.Source_Orgin));

                    logExep.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }
        public static void LogTrace(CustomTrace ex)
        {
            try
            {
                StringBuilder contentBuilder = new StringBuilder();

                contentBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " => ");
                contentBuilder.Append("UserID : " + ex.UserId + " ; ");
                contentBuilder.Append("Message : " + ex.Message + " ; ");
                contentBuilder.Append("TrStatus : " + ex.Status + " ; ");
                contentBuilder.Append("SessionId : " + ex.SessionId + " ; ");

                DirectoryInfo ds = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName);
                string fileNameFormat = DateTime.Now.ToString("yyyy-MM-dd") + "_TraceLog";

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName);
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt"))
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt").Close();
                else
                {
                    File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt", FileAttributes.Normal);

                }
                // Write single line to new file.
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Constants.LogFileFolderName + "\\" + fileNameFormat + ".txt", true))
                {
                    writer.WriteLine(contentBuilder.ToString());
                }

            }
            catch
            {
                //Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }

            //try
            //{
            //    using (MySqlConnection con = MySqlConnector.OpenConnection())
            //    {
            //        MySqlCommand logTrace = new MySqlCommand("Sp_TraceInsert", con);
            //        logTrace.CommandType = System.Data.CommandType.StoredProcedure;
            //        logTrace.Parameters.AddWithValue("UserID", ex.UserId);
            //        logTrace.Parameters.AddWithValue("Message", ex.Message);
            //        logTrace.Parameters.AddWithValue("TrStatus", ex.Status);
            //        logTrace.Parameters.AddWithValue("SessionId", ex.SessionId);
            //        logTrace.ExecuteNonQuery();
            //    }
            //}
            //catch (Exception exep)
            //{

            //}
        }
        public static void LogRequestorInfo(string userId, string sessionId, string hostName, string hostaddress, string useragent, string urlreferrer, string isMobileDevice)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlCommand logTrace = new MySqlCommand(Constants.SP_INSERTARTREQUESTORINFO, con);
                    logTrace.CommandType = System.Data.CommandType.StoredProcedure;
                    logTrace.Parameters.AddWithValue("user_hostname", hostName);
                    logTrace.Parameters.AddWithValue("user_hostaddress", hostaddress);
                    logTrace.Parameters.AddWithValue("user_agent", useragent);
                    logTrace.Parameters.AddWithValue("url_referrer", urlreferrer);
                    logTrace.Parameters.AddWithValue("user_id", userId);
                    logTrace.Parameters.AddWithValue("session_id", sessionId);
                    logTrace.Parameters.AddWithValue("req_datetime", DateTime.Now);
                    logTrace.Parameters.AddWithValue("Is_MobileDevice", isMobileDevice);
                    logTrace.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }

    }
}
