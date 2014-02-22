using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();

        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            //UI.Models.Job job = new Models.Job();
            //job.InsertJobDetials(jobdata);
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string newpassword, string accesstoken)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.ChangePassword(oldpassword, newpassword, Session["usertoken"].ToString());
            return View();
        }

    }
}
