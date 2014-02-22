using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class BaseObject
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class Invoice
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class attachment
    {
        public string id { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public string type { get; set; }
    }

    public class project : BaseObject
    {
        public project()
        {
            this.id = "";
            this.title = "";
        }
    }

    #region Time Sheet Objects
    public class timesheettask : BaseObject
    {
        public project project { get; set; }
        public EnumObjects.taskstatus status { get; set; }
        public double workinghours { get; set; }
    }

    public class timesheetitem : BaseObject
    {
        public string date { get; set; }
        public List<timesheettask> tasks { get; set; }
        public double totalhours { get; set; }
        public double workhours { get; set; }
        public double overtime { get; set; }

    }

    public class timesheetitems
    {
        public List<timesheetitem> items { get; set; }
        public int itemcount { get; set; }
        public string startdate { get; set; }
        public string endddate { get; set; }
    }

    public class timesheetmetadata
    {
        public double totalhours { get; set; }
        public double overtimehours { get; set; }
    }

    public class forgotpassword
    {
        public string useremail { get; set; }
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }

    #endregion


    #region address



    #endregion


    public class info
    {
        public string name { get; set; }
        public string email { get; set; }
        public string company { get; set; }
    }

    public class address
    {
        public string id { get; set; }
        public string line { get; set; }
        public string line2 { get; set; }
        public string cityid { get; set; }
        public string city { get; set; }
        public string stateid { get; set; }
        public string state { get; set; }
        public string countryid { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
    }

    public class company :BaseObject
    {
        public info info { get; set; }
        public address address {get;set;}
        public projecttask projects { get; set; }
    }

    public class client : BaseObject
    {
        public info info { get; set; }
        public address address { get; set; }
    }

    public class self : BaseObject
    {
        public info info { get; set; }
        public address address { get; set; }
    }

    public class Mailinglist : BaseObject
    {
       public List<string> mails { get; set; }

       public Mailinglist()
       {
           this.id = "";
           this.mails = new List<string>();
           this.title = "";
       }
    }

    public class mailmessage
    {
        public string mailitemid { get; set; }
        public string sender { get; set; }
        public string receipients { get; set; }
        public string mailbrief { get; set; }
        public string mailtime { get; set; }
        public tags  properties{ get; set; }
    }

    public class tags
    {
        public string read { get; set; }
        public string important { get; set; }
        public string attachments { get; set; }
    }
    
    public class appNotes : BaseObject
    {
        public string content { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public bool isHtmlContent { get; set; } 
    }

    public class tasks 
    {
        public string taskid { get; set; }
        public string ownerid { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string workassignedby { get; set; }
        public string description { get; set; }
        public string startedon { get; set; }
        public string endedon { get; set; }
       
    }

    public class projecttask
    {
        public string projectid { get; set; }
        public string projectname { get; set; }
        public string ownerid { get; set; }
        public string date { get; set; }
        public client clientinfo { get; set; }
        public self users { get; set; }
        public string startdate { get; set; }
        public string submissiondate { get; set; }
        public string title { get; set; }
        public tasks projecttasks { get; set; }
    }
  



#region Application Customer Objects

    public class Supplier
    {
        public string id { get;set;}
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        
    }
#endregion
}