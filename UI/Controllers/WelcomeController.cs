using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class WelcomeController : Controller
    {
        //
        // GET: /Welcome/
        Business.DataLayer Businessdata = new Business.DataLayer();
        public ActionResult Index()
        {

            return View(new UI.Models.PTAccount().GetUserAccounts(Session["usertoken"].ToString()));
        }

        [HttpPost]
        public void CreateAccount(string accountname,
                                  string userid,
                                 string accounttype,
                                 string catid,
                                 string catname,
                                 string desc,
                                 string providerid,
                                 string providername,
                                 string amount,
                                 int duemonth,
                                 string recurrence,
                                 int duedate)
        {
           
            string newsubcatid = Guid.NewGuid().ToString().Replace("-", "");
            string acid = Businessdata.createUserAccount(accountname, userid, accounttype, newsubcatid, catid, catname, desc, providerid, providername, amount, duemonth, recurrence, duedate, false, false);
            Businessdata.createTransactionOverAccount(acid, userid, newsubcatid, catid, desc, amount, duemonth, duedate, 0, 0, 0, DataLayer.getRecurrence(recurrence));
        }

               
        public ActionResult ChangeSetupStatusTotrue()
        {
            Businessdata.ChangeSetupStatus(Session["userid"].ToString(),true);

           return  RedirectToAction("Index", "Payments");
        }
        [HttpPost]
        public void ChangeSetupStatusTofalse()
        {
            Businessdata.ChangeSetupStatus(Session["userid"].ToString(), false);
        }
    }
}
