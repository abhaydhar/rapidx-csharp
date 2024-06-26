using BotDetect.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ART.Controllers
{
    public class BotDetectController : Controller
    {
        //
        // GET: /BotDetect/
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckCaptcha(string captchaId, string instanceId, string userInput)
        {
            bool ajaxValidationResult = Captcha.AjaxValidate(captchaId, userInput, instanceId);
            return Json(ajaxValidationResult, JsonRequestBehavior.AllowGet);
        }
	}
}