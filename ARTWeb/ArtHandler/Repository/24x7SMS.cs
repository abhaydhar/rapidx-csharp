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
        private readonly string RestURL = string.Empty;
        private readonly string PhoneNumber = string.Empty;
        private string sessionId = HttpContext.Current.Session.SessionID;
        private readonly string UnlockMsg = string.Empty;
        private readonly string PwdResetMsg = string.Empty;
        private readonly string EnrollmentMsg = string.Empty;

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
                    UnlockMsg = Convert.ToString(dt.Rows[0]["UnlockMsg"]);
                    PwdResetMsg = Convert.ToString(dt.Rows[0]["PwdResetMsg"]);
                    EnrollmentMsg = Convert.ToString(dt.Rows[0]["EnrollmentMsg"]);
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
        bool ISms.SendSMS(string userId, string mobileNo, string activity, string userName, string userCountryCode = null)
        {
            UserRepository objUserRepo = new UserRepository();
            string formattedUserName = FormatUserName(userName);
            string otpMessage = GetOTPMessage(activity);

            //log the mobile number 
            Log.LogTrace(new CustomTrace(userId, activity, "Mobile Number : " + Utility.Encryptor.Encrypt(mobileNo, Constants.PASSPHARSE)));

            //if Mobile number is empty
            if (string.IsNullOrEmpty(mobileNo))
            {
                Log.LogTrace(new CustomTrace(userId, activity, "Mobile Number is Empty"));
                return false;
            }

            if (userCountryCode == null)
            {
                string countryCode = objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);

                //if country code is empty from AD then it will be treated as Mobile number empty
                if (string.IsNullOrEmpty(countryCode))
                {
                    Log.LogTrace(new CustomTrace(userId, activity, "Country Code is Empty"));
                    return false;
                }
                //Validate the include the country code
                mobileNo = new SettingsRepository().ValidateAndAddCountryCode(mobileNo.Trim(), countryCode);
            }
            else
                mobileNo = userCountryCode + mobileNo;

            //check the existing unused otp for the user / session and activity , if yes reuse, else create new one
            string existingOtp = objUserRepo.CheckUnUsedOTPExist(userId, activity, sessionId);

            string OTP = string.Empty;
            if (!string.IsNullOrEmpty(existingOtp) && existingOtp == "0")
            {
                OTP = new PasswordGenerator().Generate();
            }
            else
            {
                OTP = existingOtp;
            }


            string responseMsg = string.Empty;
            string servicename = string.Empty;
            string MsgID = string.Empty;
            string tempBodyMsgFormat = otpMessage.Replace("{username}", formattedUserName).Replace("{otp}", OTP).Replace("{phonenum}", PhoneNumber); //string.Empty;
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

                //if Mobile number is empty
                if (string.IsNullOrEmpty(mobileNo))
                {
                    Log.LogTrace(new CustomTrace(userId, activity, "Mobile Number is Empty" + url));
                    return false;
                }

                MsgID = MakeWebRequestGET(url);

                //log the otp attempts for locking 
                bool isOtpAttemptInsert = objUserRepo.InsertArtUserOTPAttemptInfo(userId, sessionId, activity);

                if (MsgID.StartsWith("MsgID:"))
                {
                    // Insert for OTP check 
                    bool result = objUserRepo.InsertUserOtp(userId, OTP, sessionId, activity);

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
        private string FormatUserName(string tempusername)
        {
            try
            {
                if (!string.IsNullOrEmpty(tempusername))
                {
                    var FirstSpaceString = tempusername.IndexOf(" ");
                    if (FirstSpaceString != -1)
                    {
                        var ShortName = tempusername.Substring(0, FirstSpaceString);
                        if (ShortName.Trim().Length > 3)
                            tempusername = ShortName.Length > Constants.NameLength ? ShortName.Substring(0, Constants.NameLength) : ShortName;
                        else
                            tempusername = tempusername.Length > Constants.NameLength ? tempusername.Substring(0, Constants.NameLength) : tempusername;
                    }
                    else
                    {
                        tempusername = tempusername.Length > Constants.NameLength ? tempusername.Substring(0, Constants.NameLength) : tempusername;
                    }

                }
                else
                {
                    tempusername = "User";
                }

                return tempusername;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return string.Empty;
            }
        }

        private string GetOTPMessage(string activity)
        {
            if (activity == Constants.RESET_PASSWORD)
            {
                return PwdResetMsg;
            }
            else if (activity == Constants.UNLOCK_ACCOUNT)
            {
                return UnlockMsg;
            }
            else if (activity == Constants.USER_REGISTER)
            {
                return EnrollmentMsg;
            }
            else
            {
                return OTPMsg;
            }
        }
    }
}