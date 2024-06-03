#region UsingStatements
using ART.crypt;
using ART.Filters;
using ArtHandler;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Text;
using System.Configuration;
#endregion
namespace ART.Controllers
{
    [UserCultureFilter]
    public class HomeController : Controller
    {
        private string sessionId = System.Web.HttpContext.Current.Session.SessionID;
        string LdapPath = SingletonLDAPSettings.Instance.LDAPSettings.LdapConnectionPath;
        string DomainName = SingletonLDAPSettings.Instance.LDAPSettings.DomainName;
        string DomainExtn = SingletonLDAPSettings.Instance.LDAPSettings.DomainExtn;
        string AdContainer = SingletonLDAPSettings.Instance.LDAPSettings.LdapConnectionContainer;
        string NetUsername = SingletonLDAPSettings.Instance.LDAPSettings.LdapnetworkUsername;
        string NetUserCred = SingletonLDAPSettings.Instance.LDAPSettings.LdapNetworkUserPass;
        //
        // GET: /Home/
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = Constants.CACHE_DURATION)]
        public ActionResult Index()
        {
            ViewBag.Is_OTP_Enabled = Singleton.Instance.ClientSessionID.Is_OTP_Enabled;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(FormCollection Details)
        {
            string Message = string.Empty;
            UserRepository objUserRepo = new UserRepository();

            //Check for non-Hexa users
            var UserID = GetUserHexaorNonHexa(Details["inputEmail"].ToString());
            //var UserID = Details["inputEmail"].ToString();
            //Check for non-Hexa users

            var OTPhash = Details["OTPPassword"].ToString();
            var Passwordhash = Details["NewPassword"].ToString();
            try
            {
                #region ServerValidations

                string OTP = Crypto.DecryptStringAES(OTPhash);
                string Password = Crypto.DecryptStringAES(Passwordhash);

                if (string.IsNullOrEmpty(UserID))
                    Message = Art.MissingUserID;
                else
                {
                    bool isValidUserId = Utility.IsValidUserId(UserID);
                    if (!isValidUserId)
                    {
                        Message = Art.InvalidUserId;
                    }
                }

                //Check the current password is not empty
                if (string.IsNullOrEmpty(OTP))
                {
                    Message = Art.EnterCurrentPassword;
                }
                //check the current password length
                if (OTP.Length < SingletonPasswordSettings.Instance.PasswordSettings.MinLength || OTP.Length > SingletonPasswordSettings.Instance.PasswordSettings.MaxLength)
                {
                    Message = Art.PasswordLength;
                }
                //check the new password has atleast one caps 
                if (SingletonPasswordSettings.Instance.PasswordSettings.CapsLength > 0)
                {
                    if (!Password.Any(char.IsUpper))
                    {
                        Message = Art.PasswordPolicy;
                    }
                }
                //check the new password has atleast one small length
                if (SingletonPasswordSettings.Instance.PasswordSettings.SmallLength > 0)
                {
                    if (!Password.Any(char.IsLower))
                    {
                        Message = Art.PasswordPolicy;
                    }
                }
                //check the new password has atlease one numeric
                if (SingletonPasswordSettings.Instance.PasswordSettings.NumericLength > 0)
                {
                    if (!Password.Any(char.IsDigit))
                    {
                        Message = Art.PasswordPolicy;
                    }
                }

                //check the new password has specified special characters
                char[] allowedchars = SingletonPasswordSettings.Instance.PasswordSettings.AllowedSplChars.ToCharArray();
                char[] symbolsPresent = Regex.Replace(Password, "[0-9a-zA-Z]", "", RegexOptions.IgnoreCase).ToCharArray();
                bool noextracharfound = symbolsPresent.All(x => allowedchars.Contains(x));
                if (!noextracharfound)
                {
                    Message = Art.PasswordPolicy;
                }

                //check the new password length
                if (Password.Length < SingletonPasswordSettings.Instance.PasswordSettings.MinLength || Password.Length > SingletonPasswordSettings.Instance.PasswordSettings.MaxLength)
                    Message = Art.PasswordLength;

                if (Utility.ContainsHTML(UserID))
                    Message = Art.PasswordPolicy;

                if (Utility.ContainsHTML(OTP))
                    Message = Art.PasswordPolicy;

                if (Utility.ContainsHTML(Password))
                    Message = Art.PasswordPolicy;


                //Regex reg = new Regex(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{9,13}$");
                //if (!reg.IsMatch(Password))
                //    Message = Art.PasswordPolicy;
                #endregion
                if (!string.IsNullOrEmpty(Message))
                {
                    ViewBag.result = Message;
                    TempData["Message"] = Message;
                    TempData["IsPasswordChanged"] = false;
                    //return View("Index");

                    Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Change Password Error - " + Message));

                    return RedirectToAction("ChangePassword", "User");
                }
                if (objUserRepo.CheckUserExists(UserID))
                {
                    if (!objUserRepo.CheckExcemptedUser(UserID))
                    {
                        Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "User Existance - Success"));
                        if (objUserRepo.Authenticate(UserID, OTP))
                        {
                            Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "OTPValidation - Success"));
                            if (ResetAndUnlock(OTP, Password, UserID))
                            {
                                Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Change Password - Success"));

                                ArtHandler.Repository.LoggingRepository.InsertUserActivityLog(UserID, sessionId, Constants.CHANGE_PASSWORD, "1", Constants.COMPLETED, DateTime.Now);

                                ArtHandler.Repository.LoggingRepository.InsertChangePasswordLog(UserID, Constants.CHANGE_PASSWORD_SUCCESS, sessionId);

                                TempData["IsPasswordChanged"] = true;

                                if (Singleton.Instance.ClientSessionID.Is_ITSM_Enabled == "Y")
                                {
                                    string itsmProvider = string.Empty;

                                    Iitsmtool objItsm = new ITSM().GetITSMInstance(ref itsmProvider);

                                    string sysid = string.Empty;

                                    string email = new UserRepository().GetUserInfoFromAD(UserID, Constants.Email);
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        bool isIncidentCreate = new ITSM().CreateIncident(UserID, Constants.CHANGE_PASSWORD, email, Constants.PASSWORDCHANGEDESCRIPTION,
                                                     Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Change_Password_Category);

                                        //Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Create " + itsmProvider + " ITSM incident - START"));

                                        //// Create a incident with resolved status in SNOW ITSM
                                        //string itsmresult = objItsm.CreateIncident(UserID, email, Constants.PASSWORDCHANGEDESCRIPTION, Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Change_Password_Category, Constants.PASSWORDCHANGEDESCRIPTION, ref sysid);

                                        //if (!string.IsNullOrEmpty(itsmresult))
                                        //{
                                        //    //log itsm ticket no
                                        //    Log.LogITSM(UserID, Constants.CHANGE_PASSWORD, itsmProvider, itsmresult);

                                        //    Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Create " + itsmProvider + " ITSM incident - END"));

                                        //    //if (itsmProvider == Constants.ITSMSNOW)
                                        //    //{
                                        //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.PASSWORDCHANGEDESCRIPTION, Constants.PASSWORDCHANGEDESCRIPTION, email, Singleton.Instance.ClientSessionID.Change_Password_Category, sysid);

                                        //    Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Resolved " + itsmProvider + " ITSM incident - END"));
                                        //    //}
                                        //}
                                    }
                                    }
                                //return RedirectToAction("success");
                                return RedirectToAction("ChangePassword", "User");
                                //return View("Success");
                            }
                            else
                            {
                                ArtHandler.Repository.LoggingRepository.InsertChangePasswordLog(UserID, Constants.CHANGE_PASSWORD_FAILURE, sessionId);

                                Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "Change Password - Failure"));
                                Message = Art.PasswordHistoryErrMsg;
                                TempData["IsPasswordChanged"] = false;
                                TempData["Message"] = Message;
                            }
                        }
                        else
                        {
                            bool isAccountLock = new UserRepository().CheckAccountIsLock(UserID);
                            if (isAccountLock)
                            {
                                Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "User account is locked"));
                                Message = Art.AccountIsLocked;
                                TempData["IsPasswordChanged"] = false;
                                TempData["IsAccountLocked"] = "Y";
                                TempData["Message"] = Message;
                            }
                            else
                            {
                                Log.LogTrace(new CustomTrace(UserID, Constants.CHANGE_PASSWORD, "User Name / Password Validation - Failure"));
                                //if (ArtHandler.Model.Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                                //{
                                //    Message = Art.InvalidOTP;
                                //}
                                //else
                                //{
                                Message = Art.InvalidPassword;
                                //}

                                TempData["IsPasswordChanged"] = false;
                                TempData["Message"] = Message;
                            }
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(UserID, "Excempted User", "Failure"));
                        Message = Art.ExcemptedUserMsg;
                        TempData["IsPasswordChanged"] = false;
                        TempData["Message"] = Message;
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(UserID, "User Existance", "Failure"));
                    Message = Art.InvalidUser;
                    TempData["IsPasswordChanged"] = false;
                    TempData["Message"] = Message;
                }
                ViewBag.result = Message;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(UserID, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                Log.WriteFile("*****************" + DateTime.Now.ToLongDateString() + "****************" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString());
                if (ex.Source.ToString().Contains("System.DirectoryServices"))
                {
                    //return View("Failure");
                    return RedirectToAction("Failure");
                }
            }
            //return View("Index");
            HttpRuntime.Cache.Insert("ClearOutputCache", true);
            return RedirectToAction("ChangePassword", "User");
        }
        /// <summary>
        /// Success View Prevents Postback on Refresh
        /// </summary>
        /// <returns></returns>
        public ActionResult success()
        {
            ViewBag.result = "Set Password Successful";
            return View("Success");
        }
        /// <summary>
        /// Failure View Prevents Postback on Refresh
        /// </summary>
        /// <returns></returns>
        public ActionResult Failure()
        {
            return View("Failure");
        }
        /// <summary>
        /// Check if user is present in the AD
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool CheckUserExists(string Username)
        {

            try
            {

                //DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                DirectorySearcher search = new DirectorySearcher(directoryEntry);
                search.Filter = "(SAMAccountName=" + Username + ")";
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    DirectoryEntry userEntry = result.GetDirectoryEntry();
                    userEntry.Properties["pwdLastSet"][0] = -1;
                    userEntry.CommitChanges();
                    userEntry.Close();
                    userEntry.Dispose();
                    directoryEntry.Close();
                    directoryEntry.Dispose();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                Log.WriteFile("*****************" + DateTime.Now.ToLongDateString() + "****************" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString());
                if (ex.Source.ToString().Contains("System.DirectoryServices"))
                {
                    Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                    throw ex;
                }
                return false;
            }
        }
        /// <summary>
        /// method to authenticate otp
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Otp"></param>
        /// <returns></returns>
        public bool Authenticate(string Username, string Otp)
        {
            bool result;
            try
            {
                bool isValid = false;
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName + "." + DomainExtn))
                {
                    // validate the credentials
                    isValid = pc.ValidateCredentials(Username, Otp);
                }
                return isValid;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                Log.WriteFile("*****************" + DateTime.Now.ToLongDateString() + "****************" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString());
                if (ex.Source.ToString().Contains("System.DirectoryServices"))
                {
                    Log.LogException(new CustomException(Username, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                    throw ex;
                }
                result = false;
            }
            return result;
        }


        /// <summary>
        /// call this method to reset and unlock the account
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userid"></param>
        /// <param name="onlyUnlock"></param>
        /// <param name="isexception"></param>
        /// 
        public bool ResetAndUnlock(string oldpassword, string newPassword, string userid)
        {
            try
            {
                try
                {
                    PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName + "." + DomainExtn, GenerateContainerPath(LdapPath), ContextOptions.Negotiate, DomainName + "\\" + NetUsername, NetUserCred);
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userid);
                    up.ChangePassword(oldpassword, newPassword);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.LogTrace(new CustomTrace(userid, "ResetAndUnlock", ex.Message));
                    int result = ex.HResult; // -2146233087
                    return false;
                }

                ////DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://OU=Users,OU=Campus,DC=pwtest1,DC=com", @"pwtest1\32918", "Wks@060s@");
                //DirectoryEntry directoryEntry = new DirectoryEntry(LdapPath, DomainName + "\\" + NetUsername, NetUserCred);
                //DirectorySearcher search = new DirectorySearcher(directoryEntry);
                //search.Filter = "(SAMAccountName=" + userid + ")";
                //SearchResult result = search.FindOne();
                //DirectoryEntry userEntry = result.GetDirectoryEntry();

                //userEntry.Properties["pwdLastSet"][0] = -1;

                //userEntry.Invoke("SetPassword", new object[] { password });
                //userEntry.Properties["LockOutTime"].Value = 0; //unlock account
                ////userEntry.Properties["pwdLastSet"][0] = -1;

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
                Log.WriteFile("*****************" + DateTime.Now.ToLongDateString() + "****************" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString());

                if (ex.Source.ToString().Contains("System.DirectoryServices"))
                {
                    Log.LogException(new CustomException(userid, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                    throw ex;
                }
                return false;
            }
        }
        public string GenerateContainerPath(string fullHostName)
        {
            try
            {
                //"DC=corp,DC=hexaware,DC=com"
                string hostName = fullHostName.Replace("LDAP://", "");
                string[] hostArr = hostName.Split('.');
                StringBuilder strBul = new StringBuilder();

                foreach (string item in hostArr)
                {
                    strBul.Append("DC=" + item + ",");
                }

                return strBul.ToString().TrimEnd(',');
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(string.Empty, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Client, Duration = Constants.CACHE_DURATION)]
        public ActionResult HomePage()
        {
            Session.Clear();
            //Session.Abandon();
            Session.RemoveAll();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                //Request.Cookies["ASP.NET_SessionId"].Secure = true;
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            HttpCookie userlangCookie = Request.Cookies["ArtCookie"];
            // ViewBag.DateTime = DateTime.Now;

            if (userlangCookie == null || string.IsNullOrEmpty(userlangCookie.Values[Constants.ARTUSERLANG]))
            {
                HttpCookie Cookie = new HttpCookie("ArtCookie");

                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Default_Lang))
                {
                    ViewBag.DefaultLang = Singleton.Instance.ClientSessionID.Default_Lang;

                    Cookie.Values.Add(Constants.ARTUSERLANG, Singleton.Instance.ClientSessionID.Default_Lang); ;
                    Cookie.Expires = DateTime.MaxValue; // never expire
                    Cookie.Secure = true;
                    System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                }
                else
                {
                    ViewBag.DefaultLang = "en-US";

                    Cookie.Values.Add(Constants.ARTUSERLANG, "en-US");
                    Cookie.Expires = DateTime.MaxValue; // never expire
                    System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                }

                string UserLang = Server.HtmlEncode(Cookie.Values[Constants.ARTUSERLANG].ToString());
                Art.Culture = CultureInfo.GetCultureInfo(UserLang);
                Session[Constants.ARTUSERLANG] = UserLang;
            }
            else
            {
                string UserLang = Server.HtmlEncode(userlangCookie.Values[Constants.ARTUSERLANG]);
                Session[Constants.ARTUSERLANG] = UserLang;
                Art.Culture = CultureInfo.GetCultureInfo(UserLang);
                ViewBag.DefaultLang = UserLang;
            }

            Session["UserId"] = null;

            return View();
        }

        [HttpGet]
        public string CheckMultiLingualEnabled()
        {
            string isMultilingualEnabled = Singleton.Instance.ClientSessionID.Is_Multilingual_Enabled;

            return JsonConvert.SerializeObject(isMultilingualEnabled);
        }
        [HttpGet]
        public string GetOptions()
        {
            SettingsRepository objQuestionAnsRepo = new SettingsRepository();
            List<OptionsModel> lstSettings = objQuestionAnsRepo.GetOptions();

            return JsonConvert.SerializeObject(lstSettings);
        }
        [HttpGet]
        public string GetLanguages()
        {
            SettingsRepository objQuestionAnsRepo = new SettingsRepository();
            List<LanguageModel> lstSettings = objQuestionAnsRepo.Getlanguages();

            return JsonConvert.SerializeObject(lstSettings);
        }

        public ActionResult UserAccountLock()
        {
            AccountLockModelResponse objAccLock = new AccountLockModelResponse();
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            objAccLock = (AccountLockModelResponse)TempData["LockInfo"];
            if (objAccLock == null)
            {
                UserRepository objUserRepo = new UserRepository();
                string userId = Session["LockedUserId"].ToString();
                objAccLock = objUserRepo.GetAccountLockDetails(userId);
                if (!objAccLock.islocked)
                    return RedirectToAction("HomePage", "Home");
            }
            return View(objAccLock);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public string GetUserEventsDetails()
        {
            string userId = Convert.ToString(Session["UserId"]);

            UserRepository objUserRepo = new UserRepository();
           string lstSettings = objUserRepo.GetUserEventsDetails(userId);

            return JsonConvert.SerializeObject(lstSettings);
        }

        //Check for non-Hexa users
        public string GetUserHexaorNonHexa(string userName)
        {
            UserRepository objUserRepouser = new UserRepository();
            if (!Utility.IsNumberonly(userName))
            {
                userName = objUserRepouser.GetnonHexaUserID(userName);
            }
            return userName;
        }
        //Check for non-Hexa users


        [HttpPost]
        public JsonResult GetAppSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return Json(value);
        }

    }
}