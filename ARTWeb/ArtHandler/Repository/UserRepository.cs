using ArtHandler.DAL;
using ArtHandler.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArtHandler.Repository
{
    public class UserRepository
    {
        string DomainName = SingletonLDAPSettings.Instance.LDAPSettings.DomainName;
        string DomainExtn = SingletonLDAPSettings.Instance.LDAPSettings.DomainExtn;
        string LdapPath = SingletonLDAPSettings.Instance.LDAPSettings.LdapConnectionPath;
        string NetUsername = SingletonLDAPSettings.Instance.LDAPSettings.LdapnetworkUsername;
        string NetUserCred = SingletonLDAPSettings.Instance.LDAPSettings.LdapNetworkUserPass;

        #region User
        public string GetUserDateOfBirth(string userId)
        {
            DAL_User objDALUser = new DAL_User();

            return objDALUser.GetUserDateOfBirth(userId);
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.GetAccountLockDetails(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.ArtAccountLock(userId, sessionId, activity, status);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.DeleteArtAccountLockLogs(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.EnableUserOTP(userId, isOtpEnabled, isMobileNumberPrivate);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.RegisterUser(userId, isRegistered);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool Enrollmentlink(string userId, bool isRegistered, string userkey, string sessionid)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.Enrollmentlink(userId, isRegistered, userkey, sessionid);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.CheckUserOTPEnabled(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.CheckUserRegistered(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.InsertUserOtp(userId, otp, sessionId, activity);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        /// <summary>
        /// To insert the SMS sent details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mobileNum"></param>
        /// <param name="sentdatetime"></param>
        /// <param name="messageId"></param>
        /// <param name="sessionId"></param>
        /// <param name="activity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool InsertArtUserSMSSent(string userId, string mobileNum, DateTime sentdatetime, string messageId, string sessionId,
          string activity, string message)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.InsertArtUserSMSSent(userId, mobileNum, sentdatetime, messageId, sessionId, activity, message);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.ValidateUserOtp(userId, otp, sessionId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool ValidateEnrollmentOtp(string userId, string otp, string sessionId)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.ValidateEnrollmentOtp(userId, otp, sessionId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool InsertArtUserOTPAttemptInfo(string userId, string sessionId, string activity)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.InsertArtUserOTPAttemptInfo(userId, sessionId, activity);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool ResetArtUserOtpAttempts(string userId, string activity)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.ResetArtUserOtpAttempts(userId, activity);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public UserOtpAttemptModel CheckUserOtpAttemptExceed(string userId, string activity)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.CheckUserOtpAttemptExceed(userId, activity);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public string CheckUnUsedOTPExist(string userId, string activity, string sessionId)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.CheckUnUsedOTPExist(userId, activity, sessionId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public bool UpdateUserInfo(string userId, string countryCode, string mobileNumber)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.UpdateUserInfo(userId, countryCode, mobileNumber);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public List<UserModel> GetUserInfo(string userId)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.GetUserInfo(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public string GetUserEventsDetails(string userId)
        {
            string eventSource = string.Empty;
            try
            {
                DAL_User objDALUser = new DAL_User();
                DataSet ds = objDALUser.GetUserEventsDetails(userId);

                if (ds != null)
                {
                    if (ds.Tables.Count >= 1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                string usergent = ds.Tables[1].Rows[i]["UserAgent"].ToString();
                                string result = GetSubstringByString("(", ")", usergent).ToString();
                                string[] device = result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray<string>();
                                ds.Tables[1].Rows[i]["Device"] = device[2].ToString();
                                ds.Tables[1].Rows[i]["OS"] = device[1].ToString();
                            }
                        }
                    }
                }
                eventSource = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return eventSource;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
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
        /// To Get the User Info from DB
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetUserInfoFromDB(string userid, string propertyName)
        {
            string value = string.Empty;
            List<UserModel> lstUserModel = GetUserInfo(userid);
            if (lstUserModel != null && lstUserModel.Count > 0)
            {
                if (propertyName == Constants.Mobile)
                {
                    if (!string.IsNullOrEmpty(lstUserModel[0].mobilenumber))
                        value = Utility.Encryptor.Decrypt(lstUserModel[0].mobilenumber, Constants.PASSPHARSE);
                }
                else if (propertyName == Constants.ADCOUNTRYCODE)
                {
                    if (!string.IsNullOrEmpty(lstUserModel[0].mobilenumber))
                        value = Utility.Encryptor.Decrypt(lstUserModel[0].countrycode, Constants.PASSPHARSE);
                }
            }
            return value;
        }

        /// <summary>
        /// Update password expiry notification in web user table
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserPasswordExpiry(string userId,bool IsPasswordExpiry)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.UserPasswordExpiry(userId, IsPasswordExpiry);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        #endregion

        #region Active Directory

        public bool Authenticate(string Username, string Otp)
        {
            bool result;
            try
            {
                bool isValid = false;
                Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName + "." + DomainExtn, DomainName + "\\" + NetUsername, NetUserCred))
                {
                    // validate the credentials
                    isValid = pc.ValidateCredentials(Username, Otp);
                }
                Log.LogTrace(new CustomTrace(Username, "User Authentication", isValid.ToString()));
                return isValid;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                result = false;
            }
            return result;
        }
        public bool CheckUserExists(string Username)
        {

            try
            {
                Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + Username + ")";
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    //DirectoryEntry userEntry = result.GetDirectoryEntry();
                    //userEntry.Properties["pwdLastSet"][0] = -1;
                    //userEntry.CommitChanges();
                    //userEntry.Close();
                    //userEntry.Dispose();

                    directoryEntry.Close();
                    directoryEntry.Dispose();
                    Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "True"));
                    return true;
                }
                else
                {
                    directoryEntry.Close();
                    directoryEntry.Dispose();
                    Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "False"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                DAL_User obj = new DAL_User();               
                obj.SendMailUsingSMTP(Username,string.Empty, "AD - Connectivity Issue while CheckUserExists", ex, string.Empty);
                Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool CheckExcemptedUser(string Username)
        {
            try
            {
                Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + Username + ")";
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    string transformedLDAPUrl = TransformLDAP(result.Path);
                    bool isExcemptedUser = CheckIsExemptedOU(transformedLDAPUrl);

                    directoryEntry.Close();
                    directoryEntry.Dispose();

                    Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "True"));
                    return isExcemptedUser;
                }
                else
                {
                    directoryEntry.Close();
                    directoryEntry.Dispose();

                    Log.LogTrace(new CustomTrace(Username, System.Reflection.MethodBase.GetCurrentMethod().Name, "False"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(Username,string.Empty,"AD - Connectivity Issue while CheckExcemptedUser ", ex, string.Empty);
                Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool ResetAndUnlock(string password, string userid, ref string message)
        {
            try
            {
                ADResetPassword objADPwdReset = new ADResetPassword();
                return objADPwdReset.ResetPassword(userid, password, ref message);

                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                //DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                //DirectorySearcher search = new DirectorySearcher(directoryEntry);
                //search.Filter = "(SAMAccountName=" + userid + ")";
                //SearchResult result = search.FindOne();
                //DirectoryEntry userEntry = result.GetDirectoryEntry();

                //userEntry.Properties["pwdLastSet"][0] = -1;

                //userEntry.Invoke("SetPassword", new object[] { password });
                //userEntry.Properties["LockOutTime"].Value = 0; //unlock account

                //userEntry.CommitChanges();
                //userEntry.Close();
                //userEntry.Dispose();
                //directoryEntry.Close();
                //directoryEntry.Dispose();
                //return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userid, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool CheckAccountIsLock(string userid)
        {
            try
            {
                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));
                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + userid + ")";
                SearchResult result = search.FindOne();
                bool isAccountLock = false;
                if (result != null)
                {
                    DirectoryEntry userEntry = result.GetDirectoryEntry();
                    isAccountLock = (bool)userEntry.InvokeGet("IsAccountLocked");

                    userEntry.Close();
                    userEntry.Dispose();
                }

                directoryEntry.Close();
                directoryEntry.Dispose();

                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, isAccountLock.ToString()));
                return isAccountLock;
            }
            catch (Exception ex)
            {
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(userid,string.Empty,"AD - Connectivity Issue while CheckAccountIsLock ", ex, string.Empty);
                Log.LogException(new CustomException(userid, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool UnlockAccount(string userid)
        {
            try
            {
                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));
                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + userid + ")";
                SearchResult result = search.FindOne();
                DirectoryEntry userEntry = result.GetDirectoryEntry();

                userEntry.Properties["LockOutTime"].Value = 0; //unlock account
                userEntry.Properties["pwdLastSet"][0] = -1;

                userEntry.CommitChanges();
                userEntry.Close();
                userEntry.Dispose();
                directoryEntry.Close();
                directoryEntry.Dispose();

                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, "True"));
                return true;
            }
            catch (Exception ex)
            {
                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, "True"));
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(userid,string.Empty,"AD - Connectivity Issue while Unlock Account", ex, string.Empty);
                Log.LogException(new CustomException(userid, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public string GetUserInfoFromAD(string userid, string propertyName)
        {
            try
            {
                string mobileNumber = string.Empty;

                List<UserModel> lstUserModel = GetUserInfo(userid);

                if (!string.IsNullOrEmpty(userid))
                {
                    bool isMobileNumberPrivate = false;

                    if (lstUserModel.Count > 0 && lstUserModel[0].ismobilenumberprivate)
                        isMobileNumberPrivate = true;

                    if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "N" && (propertyName == Constants.Mobile || propertyName == Constants.ADCOUNTRYCODE))
                    {
                        Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, propertyName));
                        return GetUserInfoFromDB(userid, propertyName);
                    }
                    else if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "Y" && isMobileNumberPrivate && (propertyName == Constants.Mobile || propertyName == Constants.ADCOUNTRYCODE))
                    {
                        Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, propertyName));
                        return GetUserInfoFromDB(userid, propertyName);
                    }

                    Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));

                    //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                    DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                    DirectorySearcher search = new DirectorySearcher(directoryEntry);
                    search.Filter = "(SAMAccountName=" + userid + ")";
                    SearchResult result = search.FindOne();
                    DirectoryEntry userEntry = result.GetDirectoryEntry();

                    if (result.Properties.Contains(propertyName))
                        if (result.Properties[propertyName].Count > 0)
                            mobileNumber = Convert.ToString(result.Properties[propertyName][0]);

                    userEntry.Close();
                    userEntry.Dispose();
                    directoryEntry.Close();
                    directoryEntry.Dispose();

                }
                Log.LogTrace(new CustomTrace(userid, System.Reflection.MethodBase.GetCurrentMethod().Name, propertyName + " : "+ mobileNumber));
                return mobileNumber;
            }
            catch (Exception ex)
            {
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(userid,string.Empty,"AD - Connectivity Issue while getting user information", ex, string.Empty);
                Log.LogException(new CustomException(userid, ex.Message.ToString() + ": User Id :" + userid + " : Propertyname: " + propertyName, ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public bool SetUserInfoInAD(string userId, string propertyName, string value)
        {
            try
            {
                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "N" && (propertyName == Constants.Mobile || propertyName == Constants.ADCOUNTRYCODE))
                {
                    return true;
                }

                Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, string.Format("AdDomain Name :{0}, username :{1} , password: {2}", DomainName, NetUsername, NetUserCred)));

                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + userId + ")";
                SearchResult result = search.FindOne();
                DirectoryEntry userEntry = result.GetDirectoryEntry();

                userEntry.Properties[propertyName].Value = value;
                userEntry.CommitChanges();

                userEntry.Close();
                userEntry.Dispose();
                directoryEntry.Close();
                directoryEntry.Dispose();
                Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, propertyName + " : " + value + " : " + "True"));
                return true;
            }
            catch (Exception ex)
            {
                Log.LogTrace(new CustomTrace(userId, System.Reflection.MethodBase.GetCurrentMethod().Name, propertyName + " : " + value + " : " + "False"));
                DAL_User obj = new DAL_User();
                obj.SendMailUsingSMTP(userId,string.Empty,"AD - Connectivity Issue while Updating User Information", ex, string.Empty);
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        private bool CheckIsExemptedOU(string url)
        {
            List<ExcemptedOUs> lstExcemptOU = SingletonExcemptedOUs.Instance.ExcemptedOUs;
            bool IsUserExempted = false;

            List<ExcemptedOUs> lstExcemptFilter = lstExcemptOU.Where(c => c.OUPath == url).ToList();
            if (lstExcemptFilter.Count > 0)
            {
                IsUserExempted = true;
            }
            else
            {
                IsUserExempted = false;
            }

            return IsUserExempted;
        }
        //transform the LDAP url to match the exempted url
        public string TransformLDAP(string ldapurl)
        {
            string transformedURL = string.Empty;
            StringBuilder strbul = new StringBuilder();
            try
            {
                string[] paths = ldapurl.Split(',');
                for (int i = 0; i < paths.Length; i++)
                {
                    if (paths[i].StartsWith("DC="))
                    {
                        strbul.Append(paths[i].Trim().Split('=').GetValue(1) + ".");
                    }
                }
                transformedURL = strbul.ToString();
                transformedURL = transformedURL.Substring(0, transformedURL.Length - 1);
                strbul = new StringBuilder();
                strbul.Append("/");
                for (int i = paths.Length - 1; i > 0; i--)
                {
                    if (paths[i].StartsWith("OU="))
                    {
                        strbul.Append(paths[i].Trim().Split('=').GetValue(1) + "/");
                    }
                }
                transformedURL += strbul.ToString();
                transformedURL = transformedURL.Substring(0, transformedURL.Length - 1);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(string.Empty, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
            return transformedURL;
        }

        public string GetUserEnrollmentDetails(string userkey, string sessionId)
        {
            DAL_User objDALUser = new DAL_User();

            return objDALUser.GetUserEnrollmentDetails(userkey, sessionId);
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
                DAL_User objDALUser = new DAL_User();
                return objDALUser.GetUserEventSourceLink(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool GetUserPasswordExpiry(string userId)
        {
            try
            {
                DAL_User objDALUser = new DAL_User();
                return objDALUser.GetUserPasswordExpiry(userId);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        //Check for non-Hexa users
        public string GetnonHexaUserID(string userId)
        {
            DAL_User objDALUser = new DAL_User();
            return objDALUser.GetUser_LoginID(userId);
        }
        //Check for non-Hexa users

        #endregion
    }
}
