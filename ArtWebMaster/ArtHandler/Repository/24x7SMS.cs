using ArtHandler;
using ArtHandler.DAL;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Repository;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ArtHandler.Repository
{
    /// <summary>
    /// 24x7SMS gatewway
    /// </summary>
    public class SMS24x7 : ISms
    {
        private readonly string SenderID = string.Empty;
        private readonly string APIKey = string.Empty;
        private readonly string DomesticSName = string.Empty;
        private readonly string IntSName = string.Empty;
        private readonly string OTPMsg = string.Empty;
        private readonly string PwdResetMsg = string.Empty;
        private readonly string RestURL = string.Empty;
        private readonly string PhoneNumber = string.Empty;
        private readonly string ArtQckEnrlOTP = string.Empty;
        private readonly string ARTQckEnrlKey = string.Empty;
        private readonly string SupportPwdResetMsg = string.Empty;


        private string sessionId = "Rest_Api_Reset_Msg"; //HttpContext.Current.Session.SessionID;
        private string EnrollsessionId = System.Web.HttpContext.Current.Session.SessionID;
        public SMS24x7()
        {
            using (DataTable dt = new DAL_Settings().GetSMSGateway(Constants.T24x7SMS))
            {
                if (dt.Rows.Count > 0)
                {
                    SenderID = Convert.ToString(dt.Rows[0]["SenderID"]);
                    APIKey = Convert.ToString(dt.Rows[0]["APIKey"]);
                    DomesticSName = Convert.ToString(dt.Rows[0]["DomesticServiceName"]);
                    IntSName = Convert.ToString(dt.Rows[0]["InternationalServiceName"]);
                    OTPMsg = Convert.ToString(dt.Rows[0]["OTPMsg"]);
                    RestURL = Convert.ToString(dt.Rows[0]["RestURL"]);
                    PhoneNumber = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
                    PwdResetMsg = Convert.ToString(dt.Rows[0]["PwdResetMsg"]);
                    ArtQckEnrlOTP = Convert.ToString(dt.Rows[0]["ArtQckEnrlOTP"]);
                    ARTQckEnrlKey = Convert.ToString(dt.Rows[0]["ARTQckEnrlKey"]);
                    SupportPwdResetMsg = Convert.ToString(dt.Rows[0]["SupportPwdResetMsg"]);

                }
            }
        }

        /// <summary>
        /// call the webapi to send sms
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string MakeWebRequestGET(string url)
        {
            string result;
            try
            {
                //Utility.WriteLog("SMS URL" + url);
                WebRequest WReq = WebRequest.Create(url);
                WebResponse wResp = WReq.GetResponse();
                StreamReader sr = new StreamReader(wResp.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
                wResp.Dispose();
                WReq = null;
            }
            catch (Exception ex)
            {
                result = "Failed: " + ex.Message;
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
            return result; //result gives you message id
        }

        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="corpID"></param>
        /// <param name="OTP"></param>
        /// <returns></returns>
        bool ISms.SendSMS(string userId, string mobileNo, string activity, string userName,string otp, string userCountryCode = null)
        {
            UserRepository objUserRepo = new UserRepository();

            if (userCountryCode == null)
            {
                string countryCode = objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);

                //if country code is empty from AD then it will be treated as Mobile number empty
                if (string.IsNullOrEmpty(countryCode))
                {
                    return false;
                }
                //Validate the include the country code
                mobileNo = new SettingsRepository().ValidateAndAddCountryCode(mobileNo.Trim(), countryCode);
            }
            else
            {

                mobileNo = userCountryCode + mobileNo;
            }


            //if Mobile number is empty
            if (string.IsNullOrEmpty(mobileNo))
            {
                Log.LogTrace(new CustomTrace(userId, activity, "Mobile Number is Empty"));
                return false;
            }

            //check the existing unused otp for the user / session and activity , if yes reuse, else create new one
            //string existingOtp = objUserRepo.CheckUnUsedOTPExist(userId, activity, sessionId);

            //string OTP = string.Empty;
            //if (!string.IsNullOrEmpty(existingOtp) && existingOtp == "0")
            //{
            //OTP = new PasswordGenerator().Generate();
            //}
            //else
            //{
            //    OTP = existingOtp;
            //}


            string responseMsg = string.Empty;
            string servicename = string.Empty;
            string MsgID = string.Empty;
            string tempBodyMsgFormat = SupportPwdResetMsg.Replace("{username}", userName).Replace("{otp}", otp).Replace("{phonenum}", PhoneNumber); //string.Empty;
            try
            {

                tempBodyMsgFormat = HttpUtility.UrlEncode(tempBodyMsgFormat);

                if (mobileNo.StartsWith("91") && (mobileNo.Length == 12))
                {
                    servicename = DomesticSName;
                }
                else
                {
                    servicename = IntSName;
                }
                string url = string.Format(RestURL, APIKey, mobileNo, SenderID, tempBodyMsgFormat, servicename);
                MsgID = MakeWebRequestGET(url);

                //log the otp attempts for locking 
                //bool isOtpAttemptInsert = objUserRepo.InsertArtUserOTPAttemptInfo(userId, sessionId, activity);

                if (MsgID.StartsWith("MsgID:"))
                {
                    // Insert for OTP check 
                    //bool result = objUserRepo.InsertUserOtp(userId, OTP, sessionId, activity);

                    // SMS sent log details
                    bool isSMSSent = objUserRepo.InsertArtUserSMSSent(userId, mobileNo, DateTime.Now, MsgID, sessionId, activity, tempBodyMsgFormat);

                    return true;
                }
                else
                {
                    Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, MsgID, url, System.Reflection.MethodBase.GetCurrentMethod().Name));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        string ISms.SendEnrollmentSMS(string userId, string mobileNo, string activity, string userName, string activityType, string SendOTP,string userCountryCode = null)
        {
            UserRepository objUserRepo = new UserRepository();

            if (userCountryCode == null)
            {
                string countryCode = objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);

                //if country code is empty from AD then it will be treated as Mobile number empty
                if (string.IsNullOrEmpty(countryCode))
                {
                    return "false";
                }
                //Validate the include the country code
                mobileNo = new SettingsRepository().ValidateAndAddCountryCode(mobileNo.Trim(), countryCode);
            }
            else
            {

                mobileNo = userCountryCode + mobileNo;
            }


            //if Mobile number is empty
            if (string.IsNullOrEmpty(mobileNo))
            {
                Log.LogTrace(new CustomTrace(userId, activity, "Mobile Number is Empty"));
                return "false";
            }
            string OTP = string.Empty;
            string Url = string.Empty;
            string key = string.Empty;
            string tempBodyMsgFormat = string.Empty;
            if (activityType == "SendOTP")
            {
                string existingOtp = objUserRepo.CheckUnUsedOTPExist(userId, activity, EnrollsessionId);
                if (!string.IsNullOrEmpty(existingOtp) && (existingOtp == "0" || existingOtp == ""))
                {
                    OTP = new PasswordGenerator("SendOTP").Generate("SendOTP");
                }
                else
                {
                    OTP = existingOtp;                    
                }

                tempBodyMsgFormat = ArtQckEnrlOTP.Replace("{username}", userName).Replace("{otp}", OTP).Replace("{phonenum}", PhoneNumber); //string.Empty;
            }
            else
            {
                OTP = new PasswordGenerator("EnrollmentLink").Generate("EnrollmentLink");             
                tempBodyMsgFormat = ARTQckEnrlKey.Replace("{username}", userName).Replace("{key}", OTP).Replace("{phonenum}", PhoneNumber); //string.Empty;                      
            }

            string responseMsg = string.Empty;
            string servicename = string.Empty;
            string MsgID = string.Empty;
            
            try
            {
               // tempBodyMsgFormat = PwdResetMsg.Replace("{username}", userName).Replace("{otp}", OTP).Replace("{phonenum}", PhoneNumber); //string.Empty;

                tempBodyMsgFormat = HttpUtility.UrlEncode(tempBodyMsgFormat);

                if (mobileNo.StartsWith("91") && (mobileNo.Length == 12))
                {
                    servicename = DomesticSName;
                }
                else
                {
                    servicename = IntSName;
                }
                string url = string.Format(RestURL, APIKey, mobileNo, SenderID, tempBodyMsgFormat, servicename);
                MsgID = MakeWebRequestGET(url);

                //log the otp attempts for locking 
                bool isOtpAttemptInsert = objUserRepo.InsertArtUserOTPAttemptInfo(userId, EnrollsessionId, activity);

                if (MsgID.StartsWith("MsgID:"))
                {                    // Insert for OTP check 
                    bool result = false;                
                    if (activityType == "EnrollmentLink")
                    {
                        bool isResgistered = objUserRepo.RegisterUser(userId, false);
                        result = objUserRepo.InsertEnrollmentLink(userId, mobileNo, DateTime.Now, MsgID, EnrollsessionId, activity, tempBodyMsgFormat, "InProgress", OTP, SendOTP);
                       
                    }
                    else
                    {
                        result = objUserRepo.InsertUserOtp(userId, OTP, EnrollsessionId, activity);
                    }
                    // SMS sent log details
                    bool isSMSSent = objUserRepo.InsertArtUserSMSSent(userId, mobileNo, DateTime.Now, MsgID, EnrollsessionId, activity, tempBodyMsgFormat);
                   
                    return OTP;
                }
                else
                {
                    Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, MsgID, url, System.Reflection.MethodBase.GetCurrentMethod().Name));
                    return "false";
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return "false";
            }
        }
             
    }
}