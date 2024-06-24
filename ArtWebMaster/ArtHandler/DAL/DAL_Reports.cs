using ArtHandler.Model;
using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.DAL
{
    public class DAL_Reports
    {
        /// <summary>
        /// get the user activity information from database.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public DataSet GetUserActivityGSDDashboard(string mode,string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERACTICITYGSDDASHBOARD, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDate));
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Fetch the user registration details from database.
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserRegistrationGSDDashboard()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETUSERREGISTRATIONGSDDASHBOARD, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public DataSet GetUserRegistrationByMonth()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETUSERREGISTRATIONBYMONTH, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public bool InsertGSDLog(string userid, string activity, string gsduserId)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTINSERTGSDLOG, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userid));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_activity", activity));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("gsd_userid", gsduserId));
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

        public DataSet GetGSDActivityForDashboard(string mode,string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETGSDACTIVITYFORDASHBOARD, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDate));


                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public DataSet GetGSDActivityLog(string mode,string startDt,string endDt)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETGSDACTIVITYLOG, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDt));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDt));


                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<FrequentAccountlockoutUser> GetFrequentUserLock(string mode,string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETFREQUENTUSERLOCK, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDate));

                    adapter.Fill(ds);
                }
                List<FrequentAccountlockoutUser> lstUser = (from rw in ds.Tables[0].AsEnumerable()
                                                                  select new FrequentAccountlockoutUser()
                                                              {
                                                                  UserId = Convert.ToString(rw["userId"]),
                                                                  Count = Convert.ToString(rw["lockcount"]),
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
        /// Get the userincompleteactivity information from database
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public DataSet GetUserIncompleteActivity(string mode,string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETUSERINCOMPLETEACTIVITY, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDate));

                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        #region GSD Dashboard

        /// <summary>
        /// To Get the account lock details month over month - GSD View Dashboard
        /// </summary>
        /// <returns></returns>
        public DataSet GetAccountlockByMonth()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETACCOUNTLOCKBYMONTH, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To Get the account lock details day wise - GSD View Dashboard
        /// </summary>
        /// <returns></returns>
        public DataSet GetAccountlockByDay()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETACCOUNTLOCKBYDAY, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Service Activity Information 
        /// </summary>
        /// <returns></returns>
        public DataSet GetGsdActivityByDay()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETGSDACTIVITYBYDAY, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public DataSet GetFrequentCallers()
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETFREQUENTCALLERS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To get the user privilege information
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<UserAccessPrivilege> GetUserPrivilege(string userid)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERPRIVILEGE, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userid));

                    adapter.Fill(ds);
                }
                List<UserAccessPrivilege> lstUser = (from rw in ds.Tables[0].AsEnumerable()
                                              select new UserAccessPrivilege()
                                              {
                                                  entityname = Convert.ToString(rw["entityname"])
                                                 // count = Convert.ToInt16(rw["count"])                                                
                                              }).ToList();

                return lstUser;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public List<DeviceLockout> GetDeviceLock(string mode, string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_RPTGETDEVICELOCKOUT, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("mode", mode));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("startDate", startDate));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("endDate", endDate));

                    adapter.Fill(ds);
                }
                List<DeviceLockout> lstUser = (from rw in ds.Tables[0].AsEnumerable()
                                                            select new DeviceLockout()
                                                            {
                                                                EventSource = Convert.ToString(rw["EventSource"]),
                                                                Count = Convert.ToString(rw["lockcount"]),
                                                            }).ToList();

                return lstUser;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(null, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        #endregion
    }
}
