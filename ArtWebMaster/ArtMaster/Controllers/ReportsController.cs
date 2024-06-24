using ArtHandler;
using ArtHandler.Interface;
using ArtHandler.Model;
using ArtHandler.Repository;
using ArtMaster.crypt;
using ArtMaster.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ArtMaster.Controllers
{
    public class ReportsController : Controller
    {
        private string sessionId = System.Web.HttpContext.Current.Session.SessionID;
        //
        // GET: /Reports/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Reports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reports/Create
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
        // GET: /Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Edit/5
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
        // GET: /Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Delete/5
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

        //
        // GET: /Reports/Create
        [SessionFilter]
        public ActionResult EmployeeSearch(string value)
        {
            string empId = string.Empty;
            string stratDt = string.Empty;
            string endDt = string.Empty;
            string empinfo = string.Empty;
            string mode = string.Empty;
            UserRepository objUserRepo = new UserRepository();
            UserModel objUserModel = new UserModel();
            ReportsRepository objReportRepo = new ReportsRepository();

            if (!string.IsNullOrEmpty(value))
            {
                empinfo = Crypto.DecryptStringAES(value);
                if (!string.IsNullOrEmpty(empinfo))
                {
                    //empId = Utility.DecryptQueryString(value);
                    if (empinfo.Split('|')[0] == Constants.RegisterUserInfo)
                    {
                        empId = empinfo.Split('|')[1];
                    }
                    else if ((empinfo.Split('|')[0] == Constants.ARTDashboardPage) || (empinfo.Split('|')[0] == Constants.ARTGSDDashboardPage))
                    {
                        empId = empinfo.Split('|')[1];
                        stratDt = empinfo.Split('|')[2];
                        endDt = empinfo.Split('|')[3];
                        mode = empinfo.Split('|')[4];

                    }
                }
            }           

            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ViewBag.UserId = Convert.ToString(Session["UserId"]);
            ViewBag.EmpId =empId;
            ViewBag.StartDt = stratDt;
            ViewBag.EndDt = endDt;
            ViewBag.Mode = mode;
            List<UserInfoModel> lstUser = objUserRepo.GetUserInfo(empId);
            
            if (lstUser != null && lstUser.Count > 0)
            {
                objUserModel.IsRegistered = lstUser[0].isregistered ? "Yes" : "No";
            }

           
            List<UserAccessPrivilege> getUsrPrivilege = objReportRepo.GetUserPrivilege(ViewBag.UserId);
            if (getUsrPrivilege != null)
            {
                if (getUsrPrivilege.Count > 0)
                {
                    if (getUsrPrivilege.Where(a => a.entityname == "pwdReset").ToList().Count > 0)
                        ViewBag.ResetBtn = Convert.ToString(getUsrPrivilege.Where(a => a.entityname == "pwdReset").ToList()[0].entityname);
                    if (getUsrPrivilege.Where(a => a.entityname == "accUnlock").ToList().Count > 0)
                        ViewBag.UnlockBtn = Convert.ToString(getUsrPrivilege.Where(a => a.entityname == "accUnlock").ToList()[0].entityname);
                }
            }
            return View();
        }
        /// <summary>
        /// Dashboad landing page 
        /// </summary>
        /// <returns></returns>
        ///
        [SessionFilter]
        public ActionResult Dashboard()
        {
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ViewBag.UserId = Convert.ToString(Session["UserId"]);
            return View();
        }
        /// <summary>
        /// GSD Dashboad page landing
        /// </summary>
        /// <returns></returns>
        [SessionFilter]
        public ActionResult GSDView()
        {
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            ViewBag.UserId = Convert.ToString(Session["UserId"]);
            return View();
        }

        //
        // POST: /Reports/Create
        [HttpPost]
        public ActionResult EmployeeSearch(FormCollection collection)
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

        public void DownloadCsv(FormCollection coll)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                UserEventsInput objUserEventInput = new UserEventsInput();
                string employeeId = Convert.ToString(coll["UserId"]);

                objUserEventInput.UserId = employeeId;
                objUserEventInput.StartDate = Convert.ToString(coll["StartDate"]);
                objUserEventInput.EndDate = Convert.ToString(coll["EndDate"]);

                List<UserEventsModel> lstUserEvent = objUserRepo.GetUserEventsDetails(objUserEventInput);

                string facsCsv = GetCsvString(employeeId, lstUserEvent);

                // Return the file content with response body. 
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=AccountLockOut_" + employeeId + ".csv");
                Response.Write(facsCsv);
                Response.End();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("-NA-", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }

        private string GetCsvString(string employeeId, List<UserEventsModel> lstUserEvent)
        {
            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Employee Id,Locked out date,Locked out source");

            foreach (UserEventsModel item in lstUserEvent)
            {
                csv.Append(employeeId + ",");
                csv.Append(item.EventDate + ",");
                csv.Append(item.EventSource + ",");
                csv.AppendLine();
            }

            return csv.ToString();
        }
        public void DownloadGSDUserActivityLog(FormCollection coll)
        {
            try
            {
                string mode = Convert.ToString(coll["Mode"]);
                string startDt = Convert.ToString(coll["StartDate"]);
                string endDt = Convert.ToString(coll["EndDate"]);

                ReportsRepository objReportsRepo = new ReportsRepository();

                objReportsRepo.DownloadGSDActivityLog(mode, startDt, endDt);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("-NA-", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }

        public void DownloadAccountlockByMonthCsv(FormCollection coll)
        {
            try
            {
                string userId = Convert.ToString(coll["UserId"]);

                ReportsRepository objReportRepo = new ReportsRepository();
                string facsCsv = objReportRepo.AccountlockByMonthCsvData(userId);

                // Return the file content with response body. 
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=AccountLockOutByMonth_" + userId + ".csv");
                Response.Write(facsCsv);
                Response.End();
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("-NA-", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }
        [HttpGet]
        public ActionResult RegisterUserInfo(string value)
        {
            try
            {
                string userId = string.Empty;
                string empinfo = string.Empty;
                UserInfoModel objUserInfo = new UserInfoModel();
                UserRepository objUserRepo = new UserRepository();
                if (!string.IsNullOrEmpty(value))
                {
                    empinfo = Crypto.DecryptStringAES(value);
                    userId = empinfo.Split('|')[0];
                    Session["EnrollUserId"] = userId;
                }
                else
                {
                    userId = Convert.ToString(Session["EnrollUserId"]);
                    if (string.IsNullOrEmpty(Convert.ToString(Session["EnrollUserId"])))
                        return RedirectToAction("Login", "User");
                }
                objUserInfo.userid = userId;

                ViewBag.DefaultLang = Convert.ToString(Session[Constants.ARTUSERLANG]);

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


                if (!string.IsNullOrEmpty(Convert.ToString(TempData["MobileNumber"])))
                {
                    objUserInfo.mobilenumber = Convert.ToString(TempData["MobileNumber"]);
                    objUserInfo.countrycode = Convert.ToString(TempData["CountryCode"]);
                    objUserInfo.IsOTPSent = Convert.ToString(TempData["IsOTPSent"]);
                    objUserInfo.OTPValidateMsg = Convert.ToString(TempData["OTPValidateMsg"]);
                    objUserInfo.IsValidOTP = Convert.ToString(TempData["IsValidOTP"]);
                }
                else
                {
                    string mobileNumber = GetUserMobileNumber(userId);
                    string countryCode = GetUserCountryCode(userId);
                    objUserInfo.IsOTPSent = Convert.ToString(TempData["IsOTPSent"]);
                    objUserInfo.mobilenumber = mobileNumber;//objUserRepo.GetUserInfoFromAD(userId, Constants.Mobile);
                    objUserInfo.countrycode = countryCode;//objUserRepo.GetUserInfoFromAD(userId, Constants.ADCOUNTRYCODE);

                    if (objUserInfo.mobilenumber != "" || objUserInfo.countrycode != "")
                        Log.LogTrace(new CustomTrace("User mobile no and country code retrieve from AD:", userId, mobileNumber + "" + countryCode));
                }
                ViewBag.Success = TempData["IsOTPSuccess"];
                objUserInfo.lstContryCodes = new SettingsRepository().GetAllCountryCodeDetails();
                if (!String.IsNullOrEmpty(Convert.ToString(TempData["GenOTP"])))
                    objUserInfo.SentOTP = Convert.ToString(TempData["GenOTP"]);


                return View(objUserInfo);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("RegisterUserInfo", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }

        /// <summary>
        /// GSD Dashboad page landing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUserInfo(FormCollection collection)
        {
            if (collection == null)
            {
                return RedirectToAction("RegisterUserInfo");
            }

            UserRepository objUserRepo = new UserRepository();
            ISms objSMS = new SMS24x7();
            string OTP = string.Empty;
            bool isMobileNumPrivate = false;

            string action = collection["btnAction"];
            string userId = Convert.ToString(Session["EnrollUserId"]);
            string mobileNumber = Convert.ToString(collection["txtMobileNum"]);
            string countryValue = Convert.ToString(collection["countryCode"]);
            string countryCode = countryValue.Split('_')[0];
            string telephoneCode = countryValue.Split('_')[1];
            string userName = objUserRepo.GetUserInfoFromAD(userId, Constants.ADGIVENNAME);
            string sendOTP = Convert.ToString(collection["hdnsentOTP"]);

            TempData["MobileNumber"] = mobileNumber;
            TempData["CountryCode"] = countryCode;
            TempData["IsMobileNumPrivate"] = isMobileNumPrivate;
            if (userName == "" || userName == null)
                userName = "User";
            if (action == "SendOTP")
            {
                OTP = objSMS.SendEnrollmentSMS(userId, mobileNumber, "EnrollmentSendOTP", userName, "SendOTP", sendOTP, telephoneCode);
                TempData["GenOTP"] = OTP;
                TempData["IsOTPSent"] = 1;
                Log.LogTrace(new CustomTrace(userId, "OTP sent successfully", "OTP :'" + OTP + "'"));
            }
            else if (action == "EnrollmentLink")
            {
                OTP = objSMS.SendEnrollmentSMS(userId, mobileNumber, "EnrollmentLink", userName, "EnrollmentLink", sendOTP, telephoneCode);
                TempData["IsOTPSuccess"] = "Success";
            }
            else
            {
                Log.LogTrace(new CustomTrace(userId, "UserName  is empty", ""));
                return RedirectToAction("RegisterUserInfo");
            }
            if (!string.IsNullOrEmpty(OTP))
            {
                Log.LogTrace(new CustomTrace(userId, "ART '" + action + "'  sent successfully", "OTP sent :'" + OTP + "'"));
                if (EnrollUpdateUserMobileNumberInAD(userId, countryCode, mobileNumber, action))
                {
                    Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "User mobile number updated in AD"));

                    TempData["OTPValidateMsg"] = Art.MobileNumRegisterSuccess;
                }
                else
                {
                    TempData["OTPValidateMsg"] = Art.MobileNumRegisterFailure;
                }
                TempData["MobileNumber"] = "";
                return RedirectToAction("RegisterUserInfo");
            }

            else
            {
                Log.LogTrace(new CustomTrace(userId, Constants.Register_User_Info, "Sending OTP - Failure"));
                TempData["OpenRegisterForOTP"] = "1";
                TempData["UserMessageMOB"] = Art.ErrorSendMsg;
                return RedirectToAction("RegisterUserInfo");
            }

        }
        public bool UpdateUserMobileNumberInAD(string userId, string countryCode, string mobileNumber)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                objUserRepo.UpdateUserInfo(userId, Utility.Encryptor.Encrypt(countryCode, Constants.PASSPHARSE), Utility.Encryptor.Encrypt(mobileNumber, Constants.PASSPHARSE));
                objUserRepo.SetUserInfoInAD(userId, Constants.Mobile, mobileNumber);
                if (mobileNumber != "" || countryCode != "")
                    Log.LogTrace(new CustomTrace("User mobile no and country code updated to AD :", userId, mobileNumber + "" + countryCode));
                objUserRepo.SetUserInfoInAD(userId, Constants.ADCOUNTRYCODE, countryCode);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool EnrollUpdateUserMobileNumberInAD(string userId, string countryCode, string mobileNumber, string action)
        {
            try
            {
                UserRepository objUserRepo = new UserRepository();
                if (action == "EnrollmentLink")
                {
                    objUserRepo.UpdateUserInfo(userId, Utility.Encryptor.Encrypt(countryCode, Constants.PASSPHARSE), Utility.Encryptor.Encrypt(mobileNumber, Constants.PASSPHARSE));
                }
                else
                {
                    objUserRepo.SetUserInfoInAD(userId, Constants.Mobile, mobileNumber);
                    if (mobileNumber != "" || countryCode != "")
                        Log.LogTrace(new CustomTrace("User mobile no and country code updated to AD :", userId, mobileNumber + "" + countryCode));
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
        private string GetUserMobileNumber(string userId)
        {
            string mobileNumber = string.Empty;

            try
            {
                UserRepository objUserRepo = new UserRepository();
                // List<UserModel> lstUserModel = objUserRepo.GetUserInfo(userId);
                List<UserInfoModel> lstUserModel = objUserRepo.GetUserInfo(userId);
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
                // List<UserModel> lstUserModel = objUserRepo.GetUserInfo(userId);
                List<UserInfoModel> lstUserModel = objUserRepo.GetUserInfo(userId);

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
    }
}
