#region UsingStatement
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
#endregion
namespace ART.Models
{

    //public class CustomException
    //{
    //    public CustomException(string User, string Exmsg, string Stackmsg)
    //    {
    //        this.UserId = User;
    //        this.ExceptionMessage = Exmsg;
    //        this.StackTrace = Stackmsg;
    //    }
    //    public string UserId { get; set; }
    //    public string ExceptionMessage { get; set; }
    //    public string StackTrace { get; set; }
    //}
    //public class CustomTrace
    //{
    //    public CustomTrace(string user, string message, string statmsg)
    //    {
    //        this.UserId = user;
    //        this.Message = message;
    //        this.Status = statmsg;
    //        this.SessionId = HttpContext.Current.Session.SessionID;
    //    }
    //    public string UserId { get; set; }
    //    public string Message { get; set; }
    //    public string Status { get; set; }
    //    public string SessionId { get; set; }
    //}
    //public static class Log
    //{
    //    public static bool WriteFile(string content)
    //    {
    //        try
    //        {
    //            DirectoryInfo ds = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Logs");
    //            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
    //                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");
    //            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
    //                File.Create(AppDomain.CurrentDomain.BaseDirectory + "Logs\\Log.txt").Close();
    //            else
    //            {
    //                File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + "Logs\\Log.txt", FileAttributes.Normal);
                
    //            }
    //                // Write single line to new file.
    //            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Logs\\Log.txt", true))
    //            {
    //                writer.WriteLine(content);
    //                writer.WriteLine("----------- END OF CONTENT -----------------");
    //            }

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }
    //    public static MySqlConnection OpenConnection(MySqlConnection Connection)
    //    {
    //        if (Connection.State == System.Data.ConnectionState.Closed || Connection.State == System.Data.ConnectionState.Broken)
    //            Connection.Open();
    //        return Connection;
    //    }
    //    public static void LogException(CustomException ex)
    //    {
    //        try
    //        {
    //            string data = ConfigurationManager.ConnectionStrings["Dbconnection"].ToString();
    //            MySqlConnection LogConnect = OpenConnection(new MySqlConnection(data.ToString()));
    //            MySqlCommand logExep = new MySqlCommand("Sp_ExceptionInsert", LogConnect);
    //            logExep.CommandType = System.Data.CommandType.StoredProcedure;
    //            logExep.Parameters.Add(new MySqlParameter("UserId", Convert.ToInt32(ex.UserId)));
    //            logExep.Parameters.Add(new MySqlParameter("ExceptionMsg", ex.ExceptionMessage));
    //            logExep.Parameters.Add(new MySqlParameter("StackTraceMsg", ex.StackTrace));
    //            logExep.ExecuteNonQuery();
    //        }
    //        catch (MySqlException exep)
    //        {

    //        }
    //    }
    //    public static void LogTrace(CustomTrace ex)
    //    {
    //        try
    //        {
    //            string data = ConfigurationManager.ConnectionStrings["Dbconnection"].ToString();
    //            MySqlConnection LogConnect = OpenConnection(new MySqlConnection(data.ToString()));
    //            MySqlCommand logTrace = new MySqlCommand("Sp_TraceInsert", LogConnect);
    //            logTrace.CommandType = System.Data.CommandType.StoredProcedure;
    //            logTrace.Parameters.AddWithValue("UserID", ex.UserId);
    //            logTrace.Parameters.AddWithValue("Message", ex.Message);
    //            logTrace.Parameters.AddWithValue("TrStatus", ex.Status);
    //            logTrace.Parameters.AddWithValue("SessionId", ex.SessionId);
    //            logTrace.ExecuteNonQuery();
    //        }
    //        catch (Exception exep)
    //        {

    //        }
    //    }
    //}
}