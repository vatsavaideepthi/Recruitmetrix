using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class listsController : Controller
    {
        //
        // GET: /lists/

        [HttpGet]
        public ActionResult Index()
        {
            Business.DataLayer datalayer = new Business.DataLayer();

            Business.ApplicationService.maillist[] mailists = datalayer.GetMailingLists(Session["companyid"].ToString(), Session["usertoken"].ToString());
            return View(mailists);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UI.Models.Mailinglist mailist)
        {
            Business.DataLayer datalayer = new Business.DataLayer();
            datalayer.CreateMailinglist(mailist.title, Session["companyid"].ToString(), Session["usertoken"].ToString());
            return RedirectToAction("index");
        }

        public ActionResult list()
        {
            string listid = Request.QueryString["listid"].ToString();
            Business.DataLayer datalayer = new Business.DataLayer();
            Business.ApplicationService.maillist maillist = datalayer.GetMailinglist(listid, Session["companyid"].ToString(), Session["usertoken"].ToString());

            return View(maillist);
        }

        public ActionResult manage(string listid)
        {
            return View();
        }

        public void addmember(string listid, string address, string name, string description)
        {
            Business.DataLayer datalayer = new Business.DataLayer();
            datalayer.Addlistmember(listid, address, name, description, Session["usertoken"].ToString());
        }

        public void createlist(string listname)
        {
            Business.DataLayer datalayer = new Business.DataLayer();
            datalayer.CreateMailinglist(listname, Session["companyid"].ToString(), Session["usertoken"].ToString());
        }

        [HttpGet]
        public ActionResult import(Business.ApplicationService.maillist maillist)
        {
            return View();
        }

        [HttpPost]
        public ActionResult import(UI.Models.Mailinglist maillist, HttpPostedFileBase file)
        {
            string strPath = string.Empty;
            string filename = string.Empty;
            string redirecturl = "http://" + Request.Url.Authority + "/lists/list?listid=" + maillist.id;
            //file upload path
            string filenewname = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Request.Files[0].FileName);

            string UploadFileFolderPath = HttpContext.Server.MapPath("~/Content");//System.Configuration.ConfigurationManager.AppSettings["ExcelSheet"]; //folder shld exists in appln path

            if (filenewname.ToString().Trim().Length > 0)
            {

                if (!Directory.Exists(Server.MapPath("~/Content")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content"));
                }

                string savepath = Server.MapPath("~/Content" + "/");
                string pathToCheck = savepath + filenewname;

                Request.Files[0].SaveAs(Server.MapPath("~/Content") + "/" + filenewname);
            }

            strPath = Server.MapPath("~/Content") + "/" + filenewname;

            //Create connection string to Excel work book
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties='Excel 12.0;Persist Security Info=False;HDR=NO;'";

            //Create Connection to Excel work book
            System.Data.OleDb.OleDbConnection excelConnection = new System.Data.OleDb.OleDbConnection(excelConnectionString);

            //
            excelConnection.Open();
            DataTable exceldatatable = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (exceldatatable == null)
            {
                return null;
            }
            String[] excelSheets = new String[exceldatatable.Rows.Count];
            int i = 0;

            foreach (DataRow row in exceldatatable.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            //
            System.Data.OleDb.OleDbDataAdapter MyCommand = null;
            System.Data.DataSet DtSet = null;
            DataTable dt = new DataTable();


            string message = string.Empty;
            string commandtext = " select * from [" + excelSheets[0].ToString() + "]";
            MyCommand = new System.Data.OleDb.OleDbDataAdapter(commandtext, excelConnection);
            DtSet = new System.Data.DataSet();
            MyCommand.Fill(DtSet, "[" + excelSheets[0].ToString() + "]");


            dt = DtSet.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string name = "";
                string description = "";
                string email = "";
                try
                {
                    name = dr[0].ToString();
                    description = dr[2].ToString();
                }

                catch { }
                try
                {
                    email = dr.ItemArray[1].ToString();
                    if (email.Trim() != "")
                    {
                        Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
                        appclient.AddListMember(maillist.id, email.Trim(), name, description, Session["usertoken"].ToString());
                    }
                    else
                        return Redirect(redirecturl);
                }
                catch
                {

                }
            }

            return Redirect(redirecturl);

        }

        public Business.ApplicationService.AppRestResponse Deletemailinglist(string mailistid)
        {
            Business.DataLayer datalayer = new Business.DataLayer();
            return datalayer.deletemailinglist(mailistid + "@rmkonnekt.com", Session["userid"].ToString(), Session["companyid"].ToString());

        }

    }
}
