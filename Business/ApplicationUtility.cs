using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class ApplicationUtility
    {


        public static Dictionary<string, string> AfterLoginRoute(string token)
        {

            Dictionary<string, string> afterloginroute = new Dictionary<string, string>();

            string loggedintoken = token;

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            Business.ApplicationService.appuser loggedinuser = appclient.GetUserObject(loggedintoken);

            List<menuitem> topmenu = new List<menuitem>();
            switch (loggedinuser.ObjectType.ToString())
            {
                case "private":
                    afterloginroute.Add("action", "index");
                    afterloginroute.Add("controller", "jobs");
                    break;
                case "comadmin":
                    afterloginroute.Add("action", "index");
                    afterloginroute.Add("controller", "jobs");
                    break;
                case "comrecruit":
                    afterloginroute.Add("action", "index");
                    afterloginroute.Add("controller", "jobs");
                    break;
                case "tsadmin":
                    afterloginroute.Add("action", "admindashboard");
                    afterloginroute.Add("controller", "timesheet");
                    break;
                case "consultant":
                    afterloginroute.Add("action", "dashboard");
                    afterloginroute.Add("controller", "timesheet");
                    break;
            }


            return afterloginroute;
        }


        public List<menuitem> GetTopmenu(string token)
        {
            string loggedintoken = token;

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            Business.ApplicationService.appuser loggedinuser = appclient.GetUserObject(loggedintoken);

            List<menuitem> topmenu = new List<menuitem>();
            switch (loggedinuser.ObjectType.ToString())
            {
                case "private":
                    topmenu = BuildSuperAdminMenu();
                    break;
                case "comadmin":
                    topmenu = BuildJobUserMenu();
                    break;
                case "comrecruit":
                    topmenu = BuildSuperAdminMenu();
                    break;
                case "tsadmin":
                    topmenu = BuildTimeSheetAdminMenu();
                    break;
                case "consultant":
                    topmenu = BuildConsultantMenu();
                    break;
            }


            return topmenu;
        }

        private List<menuitem> BuildSuperAdminMenu()
        {
            List<menuitem> menu = new List<menuitem>();

            menuitem jobsmenu = new menuitem();

            jobsmenu.linkname = "Jobs";
            jobsmenu.linkpath = "/jobs";
            jobsmenu.enableactivecode = 1;
            jobsmenu.isparentmenuitem = true;
            jobsmenu.hasChildren = false;
            jobsmenu.hasParent = false;
            jobsmenu.childmenus = null;
            jobsmenu.childumenusettag = "jobs";
            menu.Add(jobsmenu);

            menuitem contactsmenu = new menuitem();
            contactsmenu.linkname = "Contacts";
            contactsmenu.linkpath = "/contacts";
            contactsmenu.enableactivecode = 2;
            contactsmenu.isparentmenuitem = true;
            contactsmenu.hasChildren = false;
            contactsmenu.hasParent = false;
            contactsmenu.childmenus = null;
            contactsmenu.childumenusettag = "contacts";
            menu.Add(contactsmenu);

            menuitem mailinglistmenu = new menuitem();
            mailinglistmenu.linkname = "Mailing Lists";
            mailinglistmenu.linkpath = "/lists";
            mailinglistmenu.enableactivecode = 3;
            mailinglistmenu.isparentmenuitem = true;
            mailinglistmenu.hasChildren = false;
            mailinglistmenu.hasParent = false;
            mailinglistmenu.childmenus = null;
            mailinglistmenu.childumenusettag = "mailinglist";
            menu.Add(mailinglistmenu);

            menuitem communicationmenu = new menuitem();
            communicationmenu.linkname = "Communication";
            communicationmenu.linkpath = "/communication";
            communicationmenu.enableactivecode = 4;
            communicationmenu.isparentmenuitem = true;
            communicationmenu.hasChildren = false;
            communicationmenu.hasParent = false;
            communicationmenu.childmenus = null;
            communicationmenu.childumenusettag = "communication";
            menu.Add(communicationmenu);

            menuitem timesheetsmenu = new menuitem();
            timesheetsmenu.linkname = "Timesheets";
            timesheetsmenu.linkpath = "/timesheet/dashboard";
            timesheetsmenu.enableactivecode = 5;
            timesheetsmenu.isparentmenuitem = true;
            timesheetsmenu.hasChildren = false;
            timesheetsmenu.hasParent = false;
            timesheetsmenu.childmenus = null;
            timesheetsmenu.childumenusettag = "timesheets";
            menu.Add(timesheetsmenu);

            menuitem candidatesmenu = new menuitem();
            candidatesmenu.linkname = "Candidates";
            candidatesmenu.linkpath = "/candidate";
            candidatesmenu.enableactivecode = 6;
            candidatesmenu.isparentmenuitem = true;
            candidatesmenu.hasChildren = false;
            candidatesmenu.hasParent = false;
            candidatesmenu.childmenus = null;
            candidatesmenu.childumenusettag = "candidates";
            menu.Add(candidatesmenu);

            menuitem usersmenu = new menuitem();
            usersmenu.linkname = "Users";
            usersmenu.linkpath = "/users";
            usersmenu.enableactivecode = 7;
            usersmenu.isparentmenuitem = true;
            usersmenu.hasChildren = false;
            usersmenu.hasParent = false;
            usersmenu.childmenus = null;
            usersmenu.childumenusettag = "users";
            menu.Add(usersmenu);

            menuitem suppliermenu = new menuitem();
            suppliermenu.linkname = "Suppliers";
            suppliermenu.linkpath = "/suppliers";
            suppliermenu.enableactivecode = 8;
            suppliermenu.isparentmenuitem = true;
            suppliermenu.hasChildren = false;
            suppliermenu.hasParent = false;
            suppliermenu.childmenus = null;
            suppliermenu.childumenusettag = "suppliers";
            menu.Add(suppliermenu);


            return menu;
        }

        private List<menuitem> BuildJobUserMenu()
        {
            List<menuitem> menu = new List<menuitem>();

            menuitem jobsmenu = new menuitem();

            jobsmenu.linkname = "Jobs";
            jobsmenu.linkpath = "/jobs";
            jobsmenu.enableactivecode = 1;
            jobsmenu.isparentmenuitem = true;
            jobsmenu.hasChildren = false;
            jobsmenu.hasParent = false;
            jobsmenu.childmenus = null;
            jobsmenu.childumenusettag = "jobs";
            menu.Add(jobsmenu);

            menuitem contactsmenu = new menuitem();
            contactsmenu.linkname = "Contacts";
            contactsmenu.linkpath = "/contacts";
            contactsmenu.enableactivecode = 2;
            contactsmenu.isparentmenuitem = true;
            contactsmenu.hasChildren = false;
            contactsmenu.hasParent = false;
            contactsmenu.childmenus = null;
            contactsmenu.childumenusettag = "contacts";
            menu.Add(contactsmenu);

            menuitem mailinglistmenu = new menuitem();
            mailinglistmenu.linkname = "Mailing Lists";
            mailinglistmenu.linkpath = "/lists";
            mailinglistmenu.enableactivecode = 3;
            mailinglistmenu.isparentmenuitem = true;
            mailinglistmenu.hasChildren = false;
            mailinglistmenu.hasParent = false;
            mailinglistmenu.childmenus = null;
            mailinglistmenu.childumenusettag = "mailinglist";
            menu.Add(mailinglistmenu);

            menuitem communicationmenu = new menuitem();
            communicationmenu.linkname = "Communication";
            communicationmenu.linkpath = "/communication";
            communicationmenu.enableactivecode = 4;
            communicationmenu.isparentmenuitem = true;
            communicationmenu.hasChildren = false;
            communicationmenu.hasParent = false;
            communicationmenu.childmenus = null;
            communicationmenu.childumenusettag = "communication";
            menu.Add(communicationmenu);
            return menu;
        }

        private List<menuitem> BuildTimeSheetAdminMenu()
        {
            List<menuitem> menu = new List<menuitem>();

            menuitem timesheetsmenu = new menuitem();
            timesheetsmenu.linkname = "Timesheets";
            timesheetsmenu.linkpath = "/timesheet/admindashboard";
            timesheetsmenu.enableactivecode = 5;
            timesheetsmenu.isparentmenuitem = true;
            timesheetsmenu.hasChildren = false;
            timesheetsmenu.hasParent = false;
            timesheetsmenu.childmenus = null;
            timesheetsmenu.childumenusettag = "timesheets";
            menu.Add(timesheetsmenu);



            menuitem usersmenu = new menuitem();
            usersmenu.linkname = "Users";
            usersmenu.linkpath = "/users";
            usersmenu.enableactivecode = 7;
            usersmenu.isparentmenuitem = true;
            usersmenu.hasChildren = false;
            usersmenu.hasParent = false;
            usersmenu.childmenus = null;
            usersmenu.childumenusettag = "users";
            menu.Add(usersmenu);


            return menu;
        }

        private List<menuitem> BuildConsultantMenu()
        {
            List<menuitem> menu = new List<menuitem>();


            menuitem timesheetsmenu = new menuitem();
            timesheetsmenu.linkname = "Timesheets";
            timesheetsmenu.linkpath = "/timesheet/dashboard";
            timesheetsmenu.enableactivecode = 5;
            timesheetsmenu.isparentmenuitem = true;
            timesheetsmenu.hasChildren = false;
            timesheetsmenu.hasParent = false;
            timesheetsmenu.childmenus = null;
            timesheetsmenu.childumenusettag = "timesheets";
            menu.Add(timesheetsmenu);
            return menu;
        }

        private List<menuitem> BuildRecruiterMenu()
        {
            List<menuitem> menu = new List<menuitem>();
            menuitem jobsmenu = new menuitem();

            jobsmenu.linkname = "Jobs";
            jobsmenu.linkpath = "/jobs";
            jobsmenu.enableactivecode = 1;
            jobsmenu.isparentmenuitem = true;
            jobsmenu.hasChildren = false;
            jobsmenu.hasParent = false;
            jobsmenu.childmenus = null;
            jobsmenu.childumenusettag = "jobs";
            menu.Add(jobsmenu);

            menuitem contactsmenu = new menuitem();
            contactsmenu.linkname = "Contacts";
            contactsmenu.linkpath = "/contacts";
            contactsmenu.enableactivecode = 2;
            contactsmenu.isparentmenuitem = true;
            contactsmenu.hasChildren = false;
            contactsmenu.hasParent = false;
            contactsmenu.childmenus = null;
            contactsmenu.childumenusettag = "contacts";
            menu.Add(contactsmenu);

            menuitem mailinglistmenu = new menuitem();
            mailinglistmenu.linkname = "Mailing Lists";
            mailinglistmenu.linkpath = "/lists";
            mailinglistmenu.enableactivecode = 3;
            mailinglistmenu.isparentmenuitem = true;
            mailinglistmenu.hasChildren = false;
            mailinglistmenu.hasParent = false;
            mailinglistmenu.childmenus = null;
            mailinglistmenu.childumenusettag = "mailinglist";
            menu.Add(mailinglistmenu);

            menuitem communicationmenu = new menuitem();
            communicationmenu.linkname = "Communication";
            communicationmenu.linkpath = "/communication";
            communicationmenu.enableactivecode = 4;
            communicationmenu.isparentmenuitem = true;
            communicationmenu.hasChildren = false;
            communicationmenu.hasParent = false;
            communicationmenu.childmenus = null;
            communicationmenu.childumenusettag = "communication";
            menu.Add(communicationmenu);
            return menu;
        }

    }

    public class menuitem
    {
        public string linkname { get; set; }
        public string linkpath { get; set; }
        public int enableactivecode { get; set; }
        public bool isparentmenuitem { get; set; }
        public bool hasParent { get; set; }
        public bool hasChildren { get; set; }
        public string childumenusettag { get; set; }
        public List<menuitem> childmenus { get; set; }
        public int menuposition { get; set; }


        public menuitem()
        {
            this.linkname = "";
            this.linkpath = "";
            this.enableactivecode = 0;
            this.isparentmenuitem = false;
            this.hasParent = false;
            this.hasChildren = false;
            this.childumenusettag = "";
            this.childmenus = new List<menuitem>();
            this.menuposition = 0;
        }
    }
}
