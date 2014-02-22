using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/

        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult Expenses()
        {
            return View(new UI.Models.PTAccount().GetUserAccounts(Session["usertoken"].ToString()));
        }

        public ActionResult Accounts()
        {
            return View();
        }
    }
}
