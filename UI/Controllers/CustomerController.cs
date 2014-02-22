using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace UI.Controllers
{
    public class CustomerController : Controller
    {
        UI.Models.CustomerModel customermodel = new Models.CustomerModel();
      

        public ActionResult Index()
        {

            return View(customermodel.GetCustomerCollection(Session["companyid"].ToString()));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult View()
        {
            string customerid = Request.QueryString["cid"].ToString();
            return View(customermodel.GetCustomerCollection(Session["companyid"].ToString(), customerid));
        }

    }

    public class Customerlistitem
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
