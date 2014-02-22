using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class SupplierController : Controller
    {
        //
        // GET: /Supplier/

        Business.DataLayer businessdata = new Business.DataLayer();

        public ActionResult Index()
        {
            //Displays All the supppliers of the Company

            List<Supplier> AllSuppliers = new List<Supplier>();

            List<Business.ApplicationService.customer> suppliers = businessdata.GetAllSuppliers(Session["companyid"].ToString());

            foreach (Business.ApplicationService.customer appSupplier in suppliers)
            {
                Supplier objSupplier = new Supplier();

                objSupplier.email = appSupplier.email;
                objSupplier.fname = appSupplier.name;
                objSupplier.lname= appSupplier.name;

                AllSuppliers.Add(objSupplier);
            }

            return View(AllSuppliers);
        }

        [HttpGet]
        public ActionResult Add()
        {
            //Displays the Add Supplier form to create a new supplier

            return View();
        }

        [HttpPost]
        public ActionResult Add(UI.Models.Supplier supplierdata)
        {
            //Creates Supplier from he supplier data
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            //Displays the Edit form the Supplier Information
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UI.Models.Supplier supplierdata)
        {
            //Edits and modifies the Supplier Information
            return View();
        }

        public ActionResult supplier()
        {

            //Displays the individual supplier information
            return View();
        }

    }
}
