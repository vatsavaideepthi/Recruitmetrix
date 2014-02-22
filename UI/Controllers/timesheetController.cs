using Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Models;
using Business.ApplicationService;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using UI.Utilities;

namespace UI.Controllers
{
    public class timesheetController : Controller
    {
        //
        // GET: /timesheet/

        public ActionResult Index()
        {

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.timesheet timesheet = appclient.Gettimesheet("0ae295c08a6711e3a3f900155d613005", Session["companyid"].ToString(), Session["userid"].ToString());
            Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), "", "1234567890", "", "2013-08-22 12:25:49", "2013-08-22 12:25:49");
            UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();

            singletimesheet.id = timesheet.id;
            singletimesheet.createdon = timesheet.createddate;
            singletimesheet.data = timesheet.data;
            UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheet.data);
            //  UI.Models.timesheetitem tsitem = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.Models.timesheetitem>(tsitems.items.ToString());         
            singletimesheet.Company = objsheet.Company;
            singletimesheet.Client = objsheet.Client;
            singletimesheet.Items = objsheet.Items;
            singletimesheet.Metadata = objsheet.Metadata;
            return View(singletimesheet);
        }



        [HttpGet]
        public ActionResult Add()
        {

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            List<customweek> weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(DateTime.Now.Month, DateTime.Now.Year);
            customweek editingweek = new customweek();

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int week = 1;

            try
            {
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
                month = Convert.ToInt32(Request.QueryString["month"].ToString());
                week = Convert.ToInt32(Request.QueryString["week"].ToString()) - 1;
                weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(month, year);
                editingweek = weekcollection[week];
            }
            catch
            {
                foreach (customweek wk in weekcollection)
                {
                    if (wk.isCurrentweek)
                    {
                        editingweek = wk;
                    }
                }
            }

            return View(editingweek);
        }
        [HttpPost]
        public ActionResult Add(FormCollection data)
        {
            int selectedweek = Convert.ToInt32(data["selectedweek"]);
            List<UI.Utilities.customweek> periods = UI.Utilities.DateTimeUtility.GetWeeks(2, 2014);
            return View(periods[selectedweek]);
        }

        [HttpGet]
        public ActionResult dashboard()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            userdashboardtimesheet usermonthlydashboardtimesheet = new userdashboardtimesheet();

            Business.ApplicationService.appuser dashboarduser = appclient.GetUserObject(Session["usertoken"].ToString());

            usermonthlydashboardtimesheet.User = dashboarduser;
            usermonthlydashboardtimesheet.timesheetmonth = UI.Utilities.DateTimeUtility.ToLongMonthName(DateTime.Now.Month);
            usermonthlydashboardtimesheet.timesheetsstartdate = "";
            usermonthlydashboardtimesheet.timesheetenddate = "";

            List<AppWeekTimeSheet> weektimesheetscollection = new List<AppWeekTimeSheet>();
            List<customweek> weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(DateTime.Now.Month, DateTime.Now.Year);

            foreach (customweek custweek in weekcollection)
            {
                AppWeekTimeSheet dashboarddata = new AppWeekTimeSheet();

                dashboarddata.timesheetweek = custweek;

                Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), Session["userid"].ToString(), "", "", custweek.startdate.ToString("yyyy-MM-dd hh:mm:ss"), custweek.enddate.ToString("yyyy-MM-dd hh:mm:ss"));
                UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();
                if (timesheets.Length > 0)
                {
                    UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheets[0].data);
                    dashboarddata.weektimesheetdata = objsheet;
                }

                weektimesheetscollection.Add(dashboarddata);
            }

            usermonthlydashboardtimesheet.weektimesheets = weektimesheetscollection;

            return View(usermonthlydashboardtimesheet);
        }

        [HttpPost]
        public ActionResult dashboard(FormCollection collection)
        {


            int selectedmonth = Convert.ToInt32(collection["selectedmonth"]);
            int selectedyear = Convert.ToInt32(collection["selectedyear"]);

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            userdashboardtimesheet usermonthlydashboardtimesheet = new userdashboardtimesheet();
            Business.ApplicationService.appuser dashboarduser = appclient.GetUserObject(Session["usertoken"].ToString());

            usermonthlydashboardtimesheet.User = dashboarduser;
            usermonthlydashboardtimesheet.timesheetmonth = UI.Utilities.DateTimeUtility.ToLongMonthName(selectedmonth);
            usermonthlydashboardtimesheet.timesheetsstartdate = "";
            usermonthlydashboardtimesheet.timesheetenddate = "";

            List<AppWeekTimeSheet> weektimesheetscollection = new List<AppWeekTimeSheet>();
            List<customweek> weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(selectedmonth, selectedyear);

            foreach (customweek custweek in weekcollection)
            {
                AppWeekTimeSheet dashboarddata = new AppWeekTimeSheet();

                dashboarddata.timesheetweek = custweek;

                Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), "", "", "", custweek.startdate.ToString("yyyy-MM-dd hh:mm:ss"), custweek.enddate.ToString("yyyy-MM-dd hh:mm:ss"));
                UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();
                if (timesheets.Length > 0)
                {
                    UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheets[0].data);
                    dashboarddata.weektimesheetdata = objsheet;
                }

                weektimesheetscollection.Add(dashboarddata);
            }

            usermonthlydashboardtimesheet.weektimesheets = weektimesheetscollection;

            return View(usermonthlydashboardtimesheet);


        }

        [HttpGet]
        public ActionResult Admindashboard()
        {
            admintimesheet adminmonthlydashboard = new admintimesheet();
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            adminmonthlydashboard.year = DateTime.Now.Year;
            adminmonthlydashboard.month = DateTime.Now.Month;
            adminmonthlydashboard.monthname = UI.Utilities.DateTimeUtility.ToLongMonthName(DateTime.Now.Month);
            adminmonthlydashboard.monthstarts = DateTime.Now;
            adminmonthlydashboard.monthends = DateTime.Now;
            adminmonthlydashboard.weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(DateTime.Now.Month, DateTime.Now.Year);
            adminmonthlydashboard.numberofweeks = adminmonthlydashboard.weekcollection.Count;


            adminmonthlydashboard.usertimesheets = new List<userdashboardtimesheet>();

            List<Business.ApplicationService.appuser> consultants = new List<Business.ApplicationService.appuser>();
            consultants = appclient.GetAdmins(Session["companyid"].ToString(), "consultant", Session["usertoken"].ToString()).ToList();

            foreach (Business.ApplicationService.appuser consultant in consultants)
            {
                userdashboardtimesheet usermonthlydashboardtimesheet = new userdashboardtimesheet();

                //Retrieving Consultant Information
                usermonthlydashboardtimesheet.User = consultant;

                usermonthlydashboardtimesheet.timesheetmonth = UI.Utilities.DateTimeUtility.ToLongMonthName(DateTime.Now.Month);
                usermonthlydashboardtimesheet.timesheetsstartdate = "";
                usermonthlydashboardtimesheet.timesheetenddate = "";

                List<AppWeekTimeSheet> weektimesheetscollection = new List<AppWeekTimeSheet>();

                foreach (customweek custweek in adminmonthlydashboard.weekcollection)
                {
                    AppWeekTimeSheet dashboarddata = new AppWeekTimeSheet();

                    dashboarddata.timesheetweek = custweek;

                    Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), consultant.Id, "", "", custweek.startdate.ToString("yyyy-MM-dd hh:mm:ss"), custweek.enddate.ToString("yyyy-MM-dd hh:mm:ss"));
                    UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();
                    if (timesheets.Length > 0)
                    {
                        UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheets[0].data);
                        dashboarddata.weektimesheetdata = objsheet;
                        dashboarddata.hasTimeSheet = true;
                    }
                    else
                    {
                        dashboarddata.hasTimeSheet = false;
                    }

                    weektimesheetscollection.Add(dashboarddata);
                }

                usermonthlydashboardtimesheet.weektimesheets = weektimesheetscollection;

                adminmonthlydashboard.usertimesheets.Add(usermonthlydashboardtimesheet);
            }

            return View(adminmonthlydashboard);

        }


        [HttpGet]
        public ActionResult Edit()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            List<customweek> weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(DateTime.Now.Month, DateTime.Now.Year);
            customweek editingweek = new customweek();

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int week = 1;

            try
            {
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
                month = Convert.ToInt32(Request.QueryString["month"].ToString());
                week = Convert.ToInt32(Request.QueryString["week"].ToString()) - 1;
                weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(month, year);
                editingweek = weekcollection[week];
            }
            catch
            {
                foreach (customweek wk in weekcollection)
                {
                    if (wk.isCurrentweek)
                    {
                        editingweek = wk;
                    }
                }
            }
            userdashboardtimesheet usermonthlydashboardtimesheet = new userdashboardtimesheet();
            Business.ApplicationService.appuser dashboarduser = appclient.GetUserObject(Session["usertoken"].ToString());

            usermonthlydashboardtimesheet.User = dashboarduser;
            usermonthlydashboardtimesheet.timesheetmonth = UI.Utilities.DateTimeUtility.ToLongMonthName(editingweek.month);
            usermonthlydashboardtimesheet.timesheetsstartdate = editingweek.startdate.ToShortDateString();
            usermonthlydashboardtimesheet.timesheetenddate = editingweek.enddate.ToShortDateString();

            List<AppWeekTimeSheet> weektimesheetscollection = new List<AppWeekTimeSheet>();


            AppWeekTimeSheet dashboarddata = new AppWeekTimeSheet();

            dashboarddata.timesheetweek = editingweek;

            Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), "", "", "", editingweek.startdate.ToString("yyyy-MM-dd hh:mm:ss"), editingweek.enddate.ToString("yyyy-MM-dd hh:mm:ss"));
            UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();
            if (timesheets.Length > 0)
            {
                UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheets[0].data);
                dashboarddata.timesheetid = timesheets[0].id;
                dashboarddata.weektimesheetdata = objsheet;
            }
            weektimesheetscollection.Add(dashboarddata);

            usermonthlydashboardtimesheet.weektimesheets = weektimesheetscollection;
            return View(usermonthlydashboardtimesheet);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection timesheetdata)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Admindashboard(FormCollection collection)
        {

            int selectedmonth = Convert.ToInt32(collection["selectedmonth"]);
            int selectedyear = Convert.ToInt32(collection["selectedyear"]);

            admintimesheet adminmonthlydashboard = new admintimesheet();
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            adminmonthlydashboard.year = selectedyear;
            adminmonthlydashboard.month = selectedmonth;
            adminmonthlydashboard.monthname = UI.Utilities.DateTimeUtility.ToLongMonthName(selectedmonth);
            adminmonthlydashboard.monthstarts = new DateTime(selectedyear,selectedmonth,1);
            adminmonthlydashboard.monthends = new DateTime(selectedyear, selectedmonth,1);
            adminmonthlydashboard.weekcollection = UI.Utilities.DateTimeUtility.GetWeeks(selectedmonth, selectedyear);
            adminmonthlydashboard.numberofweeks = adminmonthlydashboard.weekcollection.Count;


            adminmonthlydashboard.usertimesheets = new List<userdashboardtimesheet>();

            List<Business.ApplicationService.appuser> consultants = new List<Business.ApplicationService.appuser>();
            consultants = appclient.GetAdmins(Session["companyid"].ToString(), "consultant", Session["usertoken"].ToString()).ToList();

            foreach (Business.ApplicationService.appuser consultant in consultants)
            {
                userdashboardtimesheet usermonthlydashboardtimesheet = new userdashboardtimesheet();

                //Retrieving Consultant Information
                usermonthlydashboardtimesheet.User = consultant;

                usermonthlydashboardtimesheet.timesheetmonth = UI.Utilities.DateTimeUtility.ToLongMonthName(selectedmonth);
                usermonthlydashboardtimesheet.timesheetsstartdate = "";
                usermonthlydashboardtimesheet.timesheetenddate = "";

                List<AppWeekTimeSheet> weektimesheetscollection = new List<AppWeekTimeSheet>();

                foreach (customweek custweek in adminmonthlydashboard.weekcollection)
                {
                    AppWeekTimeSheet dashboarddata = new AppWeekTimeSheet();

                    dashboarddata.timesheetweek = custweek;

                    Business.ApplicationService.timesheet[] timesheets = appclient.Gettimesheets(Session["companyid"].ToString(), consultant.Id, "", "", custweek.startdate.ToString("yyyy-MM-dd hh:mm:ss"), custweek.enddate.ToString("yyyy-MM-dd hh:mm:ss"));
                    UI.testmodel.timesheet singletimesheet = new UI.testmodel.timesheet();
                    if (timesheets.Length > 0)
                    {
                        UI.testmodel.timesheet objsheet = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.testmodel.timesheet>(timesheets[0].data);
                        dashboarddata.weektimesheetdata = objsheet;
                        dashboarddata.hasTimeSheet = true;
                    }
                    else
                    {
                        dashboarddata.hasTimeSheet = false;
                    }

                    weektimesheetscollection.Add(dashboarddata);
                }

                usermonthlydashboardtimesheet.weektimesheets = weektimesheetscollection;

                adminmonthlydashboard.usertimesheets.Add(usermonthlydashboardtimesheet);
            }

            return View(adminmonthlydashboard);

        }

        public string AddWeekTimesheet(string employeeid, string employeename, string clientid, string clientname, string startdate, string enddate, string data)
        {
            DataLayer datalayer = new DataLayer();
            string timesheetid = datalayer.CreateTimeSheet(Session["companyid"].ToString(), Session["userid"].ToString(), employeeid, employeename, clientid, clientname, startdate, enddate, data);
            return timesheetid;
        }

        public string UpdateWeekTimesheet(string id, string employeeid, string employeename, string clientid, string clientname, string startdate, string enddate, int status, string data)
        {
            //dynamic invoiceobject = System.Web.Helpers.Json.Decode(data);
            //string xmldocument = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(data, "timesheet").InnerXml;
            //xmldocument = xmldocument.Replace("  ", "");
            DataLayer datalayer = new DataLayer();
            bool timesheetstat = datalayer.UpdateTimeSheet(id, Session["companyid"].ToString(), Session["userid"].ToString(), employeeid, employeename, clientid, clientname, startdate, enddate, data);
            return timesheetstat.ToString();
        }

        //public string UpdateWeekTimesheet(string employeeid, string employeename, string clientid, string clientname, string startdate, string enddate, string data)
        //{
        //    DataLayer datalayer = new DataLayer();
        //    string timesheetid = datalayer.CreateTimeSheet(Session["companyid"].ToString(), Session["userid"].ToString(), employeeid, employeename, clientid, clientname, startdate, enddate, data);
        //    return timesheetid;
        //}
    }
}