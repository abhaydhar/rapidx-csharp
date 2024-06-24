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
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System.Net.Security;
using Newtonsoft.Json.Linq;

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
        public bool EnableUserOTP(string userId, bool isOtpEnabled)
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


        public bool InsertEnrollmentLink(string userId, string mobileno, DateTime date, string messageId, string sessionId, string activity, string tempbody, string status, string key, string sendOTP)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_InsertUserEnrollmentLink, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mobileNo", mobileno));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("otp_sentdatetime", DateTime.Now));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("message_Id", messageId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("Session_Id", sessionId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("tempbody", tempbody));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("status", status));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_key", key));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("sendOTP", sendOTP));


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
        /// To Check the user has access to GSD login
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RptUserModel> CheckUserAccess(string userId)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_CHECKUSERACCESS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.Fill(ds);
                }

                List<RptUserModel> lstUser = (from rw in ds.Tables[0].AsEnumerable()
                                              select new RptUserModel()
                                              {
                                                  UserId = Convert.ToString(rw["userid"]),
                                                  RoleName = Convert.ToString(rw["rolename"]),
                                                  Defaultlandingurl = Convert.ToString(rw["defaultlandingurl"]),
                                                  //Isadmin = Convert.ToBoolean(rw["isadmin"]),
                                                  IsReadOnly = Convert.ToBoolean(rw["isreadonly"]),
                                              }).ToList();

                return lstUser;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        /// To get the user question and answer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserQuestionAndAnswer> GetUserQuestionAndAnswer(string userId)
        {
            try
            {
                DataSet dsContryCodes = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERQUESTIONANDANSWER, con);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsContryCodes);
                }

                List<UserQuestionAndAnswer> lstUserQuestionAndAnswer = (from rw in dsContryCodes.Tables[0].AsEnumerable()
                                                                        select new UserQuestionAndAnswer()
                                                                        {
                                                                            Question = Convert.ToString(rw["Question"]),
                                                                            Answer = Convert.ToString(rw["Answer"]),
                                                                        }).ToList();


                return lstUserQuestionAndAnswer;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<UserEventsModelDateWise> GetUserEventsModelDateWise(UserEventsInput objUserEventInput)
        {
            try
            {
                DataSet dsUserEvents = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSEREVENTSDATEWISE, con);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", objUserEventInput.UserId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("stdate", objUserEventInput.StartDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", objUserEventInput.EndDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", objUserEventInput.Mode));

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsUserEvents);
                }

                List<UserEventsModelDateWise> lstUserEventsModelDateWise = (from rw in dsUserEvents.Tables[0].AsEnumerable()
                                                                            select new UserEventsModelDateWise()
                                                                            {
                                                                                Date = Convert.ToString(rw["Date"]),
                                                                                Attempts = Convert.ToString(rw["Attempts"]),
                                                                            }).ToList();


                return lstUserEventsModelDateWise;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<UserEventsModel> GetUserEventsDetails(UserEventsInput objUserEventInput)
        {
            try
            {
                DataSet dsUserEvents = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSEREVENTSDETAILS, con);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", objUserEventInput.UserId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("stdate", objUserEventInput.StartDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", objUserEventInput.EndDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", objUserEventInput.Mode));

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dsUserEvents);
                }

                List<UserEventsModel> lstUserEvents = (from rw in dsUserEvents.Tables[0].AsEnumerable()
                                                       select new UserEventsModel()
                                                       {
                                                           EventDate = Convert.ToString(rw["EventDate"]),
                                                           Eventdt = Convert.ToDateTime(rw["EventDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                                           EventSource = Convert.ToString(rw["EventSource"]),
                                                           UserId = Convert.ToString(rw["UserID"]),
                                                       }).ToList();


                return lstUserEvents;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To Get the user Info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 
        public List<UserAgentModel> GetUserAgentDetails(UserAgentInput objUserEventInput)
        {
            List<UserAgentModel> lstUserEvents = new List<UserAgentModel>();

            if (objUserEventInput.EventSource.Contains("TMG"))
                lstUserEvents = GetTMGUserAgentDetails(objUserEventInput);
            else
                lstUserEvents = GetADFSAgentDetails(objUserEventInput);


            return lstUserEvents;
        }

        private List<UserAgentModel> GetADFSAgentDetails(UserAgentInput objUserEventInput)
        {
            try
            {
                DataSet dsUserEvents = new DataSet();
                string dt = objUserEventInput.EventDate;
                //  dt.ToString("yyyy/MM/dd mm:hh");
                List<UserAgentModel> lstUserEvents = new List<UserAgentModel>();
                DateTime FromDateVal = Convert.ToDateTime(dt);
                string ipAddress = ConfigurationManager.AppSettings["ElasticsearchUrl"];
                string chkJson = string.Empty;
                string json = string.Empty;
                string retdata = string.Empty;
                DataSet dsevents = new DataSet();
                List<eventsData> lstevents = new List<eventsData>();
                String FromDate = FromDateVal.ToString(Constants.Dateformat, System.Globalization.CultureInfo.InvariantCulture);
                objUserEventInput.EventDate = FromDate;
                //using (MySqlConnection con = MySqlConnector.OpenConnection())
                //{
                //    MySqlDataAdapter adapter = new MySqlDataAdapter();
                //    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERAGENTDETAILS, con);
                //    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", objUserEventInput.UserId));
                //    adapter.SelectCommand.Parameters.Add(new MySqlParameter("eventdate", Convert.ToDateTime(FromDate)));
                //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                //    adapter.Fill(dsUserEvents);
                //}


                DateTime dtval = Convert.ToDateTime(FromDate);               
                dtval.ToString(Constants.DateTimeformat);
                // chkJson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n      \"must\": [\r\n          { \"match\": { \"EventId\": \"403\" } },\r\n          { \"match\": { \"UserID\": \"" + objUserEventInput.UserId + "\" } },\r\n  { \"match\": { \"EventTimestamp\": \"" + dtval.ToString(Constants.DateTimeformat) + "\"} }\r\n      ]\r\n    }\r\n  }\r\n}";
                 chkJson = "{  \"query\": {    \"bool\": {      \"must\": [          { \"match\": { \"EventId\": \"403\" } },          { \"match\": { \"UserID\": \"" + objUserEventInput.UserId + "\" } },  { \"match\": { \"EventTimestamp\": \"" + dtval.ToString(Constants.DateTimeformat) + "\"} }      ]    }  }}"; 
                string response = PostAPI(ipAddress + Constants.elasticURL, chkJson);
                JObject objRes = JObject.Parse(response);
                int eventsCnt = ((JContainer)objRes.SelectToken("hits.hits")).Count;
                Parallel.For(0, eventsCnt, i =>
                {
                    eventsData obj = new eventsData();
                    obj.UserID = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.UserID"));
                    obj.EventTimestamp = Convert.ToDateTime(objRes.SelectToken("hits.hits[" + i + "]._source.EventTimestamp"));
                    obj.EventSource = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.EventSource"));
                    obj.HostMachine = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.HostMachine"));
                    obj.EventDesc = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.EventDesc"));
                    obj.EventId = Convert.ToInt32(objRes.SelectToken("hits.hits[" + i + "]._source.EventId"));
                    obj.ClientIP = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.ClientIP"));
                    obj.ActivityID = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.ActivityID"));
                    obj.UserAgent = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.UserAgent"));
                    lstevents.Add(obj);

                });

                Char splitchar = ':';
                dsevents = ConvertListToDataset(lstevents);
                string userAgent = string.Empty;
                string activityID403 = string.Empty;
                activityID403 = dsevents != null && (dsevents.Tables.Count > 0 && dsevents.Tables[0].Rows.Count > 0) ? Convert.ToString(dsevents.Tables[0].Rows[0][Constants.ActivityID]) : "";
                userAgent = dsevents != null && (dsevents.Tables.Count > 0 && dsevents.Tables[0].Rows.Count > 0) ? Convert.ToString(dsevents.Tables[0].Rows[0][Constants.UserAgent]) : "";
                if (string.IsNullOrEmpty(userAgent))
                {
                    dsevents = getArtevents(activityID403, dtval, ipAddress, objUserEventInput.UserId);
                    userAgent = dsevents != null && (dsevents.Tables.Count > 0 && dsevents.Tables[0].Rows.Count > 0) ? Convert.ToString(dsevents.Tables[0].Rows[0]["UserAgent"]) : "";

                }
                if (!string.IsNullOrEmpty(userAgent))
                {
                    if (userAgent.Contains(Constants.Android))
                        dsevents.Tables[0].Rows[0][Constants.OSName] = Constants.Android;
                    else if (userAgent.Contains(Constants.Windows))
                        dsevents.Tables[0].Rows[0][Constants.OSName] = Constants.Windows;
                    else if (userAgent.Contains(Constants.Linux))
                        dsevents.Tables[0].Rows[0][Constants.OSName] = Constants.Linux;
                    else if (userAgent.Contains(Constants.iPhone))
                        dsevents.Tables[0].Rows[0][Constants.OSName] = Constants.iPhone;
                    else if (userAgent.Contains(Constants.Intel))
                        dsevents.Tables[0].Rows[0][Constants.OSName] = Constants.Intel;

                }
                dsUserEvents = dsevents.Copy();

                if (dsUserEvents.Tables.Count > 0 && dsUserEvents.Tables[0].Rows.Count >= 0)
                {
                    lstUserEvents = (from rw in dsUserEvents.Tables[0].AsEnumerable()
                                     let UserAgent = Convert.ToString(rw["UserAgent"])
                                     let result = GetSubstringByString("(", ")", UserAgent).ToString()
                                     select new UserAgentModel()
                                     {
                                         EventDate = Convert.ToString(rw["EventDate"]),
                                         substrings = Convert.ToString(rw["EventSource"]).Split(splitchar),
                                         device = result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray<string>(),
                                         deviceinfo = result,
                                         UserAgent = Convert.ToString(rw["UserAgent"]),
                                         OSname = Convert.ToString(rw["OSName"]),
                                         ClientIP = Convert.ToString(rw["ClientIP"]),
                                     }).ToList();
                }


                //  DataSet ds = new DataSet();



                return lstUserEvents;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public DataSet getArtevents(string activityID403, DateTime dtval, string ipAddress, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                string chkjson = string.Empty;
                //  chkjson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n      \"must\": [\r\n          { \"match\": { \"EventId\": 410} },\r\n  { \"match\": { \"EventTimestamp\": \"" + dtval.ToString(Constants.DateTimeformat) + "\"} },\r\n  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }\r\n      ]\r\n    }\r\n  }\r\n}";
                chkjson = "{  \"query\": {    \"bool\": {      \"must\": [          { \"match\": { \"EventId\": 410} },  { \"match\": { \"EventTimestamp\": \"" + dtval.ToString(Constants.DateTimeformat) + "\"} },  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }      ]    }  }}";

                string response = PostAPI(ipAddress + Constants.elasticURL, chkjson);
                JObject objRes = JObject.Parse(response);
                int Cnt = Convert.ToInt32(objRes.SelectToken("hits.total"));
                
                    string userAgent, clientIP = string.Empty;
                    userAgent = Cnt > 0 ? Convert.ToString(objRes.SelectToken("hits.hits[0]._source.UserAgent")) : "";
                    clientIP = Cnt > 0 ? Convert.ToString(objRes.SelectToken("hits.hits[0]._source.ClientIP")) : "";

                    if (!string.IsNullOrEmpty(userAgent))
                    {
                        //  chkjson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n      \"must\": [\r\n    { \"match\": { \"EventId\": 410} },\r\n  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }\r\n      ]\r\n    }\r\n  }\r\n}";
                        chkjson = "{  \"query\": {    \"bool\": {      \"must\": [    { \"match\": { \"EventId\": 410} },  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }      ]    }  }}";
                        response = PostAPI(ipAddress + Constants.elasticURL, chkjson);
                    }
                    else if (!string.IsNullOrEmpty(clientIP))
                    {
                        //chkjson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n      \"must\": [\r\n  { \"match\": { \"EventId\": 410} },\r\n  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }\r\n      ]\r\n    }\r\n  }\r\n}";
                        chkjson = "{  \"query\": {    \"bool\": {      \"must\": [  { \"match\": { \"EventId\": 410} },  { \"match\": { \"ActivityID\": \"" + activityID403 + "\"} }      ]    }  }}";
                        response = PostAPI(ipAddress + Constants.elasticURL, chkjson);
                    }
                    else
                    {
                        DateTime addOnemint = dtval.AddMinutes(1);
                        DateTime minsOnemint = dtval.AddMinutes(-1);
                        //chkjson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n      \"must\": [\r\n          { \"match\": { \"EventId\": 411} },\r\n          { \"match\": { \"UserID\": \"" + userId + "\" } },\r\n  { \"match\": { \"EventTimestamp\": \"" + dtval.ToString("yyyy-MM-ddTHH:mm:ss") + "\"} }\r\n      ]\r\n    }\r\n  }\r\n}";
                        // chkjson = "{\r\n\r\n  \"query\": {\r\n    \"bool\": {\r\n        \"must\":[     { \"match\": { \"UserID\": \"" + userId + "\" }},\r\n         { \"match\": { \"EventId\":411} },\r\n          { \"range\": {\"EventTimestamp\": {\"gte\": \"" + minsOnemint.ToString(Constants.DateTimeformat) + "\",\"lte\": \"" + addOnemint.ToString(Constants.DateTimeformat) + "\"}}}\r\n        ]\r\n    }\r\n}\r\n}";
                        chkjson = "{  \"query\": {    \"bool\": {        \"must\":[     { \"match\": { \"UserID\": \"" + userId + "\" }},         { \"match\": { \"EventId\":411} },          { \"range\": {\"EventTimestamp\": {\"gte\": \"" + minsOnemint.ToString(Constants.DateTimeformat) + "\",\"lte\": \"" + addOnemint.ToString(Constants.DateTimeformat) + "\"}}}        ]    }}}";
                        response = PostAPI(ipAddress + Constants.elasticURL, chkjson);
                    }
              

                objRes = JObject.Parse(response);
                Cnt = Convert.ToInt32(objRes.SelectToken("hits.total"));
                List<eventsData> lstevents = new List<eventsData>();
                Parallel.For(0, Cnt, i =>
                {
                    eventsData obj = new eventsData();
                    obj.UserID = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.UserID"));
                    obj.EventTimestamp = Convert.ToDateTime(objRes.SelectToken("hits.hits[" + i + "]._source.EventTimestamp"));
                    obj.EventSource = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.EventSource"));
                    obj.HostMachine = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.HostMachine"));
                    obj.EventDesc = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.EventDesc"));
                    obj.EventId = Convert.ToInt32(objRes.SelectToken("hits.hits[" + i + "]._source.EventId"));
                    obj.ClientIP = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.ClientIP"));
                    obj.ActivityID = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.ActivityID"));
                    obj.UserAgent = Convert.ToString(objRes.SelectToken("hits.hits[" + i + "]._source.UserAgent"));
                    obj.OSName = string.Empty;
                    lstevents.Add(obj);
                });
                lstevents.Distinct();
                ds = ConvertListToDataset(lstevents);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }


            return ds;
        }

        static DataSet ConvertListToDataset(List<eventsData> records)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add(Constants.UserID, typeof(string));
            ds.Tables[0].Columns.Add(Constants.EventDate, typeof(DateTime));
            ds.Tables[0].Columns.Add(Constants.EventSource, typeof(string));
            ds.Tables[0].Columns.Add(Constants.UserAgent, typeof(string));
            ds.Tables[0].Columns.Add(Constants.OSName, typeof(string));
            ds.Tables[0].Columns.Add(Constants.EventID, typeof(int));
            ds.Tables[0].Columns.Add(Constants.ClientIP, typeof(string));
            ds.Tables[0].Columns.Add(Constants.ActivityID, typeof(string));
            ds.Tables[0].Columns.Add(Constants.HostName, typeof(string));
            foreach (var record in records)
            {
                ds.Tables[0].Rows.Add(record.UserID, record.EventTimestamp, record.EventSource, record.UserAgent, record.OSName, record.EventId, record.ClientIP, record.ActivityID, record.HostMachine);
            }
            return ds;
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
        private List<UserAgentModel> GetTMGUserAgentDetails(UserAgentInput objUserEventInput)
        {
            DataSet dsUserEvents = new DataSet();
            List<UserAgentModel> lstUserEvents = new List<UserAgentModel>();
            try
            {
                string data = ConfigurationManager.ConnectionStrings["TMGSqlDBConnection"].ToString();
                SqlConnection connection = new SqlConnection(Utility.Encryptor.Decrypt(data, Constants.PASSPHARSE));

                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    SqlParameter param;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Constants.SP_GETUSERAGENTDETAILS;
                    param = new SqlParameter("@user_id", objUserEventInput.UserId);
                    command.Parameters.Add(param);
                    param = new SqlParameter("@eventdate", Convert.ToDateTime(objUserEventInput.EventDate));
                    command.Parameters.Add(param);
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(dsUserEvents);
                    Char splitchar = ':';
                    if (dsUserEvents.Tables.Count > 0 && dsUserEvents.Tables[0].Rows.Count > 0)
                    {
                        lstUserEvents = (from rw in dsUserEvents.Tables[0].AsEnumerable()
                                         let UserAgent = Convert.ToString(rw["UserAgent"])
                                         let result = GetSubstringByString("(", ")", UserAgent).ToString()
                                         select new UserAgentModel()
                                         {
                                             EventDate = Convert.ToString(rw["EventDate"]),
                                             substrings = Convert.ToString(rw["EventSource"]).Split(splitchar),
                                             device = result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray<string>(),
                                             deviceinfo = result,
                                             UserAgent = Convert.ToString(rw["UserAgent"]),
                                             OSname = Convert.ToString(rw["OSName"]),
                                         }).ToList();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }

            return lstUserEvents;

        }


        public List<UserInfoModel> GetUserInfo(string userId)
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

                List<UserInfoModel> lstuserModel = (from rw in dsUserInfo.Tables[0].AsEnumerable()
                                                    select new UserInfoModel()
                                                    {
                                                        id = Convert.ToInt32(rw["id"]),
                                                        userid = Convert.ToString(rw["userid"]),
                                                        mobilenumber = Convert.ToString(rw["mobilenumber"]),
                                                        countrycode = Convert.ToString(rw["countrycode"]),
                                                        ismobilenumberprivate = Convert.ToBoolean(rw["ismobilenumberprivate"]),
                                                        isregistered = Convert.ToBoolean(rw["isregistered"]),
                                                        isotpenabled = Convert.ToBoolean(rw["isotpenabled"])
                                                    }).ToList();


                return lstuserModel;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public DataSet GetUserActivity(UserEventsInput objUserEventInput)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETUSERACTIVITY, con);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", objUserEventInput.UserId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("stdate", objUserEventInput.StartDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", objUserEventInput.EndDate));
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(ds);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
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
    }
}
