using ArtHandler.Model;
using ArtHandler.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ArtMaster.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "User");
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
    }
}