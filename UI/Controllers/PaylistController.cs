using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UI.Controllers
{
    public class PaylistController : Controller
    {
        //
        // GET: /Paylist/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddData()
        {
            return View();
        }

      
    }


}
