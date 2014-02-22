using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class ExpensesController : Controller
    {
        //
        // GET: /Expenses/

        public ActionResult Index()
        {
            return View(new TransactionItem().getPayments(Session["usertoken"].ToString()));
        }

        public ActionResult history()
        {
            return View(new TransactionItem().getPaymentHistory(Session["usertoken"].ToString()));
        }

    }
}
