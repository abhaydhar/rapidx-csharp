using ArtHandler;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Repository;
using ART.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using ART.crypt;

namespace ART.Controllers
{
    [UserCultureFilter]
    public class UserController : Controller
    {
        private string sessionId = System.Web.HttpContext.Current.Session.SessionID;
        private bool IsMobileNumberPrivate = false;

        #region Default Views
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region User Login

        [RequestorLogFilter]
        [Route("Enroll/Login", Name = "Login")]
        public ActionResult Login()
        {
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ViewBag.IsAccountLocked = TempData["IsAccountLocked"];
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                //Check for non-Hexa users
                string userName = GetUserHexaorNonHexa(Convert.ToString(collection["txtUserName"]));
                //string userName = Convert.ToString(collection["txtUserName"]);
                //Check for non-Hexa users
                //string password = Convert.ToString(collection["txtPassword"]);
                string password = Crypto.DecryptStringAES(Convert.ToString(collection["txtPassword"]));

                if (string.IsNullOrEmpty(userName))
                {
                    TempData["UserMessage"] = Art.EnterUserName;
                    return RedirectToRoute("Login");
                }
                else
                {
                    bool isValidUserId = Utility.IsValidUserId(userName);
                    if (!isValidUserId)
                    {
                        TempData["UserMessage"] = Art.InvalidUserId;
                        return RedirectToRoute("Login");
                    }
                }
                if (string.IsNullOrEmpty(password))
                {
                    TempData["UserMessage"] = Art.EnterPassword;
                    return RedirectToRoute("Login");
                }

                Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "Started"));
                UserRepository objUserRepo = new UserRepository();

                //Check if the userid is exist
                bool isUserExist = objUserRepo.CheckUserExists(userName);
                if (isUserExist)
                {
                    AccountLockModelResponse objAccLock = objUserRepo.GetAccountLockDetails(userName);
                    if (objAccLock.islocked)
                    {
                        Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "check user account lock in ART tool - success - account is locked"));
                        Session["UserId"] = null;
                        Session["LockedUserId"] = userName;
                        TempData["LockInfo"] = objAccLock;
                        return RedirectToAction("UserAccountLock", "Home");
                    }

                    // Check the user is in excempted 
                    if (!objUserRepo.CheckExcemptedUser(userName))
                    {
                        // Validate user name / password
                        bool isValid = objUserRepo.Authenticate(userName, password);

                        if (isValid)
                        {
                            Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "Valid login"));

                            ArtHandler.Repository.LoggingRepository.InsertUserActivityLog(userName, sessionId, Constants.USER_REGISTER, "1", Constants.FINISHED, DateTime.Now);

                            //Remove lock 
                            bool isLogDeleted = objUserRepo.DeleteArtAccountLockLogs(userName);

                            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                            {
                                Session["UserId"] = userName;
                            }
                            else
                            {
                                if (Convert.ToString(Session["UserId"]) != userName)
                                {
                                    TempData["UserMessage"] = Art.DiffSessionMsg;
                                    //Session["UserId"] = null;
                                    //TempData["UserId"] = string.Empty;
                                    return RedirectToRoute("Login");
                                }
                            }

                            //Session["UserId"] = userName;
                            if (Singleton.Instance.ClientSessionID.Is_User_Info_Required == "Y")
                                return RedirectToRoute("RegisterUserInformation");
                            else
                            {
                                Session["UserRegStp2Complete"] = "Yes";
                                return RedirectToRoute("Register");
                            }
                        }
                        else
                        {
                            bool isAccLocked = objUserRepo.ArtAccountLock(userName, sessionId, Constants.Register_Login, "Invalid_Login");

                            if (isAccLocked)
                            {
                                Log.LogTrace(new CustomTrace(userName, Constants.USER_REGISTER, "account is locked"));

                                objAccLock = new AccountLockModelResponse() { islocked = true, waitTime = Singleton.Instance.ClientSessionID.Account_Lock_Duration };
                                TempData["LockInfo"] = objAccLock;
                                Session["LockedUserId"] = userName;
                                Session["UserId"] = null;
                                return RedirectToAction("UserAccountLock", "Home");
                            }

                            Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "InValid login"));
                            // check the user account is locked / else so invalid message
                            bool isAccountLocked = objUserRepo.CheckAccountIsLock(userName);
                            if (isAccountLocked)
                            {
                                Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "Account locked"));
                                TempData["IsAccountLocked"] = "Y";
                                TempData["UserMessage"] = Art.AccountIsLocked;
                            }
                            else
                            {
                                Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "Account not locked"));
                                TempData["UserMessage"] = Art.InvalidUserNamePassword;
                            }

                            Session["UserId"] = null;

                            return RedirectToRoute("Login");
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "Excempted User"));
                        TempData["UserMessage"] = Art.ExcemptedUserMsg;
                        return RedirectToRoute("Login");
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userName, Constants.Register_Login, "User Name Not Exist"));
                    TempData["UserMessage"] = Art.UserIdNotExist;
                    return RedirectToRoute("Login");
                }

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Login, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        #endregion

        #region User Register
        [SessionFilter]
        [RequestorLogFilter]
        [Route("Enroll/SecurityQuestions", Name = "Register")]
        public ActionResult Register()
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);
                ViewBag.UserId = userId;
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];

                RegisterModel objRegister = new RegisterModel();
                //int.parse change into convert.Toint and null check
                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Questions))
                    objRegister.TotalNumberOfQuestions = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Questions);
                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Register))
                    objRegister.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Register);
                objRegister.questions = new QuestionAnswerRepo().GetQuestions(userId);

                ViewBag.Message = TempData["UserMessage"];
                ViewBag.IsRegistered = TempData["IsRegistered"];

                Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Page Load"));

                return View(objRegister);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Security_Questions, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);
                QuestionAnswerRepo objQuestionAnswer;
                List<QuestionAnswerModel> lstQuestionAns;
                UserRepository objUserRepo = new UserRepository();

                string validateResult = ValidateUserQuestionAnswer(collection);
                if (!string.IsNullOrEmpty(validateResult))
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, validateResult));
                    TempData["IsRegistered"] = false;
                    TempData["UserMessage"] = "Please enter valid answer(s)";

                    return RedirectToAction("Register");
                }

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                GenerateQuestionAnsEntity(collection, out objQuestionAnswer, out lstQuestionAns);

                //insert user answer
                bool result = objQuestionAnswer.InsertQuestionAnswer(userId, lstQuestionAns);
                if (result)
                {
                    //trace the log
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Success"));

                    //Register
                    bool isResgistered = objUserRepo.RegisterUser(userId, result);

                    if (isResgistered)
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register User in ART - Success"));

                        ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.USER_REGISTER, "3", Constants.COMPLETED, DateTime.Now);

                        //once the user register get the email id from AD and send the confirmation email
                        string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);

                        if (!string.IsNullOrEmpty(email) && Singleton.Instance.ClientSessionID.Send_Email == "Y")
                        {
                            bool isEmailSent = new Email().SendHtmlFormattedEmail(userName, email);

                            if (isEmailSent)
                                Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register Email Sent" + email));
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register User in ART - Failure"));
                    }



                    TempData["IsRegistered"] = true;
                    TempData["UserMessage"] = "Dear " + userName + ", " + Art.RegisterSuccess;
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Failure"));
                    TempData["IsRegistered"] = false;
                    TempData["UserMessage"] = Art.RegisterFailure;
                }

                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Security_Questions, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        private void GenerateQuestionAnsEntity(FormCollection collection, out QuestionAnswerRepo objQuestionAnswer, out List<QuestionAnswerModel> lstQuestionAns)
        {
            objQuestionAnswer = new QuestionAnswerRepo();

            lstQuestionAns = new List<QuestionAnswerModel>();
            //int totalNumOfQuestions = int.Parse(collection["hdnTotalQuestions"].ToString());
            try
            {
                if (collection != null && !string.IsNullOrEmpty(collection["hdnTotalQuestionsUserAnswer"]))
                {
                    int totalQuestionsUserAnswer = Convert.ToInt32(Convert.ToString(collection["hdnTotalQuestionsUserAnswer"]));

                    for (int i = 0; i < totalQuestionsUserAnswer; i++)
                    {
                        QuestionAnswerModel objQuestionAns = new QuestionAnswerModel();
                        if (collection != null)
                        {
                            objQuestionAns.question_id = Convert.ToInt32(collection["Que_" + i]);
                            objQuestionAns.answer = collection["txtQueAns_" + i];
                            objQuestionAns.createdDate = DateTime.Now;
                        }

                        lstQuestionAns.Add(objQuestionAns);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("GenerateQuestionAnsEntity", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }

        }

        private string ValidateUserQuestionAnswer(FormCollection collection)
        {
            string result = string.Empty;
            int totalQuestionsUserAnswer = 0;
            try
            {
                List<QuestionAnswerModel> lstQuestionAns = new List<QuestionAnswerModel>();
                if (collection != null && !string.IsNullOrEmpty(collection["hdnTotalQuestionsUserAnswer"]))
                {
                    totalQuestionsUserAnswer = Convert.ToInt32(Convert.ToString(collection["hdnTotalQuestionsUserAnswer"]));

                    for (int i = 0; i < totalQuestionsUserAnswer; i++)
                    {
                        if (Convert.ToString(collection["txtQueAns_" + i]).Trim() == string.Empty)
                        {
                            result = "Please enter answer for all the questions";
                            break;
                        }
                        else
                        {
                            bool isHTMLPresent = Utility.ContainsHTML(Convert.ToString(collection["txtQueAns_" + i]));
                            if (isHTMLPresent)
                            {
                                result = "Please enter valid answer";
                                break;
                            }

                            if (Utility.CheckSpecialCharacter(collection["txtQueAns_" + i]))
                            {
                                result = "Please enter valid answer";
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ValidateUserQuestionAnswer", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                result = "";

            }
            return result;
        }

        #endregion

        #region Forgot Password
        public ActionResult ForgetPassword()
        {
            ViewBag.Message = TempData["UserMessage"];
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult ForgetPassword(FormCollection collection)
        {
            try
            {
                string error = string.Empty;

                bool result = CheckUserQuestionAnswer(collection, out error);

                if (result)
                    TempData["UserMessage"] = "Answer is correct";
                else
                    TempData["UserMessage"] = "Answer is wrong, please try again";

                return RedirectToAction("ForgetPassword");
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ForgetPassword", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return View();
            }
        }

        #endregion

        #region Reset Password
        [RequestorLogFilter]
        [Route("ForgotPassword", Name = "ForgotPassword")]
        public ActionResult ResetPassword()
        {
            //ViewBag.IsUserExist = TempData["IsUserExist"];
            ViewBag.UserId = TempData["UserId"];
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.IsAccountLocked = TempData["IsAccountLocked"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ResetModel objReset = new ResetModel();
            if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate))
                objReset.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate);

            return View(objReset);
        }


        [SessionFilter]
        [RequestorLogFilter]
        [Route("ForgotPassword/SecurityQuestions", Name = "FPQuestions")]
        public ActionResult ResetPasswordQuestions()
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                //ViewBag.IsUserExist = TempData["IsUserExist"];
                ViewBag.UserId = Session["UserId"];
                ViewBag.Message = TempData["UserMessage"];
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                Log.LogTrace(new CustomTrace(Convert.ToString(Session["UserId"]), Constants.Reset_Password_QA, "Reset Password Questions page load"));

                ResetModel objReset = new ResetModel();
                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate))
                    objReset.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate);
                return View(objReset);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordQuestions", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordQuestionAnswer(FormCollection collection)
        {
            try
            {
                string error = string.Empty;
                string userId = Convert.ToString(collection["hdnUserId"]);
                UserRepository objUserRepo = new UserRepository();

                Session["UserId"] = userId;
                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_QA, "Reset Password Questions submit"));

                bool result = CheckUserQuestionAnswer(collection, out error);

                if (result)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_QA, "Valid answer, now redirecting to Reset page"));

                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "3", Constants.FINISHED, null);

                    bool isLogDeleted = objUserRepo.DeleteArtAccountLockLogs(userId);

                    Session["IsValidAnswer"] = true;
                    return RedirectToAction("Reset");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_QA, "invalid answer"));
                    // if the questions answer is wrong then we proceed for account lock
                    if (error == Art.AnswerWrongErrMsg)
                    {

                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_QA, "check user account is locked"));

                        bool isAccLocked = objUserRepo.ArtAccountLock(userId, sessionId, Constants.RESET_PASSWORD, Constants.INVALID_ANSWER);

                        if (isAccLocked)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_QA, "account is locked"));

                            AccountLockModelResponse objAccLock = new AccountLockModelResponse() { islocked = true, waitTime = Singleton.Instance.ClientSessionID.Account_Lock_Duration };
                            TempData["LockInfo"] = objAccLock;
                            Session["LockedUserId"] = userId;
                            Session["UserId"] = null;
                            return RedirectToAction("UserAccountLock", "Home");
                        }
                    }

                    TempData["UserMessage"] = error;
                    Session["IsValidAnswer"] = false;

                    return RedirectToAction("ResetPasswordQuestions");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordQuestionAnswer", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("ForgotPassword/ResetPassword", Name = "FPResetPassword")]
        public ActionResult Reset()
        {
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.IsPasswordReset = TempData["IsPasswordReset"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ViewBag.UserId = Session["UserId"];
            Log.LogTrace(new CustomTrace(Convert.ToString(Session["UserId"]), Constants.Reset_Password, "Reset Password page load"));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionFilter]
        public ActionResult Reset(FormCollection collection)
        {
            try
            {
                string newpassword = Crypto.DecryptStringAES(Convert.ToString(collection["txtNewPassword"]));
                string confNewPassword = Crypto.DecryptStringAES(Convert.ToString(collection["txtConfNewPassword"]));

                //Validate newpassword and confirm password
                string message = CheckResetPasswordPolicy(newpassword, confNewPassword);

                if (!string.IsNullOrEmpty(message))
                {
                    TempData["UserMessage"] = message;
                    TempData["IsPasswordReset"] = false;
                    return RedirectToAction("Reset");
                }

                string userId = Convert.ToString(Session["UserId"]);

                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "STARTED"));
                string resetMessage = string.Empty;

                UserRepository objUserRepo = new UserRepository();
                bool result = objUserRepo.ResetAndUnlock(newpassword, userId, ref resetMessage);

                //Session["UserId"] = null;
                //Session["IsValidAnswer"] = null;

                if (result)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "SUCCESS"));

                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "4", Constants.COMPLETED, DateTime.Now);

                    if (Singleton.Instance.ClientSessionID.Is_ITSM_Enabled == "Y")
                    {

                        string email = new UserRepository().GetUserInfoFromAD(userId, Constants.Email);

                        if (!string.IsNullOrEmpty(email))
                        {
                            bool isIncidentCreate = new ITSM().CreateIncident(userId, Constants.RESET_PASSWORD, email, Constants.PASSWORDCHANGEDESCRIPTION,
                               Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Forgot_Password_Category);

                            //Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "Create " + itsmProvider + " ITSM incident - START"));
                            //// Create a incident with resolved status in SNOW ITSM
                            //string itsmresult = objItsm.CreateIncident(userId, email, Constants.PASSWORDCHANGEDESCRIPTION, Constants.CHANGEPASSWORDSNOWSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Forgot_Password_Category,
                            //    Constants.PASSWORDCHANGEDESCRIPTION, ref sysid);

                            //if (!string.IsNullOrEmpty(itsmresult))
                            //{
                            //    //log itsm ticket no
                            //    Log.LogITSM(userId, Constants.RESET_PASSWORD, itsmProvider, itsmresult);

                            //    //log trace
                            //    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "Create " + itsmProvider + " ITSM incident - END"));

                            //    //if (itsmProvider == Constants.ITSMSNOW)
                            //    //{
                            //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.PASSWORDCHANGEDESCRIPTION, Constants.PASSWORDCHANGEDESCRIPTION, email, Singleton.Instance.ClientSessionID.Forgot_Password_Category, sysid);

                            //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Resolved " + itsmProvider + " ITSM incident - END"));
                            //    //}
                            //}
                        }
                    }

                    TempData["UserMessage"] = Art.PasswordResetSuccess;
                    TempData["IsPasswordReset"] = true;

                    return RedirectToAction("Reset");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password, "FAILURE"));
                    if (string.IsNullOrEmpty(resetMessage))
                    {
                        TempData["UserMessage"] = Art.SomethingWentWrong;
                    }
                    else
                        TempData["UserMessage"] = Art.PasswordHistoryErrMsg;

                    TempData["IsPasswordReset"] = false;
                    return RedirectToAction("Reset");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("Reset", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckUserExist(FormCollection collection)
        {
            try
            {
                //Check for non-Hexa users
                string userId = GetUserHexaorNonHexa(Convert.ToString(collection["txtUserId"]));
                //string userId = Convert.ToString(collection["txtUserId"]);
                //Check for non-Hexa users
                //Empty Validation Check

                if (string.IsNullOrEmpty(userId.Trim()))
                {
                    TempData["UserMessage"] = Art.EnterUserId;
                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    bool isValidUserId = Utility.IsValidUserId(userId);
                    if (!isValidUserId)
                    {
                        TempData["UserMessage"] = Art.InvalidUserId;
                        return RedirectToAction("ResetPassword");
                    }
                }

                UserRepository objUserRepo = new UserRepository();
                QuestionAnswerRepo objQueAns = new QuestionAnswerRepo();

                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "Submit - check user exist - entry"));

                //Check if the userid is exist
                bool isUserExist = objUserRepo.CheckUserExists(userId);
                if (isUserExist)
                {
                    // Check the user is in excempted 
                    if (!objUserRepo.CheckExcemptedUser(userId))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user exist - success"));

                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account is locked - entry"));
                        //Check the user account is locked in AD or not
                        bool isAccountLock = objUserRepo.CheckAccountIsLock(userId);
                        if (isAccountLock)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account is locked - success"));
                            //TempData["IsUserExist"] = false;
                            TempData["UserMessage"] = Art.AccountIsLocked;
                            TempData["IsAccountLocked"] = "Y";
                            Session["UserId"] = null;
                            return RedirectToAction("ResetPassword");
                        }
                        else
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account is locked - failure"));

                            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user registered - entry"));
                            //Check if the user is already registered
                            bool isUserRegistered = objUserRepo.CheckUserRegistered(userId);
                            if (!isUserRegistered)
                            {
                                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user registered - failure"));
                                //TempData["IsUserExist"] = false;
                                TempData["UserMessage"] = Art.UserNotRegistered;
                                Session["UserId"] = null;
                                //TempData["UserId"] = string.Empty;
                                return RedirectToAction("ResetPassword");
                            }
                            else
                            {
                                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user registered - success"));

                                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account lock in ART tool - entry"));
                                AccountLockModelResponse objAccLock = objUserRepo.GetAccountLockDetails(userId);
                                if (objAccLock.islocked)
                                {
                                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account lock in ART tool - success - account is locked"));
                                    //TempData["IsUserExist"] = false;
                                    //TempData["UserMessage"] = Art.AccountIsLocked;
                                    Session["UserId"] = null;
                                    Session["LockedUserId"] = userId;
                                    //TempData["UserId"] = string.Empty;
                                    TempData["LockInfo"] = objAccLock;
                                    return RedirectToAction("UserAccountLock", "Home");
                                    //return RedirectToAction("ResetPassword");
                                }
                                else
                                {
                                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user account lock in ART tool - account is not locked"));

                                    //TempData["IsUserExist"] = isUserExist;
                                    if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                                    {
                                        Session["UserId"] = userId;
                                    }
                                    else
                                    {
                                        if (Convert.ToString(Session["UserId"]) != userId)
                                        {
                                            TempData["UserMessage"] = Art.DiffSessionMsg;
                                            //Session["UserId"] = null;
                                            //TempData["UserId"] = string.Empty;
                                            return RedirectToAction("ResetPassword");
                                        }
                                    }

                                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user OTP enabled - entry"));

                                    //TempData["UserId"] = userId;
                                    bool isUserOtpEnabled = objUserRepo.CheckUserOTPEnabled(userId);

                                    ArtHandler.Repository.LoggingRepository.InsertUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "1", Constants.FINISHED, DateTime.Now);

                                    if (Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                                    {
                                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user OTP enabled - OTP enabled - for both user and application level , now redirecting to ResetPasswordAuthType"));
                                        //return RedirectToAction("ForgotPasswordAuthType");
                                        Session["IsOtpEnabled"] = isUserOtpEnabled ? "Y" : "N";
                                        return RedirectToRoute("ForgotPasswordAuthType");
                                    }
                                    else
                                    {
                                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user OTP enabled - OTP NOT enabled - now redirecting to ResetPasswordQuestions"));
                                        //return RedirectToAction("ResetPasswordQuestions");
                                        return RedirectToRoute("FPQuestions");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "user excempted - failure"));
                        Session["UserId"] = null;
                        TempData["UserId"] = string.Empty;

                        TempData["UserMessage"] = Art.ExcemptedUserMsg;
                        return RedirectToAction("ResetPassword");
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_Login, "check user exist - failure"));
                    //TempData["IsUserExist"] = false;
                    Session["UserId"] = null;
                    TempData["UserId"] = string.Empty;

                    TempData["UserMessage"] = Art.UserIdNotExist;
                    return RedirectToAction("ResetPassword");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("CheckUserExist", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("ForgotPassword/OTP", Name = "FPOtp")]
        public ActionResult ResetPasswordOTP()
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);

                ViewBag.Message = TempData["UserMessage"];
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                UserRepository objUserRepo = new UserRepository();

                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.RESET_PASSWORD);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                }
                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "Page load"));
                return View();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordOTP(FormCollection collection)
        {
            try
            {
                string otp = Convert.ToString(collection["txtOtp"]);
                string userId = Convert.ToString(Session["UserId"]);
                UserRepository objUserRepo = new UserRepository();

                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "Submit - Validate user otp"));

                bool isValidOtp = objUserRepo.ValidateUserOtp(userId, otp, sessionId);
                if (isValidOtp)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "valid OTP, now redirecting to reset password"));

                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "3", Constants.FINISHED, null);

                    //to reset the otp attempts
                    bool resetArtOtpAttempts = objUserRepo.ResetArtUserOtpAttempts(userId, Constants.RESET_PASSWORD);

                    //to reset the account lock logs 
                    bool result = objUserRepo.DeleteArtAccountLockLogs(userId);

                    Session["IsValidAnswer"] = true;
                    return RedirectToRoute("FPResetPassword");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "Invalid OTP"));

                    TempData["UserMessage"] = Art.InvalidOTP;

                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "Check for ART user account lock"));
                    // if the user enter invalid OTP for configured time , we lock the account
                    bool isAccLocked = objUserRepo.ArtAccountLock(userId, sessionId, Constants.RESET_PASSWORD, Constants.INVALID_OTP);
                    if (isAccLocked)
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "user account is locked , now redirecting to account lock page"));

                        AccountLockModelResponse objAccLock = new AccountLockModelResponse() { islocked = true, waitTime = Singleton.Instance.ClientSessionID.Account_Lock_Duration };

                        TempData["UserMessage"] = string.Empty;
                        TempData["LockInfo"] = objAccLock;
                        Session["LockedUserId"] = userId;
                        Session["UserId"] = null;
                        return RedirectToAction("UserAccountLock", "Home");
                    }

                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_OTP, "user account is NOT locked , now refresh the page"));
                    return RedirectToRoute("FPOtp");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("ForgotPassword/AuthenticationType", Name = "ForgotPasswordAuthType")]
        public ActionResult ResetPasswordAuthType()
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                ViewBag.Message = TempData["UserMessage"];
                ViewBag.IsOtpEnabled = Session["IsOtpEnabled"];
                string userId = Convert.ToString(Session["UserId"]);

                //Check the user reached his maximum OTP attempts
                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.RESET_PASSWORD);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                }

                if (Session["IsOtpEnabled"] != null && Convert.ToString(Session["IsOtpEnabled"]) != "N")
                {
                    string mobileNumber = GetUserMobileNumber(userId);

                    ViewBag.MobileNumber = Utility.MaskMobile(mobileNumber, 0, "********");
                }
                Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthType, "Page load"));

                return View();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordAuthType", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordAuthOTP(FormCollection collection)
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);

                ISms objSMS = new SMS24x7();
                UserRepository objUserRepo = new UserRepository();

                //string mobileNumber = objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                string mobileNumber = GetUserMobileNumber(userId);

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.RESET_PASSWORD, userName);
                if (isSmsSent)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthOTP, "OTP sent , now redirecting to ResetPasswordOTP"));

                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "2", Constants.FINISHED, null);
                    return RedirectToRoute("FPOtp");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthOTP, "Sending OTP - Failure"));

                    TempData["UserMessage"] = Art.ErrorSendMsg;
                    return RedirectToRoute("ForgotPasswordAuthType");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordAuthOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordResendOTP(FormCollection collection)
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);

                ISms objSMS = new SMS24x7();
                UserRepository objUserRepo = new UserRepository();
                //string mobileNumber = objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                string mobileNumber = GetUserMobileNumber(userId);
                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.RESET_PASSWORD, userName);
                if (isSmsSent)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthOTP, "RESEND :: OTP sent , now redirecting to ResetPasswordOTP"));
                    return RedirectToRoute("FPOtp");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthOTP, "RESEND :: Sending OTP - Failure"));

                    TempData["UserMessage"] = Art.ErrorSendMsg;
                    return RedirectToRoute("FPOtp");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("ResetPasswordResendOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordAuthSecQue(FormCollection collection)
        {
            string userId = Convert.ToString(Session["UserId"]);
            Log.LogTrace(new CustomTrace(userId, Constants.Reset_Password_AuthSecQue, "Page load , now redirecting to ResetPasswordQuestions"));
            ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.RESET_PASSWORD, "2", Constants.FINISHED, null);
            return RedirectToRoute("FPQuestions");
        }

        #endregion

        #region Account Unlock

        [RequestorLogFilter]
        [Route("UnlockAccount", Name = "UnlockAccount")]
        public ActionResult AccountUnlock()
        {
            ViewBag.UserId = TempData["UserId"];
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];

            AccountUnlockModel objAccUnlock = new AccountUnlockModel();
            if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate))
                objAccUnlock.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate);

            return View(objAccUnlock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckUserAccountUnlockStatus(FormCollection collection)
        {
            try
            {
                //Check for non-Hexa users
                string userId = GetUserHexaorNonHexa(Convert.ToString(collection["txtUserId"]));
                //string userId = Convert.ToString(collection["txtUserId"]);
                //Check for non-Hexa users

                //Empty Validation Check
                if (string.IsNullOrEmpty(userId.Trim()))
                {
                    TempData["UserMessage"] = Art.EnterUserId;
                    return RedirectToAction("AccountUnlock");
                }
                else
                {
                    bool isValidUserId = Utility.IsValidUserId(userId);
                    if (!isValidUserId)
                    {
                        TempData["UserMessage"] = Art.InvalidUserId;
                        return RedirectToAction("AccountUnlock");
                    }
                }

                UserRepository objUserRepo = new UserRepository();
                QuestionAnswerRepo objQueAns = new QuestionAnswerRepo();

                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "Submit - check user exist - ENTRY"));
                //Check if the userid is exist
                bool isUserExist = objUserRepo.CheckUserExists(userId);

                if (isUserExist)
                {
                    // Check the user is in excempted 
                    if (!objUserRepo.CheckExcemptedUser(userId))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user exist - EXIST"));
                        //Check the user account is locked in AD or not

                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account is locked - ENTRY"));
                        bool isAccountLock = objUserRepo.CheckAccountIsLock(userId);
                        if (isAccountLock)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account is locked - LOCKED"));

                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user registered - ENTRY"));
                            //Check if the user is already registered
                            bool isUserRegistered = objUserRepo.CheckUserRegistered(userId);
                            if (!isUserRegistered)
                            {
                                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user registered - NOT REGISTERED"));
                                //TempData["IsUserExist"] = false;
                                TempData["UserMessage"] = Art.UserNotRegistered;
                                Session["UserId"] = null;
                                return RedirectToAction("AccountUnlock");
                            }
                            else
                            {
                                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user registered - REGISTERED"));

                                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account lock in ART tool - ENTRY"));

                                AccountLockModelResponse objAccLock = objUserRepo.GetAccountLockDetails(userId);
                                if (objAccLock.islocked)
                                {
                                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account lock in ART tool - success - ACCOUNT IS LOCKED"));
                                    Session["UserId"] = null;
                                    Session["LockedUserId"] = userId;
                                    TempData["LockInfo"] = objAccLock;
                                    return RedirectToAction("UserAccountLock", "Home");
                                }
                                else
                                {
                                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account lock in ART tool - account is not locked"));

                                    ArtHandler.Repository.LoggingRepository.InsertUserActivityLog(userId, sessionId, Constants.UNLOCK_ACCOUNT, "1", Constants.FINISHED, DateTime.Now);

                                    //TempData["IsUserExist"] = isUserExist;
                                    //Session["UserId"] = userId;

                                    if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                                    {
                                        Session["UserId"] = userId;
                                    }
                                    else
                                    {
                                        if (Convert.ToString(Session["UserId"]) != userId)
                                        {
                                            TempData["UserMessage"] = Art.DiffSessionMsg;
                                            //Session["UserId"] = null;
                                            //TempData["UserId"] = string.Empty;
                                            return RedirectToAction("AccountUnlock");
                                        }
                                    }

                                    bool isUserOtpEnabled = objUserRepo.CheckUserOTPEnabled(userId);
                                    if (Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                                    {
                                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user OTP enabled - OTP ENABLED - for both user and application level , now redirecting to AccountUnlockAuthType"));
                                        Session["IsOtpEnabled"] = isUserOtpEnabled ? "Y" : "N";
                                        return RedirectToRoute("UnlockAccountAuthType");
                                    }
                                    else
                                    {
                                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user OTP enabled - OTP NOT ENABLED - now redirecting to ResetPasswordQuestions"));
                                        return RedirectToRoute("UnlockAccountSecQuestion");
                                    }
                                }
                            }
                        }
                        else
                        {
                            //TempData["IsUserExist"] = false;
                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user account is locked - NOT LOCKED"));
                            TempData["UserMessage"] = Art.AccountNotLocked;
                            Session["UserId"] = null;
                            return RedirectToAction("AccountUnlock");
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "excempted user"));
                        //TempData["IsUserExist"] = false;
                        Session["UserId"] = null;
                        TempData["UserId"] = string.Empty;

                        TempData["UserMessage"] = Art.ExcemptedUserMsg;
                        return RedirectToAction("AccountUnlock");
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_Login, "check user exist - USER NAME NOT EXIST"));
                    //TempData["IsUserExist"] = false;
                    Session["UserId"] = null;
                    TempData["UserId"] = string.Empty;

                    TempData["UserMessage"] = Art.UserIdNotExist;
                    return RedirectToAction("AccountUnlock");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("CheckUserAccountUnlockStatus", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult UnlockAccount(FormCollection collection)
        {
            try
            {
                string error = string.Empty;
                UserRepository objUserRepo = new UserRepository();
                string userId = Convert.ToString(collection["hdnUserId"]);

                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "SUBMIT"));

                bool result = CheckUserQuestionAnswer(collection, out error);
                if (result)
                {


                    //delete user attempt lock logs
                    bool isLogDeleted = objUserRepo.DeleteArtAccountLockLogs(userId);

                    bool isAccUnlocked = objUserRepo.UnlockAccount(userId);

                    if (isAccUnlocked)
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Account Unlocked"));

                        ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.UNLOCK_ACCOUNT, "3", Constants.COMPLETED, DateTime.Now);
                        TempData["Unlocked"] = true;
                        TempData["UserMessage"] = Art.UnlockSuccess;

                        if (Singleton.Instance.ClientSessionID.Is_ITSM_Enabled == "Y")
                        {
                            string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);

                            if (!string.IsNullOrEmpty(email))
                            {
                                bool isIncidentCreate = new ITSM().CreateIncident(userId, Constants.UNLOCK_ACCOUNT, email, Constants.ACCOUNTUNLOCKDESCRIPTION,
                                    Constants.ACCOUNTUNLOCKSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Account_Unlock_Category);

                                //Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Create " + itsmProvider + " ITSM incident - START"));

                                //// Create a incident with resolved status in SNOW ITSM
                                //string itsmresult = objItsm.CreateIncident(userId, email, Constants.ACCOUNTUNLOCKDESCRIPTION, Constants.ACCOUNTUNLOCKSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Account_Unlock_Category, Constants.ACCOUNTUNLOCKDESCRIPTION, ref sysid);

                                //if (!string.IsNullOrEmpty(itsmresult))
                                //{
                                //    //log itsm ticket no
                                //    Log.LogITSM(userId, Constants.UNLOCK_ACCOUNT, itsmProvider, itsmresult);

                                //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Create " + itsmProvider + " ITSM incident - END"));

                                //    //if (itsmProvider == Constants.ITSMSNOW)
                                //    //{
                                //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.ACCOUNTUNLOCKDESCRIPTION, Constants.ACCOUNTUNLOCKDESCRIPTION, email, Singleton.Instance.ClientSessionID.Account_Unlock_Category, sysid);

                                //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Resolved " + itsmProvider + " ITSM incident - END"));
                                //    //}

                                //}
                            }
                        }
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "FAILURE"));

                        TempData["Unlocked"] = false;
                        TempData["UserMessage"] = Art.UnlockFailure;
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Wrong Answer"));
                    // if the questions answer is wrong then we proceed for account lock
                    if (error == Art.AnswerWrongErrMsg)
                    {
                        bool isAccLocked = objUserRepo.ArtAccountLock(userId, sessionId, Constants.UNLOCK_ACCOUNT, Constants.INVALID_ANSWER);
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Check the account is lock"));
                        if (isAccLocked)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "account is locked"));

                            AccountLockModelResponse objAccLock = new AccountLockModelResponse() { islocked = true, waitTime = Singleton.Instance.ClientSessionID.Account_Lock_Duration };
                            TempData["LockInfo"] = objAccLock;
                            Session["LockedUserId"] = userId;
                            Session["UserId"] = null;
                            return RedirectToAction("UserAccountLock", "Home");
                        }
                    }

                    TempData["UserMessage"] = error;
                }

                return RedirectToRoute("UnlockAccountSecQuestion");
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("UnlockAccount", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("UnlockAccount/SecurityQuestions", Name = "UnlockAccountSecQuestion")]
        public ActionResult AccountUnlockQuestions()
        {
            ViewBag.Unlocked = TempData["Unlocked"];
            ViewBag.UserId = Session["UserId"];
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            AccountUnlockModel objAccUnlock = new AccountUnlockModel();
            if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate))
                objAccUnlock.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate);

            return View(objAccUnlock);
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("UnlockAccount/AuthenticationType", Name = "UnlockAccountAuthType")]
        public ActionResult AccountUnlockAuthType()
        {
            try
            {
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                ViewBag.Message = TempData["UserMessage"];
                ViewBag.IsOtpEnabled = Session["IsOtpEnabled"];
                string userId = Convert.ToString(Session["UserId"]);
                UserRepository objUserRepo = new UserRepository();
                // Check event source details
                bool isUserEventSourceLink = objUserRepo.GetUserEventSourceLink(userId);
                ViewBag.isUserEventSourceLink = isUserEventSourceLink;

                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.UNLOCK_ACCOUNT);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                }

                if (Session["IsOtpEnabled"] != null && Convert.ToString(Session["IsOtpEnabled"]) != "N")
                {
                    string mobileNumber = GetUserMobileNumber(userId);

                    ViewBag.MobileNumber = Utility.MaskMobile(mobileNumber, 0, "********");
                }
                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthType, "Page load"));

                return View();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("AccountUnlockAuthType", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return View();
            }
        }

        [SessionFilter]
        [RequestorLogFilter]
        [Route("UnlockAccount/OTP", Name = "UnlockAccountOTP")]
        public ActionResult AccountUnlockOTP()
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                ViewBag.Unlocked = TempData["Unlocked"];
                ViewBag.Message = TempData["UserMessage"];

                UserRepository objUserRepo = new UserRepository();

                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.UNLOCK_ACCOUNT);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                }

                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Page load"));
                return View();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("AccountUnlockOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return View();
            }
        }

        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult AccountUnlockOTP(FormCollection collection)
        {
            try
            {
                string otp = Convert.ToString(collection["txtOtp"]);
                string sessionId = System.Web.HttpContext.Current.Session.SessionID;
                string userId = Convert.ToString(Session["UserId"]);
                UserRepository objUserRepo = new UserRepository();

                Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Submit - Validate user otp"));

                bool isValidOtp = objUserRepo.ValidateUserOtp(userId, otp, sessionId);
                if (isValidOtp)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "valid OTP"));

                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Unlocking user account - ENTRY"));

                    bool isAccUnlocked = objUserRepo.UnlockAccount(userId);

                    if (isAccUnlocked)
                    {
                        //to reset the otp attempts
                        bool resetArtOtpAttempts = objUserRepo.ResetArtUserOtpAttempts(userId, Constants.UNLOCK_ACCOUNT);

                        //delete user attempt lock logs
                        bool isLogDeleted = objUserRepo.DeleteArtAccountLockLogs(userId);

                        // create ITSM incident
                        if (Singleton.Instance.ClientSessionID.Is_ITSM_Enabled == "Y")
                        {
                            string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);

                            if (!string.IsNullOrEmpty(email))
                            {
                                bool isIncidentCreate = new ITSM().CreateIncident(userId, Constants.UNLOCK_ACCOUNT, email, Constants.ACCOUNTUNLOCKDESCRIPTION,
                                    Constants.ACCOUNTUNLOCKSHORTDESCRIPTION, Singleton.Instance.ClientSessionID.Account_Unlock_Category);

                                //Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Create " + itsmProvider + " ITSM incident - START"));

                                //// Create a incident with resolved status in SNOW ITSM
                                //string itsmresult = objItsm.CreateIncident(userId, email, Constants.ACCOUNTUNLOCKDESCRIPTION, Constants.ACCOUNTUNLOCKDESCRIPTION, Singleton.Instance.ClientSessionID.Account_Unlock_Category, Constants.ACCOUNTUNLOCKDESCRIPTION, ref sysid);

                                //if (!string.IsNullOrEmpty(itsmresult))
                                //{
                                //    //log itsm ticket no
                                //    Log.LogITSM(userId, Constants.UNLOCK_ACCOUNT, itsmProvider, itsmresult);

                                //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Create " + itsmProvider + " ITSM incident - END"));

                                //    //if (itsmProvider == Constants.ITSMSNOW)
                                //    //{
                                //    string resolveResult = objItsm.ResolveIncident(itsmresult, Constants.ACCOUNTUNLOCKDESCRIPTION, Constants.ACCOUNTUNLOCKDESCRIPTION, email, Singleton.Instance.ClientSessionID.Account_Unlock_Category, sysid);

                                //    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account, "Resolved " + itsmProvider + " ITSM incident - END"));
                                //    //}

                                //}
                            }
                        }

                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Unlocking user account - SUCCESS"));

                        ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.UNLOCK_ACCOUNT, "3", Constants.COMPLETED, DateTime.Now);

                        TempData["Unlocked"] = true;
                        TempData["UserMessage"] = Art.UnlockSuccess;
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Unlocking user account - FAILURE"));
                        TempData["Unlocked"] = false;
                        TempData["UserMessage"] = Art.UnlockFailure;
                    }

                    return RedirectToRoute("UnlockAccountOTP");
                }
                else
                {
                    TempData["UserMessage"] = Art.InvalidOTP;

                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "Check for ART user account lock"));

                    // if the user enter invalid OTP for configured time , we lock the account
                    bool isAccLocked = objUserRepo.ArtAccountLock(userId, sessionId, Constants.UNLOCK_ACCOUNT, Constants.INVALID_OTP);
                    if (isAccLocked)
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_OTP, "user account is locked in ART , now redirecting to account lock page"));

                        AccountLockModelResponse objAccLock = new AccountLockModelResponse() { islocked = true, waitTime = Singleton.Instance.ClientSessionID.Account_Lock_Duration };
                        TempData["LockInfo"] = objAccLock;
                        Session["LockedUserId"] = userId;
                        Session["UserId"] = null;
                        return RedirectToAction("UserAccountLock", "Home");
                    }

                    return RedirectToRoute("UnlockAccountOTP");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("AccountUnlockOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountUnlockAuthSecQue(FormCollection collection)
        {
            string userId = Convert.ToString(Session["UserId"]);

            Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthSecQue, "Page load , now redirecting to AccountUnlockQuestions"));

            ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.UNLOCK_ACCOUNT, "2", Constants.FINISHED, null);
            Session["UserAccUnlockStep2Comp"] = "Yes";

            return RedirectToRoute("UnlockAccountSecQuestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountUnlockAuthOTP(FormCollection collection)
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);

                ISms objSMS = new SMS24x7();
                UserRepository objUserRepo = new UserRepository();

                //string mobileNumber = objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                string mobileNumber = GetUserMobileNumber(userId);

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.UNLOCK_ACCOUNT, userName);

                if (isSmsSent)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthOTP, "OTP sent, now redirecting to AccountUnlockOTP"));

                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.UNLOCK_ACCOUNT, "2", Constants.FINISHED, null);
                    Session["UserAccUnlockStep2Comp"] = "Yes";
                    return RedirectToRoute("UnlockAccountOTP");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthOTP, "Sending OTP - Failure"));
                    TempData["UserMessage"] = Art.ErrorSendMsg;
                    Session["UserAccUnlockStep2Comp"] = "No";
                    return RedirectToRoute("UnlockAccountAuthType");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("AccountUnlockAuthOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountUnlockResendOTP(FormCollection collection)
        {
            try
            {
                string userId = Convert.ToString(Session["UserId"]);

                ISms objSMS = new SMS24x7();
                UserRepository objUserRepo = new UserRepository();

                //string mobileNumber = objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                string mobileNumber = GetUserMobileNumber(userId);

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.UNLOCK_ACCOUNT, userName);

                if (isSmsSent)
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthOTP, "RESEND :: OTP sent, now redirecting to AccountUnlockOTP"));
                    return RedirectToRoute("UnlockAccountOTP");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Unlock_Account_AuthOTP, "RESEND :: Sending OTP - Failure"));
                    TempData["UserMessage"] = Art.ErrorSendMsg;
                    return RedirectToRoute("UnlockAccountOTP");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("AccountUnlockResendOTP", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        #endregion

        #region Change Password

        [RequestorLogFilter]
        [Route("ChangePassword", Name = "ChangePassword")]
        public ActionResult ChangePassword()
        {
            if (TempData["IsPasswordChanged"] != null)
            {
                if (Convert.ToBoolean(TempData["IsPasswordChanged"]))
                {
                    ViewBag.IsPasswordChanged = TempData["IsPasswordChanged"];
                    ViewBag.Message = Art.ChangePasswordSuccess;
                }
                else
                {
                    ViewBag.IsPasswordChanged = TempData["IsPasswordChanged"];
                    ViewBag.Message = TempData["Message"];
                    ViewBag.IsAccountLocked = TempData["IsAccountLocked"];
                }
            }

            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];

            return View();
        }

        #endregion

        public ActionResult PasswordInfo()
        {
            return PartialView();
        }

        #region Register User Info
        [SessionFilter]
        [RequestorLogFilter]
        [Route("Enroll/Validation", Name = "RegisterUserInformation")]
        public ActionResult RegisterUserInfo()
        {
            try
            {
                ViewBag.MessageDOB = TempData["UserMessageDOB"];
                ViewBag.MessageMOB = TempData["UserMessageMOB"];
                UserInfoModel objUserInfo = new UserInfoModel();
                UserRepository objUserRepo = new UserRepository();
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                string userId = Convert.ToString(Session["UserId"]);

                if (!string.IsNullOrEmpty(Convert.ToString(TempData["OpenRegisterForOTP"])))
                    ViewBag.OpenRegisterForOTP = "1";
                else
                    ViewBag.OpenRegisterForOTP = "0";

                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.USER_REGISTER);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                    ViewBag.MaxAttempt = objUserOtp.maxattempt;
                }

                if (ArtHandler.Model.Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(TempData["MobileNumber"])))
                    {
                        objUserInfo.MobileNum = Convert.ToString(TempData["MobileNumber"]);
                        objUserInfo.DOB = Convert.ToString(TempData["dob"]);
                        objUserInfo.CountryCode = Convert.ToString(TempData["CountryCode"]);
                        objUserInfo.IsOTPSent = Convert.ToString(TempData["IsOTPSent"]);
                        objUserInfo.OTPValidateMsg = Convert.ToString(TempData["OTPValidateMsg"]);
                        objUserInfo.IsValidOTP = Convert.ToString(TempData["IsValidOTP"]);
                        objUserInfo.IsMobileNumPrivate = Convert.ToBoolean(TempData["IsMobileNumPrivate"]);
                    }
                    else
                    {
                        string mobileNumber = GetUserMobileNumber(userId);
                        string countryCode = GetUserCountryCode(userId);

                        objUserInfo.IsMobileNumPrivate = IsMobileNumberPrivate;

                        objUserInfo.MobileNum = mobileNumber;//objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                        objUserInfo.CountryCode = countryCode;//objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);
                    }

                    objUserInfo.lstContryCodes = new SettingsRepository().GetAllCountryCodeDetails();
                }

                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Page load"));

                return View(objUserInfo);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("RegisterUserInfo", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUserInfo(FormCollection collection)
        {
            //try
            //{
            string action = collection["btnAction"];
            string dob = collection["txtDOB"];
            UserRepository objUserRepo = new UserRepository();
            string userId = Convert.ToString(Session["UserId"]);

            if (action == "Submit")
            {
                //string dob = collection["txtDOB"];
                bool isOtpRegistered = false;
                string mobileNumber = string.Empty;

                //Get the mobile numnber & otp register raio button only company enabled OTP
                if (Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                {
                    isOtpRegistered = Convert.ToBoolean(collection["register-otp"].Split(',')[0]);
                    mobileNumber = Convert.ToString(collection["txtMobileNum"]);
                }


                if (Singleton.Instance.ClientSessionID.IS_DOB_Validation_Needed == "Y")
                {
                    //string adDOB = objUserRepo.GetUserDateOfBirth(userId);

                    if (CheckUserDOB(userId, dob))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "DOB Validation - Success"));

                        if (isOtpRegistered)
                        {
                            //bool result = objUserRepo.EnableUserOTP(userId, isOtpRegistered);

                            //Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User Registered For OTP"));

                            //update the mobile number in AD

                            //objUserRepo.SetUserInfoInAD(userId, Constants.Mobile, mobileNumber);

                            //trace update
                            //Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User mobile number updated in AD"));
                        }
                        else
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User NOT Registered For OTP"));
                            bool result = objUserRepo.EnableUserOTP(userId, isOtpRegistered, false);
                        }

                        ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.USER_REGISTER, "2", Constants.FINISHED, null);

                        Session["UserRegStp2Complete"] = "Yes";

                        return RedirectToRoute("Register");
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "DOB Validation - Failure"));
                        TempData["UserMessageDOB"] = Art.InvalidDOB;
                        return RedirectToRoute("RegisterUserInformation");
                    }
                }
                else
                {
                    // if the user dont want to register OTP option , then we need to update
                    if (!isOtpRegistered)
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User NOT Registered For OTP"));
                        bool result = objUserRepo.EnableUserOTP(userId, isOtpRegistered, false);
                    }
                    ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.USER_REGISTER, "2", Constants.FINISHED, null);

                    Session["UserRegStp2Complete"] = "Yes";

                    return RedirectToRoute("Register");
                }
            }
            else if (action == "SendOTP")
            {
                string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
                string countryValue = Convert.ToString(collection["countryCode"]);
                string countryCode = countryValue.Split('_')[0];
                string telephoneCode = countryValue.Split('_')[1];
                bool isMobileNumPrivate = false;

                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "Y")
                {
                    isMobileNumPrivate = Convert.ToBoolean(collection["mobile-num-private"].Split(',')[0]);
                }

                ISms objSMS = new SMS24x7();

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                TempData["MobileNumber"] = mobileNumber;
                TempData["CountryCode"] = countryCode;
                TempData["dob"] = dob;
                TempData["IsMobileNumPrivate"] = isMobileNumPrivate;

                bool isValidDOB = CheckUserDOB(userId, dob);

                if (isValidDOB)
                {
                    bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.USER_REGISTER, userName, telephoneCode);
                    if (isSmsSent)
                    {
                        TempData["IsOTPSent"] = 1;
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "OTP sent , now redirecting to RegisterUserInformation"));
                        return RedirectToRoute("RegisterUserInformation");
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Sending OTP - Failure"));
                        TempData["OpenRegisterForOTP"] = "1";
                        TempData["UserMessageMOB"] = Art.ErrorSendMsg;
                        return RedirectToRoute("RegisterUserInformation");
                    }
                }
                else
                {
                    TempData["OpenRegisterForOTP"] = "1";
                    TempData["UserMessageDOB"] = Art.InvalidDOB;
                    return RedirectToRoute("RegisterUserInformation");
                }
            }
            else if (action == "ValidateOTP")
            {
                return ValidateUserOTP(collection);
            }
            else
            {
                return RegisterUserResendOTP(collection);
            }
            //}
            //catch (Exception ex)
            //{
            //    Log.LogException(new CustomException("RegisterUserInfo", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            //    return RedirectToAction("Index", "Error");
            //}
        }

        public ActionResult ValidateUserOTP(FormCollection collection)
        {
            string otp = Convert.ToString(collection["txtOtp"]);
            string userId = Convert.ToString(Session["UserId"]);
            UserRepository objUserRepo = new UserRepository();
            string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
            string countryValue = Convert.ToString(collection["countryCode"]);
            string countryCode = countryValue.Split('_')[0];
            string dob = collection["txtDOB"] != null ? collection["txtDOB"] : "";
            bool isMobileNumPrivate = false;

            if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "Y")
            {
                isMobileNumPrivate = Convert.ToBoolean(collection["mobile-num-private"].Split(',')[0]);
            }

            TempData["MobileNumber"] = mobileNumber;
            TempData["CountryCode"] = countryCode;
            TempData["IsOTPSent"] = 1;
            TempData["dob"] = dob;
            TempData["IsMobileNumPrivate"] = isMobileNumPrivate;

            Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Submit - Validate user otp"));

            bool isValidOtp = objUserRepo.ValidateUserOtp(userId, otp, sessionId);
            if (isValidOtp)
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "valid OTP, now redirecting to reset password"));
                TempData["IsValidOTP"] = "1";

                // enable the user OTP option 
                bool result = objUserRepo.EnableUserOTP(userId, true, isMobileNumPrivate);

                if (!isMobileNumPrivate)
                {
                    if (UpdateUserMobileNumberInAD(userId, countryCode, mobileNumber))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User mobile number updated in AD"));

                        TempData["OTPValidateMsg"] = Art.MobileNumRegisterSuccess;
                    }
                    else
                    {
                        TempData["OTPValidateMsg"] = Art.MobileNumRegisterFailure;
                    }
                }
                else
                {
                    // we update the mobile number in our local DB
                    objUserRepo.UpdateUserInfo(userId, Utility.Encryptor.Encrypt(countryCode, Constants.PASSPHARSE), Utility.Encryptor.Encrypt(mobileNumber, Constants.PASSPHARSE));

                    TempData["OTPValidateMsg"] = Art.MobileNumRegisterSuccess;

                    Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Mobile Number is Private"));
                }

                //to reset the otp attempts
                bool resetArtOtpAttempts = objUserRepo.ResetArtUserOtpAttempts(userId, Constants.USER_REGISTER);

                return RedirectToRoute("RegisterUserInformation");
            }
            else
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Invalid OTP"));
                TempData["IsValidOTP"] = "0";
                TempData["OTPValidateMsg"] = Art.InvalidOTP;
                return RedirectToRoute("RegisterUserInformation");
            }
        }
        public ActionResult USRegisterUserResendOTP(FormCollection collection)
        {
            string userId = Convert.ToString(Session["UserId"]);
            UserRepository objUserRepo = new UserRepository();
            string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
            string countryValue = Convert.ToString(collection["countryCode"]);
            string countryCode = countryValue.Split('_')[0];
            string telephoneCode = countryValue.Split('_')[1];

            TempData["MobileNumber"] = mobileNumber;
            TempData["CountryCode"] = countryCode;

            TempData["IsOTPSent"] = 1;

            ISms objSMS = new SMS24x7();

            string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

            bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.USER_REGISTER, userName, telephoneCode);
            if (isSmsSent)
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "RESEND :: OTP sent , now redirecting to MobileNoChange"));
                return RedirectToRoute("MobileNoChange");
            }
            else
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "RESEND :: Sending OTP - Failure"));

                TempData["UserMessage"] = Art.ErrorSendMsg;
                return RedirectToRoute("MobileNoChange");
            }
        }
        public ActionResult USValidateUserOTP(FormCollection collection)
        {
            string otp = Convert.ToString(collection["txtOtp"]);
            string userId = Convert.ToString(Session["UserId"]);
            UserRepository objUserRepo = new UserRepository();
            string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
            string countryValue = Convert.ToString(collection["countryCode"]);
            string countryCode = countryValue.Split('_')[0];
            bool isMobileNumPrivate = false;

            if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "Y")
            {
                isMobileNumPrivate = Convert.ToBoolean(collection["mobile-num-private"].Split(',')[0]);
            }

            TempData["MobileNumber"] = mobileNumber;
            TempData["CountryCode"] = countryCode;
            TempData["IsOTPSent"] = 1;
            TempData["IsMobileNumPrivate"] = isMobileNumPrivate;

            Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "Submit - Validate user otp"));

            bool isValidOtp = objUserRepo.ValidateUserOtp(userId, otp, sessionId);
            if (isValidOtp)
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "valid OTP, now redirecting to reset password"));
                TempData["IsValidOTP"] = "1";

                // enable the user OTP option 
                bool result = objUserRepo.EnableUserOTP(userId, true, isMobileNumPrivate);

                if (!isMobileNumPrivate)
                {
                    if (UpdateUserMobileNumberInAD(userId, countryCode, mobileNumber))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "User mobile number updated in AD"));

                        TempData["OTPValidateMsg"] = Art.MobileNumRegisterSuccess;
                    }
                    else
                    {
                        TempData["OTPValidateMsg"] = Art.MobileNumRegisterFailure;
                    }
                }
                else
                {
                    // we update the mobile number in our local DB
                    objUserRepo.UpdateUserInfo(userId, Utility.Encryptor.Encrypt(countryCode, Constants.PASSPHARSE), Utility.Encryptor.Encrypt(mobileNumber, Constants.PASSPHARSE));

                    TempData["OTPValidateMsg"] = Art.MobileNumRegisterSuccess;

                    Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "Mobile Number is Private"));
                }

                //to reset the otp attempts
                bool resetArtOtpAttempts = objUserRepo.ResetArtUserOtpAttempts(userId, Constants.USER_REGISTER);

                return RedirectToRoute("MobileNoChange");
            }
            else
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "Invalid OTP"));
                TempData["IsValidOTP"] = "0";
                TempData["OTPValidateMsg"] = Art.InvalidOTP;
                return RedirectToRoute("MobileNoChange");
            }
        }


        public ActionResult RegisterUserResendOTP(FormCollection collection)
        {
            string userId = Convert.ToString(Session["UserId"]);
            UserRepository objUserRepo = new UserRepository();
            string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
            string countryValue = Convert.ToString(collection["countryCode"]);
            string countryCode = countryValue.Split('_')[0];
            string telephoneCode = countryValue.Split('_')[1];
            string dob = collection["txtDOB"];

            TempData["MobileNumber"] = mobileNumber;
            TempData["CountryCode"] = countryCode;
            TempData["dob"] = dob;

            TempData["IsOTPSent"] = 1;

            ISms objSMS = new SMS24x7();

            string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

            bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.USER_REGISTER, userName, telephoneCode);
            if (isSmsSent)
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "RESEND :: OTP sent , now redirecting to RegisterUserInformation"));
                return RedirectToRoute("RegisterUserInformation");
            }
            else
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "RESEND :: Sending OTP - Failure"));

                TempData["UserMessage"] = Art.ErrorSendMsg;
                return RedirectToRoute("RegisterUserInformation");
            }
        }

        public bool UpdateUserMobileNumberInAD(string userId, string countryCode, string mobileNumber)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();

                // will store locally if this flag is "Y"
                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "N")
                {
                    objUserRepo.UpdateUserInfo(userId, Utility.Encryptor.Encrypt(countryCode, Constants.PASSPHARSE), Utility.Encryptor.Encrypt(mobileNumber, Constants.PASSPHARSE));
                }
                else
                {
                    //update the mobile number in AD
                    objUserRepo.SetUserInfoInAD(userId, Constants.Mobile, mobileNumber);

                    //Update the user contry code
                    objUserRepo.SetUserInfoInAD(userId, Constants.ADCOUNTRYCODE, countryCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }

        }

        public bool ValidateUserRegisterInfo(string dob, string mobileNumber, bool isOtpEnabled)
        {
            if (Singleton.Instance.ClientSessionID.IS_DOB_Validation_Needed == "Y")
            {
                if (string.IsNullOrEmpty(dob))
                {
                    TempData["UserMessageDOB"] = Art.EnterDOB;
                    return false;
                }
                if (dob.Split('/').Length < 3)
                {
                    TempData["UserMessageDOB"] = Art.IncorrectDOBFormat;
                    return false;
                }
                var dateArr = dob.Split('/');

                if (dateArr[0].Length < 2 || dateArr[1].Length < 2 || dateArr[2].Length < 4)
                {
                    TempData["UserMessageDOB"] = Art.IncorrectDOBFormat;
                    return false;
                }
                DateTime dt;
                bool isValidDatetime = DateTime.TryParse(dob.Split('/')[2] + "-" + dob.Split('/')[1] + "-" + dob.Split('/')[0], out dt);
                //if (!isValidDatetime)
                //{
                //    TempData["UserMessageDOB"] = Art.InvalidDOB;
                //    return false;
                //}

                //if (isOtpEnabled)
                //{
                //    if (string.IsNullOrEmpty(mobileNumber))
                //    {
                //        TempData["UserMessageMOB"] = Art.EnterMobileNumber;
                //        return false;
                //    }
                //    if (!mobileNumber.All(char.IsDigit))
                //    {
                //        TempData["UserMessageMOB"] = Art.InvalidMobileNumber;
                //        return false;
                //    }
                //    if (mobileNumber.Length != 10)
                //    {
                //        TempData["UserMessageMOB"] = Art.InvalidMobileNumber;
                //        return false;
                //    }
                //}
            }

            return true;

        }

        #endregion

        #region Public Methods

        //[HttpGet]
        //public string GetQuestions()
        //{
        //    List<QuestionModel> lstquestion = new List<QuestionModel>();

        //    try
        //    {
        //        QuestionAnswerRepo objQuestionAnsRepo = new QuestionAnswerRepo();
        //        string userId = Session["UserId"].ToString();

        //        lstquestion = objQuestionAnsRepo.GetQuestions(userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogException(new CustomException("Register", ex.Message.ToString(), ex.StackTrace.ToString()));
        //    }

        //    return JsonConvert.SerializeObject(lstquestion);
        //}

        [HttpGet]
        public string GetQuestionsForUserToAnswer()
        {
            QuestionAnswerRepo objQuestionAnsRepo = new QuestionAnswerRepo();
            string userId = Convert.ToString(Session["UserId"]);

            List<QuestionAnswerModel> lstquestionAns = objQuestionAnsRepo.GetQuestionsForUserToAnswer(userId, Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate));

            return JsonConvert.SerializeObject(lstquestionAns);
        }



        private bool CheckUserQuestionAnswer(FormCollection collection, out string error)
        {
            error = string.Empty;

            try
            {
                QuestionAnswerRepo objQuestionAnsRepo = new QuestionAnswerRepo();
                List<QuestionAnswerModel> lstQuestionAns = new List<QuestionAnswerModel>();
                string userId = Convert.ToString(Session["UserId"]);


                for (int i = 1; i <= Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Validate); i++)
                {
                    if (string.IsNullOrEmpty(collection["txtQueAns_" + i]))
                    {
                        error = Art.AllQuestionAnswerMandateError;
                        return false;
                    }

                    if (Utility.ContainsHTML(collection["txtQueAns_" + i]))
                    {
                        error = "Please enter valid answer";
                        return false;
                    }

                    if (Utility.CheckSpecialCharacter(collection["txtQueAns_" + i]))
                    {
                        error = "Please enter valid answer";
                        return false;
                    }

                    QuestionAnswerModel objQuestionAns = new QuestionAnswerModel();
                    if (collection != null)
                    {
                        objQuestionAns.question_id = Convert.ToInt32(collection["Que_" + i]);
                        objQuestionAns.answer = collection["txtQueAns_" + i];
                    }

                    lstQuestionAns.Add(objQuestionAns);
                }

                bool result = objQuestionAnsRepo.CheckUserQuestionAnswer(userId, lstQuestionAns);

                if (!result)
                    error = Art.AnswerWrongErrMsg;

                return result;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.LogException(new CustomException("CheckUserQuestionAnswer", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public string CheckResetPasswordPolicy(string newPassword, string confirmPassword)
        {
            string message = string.Empty;

            if (string.IsNullOrEmpty(newPassword))
                message = "";
            if (string.IsNullOrEmpty(confirmPassword))
                message = "";
            if (newPassword.Length < 7 || newPassword.Length > 13)
                message = Art.PasswordLength;
            if (confirmPassword.Length < 7 || confirmPassword.Length > 13)
                message = Art.PasswordLength;

            Regex reg = new Regex(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{9,13}$");
            if (!reg.IsMatch(newPassword))
                message = Art.PasswordPolicy;
            if (!reg.IsMatch(confirmPassword))
                message = Art.PasswordPolicy;

            if (Utility.ContainsHTML(newPassword))
                message = Art.PasswordPolicy;

            if (Utility.ContainsHTML(confirmPassword))
                message = Art.PasswordPolicy;


            return message;
        }

        //public string ValidateUserRegistration(FormCollection collection,int totalNumOfQuestions)
        //{
        //    string message = string.Empty;

        //    for (int i = 1; i <= totalNumOfQuestions; i++)
        //    {
        //        if (string.IsNullOrEmpty(collection["txtQueAns_" + i]))
        //        {

        //        }
        //        objQuestionAns.question_id = int.Parse(collection["Que_" + i]);
        //        objQuestionAns.answer = collection["txtQueAns_" + i];
        //        objQuestionAns.createdDate = DateTime.Now;

        //        lstQuestionAns.Add(objQuestionAns);
        //    }
        //}

        /// <summary>
        /// To Check the user DOB
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dob"></param>
        /// <returns></returns>
        public bool CheckUserDOB(string userId, string dob)
        {
            string adDOB = string.Empty;
            DateTime dtadDOB;
            DateTime dtenteredDOB;

            try
            {
                UserRepository objUserRepo = new UserRepository();
                adDOB = objUserRepo.GetUserDateOfBirth(userId);
                if (!string.IsNullOrEmpty(dob) && !string.IsNullOrEmpty(adDOB))
                {
                    bool isvaliddob = DateTime.TryParse(adDOB, out dtadDOB);
                    bool isvalidentereddob = DateTime.TryParse(dob.Split('/')[2] + "-" + dob.Split('/')[1] + "-" + dob.Split('/')[0], out dtenteredDOB);

                    if (isvalidentereddob && isvaliddob)
                    {
                        if (dtenteredDOB == dtadDOB)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("CheckUserDOB", ex.Message.ToString() + ": DOB :" + dob + ": UserId :" + userId + ": AD_DOB : " + adDOB, ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        private string GetUserMobileNumber(string userId)
        {
            string mobileNumber = string.Empty;

            try
            {
                UserRepository objUserRepo = new UserRepository();
                List<UserModel> lstUserModel = objUserRepo.GetUserInfo(userId);

                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "N")
                {
                    if (lstUserModel.Count > 0 && !string.IsNullOrEmpty(lstUserModel[0].mobilenumber))
                        mobileNumber = Utility.Encryptor.Decrypt(lstUserModel[0].mobilenumber, Constants.PASSPHARSE);
                }
                else
                {
                    if (lstUserModel.Count > 0 && lstUserModel[0].ismobilenumberprivate)
                    {
                        mobileNumber = Utility.Encryptor.Decrypt(lstUserModel[0].mobilenumber, Constants.PASSPHARSE);
                        IsMobileNumberPrivate = true;
                    }
                    else
                    {
                        mobileNumber = new UserRepository().GetUserInfoFromAD(userId, Constants.Mobile);
                    }
                }
                return mobileNumber;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("GetUserMobileNumber", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return mobileNumber;
            }
        }
        private string GetUserCountryCode(string userId)
        {
            string countrycode = string.Empty;

            try
            {
                UserRepository objUserRepo = new UserRepository();
                List<UserModel> lstUserModel = objUserRepo.GetUserInfo(userId);

                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "N")
                {
                    if (lstUserModel.Count > 0 && !string.IsNullOrEmpty(lstUserModel[0].countrycode))
                        countrycode = Utility.Encryptor.Decrypt(lstUserModel[0].countrycode, Constants.PASSPHARSE);
                }
                else
                {
                    if (lstUserModel.Count > 0 && lstUserModel[0].ismobilenumberprivate)
                    {
                        countrycode = Utility.Encryptor.Decrypt(lstUserModel[0].countrycode, Constants.PASSPHARSE);
                    }
                    else
                    {
                        countrycode = new UserRepository().GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);
                    }
                }
                return countrycode;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("GetUserCountryCode", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return countrycode;
            }
        }
        #endregion
        [SessionFilter]
        [RequestorLogFilter]
        [Route("QuickEnrollment", Name = "QuickEnrollment")]
        public ActionResult QuickEnrollment()
        {
            try
            {
                string userId = Convert.ToString(Session["EnrollUserId"]);
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                bool validatiomMsg = false;
                RegisterModel objRegister = new RegisterModel();
                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Questions))
                    objRegister.TotalNumberOfQuestions = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Questions);
                if (!string.IsNullOrEmpty(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Register))
                    objRegister.TotalQuestionsToAnswer = Convert.ToInt32(Singleton.Instance.ClientSessionID.Total_Number_Of_Question_To_Register);
                objRegister.questions = new QuestionAnswerRepo().GetQuestions(userId);
                ViewBag.Message = TempData["UserMessage"];
                if (Convert.ToString(TempData["UserValidMessage"]) == "False")
                    validatiomMsg = false;
                else
                    validatiomMsg = true;

                ViewBag.IsRegistered = TempData["IsRegistered"];
                ViewBag.userkey = TempData["userkey"];
                if ((userId != "" && userId != null) && (validatiomMsg))
                {
                    objRegister.IsLogin = true;
                }
                else
                {
                    objRegister.IsLogin = false;
                }

                Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Page Load"));

                return View(objRegister);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Security_Questions, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult QuickEnrollment(FormCollection collection)
        {
            try
            {
                if (collection == null)
                {
                    return RedirectToAction("QuickEnrollment");
                }

                string action = collection["hbtnLogin"];
                string userId = string.Empty;
                UserRepository objUserRepo = new UserRepository();
                string userkey = string.Empty;

                if (action == "Login")
                {
                    //Check for non-Hexa users
                    userkey = GetUserHexaorNonHexa(collection["txtkey"]);
                    //userkey = collection["txtkey"];
                    //Check for non-Hexa users
                    userId = objUserRepo.GetUserEnrollmentDetails(userkey, sessionId);
                    // bool isValidOtp = objUserRepo.ValidateEnrollmentOtp(userId, userkey, sessionId);
                    Session["EnrollUserId"] = userId;

                    if ((userId != "" && userId != null))
                    {
                        ViewBag.IsSecurity = true;
                        TempData["userkey"] = userkey;
                        Session["UserKey"] = userkey;
                    }
                    else
                    {
                        TempData["UserMessage"] = "Please enter a valid key";
                        TempData["UserValidMessage"] = "False";
                    }
                }
                else
                {
                    userkey = Convert.ToString(Session["UserKey"]);
                    userId = Convert.ToString(Session["EnrollUserId"]);
                    QuestionAnswerRepo objQuestionAnswer;
                    List<QuestionAnswerModel> lstQuestionAns;


                    string validateResult = ValidateUserQuestionAnswer(collection);
                    if (!string.IsNullOrEmpty(validateResult))
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, validateResult));
                        TempData["IsRegistered"] = false;
                        TempData["UserMessage"] = "Please enter valid answer(s)";

                        return RedirectToAction("Register");
                    }

                    string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                    GenerateQuestionAnsEntity(collection, out objQuestionAnswer, out lstQuestionAns);

                    //insert user answer
                    bool result = false;
                    if (userId != "" && userId != null)
                        result = objQuestionAnswer.InsertQuestionAnswer(userId, lstQuestionAns);

                    if (result)
                    {
                        //trace the log
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Success"));

                        //Register
                        bool isResgistered = objUserRepo.RegisterUser(userId, result);
                        bool isenrollmentlink = objUserRepo.Enrollmentlink(userId, result, userkey, sessionId);
                        if (isenrollmentlink)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Enrollment Link Status Updated Successfully"));
                        }
                        else
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Enrollment Link Status Updated Failure"));
                        }
                        if (isResgistered)
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register User in ART - Success"));

                            ArtHandler.Repository.LoggingRepository.UpdateUserActivityLog(userId, sessionId, Constants.USER_REGISTER, "3", Constants.COMPLETED, DateTime.Now);

                            //once the user register get the email id from AD and send the confirmation email
                            string email = objUserRepo.GetUserInfoFromAD(userId, Constants.Email);

                            if (!string.IsNullOrEmpty(email) && Singleton.Instance.ClientSessionID.Send_Email == "Y")
                            {
                                bool isEmailSent = new Email().SendHtmlFormattedEmail(userName, email);

                                if (isEmailSent)
                                    Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register Email Sent" + email));
                            }
                        }
                        else
                        {
                            Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Register User in ART - Failure"));
                        }

                        TempData["IsRegistered"] = true;
                        TempData["UserMessage"] = "Dear " + userName + ", " + Art.RegisterSuccess;
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userId, Constants.Register_Security_Questions, "Failure"));
                        TempData["IsRegistered"] = false;
                        TempData["UserMessage"] = Art.RegisterFailure;
                    }
                }


                return RedirectToAction("QuickEnrollment");
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Security_Questions, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult CallUserSettings()
        {
            return RedirectToAction("UserSettings");
        }
        [HttpGet]
        [SessionFilter]
        [RequestorLogFilter]
        [Route("UserSettings", Name = "UserSettings")]
        public ActionResult UserSettings()

        {
            string userId = Convert.ToString(Session["UserId"]);
            UserRepository objUserRepo = new UserRepository();
            bool isPwdExpiry = objUserRepo.GetUserPasswordExpiry(userId);
            ViewBag.IsPwdExpiry = isPwdExpiry;
            string mobileNumber = GetUserMobileNumber(userId);
            ViewBag.MobileNo = mobileNumber;

            return View();
        }
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult UserSettings(FormCollection data)
        {
            string userId = Convert.ToString(Session["UserId"]);
            bool IsPasswordExpiry = Convert.ToBoolean(data["PwdName"].Split(',')[0]);
            UserRepository objUserRepo = new UserRepository();
            bool isPwdExpiry = objUserRepo.UserPasswordExpiry(userId, IsPasswordExpiry);
            return RedirectToAction("UserSettings", "User");
        }

        public ActionResult CallMobileNumberChange()
        {
            return RedirectToAction("MobileNoChange");
        }
        [HttpGet]
        [SessionFilter]
        [RequestorLogFilter]
        [Route("MobileNoChange", Name = "MobileNoChange")]
        public ActionResult MobileNoChange()
        {
            try
            {
                UserInfoModel objUserInfo = new UserInfoModel();
                UserRepository objUserRepo = new UserRepository();
                ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
                string userId = Convert.ToString(Session["UserId"]);
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["OpenRegisterForOTP"])))
                    ViewBag.OpenRegisterForOTP = "1";
                else
                    ViewBag.OpenRegisterForOTP = "0";

                //Check the user reached his maximum OTP attempts
                UserOtpAttemptModel objUserOtp = objUserRepo.CheckUserOtpAttemptExceed(userId, Constants.USER_REGISTER);
                if (objUserOtp != null)
                {
                    ViewBag.IsOtpAttemptExceed = objUserOtp.islocked;
                    ViewBag.AttemptCount = objUserOtp.attemptcount;
                    ViewBag.MaxAttempt = objUserOtp.maxattempt;
                }

                if (ArtHandler.Model.Singleton.Instance.ClientSessionID.Is_OTP_Enabled == "Y")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(TempData["MobileNumber"])))
                    {
                        objUserInfo.MobileNum = Convert.ToString(TempData["MobileNumber"]);
                        objUserInfo.CountryCode = Convert.ToString(TempData["CountryCode"]);
                        objUserInfo.IsOTPSent = Convert.ToString(TempData["IsOTPSent"]);
                        objUserInfo.OTPValidateMsg = Convert.ToString(TempData["OTPValidateMsg"]);
                        objUserInfo.IsValidOTP = Convert.ToString(TempData["IsValidOTP"]);
                        objUserInfo.IsMobileNumPrivate = Convert.ToBoolean(TempData["IsMobileNumPrivate"]);
                    }
                    else
                    {
                        string mobileNumber = GetUserMobileNumber(userId);
                        string countryCode = GetUserCountryCode(userId);

                        objUserInfo.IsMobileNumPrivate = IsMobileNumberPrivate;

                        objUserInfo.MobileNum = mobileNumber;//objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                        objUserInfo.CountryCode = countryCode;//objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);
                    }

                    objUserInfo.lstContryCodes = new SettingsRepository().GetAllCountryCodeDetails();
                }

                Log.LogTrace(new CustomTrace(userId, Constants.Register_MobileNo_Change, "Page load"));

                return View(objUserInfo);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("Register_MobileNo_Change", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult MobileNoChange(FormCollection collection)
        {

            //try
            //{
            string action = collection["btnAction"];
            UserRepository objUserRepo = new UserRepository();
            string userId = Convert.ToString(Session["UserId"]);

            if (action == "Submit")
            {

                return RedirectToRoute("MobileNoChange");


            }
            else if (action == "SendOTP")
            {
                string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
                string countryValue = Convert.ToString(collection["countryCode"]);
                string countryCode = countryValue.Split('_')[0];
                string telephoneCode = countryValue.Split('_')[1];
                bool isMobileNumPrivate = false;

                if (Singleton.Instance.ClientSessionID.Is_AD_Enabled == "Y")
                {
                    isMobileNumPrivate = Convert.ToBoolean(collection["mobile-num-private"].Split(',')[0]);
                }

                ISms objSMS = new SMS24x7();

                string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);

                TempData["MobileNumber"] = mobileNumber;
                TempData["CountryCode"] = countryCode;

                TempData["IsMobileNumPrivate"] = isMobileNumPrivate;

                bool isSmsSent = objSMS.SendSMS(userId, mobileNumber, Constants.USER_REGISTER, userName, telephoneCode);
                if (isSmsSent)
                {
                    TempData["IsOTPSent"] = 1;
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "OTP sent , now redirecting to MobileNoChange"));
                    return RedirectToRoute("MobileNoChange");
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Sending OTP - Failure"));
                    TempData["OpenRegisterForOTP"] = "1";
                    TempData["UserMessageMOB"] = Art.ErrorSendMsg;
                    return RedirectToRoute("MobileNoChange");
                }


            }
            else if (action == "ValidateOTP")
            {
                return USValidateUserOTP(collection);
            }
            else
            {
                return USRegisterUserResendOTP(collection);
            }

        }

        //Check for non-Hexa users
        public string GetUserHexaorNonHexa(string userName)
        {
            UserRepository objUserRepouser = new UserRepository();
            if(!Utility.IsNumberonly(userName))
            {
                userName = objUserRepouser.GetnonHexaUserID(userName);
            }            
            return userName;
        }
        //Check for non-Hexa users
    }

}
