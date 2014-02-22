using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            //return View();
            Session.Clear();
           return RedirectToAction("Index", "Login");
        }

    }
}
