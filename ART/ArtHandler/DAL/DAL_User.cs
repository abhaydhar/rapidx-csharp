using ArtHandler.Model;
using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.IO;
using Newtonsoft.Json.Linq;
using ArtHandler.Interface;
using static ArtHandler.Repository.Utility;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

namespace ArtHandler.DAL
{
    public class DAL_User
    {

        /// <summary>
        /// To Get the user info
        /// </summary>
        /// <returns></returns>
        public string GetUserDateOfBirth(string userId)
        {
            try
            {
                ArtHandler.Classes.MyDatabaseHandler objDataBase = new Classes.MyDatabaseHandler();
                return objDataBase.GetUserDateOfBirth(userId);

                //// Table to store the query results
                //string dataOfBirth = string.Empty;

                //// Creates a SQL connection
                //using (SqlConnection connection = SqlConnector.OpenConnection())
                //{
                //    connection.Open();

                //    // Creates a SQL command
                //    using (var command = new SqlCommand("" + Constants.DOBQUERY + "='" + userId + "'", connection))
                //    {
                //        // Loads the query results into the table
                //        dataOfBirth = Convert.ToString(command.ExecuteScalar());
                //    }

                //    connection.Close();
                //}

                //return dataOfBirth;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Get the Account lock information , islocked 1 - is account locked and 
        /// waitTime will tell how many minutes user wants to wait
        /// </summary>
        /// <returns></returns>
        public AccountLockModelResponse GetAccountLockDetails(string userId)
        {
            try
            {
                DataSet dsLang = new DataSet();
                AccountLockModelResponse objAccountLock = new AccountLockModelResponse();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKACCOUNTLOCK, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("currentDatetime", DateTime.Now));

                    adapter.Fill(dsLang);
                }

                if (dsLang.Tables[0].Rows.Count > 0)
                {
                    objAccountLock.islocked = dsLang.Tables[0].Rows[0]["islocked"].ToString() == "1" ? true : false;
                    objAccountLock.waitTime = dsLang.Tables[0].Rows[0]["waitTime"].ToString();
                }
                return objAccountLock;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Used to log the user failure attempts , if the attempts are exceed the configured limit , it will automatically lock
        /// the user , and return as true, else false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ArtAccountLock(string userId, string sessionId, string activity, string status)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTARTACCOUNTLOCK, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("attempt_datetime", DateTime.Now));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_status", status));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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
        /// <summary>
        /// delete art user account lock logs
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteArtAccountLockLogs(string userId)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_DELETEUSERACCOUNTLOCKLOGS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        /// <summary>
        /// Enable the OTP option to the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isOtpEnabled"></param>
        /// <returns></returns>
        public bool EnableUserOTP(string userId, bool isOtpEnabled, bool isMobileNumberPrivate)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_ARTUSERREGISTEROTP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("isotp_enabled", isOtpEnabled));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("otpenabled_datetime", DateTime.Now));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("is_mobilenumber_private", isMobileNumberPrivate));
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
        /// <summary>
        /// Register the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isRegistered"></param>
        /// <returns></returns>
        public bool RegisterUser(string userId, bool isRegistered)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_ARTUSERREGISTER, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("is_registered", isRegistered));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("registered_datetime", DateTime.Now));
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
        /// <summary>
        /// Register the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isRegistered"></param>
        /// <returns></returns>
        public bool Enrollmentlink(string userId, bool isRegistered, string userkey, string sessionid)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_ArtUpdateEnrollLink, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("is_registered", isRegistered));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("registered_datetime", DateTime.Now));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_key", userkey));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("sessionid", sessionid));
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
        /// <summary>
        /// To Check the user enabled OTP
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserOTPEnabled(string userId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKUSEROTPENABLED, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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
        /// <summary>
        /// Check the user is registered
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserRegistered(string userId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKUSERREGISTERED, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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

        /// <summary>
        /// Insert the user otp for the session 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="otp"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool InsertUserOtp(string userId, string otp, string sessionId, string activity)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTUSEROTP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_otp", otp));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("otp_sentdatetime", DateTime.Now));

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

        /// <summary>
        /// To validate the user otp for the session id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="otp"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool ValidateUserOtp(string userId, string otp, string sessionId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_VALIDATEUSEROTP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_otp", otp));

                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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
        /// <summary>
        /// To validate the user otp for the session id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="otp"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool ValidateEnrollmentOtp(string userId, string otp, string sessionId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_VALIDATEENROLLOTP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_otp", otp));

                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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
        public bool InsertArtUserSMSSent(string userId, string mobileNum, DateTime sentdatetime, string messageId, string sessionId,
            string activity, string message)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTARTUSERSMSSENT, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mobile_number", mobileNum));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("sent_datetime", sentdatetime));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("msg_id", messageId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_message", message));
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

        public bool InsertArtUserOTPAttemptInfo(string userId, string sessionId, string activity)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_INSERTARTUSEROTPATTEMPTINFO, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("source_origin", Constants.Source_Orgin));

                    adapter.SelectCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool ResetArtUserOtpAttempts(string userId, string activity)
        {
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RESETARTUSEROTPATTEMPTS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));

                    adapter.SelectCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public UserOtpAttemptModel CheckUserOtpAttemptExceed(string userId, string activity)
        {
            try
            {
                DataSet ds = new DataSet();
                UserOtpAttemptModel objUserOtp = new UserOtpAttemptModel();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKUSEROTPATTEMPTEXCEED, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));

                    adapter.Fill(ds);
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objUserOtp.islocked = Convert.ToInt32(ds.Tables[0].Rows[0]["islocked"].ToString());
                    objUserOtp.attemptcount = ds.Tables[0].Rows[0]["attemptcount"].ToString();
                    objUserOtp.maxattempt = ds.Tables[0].Rows[0]["maxattempt"].ToString();
                }

                return objUserOtp;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public string CheckUnUsedOTPExist(string userId, string activity, string sessionId)
        {
            string otp = "0";
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKUNUSEDOTPEXIST, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("session_id", sessionId));

                    otp = Convert.ToString(adapter.SelectCommand.ExecuteScalar());
                }
                return otp;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return otp;
            }
        }

        /// <summary>
        /// To Update the user mobile number and country code
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="countryCode"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(string userId, string countryCode, string mobileNumber)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_UPDATEUSERINFO, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mobile_number", mobileNumber));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("country_code", countryCode));
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

        /// <summary>
        /// To Get the user Info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserModel> GetUserInfo(string userId)
        {
            try
            {
                DataSet dsUserInfo = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERINFO, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));

                    adapter.Fill(dsUserInfo);
                }

                List<UserModel> lstuserModel = (from rw in dsUserInfo.Tables[0].AsEnumerable()
                                                select new UserModel()
                                                {
                                                    id = Convert.ToInt32(rw["id"]),
                                                    userid = Convert.ToString(rw["userid"]),
                                                    mobilenumber = Convert.ToString(rw["mobilenumber"]),
                                                    countrycode = Convert.ToString(rw["countrycode"]),
                                                    ismobilenumberprivate = Convert.ToBoolean(rw["ismobilenumberprivate"])
                                                }).ToList();


                return lstuserModel;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public string GetUserEnrollmentDetails(string userkey, string sessionId)
        {
            string userId = string.Empty;
            try
            {
                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.Sp_GetUserEnrollmentKey, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_key", userkey));
                    userId = Convert.ToString(adapter.SelectCommand.ExecuteScalar());
                }
                return userId;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return userId;
            }
        }

        public DataSet GetUserEventsDetails(string userId)
        {
            try
            {
                DataSet dsUserEvents = new DataSet();


                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERLOCKSOURCE, con);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsUserEvents);
                }
                if (dsUserEvents.Tables.Count > 0 && dsUserEvents.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = GetArtEvents(dsUserEvents, userId);
                    dsUserEvents.Merge(ds);
                }
                return dsUserEvents;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        private DataSet GetArtEvents(DataSet dsUserEvents, string userId)
        {
            try
            {
                DataSet ds = new DataSet();
                string chkJson = string.Empty;
                DateTime eventDate;
                int eventID = 0;
                string EventDateformat = string.Empty;
                string eventId = string.Empty;

                if (dsUserEvents.Tables.Count > 0 && dsUserEvents.Tables[0].Rows.Count > 0)
                {
                    int cnt = dsUserEvents.Tables[0].Rows.Count;
                    string ipAddress = ConfigurationManager.AppSettings["ElasticsearchUrl"];
                    List<eventsData> lstevents = new List<eventsData>();
                    for (int i = 0; i < cnt; i++)
                    {
                        eventDate = Convert.ToDateTime(dsUserEvents.Tables[0].Rows[i]["EventTMP"]);
                        eventID = Convert.ToInt32(dsUserEvents.Tables[0].Rows[i]["EventID"]);
                        EventDateformat = eventDate.ToString(Constants.DateTimeformat);
                        // chkJson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n        \"must\":[     { \"match\": { \"UserID\": \"" + userId + "\" }},\r\n         { \"match\": { \"EventId\":403} },\r\n          { \"match\": {\"EventTimestamp\": \"" + EventDateformat + "\"}}\r\n        ]\r\n    }\r\n}\r\n}";
                        chkJson = "{  \"query\": {    \"bool\": {        \"must\":[     { \"match\": { \"UserID\": \"" + userId + "\" }},         { \"match\": { \"EventId\":403} },          { \"match\": {\"EventTimestamp\": \"" + EventDateformat + "\"}}        ]    }}}";
                        string response = PostAPI(ipAddress + "artwsevents/event/_search", chkJson);

                        Log.LogTrace(new CustomTrace(userId, "GetArtEvents", response));

                        JObject objRes = JObject.Parse(response);
                        int userCnt = ((JContainer)objRes.SelectToken("hits.hits")).Count;
                        Parallel.For(0, userCnt, k =>
                        {
                            eventsData obj = new eventsData();
                            obj.useragent = Convert.ToString(objRes.SelectToken("hits.hits[" + k + "]._source.UserAgent"));
                            obj.EventID = eventID;
                            obj.Browser = Convert.ToString(objRes.SelectToken("hits.hits[" + k + "]._source.EventSource"));
                            obj.Device = string.Empty;
                            obj.OS = string.Empty;
                            lstevents.Add(obj);
                        });
                    }
                    lstevents.Distinct();

                    ds = ConvertListToDataset(lstevents);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        private string PostAPI(string URL, string json)
        {
            string APIResult = string.Empty;
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            request.ContentType = "application/json";
            request.Method = "POST";

            request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    APIResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
            return APIResult;
        }
        static DataSet ConvertListToDataset(List<eventsData> records)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add(Constants.useragent, typeof(string));
            ds.Tables[0].Columns.Add(Constants.EventID, typeof(int));
            ds.Tables[0].Columns.Add(Constants.Device, typeof(string));
            ds.Tables[0].Columns.Add(Constants.OS, typeof(string));
            ds.Tables[0].Columns.Add(Constants.Browser, typeof(string));

            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    ds.Tables[0].Rows.Add(record.useragent, record.EventID, record.Device, record.OS, record.Browser);
                }
            }
            return ds;
        }
        /// <summary>
        /// 
        /// Substring for split useragent method.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>

        public static string GetSubstringByString(string a, string b, string c)
        {
            string result = string.Empty;
            try
            {
                result = c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
            }
            catch
            {
                return result = c;
            }
            return result;
        }

        /// <summary>
        /// Check the user is registered
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetUserEventSourceLink(string userId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.Sp_CheckEventSource, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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

        /// <summary>
        /// Check the user is registered
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserPasswordExpiry(string userId, bool IsPwdExp)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_UPDATEUSERPWDEXP, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("password_expiry", IsPwdExp));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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

        /// <summary>
        /// Check the user is registered
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetUserPasswordExpiry(string userId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.Sp_ArtUserPasswordExpiry, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    result = Convert.ToInt32(adapter.SelectCommand.ExecuteScalar());
                }
                if (result == 1)
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

        /// <summary>
        /// Send Email when exception occurs.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void SendMailUsingSMTP(string userId, string srId,string header, Exception ex, string inputs, string customMessage = null, string customType = null)
        {
            string body = GenerateBody(userId,srId, header, ex, inputs, customMessage, customType);
            IConfigProvider objConfig = new ConfigProvider();
            string mailFrom = objConfig.GetConfigValue(ConfigKeys.SENDEREMAIL);// ConfigurationManager.AppSettings[ConfigKeys.SENDEREMAIL];
            string recipients = objConfig.GetConfigValue(ConfigKeys.RECEIVEREMAIL); //ConfigurationManager.AppSettings[ConfigKeys.RECEIVEREMAIL];
            string ip = objConfig.GetConfigValue(ConfigKeys.EMAILIP); //ConfigurationManager.AppSettings[ConfigKeys.EMAILIP]; ;
            int port = Convert.ToInt32(objConfig.GetConfigValue(ConfigKeys.EMAILPORT)); //Convert.ToInt32(ConfigurationManager.AppSettings[ConfigKeys.EMAILPORT]);
            string userName = objConfig.GetConfigValue(ConfigKeys.EMAILUSERNAME); //ConfigurationManager.AppSettings[ConfigKeys.EMAILUSERNAME];
            string password = objConfig.GetConfigValue(ConfigKeys.EMAILPASSWORD); //ConfigurationManager.AppSettings[ConfigKeys.EMAILPASSWORD];
            string domain = objConfig.GetConfigValue(ConfigKeys.EMAILDOMAIN); //ConfigurationManager.AppSettings[ConfigKeys.EMAILDOMAIN];
            string encryPassword = Encryptor.Decrypt(password, Constants.hashkey);


            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo = null;
            SmtpClient objSmtpClient = new SmtpClient();
            try
            {
                objMailMessage.From = new MailAddress(mailFrom);
                objMailMessage.To.Add(new MailAddress(recipients));

                objMailMessage.Subject = header;
                objMailMessage.Body = body;


                objSmtpClient = new SmtpClient(ip, port); /// Server IP"172.25.121.145"
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                objSMTPUserInfo = new System.Net.NetworkCredential(userName, encryPassword, domain);
                objSmtpClient.Credentials = objSMTPUserInfo;

                objSmtpClient.UseDefaultCredentials = false;
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
         (body, null, MediaTypeNames.Text.Html);
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                // Create a LinkedResource object for each embedded image
                LinkedResource lftpic = new LinkedResource(path + Constants.art_error_head, MediaTypeNames.Image.Jpeg);
                lftpic.ContentId = Constants.art_error_headId;
                avHtml.LinkedResources.Add(lftpic);

                LinkedResource rgtpic = new LinkedResource(path + Constants.art_ftr, MediaTypeNames.Image.Jpeg);
                rgtpic.ContentId = Constants.art_ftrId;
                avHtml.LinkedResources.Add(rgtpic);

                objMailMessage.AlternateViews.Add(avHtml);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(null, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                throw;
            }
            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }

        }

        public string GenerateBody(string userId,string srId,string header, Exception ex, string inputs, string customMessage, string customType)
        {
            string body = string.Empty;
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                bool exists = System.IO.Directory.Exists(path + @"\art_error.html");
                //Log.Information("Path:" + path);
                string PageName = path + @"\art_error.html";
                body = new StreamReader(Path.GetFullPath(PageName)).ReadToEnd();
                body = body.Replace("{UserID}",userId);
                body = body.Replace("{SessionID}", System.Web.HttpContext.Current.Session.SessionID);               
                if (!string.IsNullOrEmpty(srId))
                {
                    
                    body = body.Replace("{RequestID}", "Request Id");
                    body = body.Replace("{SRID}", srId);

                }
                else
                {
                    body = body.Replace("{statusdisplay}", "none");
                    body = body.Replace("{RequestID}", "Request Id");
                    body = body.Replace("{SRID}", srId);

                }
                body = body.Replace("{Exp_Header}", header);
                if (ex != null)
                {
                    body = body.Replace("{Exp_Type}", ex.GetType().ToString());
                    body = body.Replace("{Exp_Message}", ex.Message.ToString());
                    body = body.Replace("{Exp_Trace}", ex.StackTrace.ToString() + "\n" + inputs);
                }
                else
                {
                    body = body.Replace("{Exp_Type}", customType);
                    body = body.Replace("{Exp_Message}", customMessage);
                    body = body.Replace("{Exp_Trace}", inputs);
                }
                //Log.Information("EmpID :" + objuser.EmpID, "EmpName:" + objuser.EmpName, "days:" + objuser.passwordAge);

            }
            catch (Exception exp)
            {
                Log.LogException(new CustomException(null, exp.Message.ToString(), exp.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return body = null;
            }
            return body;
        }

        //Check for non-Hexa users
        public string GetUser_LoginID(string Username)
        {
            try
            {
                ArtHandler.Classes.MyDatabaseHandler objDataBase = new Classes.MyDatabaseHandler();
                return objDataBase.GetUser_LoginID(Username);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        //Check for non-Hexa users
    }
}
