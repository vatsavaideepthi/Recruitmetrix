using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            UI.Models.UserViewModel userviewdata = new Models.UserViewModel();
            
            List<Business.ApplicationService.appuser> allusers = new List<Business.ApplicationService.appuser>();
            List<Business.ApplicationService.appuser> admins = new List<Business.ApplicationService.appuser>();
            List<Business.ApplicationService.appuser> recruiters = new List<Business.ApplicationService.appuser>();
            List<Business.ApplicationService.appuser> consultants = new List<Business.ApplicationService.appuser>();
            List<Business.ApplicationService.appuser> timesheetadmins = new List<Business.ApplicationService.appuser>();
           
            Business.ApplicationService.appuser loggedinuser = appclient.GetUserObject(Session["usertoken"].ToString());

            if (loggedinuser.ObjectType.ToLower() == "private")
            {
                admins = appclient.GetAdmins(Session["companyid"].ToString(), "comadmin", Session["usertoken"].ToString()).ToList();
                recruiters = appclient.GetAdmins(Session["companyid"].ToString(), "comrecruit", Session["usertoken"].ToString()).ToList();
                allusers.AddRange(admins);
                allusers.AddRange(recruiters);

                Business.menuitem addrecruiter = new Business.menuitem();

                addrecruiter.linkname = "Add Recruiter";
                addrecruiter.linkpath = "/users/recruiter";
                addrecruiter.menuposition = 1;
                userviewdata.actions.Add(addrecruiter);

                Business.menuitem addadmin = new Business.menuitem();

                addadmin.linkname = "Add Admin";
                addadmin.linkpath = "/users/admin";
                addadmin.menuposition = 2;
                userviewdata.actions.Add(addadmin);

                userviewdata.UsersCollection = allusers;

            }
            else if (loggedinuser.ObjectType.ToLower() == "tsadmin")
            {
                consultants = appclient.GetAdmins(Session["companyid"].ToString(), "consultant", Session["usertoken"].ToString()).ToList();
                allusers.AddRange(consultants);

                Business.menuitem addconsultant = new Business.menuitem();

                addconsultant.linkname = "Add Consultant";
                addconsultant.linkpath = "/users/consultant";
                addconsultant.menuposition = 1;
                userviewdata.actions.Add(addconsultant);

                timesheetadmins = appclient.GetAdmins(Session["companyid"].ToString(), "tsadmin", Session["usertoken"].ToString()).ToList();
                allusers.AddRange(timesheetadmins);

                Business.menuitem addTimeSheetAdmins = new Business.menuitem();

                addTimeSheetAdmins.linkname = "Add Timesheet Admin";
                addTimeSheetAdmins.linkpath = "/users/sheetadmin";
                addTimeSheetAdmins.menuposition = 2;

                userviewdata.actions.Add(addTimeSheetAdmins);

                userviewdata.UsersCollection = allusers;
            }





            return View(userviewdata);
        }

        [HttpGet]
        public ActionResult admin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult recruiter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult admin(UI.Models.portaluser admindata)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.AddAdmin(admindata.usermail, admindata.userpassword, admindata.usertitle, Session["usertoken"].ToString());

            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult recruiter(UI.Models.portaluser recruiterdata)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.AddRecruiter(recruiterdata.usermail, recruiterdata.userpassword, recruiterdata.usertitle, Session["usertoken"].ToString());

            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult consultant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult consultant(UI.Models.portaluser recruiterdata)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.Addconsultant(recruiterdata.usermail, recruiterdata.userpassword, recruiterdata.usertitle, Session["usertoken"].ToString());

            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult sheetadmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult sheetadmin(UI.Models.portaluser recruiterdata)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.moduleadmin(recruiterdata.usermail, recruiterdata.userpassword, recruiterdata.usertitle, Session["usertoken"].ToString());

            return RedirectToAction("index");
        }

    }
}
