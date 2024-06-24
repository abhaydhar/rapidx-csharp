using ArtHandler;
using ArtHandler.Model;
using ArtHandler.Repository;
using ArtMaster;
using ArtMaster.Filters;
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

namespace ART.Controllers
{
    [UserCultureFilter]
    public class UserController : Controller
    {
        private string sessionId = System.Web.HttpContext.Current.Session.SessionID;
        #region User Login
        /// <summary>
        /// User Login Page Get Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.Message = TempData["UserMessage"];
            ViewBag.DefaultLang = Session[Constants.ARTUSERLANG];
            return View(); // user login page return to view
        }

        /// <summary>
        /// User Login Page Post Action 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string userName = Convert.ToString(collection["txtUserName"]);
                string password = Convert.ToString(collection["txtPassword"]);


                if (string.IsNullOrEmpty(userName))
                {
                    TempData["UserMessage"] = Art.EnterUserName;
                    return RedirectToRoute("Login"); // If username is null or empty ,its redirect to login page.
                }
                if (string.IsNullOrEmpty(password))
                {
                    TempData["UserMessage"] = Art.EnterPassword;
                    return RedirectToRoute("Login");// If password is null or empty ,its redirect to login page.
                }

                // first check the entered user has access to GSD Screens
                UserRepository objUserRepo = new UserRepository();
                List<RptUserModel> lstUser = objUserRepo.CheckUserAccess(userName);
                //Check the user count is greater than or not
                if (lstUser.Count == 1)
                {
                    // Validate user name / password
                    bool isValid = objUserRepo.Authenticate(userName, password);

                    if (isValid)
                    {
                        Log.LogTrace(new CustomTrace(userName, Constants.GSDLOGIN, "Valid User Name"));

                        Session["UserId"] = userName;
                        //Session["IsAdmin"] = lstUser[0].Isadmin;
                        Session["IsReadOnly"] = lstUser[0].IsReadOnly;
                        //user page redirect to dashboad or employee search page based on the user login
                        return Redirect(lstUser[0].Defaultlandingurl);
                        //return RedirectToAction("EmployeeSearch", "Reports");
                    }
                    else
                    {
                        Log.LogTrace(new CustomTrace(userName, Constants.GSDLOGIN, "Invalid User Name"));

                        TempData["UserMessage"] = "Invalid UserName / Password";
                        //Invalid User redirect to User Login Page
                        return RedirectToAction("Login", "User");
                    }
                }
                else
                {
                    Log.LogTrace(new CustomTrace(userName, Constants.GSDLOGIN, "You are not having the access to this site, please contact administrator"));
                    TempData["UserMessage"] = "You are not having the access to this site, please contact administrator";
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(Constants.Register_Login, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return RedirectToAction("Index", "Error");
            }
        }



        #endregion
    }
}
