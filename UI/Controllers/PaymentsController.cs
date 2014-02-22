using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class PaymentsController : Controller
    {
        Business.DataLayer bdata = new Business.DataLayer();
        //
        // GET: /Payments/

        public ActionResult Index()
        {
            return View(new TransactionItem().getTransactions(Session["usertoken"].ToString()));
        }

        

    }
}
