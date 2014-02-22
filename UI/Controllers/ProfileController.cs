using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UI.Models.ProfileModelObject.ChangePassword passworddata)
        {
            return View();
        }
    }
}
