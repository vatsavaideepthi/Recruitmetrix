using Business;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers;
using UI.Utilities;

namespace UI.Models
{

    public class admintimesheet
    {
        public int year { get; set; }
        public int month { get; set; }
        public string monthname { get; set; }
        public DateTime monthstarts { get; set; }
        public DateTime monthends { get; set; }
        public int numberofweeks { get; set; }
        public List<customweek> weekcollection { get; set; }
        public List<userdashboardtimesheet> usertimesheets { get; set; }
    }

    public class AppWeekTimeSheet
    {
        public string timesheetid { get; set; }
        public customweek timesheetweek { get; set; }
        public bool hasTimeSheet { get; set; }
        public UI.testmodel.timesheet weektimesheetdata { get; set; }
    }

    public class userdashboardtimesheet
    {
        public Business.ApplicationService.appuser User { get; set; }
        public string timesheetmonth { get; set; }
        public string timesheetsstartdate { get; set; }
        public string timesheetenddate { get; set; }
        public List<AppWeekTimeSheet> weektimesheets { get; set; }
    }

}



