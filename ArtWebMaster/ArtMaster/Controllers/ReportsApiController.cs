using ArtHandler;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Repository;
using ArtMaster.ArtFilter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtMaster.Controllers
{
    [WebApiFilter]
    public class ReportsApiController : ApiController
    {
        // GET api/reportsapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/reportsapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/reportsapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/reportsapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reportsapi/5
        public void Delete(int id)
        {
        }
        /// <summary>
        /// To Get the User Infromation based on the EmpId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/GetUserInfo/{userId}")]
        [HttpGet]
        public UserModel GetUserInfo(string userId)
        {
            try
            {               
                UserModel objUserModel = new UserModel();
                UserRepository objUserRepo = new UserRepository();
                string isRegistered = "No";
                string isValidUser = string.Empty;

                List<UserInfoModel> lstUser = objUserRepo.GetUserInfo(userId);

                objUserModel.UserId = userId;
                isValidUser = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);
                if (isValidUser == string.Empty)
                {
                    objUserModel.IsInValid = true;
                    return objUserModel;
                }
                else
                {
                    objUserModel.IsInValid = false;
                }

                objUserModel.UserName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);
                objUserModel.MobileNumber = objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                objUserModel.UserAccountStatus = objUserRepo.CheckAccountIsLock(userId);
                objUserModel.UserDOB = objUserRepo.GetUserDateOfBirth(userId);
                objUserModel.PasswordExpired = objUserRepo.CheckUserPasswordExpired(userId) ? "Yes" : "No";
                if (objUserModel.PasswordExpired == "No")
                    objUserModel.PasswordAge = objUserRepo.GetUserPasswordAge(userId);
                //objUserModel.OUName = objUserRepo.GetUserOU(userId);
                else
                    objUserModel.PasswordAge = "-NA-";
                if (lstUser != null && lstUser.Count > 0)
                {
                    isRegistered = lstUser[0].isregistered ? "Yes" : "No";
                    objUserModel.IsOTPEnabled = lstUser[0].isotpenabled;
                }
                objUserModel.IsRegistered = isRegistered;
                return objUserModel;

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetUserQuestionAndAnswer/{userId}")]
        [HttpGet]
        public List<UserQuestionAndAnswer> GetUserQuestionAndAnswer(string userId)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                return objUserRepo.GetUserQuestionAndAnswer(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        [Route("~/GetUserEventsModelDateWise")]
        [HttpPost]
        public List<UserEventsModelDateWise> GetUserEventsModelDateWise(UserEventsInput objUserEventInput)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                return objUserRepo.GetUserEventsModelDateWise(objUserEventInput);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(objUserEventInput.UserId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        [Route("~/GetUserEventsDetails")]
        [HttpPost]
        public List<UserEventsModel> GetUserEventsDetails(UserEventsInput objUserEventInput)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                return objUserRepo.GetUserEventsDetails(objUserEventInput);

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(objUserEventInput.UserId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetUserAgentDetails")]
        [HttpPost]
        public List<UserAgentModel> GetUserAgentDetails(UserAgentInput objUserEventInput)
        {
            try
            {
                string[] eventsData;
                List<UserAgentModel> objuseragent = new List<UserAgentModel>();
                UserRepository objUserRepo = new UserRepository();
                if (!string.IsNullOrEmpty(objUserEventInput.Eventinput))
                {
                    eventsData = objUserEventInput.Eventinput.Split('/');
                    objUserEventInput.EventDate = (eventsData.Length>0 && eventsData[0].ToString() != "") ? eventsData[0].ToString() : "";
                    objUserEventInput.EventSource =(eventsData.Length>1 && eventsData[1].ToString() != "") ? eventsData[1].ToString() : "";
                }
                if(!string.IsNullOrEmpty(objUserEventInput.EventDate))
                objuseragent= objUserRepo.GetUserAgentDetails(objUserEventInput);

                return objuseragent;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(objUserEventInput.UserId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetUserActivity")]
        [HttpPost]
        public string GetUserActivity(UserEventsInput objUserEventInput)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                return objUserRepo.GetUserActivity(objUserEventInput);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(objUserEventInput.UserId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/ResetAndSendPassword/{userId}/{gsdUserId}/{mode}")]
        [HttpGet]
        public HttpResponseMessage ResetAndSendPassword(string userId, string gsdUserId, string mode)
        {
            try
            {
                string ITSMEnabledSupport = ConfigurationManager.AppSettings["Is_ITSM_Enabled_Support"].ToString();               
                Log.LogTrace(new CustomTrace(userId, "ResetAndSendPassword", "Employee Id:" + userId + ";GSD UserId:" + gsdUserId + ""));

                UserRepository objUserRepo = new UserRepository();
                ISms objSMS = new SMS24x7();
                string otp = string.Empty;

                string mobileNumber = objUserRepo.GetUserMobileNumber(userId);

               
                    otp = new PasswordGenerator().Generate();
                

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);
                string GSDuserName = objUserRepo.GetUserInfoFromAD(gsdUserId, Constants.ADGIVENNAME) + objUserRepo.GetUserInfoFromAD(gsdUserId, Constants.ADSN);

                bool isPasswordReset = objUserRepo.ResetAndUnlock(otp, userId);

                if (isPasswordReset)
                {
                    // if the mobile is empty skip the SMS sent
                    if (!string.IsNullOrEmpty(mobileNumber))
                    {
                        bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.ADMIN_RESET_PASSWORD, userName, otp);
                    }

                    ReportsRepository.InsertGSDLog(userId, Constants.RESET_PASSWORD, gsdUserId);

                    if (ITSMEnabledSupport=="Y")
                    {
                        string itsmProvider = string.Empty;

                        // Iitsmtool objItsm = new ITSM().GetITSMInstance(ref itsmProvider);
                        ITSM objItsm = new ITSM();

                        string sysid = string.Empty;

                        string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);
                        string gsdemail = objUserRepo.GetUserInfoFromAD(gsdUserId, Constants.Email);

                        if (!string.IsNullOrEmpty(email))
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "GSD Create " + itsmProvider + " ITSM incident - START"));

                            objItsm.CreateIncident(userId, Constants.RESET_PASSWORD, email, Constants.GSDPASSWORDCHANGEDESCRIPTION, Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Forgot_Password_Category, gsdemail, GSDuserName);

                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "GSD Resolved " + itsmProvider + " ITSM incident - END"));

                            //// Create a incident with resolved status in SNOW ITSM
                            //string itsmresult = objItsm.CreateIncident(userId, email, Constants.GSDPASSWORDCHANGEDESCRIPTION, Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION,
                            //    Singleton.Instance.ClientSessionID.Forgot_Password_Category,
                            //    Constants.GSDPASSWORDCHANGEDESCRIPTION, ref sysid);

                            //if (!string.IsNullOrEmpty(itsmresult))
                            //{
                            //    //log itsm ticket no
                            //    Log.LogITSM(userId, "GSD_" + Constants.RESET_PASSWORD, itsmProvider, itsmresult);

                            //    //log trace
                            //    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "GSD Create " + itsmProvider + " ITSM incident - END"));

                            //    //if (itsmProvider == Constants.ITSMSNOW)
                            //    //{
                            //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.GSDPASSWORDCHANGEDESCRIPTION, Constants.GSDPASSWORDCHANGEDESCRIPTION,
                            //        email, Singleton.Instance.ClientSessionID.Forgot_Password_Category, gsdemail, sysid);

                            //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "GSD Resolved " + itsmProvider + " ITSM incident - END"));
                            //    //}
                            //}
                        }
                    }
                }


                return this.Request.CreateResponse(HttpStatusCode.OK, new { Otp = otp, IsSuccess = true, Mode = mode });
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Otp = string.Empty, IsSuccess = false, Mode = mode });
            }
        }

        [Route("~/UnlockAccount/{userId}/{gsdUserId}")]
        [HttpGet]
        public bool UnlockAccount(string userId, string gsdUserId)
        {
            try
            {
                string ITSMEnabledSupport = ConfigurationManager.AppSettings["Is_ITSM_Enabled_Support"].ToString();
                Log.LogTrace(new CustomTrace(userId, "UnlockAccount", "Employee Id:" + userId + ";GSD UserId:" + gsdUserId + ""));

                UserRepository objUserRepo = new UserRepository();
                string GSDuserName = objUserRepo.GetUserInfoFromAD(gsdUserId, Constants.ADGIVENNAME);
                bool isAccUnlocked = objUserRepo.UnlockAccount(userId);

                if (isAccUnlocked)
                {
                    ReportsRepository.InsertGSDLog(userId, Constants.UNLOCK_ACCOUNT, gsdUserId);

                    if (ITSMEnabledSupport=="Y")
                    {
                        string itsmProvider = string.Empty;

                        //Iitsmtool objItsm = new ITSM().GetITSMInstance(ref itsmProvider);
                        ITSM objItsm = new ITSM();

                        string sysid = string.Empty;
                        string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);
                        string gsdemail = objUserRepo.GetUserInfoFromAD(gsdUserId, Constants.Email);


                        if (!string.IsNullOrEmpty(email))
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "GSD Create " + itsmProvider + " ITSM incident - START"));

                            objItsm.CreateIncident(userId, Constants.UNLOCK_ACCOUNT, email, Constants.GSDACCOUNTUNLOCKDESCRIPTION, Constants.ACCOUNTUNLOCKSHORTDESCRIPTION,
                                Singleton.Instance.ClientSessionID.Account_Unlock_Category, gsdemail, GSDuserName);

                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "GSD Resolved " + itsmProvider + " ITSM incident - END"));

                            //// Create a incident with resolved status in SNOW ITSM
                            //string itsmresult = objItsm.CreateIncident(userId, email, Constants.GSDACCOUNTUNLOCKDESCRIPTION,
                            //    Constants.ACCOUNTUNLOCKSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Account_Unlock_Category,
                            //    Constants.GSDACCOUNTUNLOCKDESCRIPTION, ref sysid);

                            //if (!string.IsNullOrEmpty(itsmresult))
                            //{
                            //    //log itsm ticket no
                            //    Log.LogITSM(userId, "GSD_" + Constants.UNLOCK_ACCOUNT, itsmProvider, itsmresult);

                            //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "GSD Create " + itsmProvider + " ITSM incident - END"));

                            //    //if (itsmProvider == Constants.ITSMSNOW)
                            //    //{
                            //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.GSDACCOUNTUNLOCKDESCRIPTION, Constants.GSDACCOUNTUNLOCKDESCRIPTION,
                            //        email, Singleton.Instance.ClientSessionID.Account_Unlock_Category, gsdemail, sysid);

                            //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "GSD Resolved " + itsmProvider + " ITSM incident - END"));
                            //    //}

                            //}
                        }
                    }

                }

                return isAccUnlocked;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        /// <summary>
        /// get the user activities information 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [Route("~/GetUserActivity/{userId}/{mode}/{startDate}/{endDate}")]
        [HttpGet]
        public string GetUserActivity(string userId, string mode, string startDate, string endDate)
        {
            try
            {
                startDate = startDate.Replace("|", ":");
                endDate = endDate.Replace("|", ":");
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetUserActivityGSDDashboard(userId, mode, startDate, endDate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// get the User Registration details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/GetUserRegistrationGSDDashboard/{userId}")]
        [HttpGet]
        public string GetUserRegistrationGSDDashboard(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetUserRegistrationGSDDashboard(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Month wise user registration deatils.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/GetUserRegistrationByMonth/{userId}")]
        [HttpGet]
        public string GetUserRegistrationByMonth(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetUserRegistrationByMonth(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To Get the User Activity Information by Date Wise
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>

        [Route("~/GetGSDActivity/{userId}/{mode}/{startDate}/{endDate}")]
        [HttpGet]
        public string GetGSDActivity(string userId, string mode, string startDate, string endDate)
        {
            try
            {
                startDate = startDate.Replace("|", ":");
                endDate = endDate.Replace("|", ":");
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetGSDActivityForDashboard(userId, mode, startDate, endDate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// get frequent user account lock details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [Route("~/GetFrequentUserLock/{userId}/{mode}/{startDate}/{endDate}")]
        [HttpGet]
        public string GetFrequentUserLock(string userId, string mode, string startDate, string endDate)
        {
            try
            {
                startDate = startDate.Replace("|", ":");
                endDate = endDate.Replace("|", ":");
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetFrequentUserLock(userId, mode, startDate, endDate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// get the incomplete activity information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [Route("~/GetUserInCompleteActivity/{userId}/{mode}/{startDate}/{endDate}")]
        [HttpGet]
        public string GetUserIncompleteActivity(string userId, string mode, string startDate, string endDate)
        {
            try
            {
                startDate = startDate.Replace("|", ":");
                endDate = endDate.Replace("|", ":");
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetUserIncompleteActivity(userId, mode, startDate, endDate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        /// <summary>
        ///To Get the account lock details month over month - GSD View Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/GetAccountlockByMonth/{userId}")]
        [HttpGet]
        public string GetAccountlockByMonth(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetAccountlockByMonth(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// To Get the account lock details by day - GSD View Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/GetAccountlockByDay/{userId}")]
        [HttpGet]
        public string GetAccountlockByDay(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetaccountlockbyDay(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        /// <summary>
        /// Get the Service Desk Activities information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [Route("~/GetGsdActivityByDay/{userId}")]
        [HttpGet]
        public string GetGsdActivityByDay(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetGsdActivityByDay(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetFrequentCallers/{userId}")]
        [HttpGet]
        public string GetFrequentCallers(string userId)
        {
            try
            {
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetFrequentCallers(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetUserGroupInfo/{userId}")]
        [HttpGet]
        public List<Tuple<string, string>> GetUserGroupInfo(string userId)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                return objUserRepo.GetUserGroupInfo(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        [Route("~/GetUserOUInfo/{userId}")]
        [HttpGet]
        public string GetUserOUInfo(string userId)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                return objUserRepo.GetUserOU(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }

        }

        [Route("~/GetBitLockerInfo/{userId}/{computerName}")]
        [HttpGet]
        public List<Tuple<string, string>> GetBitLockerInfo(string userId, string computerName)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                return objUserRepo.GetBitLockerInfo(userId, computerName);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }

        }
        [Route("~/GetDeviceLock/{userId}/{mode}/{startDate}/{endDate}")]
        [HttpGet]
        public string GetDeviceLock(string userId, string mode, string startDate, string endDate)
        {
            try
            {
                startDate = startDate.Replace("|", ":");
                endDate = endDate.Replace("|", ":");
                ReportsRepository objReportRepo = new ReportsRepository();
                return objReportRepo.GetDeviceLock(userId, mode, startDate, endDate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
    }
}
