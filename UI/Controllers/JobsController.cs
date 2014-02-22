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
using System.Configuration;

using System.Collections.Specialized;
using System.Text;

namespace UI.Controllers
{
    [HandleError]
    public class JobsController : Controller
    {
        //
        // GET: /Jobs/

        public ActionResult Index()
        {
            UI.Models.Job jobmodel = new Models.Job();
            List<SelectListItem> mailinglistitems = new List<SelectListItem>();

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.maillist[] maillists = appclient.jsonMailinglists(Session["companyid"].ToString(), Session["usertoken"].ToString());

            foreach (Business.ApplicationService.maillist maillist in maillists)
            {
                mailinglistitems.Add(new SelectListItem { Text = maillist.Title, Value = maillist.Id });
            }
            ViewBag.MailListCollection = mailinglistitems;


            return View(jobmodel.getAllInhouseorPublishActiveorInactiveJobsList());
        }

        public ActionResult Add()
        {
            List<string> lstPostionTypes = new List<string>();
            List<string> lstJobCatgs = new List<string>();
            List<string> lstExps = new List<string>();
            List<string> lstPartners = new List<string>();
            Business.CommonFunctions cf = new CommonFunctions();
            foreach (string item in CommonFunctions.m_listJobPosition)
            {
                lstPostionTypes.Add(item);
            }
            ViewData["PostTypes"] = lstPostionTypes;
            foreach (string item in CommonFunctions.m_listJobCategoires)
            {
                lstJobCatgs.Add(item);
            }
            ViewData["JobCatgs"] = new SelectList(lstJobCatgs);
            foreach (string item in CommonFunctions.m_listJobExperience)
            {
                lstExps.Add(item);
            }
            ViewData["Exps"] = new SelectList(lstExps);
            return View();
        }

        [HttpPost]
        public ActionResult Add(UI.Models.Job jobdata)
        {
            UI.Models.Job job = new Models.Job();
            jobdata.PayRate = jobdata.PayRate + "-" + jobdata.payratemax;
            job.InsertJobDetials(jobdata);
            return RedirectToAction("index", "jobs", null);
        }

        [HttpGet]
        public ActionResult Show()
        {
            string jobid = Request.QueryString["jobid"].ToString();
            JobPosting jobpost = new JobPosting().GetJobPosting(jobid, Session["companyid"].ToString(), Session["usertoken"].ToString());
            return View(jobpost);
        }

        public ActionResult post(UI.Models.Job job)
        {
            //string jobid = Request.QueryString["jobid"].ToString();
            //JobPosting jobpost = new JobPosting().GetJobPosting(jobid, Session["companyid"].ToString(), Session["usertoken"].ToString());
            //return View(jobpost);
            string jobid = Request.QueryString["jobid"].ToString();
            Job jobinfo = new Job().getPublicJobById(jobid);
            return View(jobinfo);
        }

        [HttpGet]
        public ActionResult Multiadd()
        {
            //UI.Models.Job job = new Models.Job();
            //job.InsertJobDetials(jobdata);
            return View();
        }

        [HttpPost]
        public ActionResult Multiadd(UI.Models.Job jobdata)
        {
            UI.Models.Job job = new Models.Job();
            job.InsertJobDetials(jobdata);
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            string jobid = Request.QueryString["jobid"].ToString();
            JobPosting jobpost = new JobPosting().GetJobPosting(jobid, Session["companyid"].ToString(), Session["usertoken"].ToString());
            List<string> lstPostionTypes = new List<string>();
            List<string> lstJobCatgs = new List<string>();
            List<string> lstExps = new List<string>();
            List<string> lstPartners = new List<string>();
            Business.CommonFunctions cf = new CommonFunctions();
            foreach (string item in CommonFunctions.m_listJobPosition)
            {
                lstPostionTypes.Add(item);
            }
            ViewData["PostTypes"] = lstPostionTypes;
            foreach (string item in CommonFunctions.m_listJobCategoires)
            {
                lstJobCatgs.Add(item);
            }
            ViewData["JobCatgs"] = new SelectList(lstJobCatgs);
            foreach (string item in CommonFunctions.m_listJobExperience)
            {
                lstExps.Add(item);
            }
            ViewData["Exps"] = new SelectList(lstExps);
            jobpost.postedjob.payratemax = jobpost.postedjob.PayRate.Split('-')[1].ToString();
            jobpost.postedjob.PayRate = jobpost.postedjob.PayRate.Split('-')[0].ToString();
            return View(jobpost.postedjob);
        }

        [HttpPost]
        public ActionResult Edit(UI.Models.Job editedjob)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            int CompID = 1;
            int CatId = 1;
            int stateId = 1;
            int Expcodeid = 1;
            Expcodeid = Convert.ToInt32(CommonFunctions.MapExperiencetoID[editedjob.Exp].ToString());


            // string PositionTypeId = CommonFunctions.MapPositiontoID[editedjob.postype].ToString();

            string[] postypearray = editedjob.postype.Split(',');
            string PositionTypeId = "";
            if (postypearray.Length > 0)
            {
                foreach (string postype in postypearray)
                {
                    if (PositionTypeId == "")
                    { PositionTypeId = CommonFunctions.MapPositiontoID[postype].ToString(); }
                    else
                    {
                        PositionTypeId = PositionTypeId + "," + CommonFunctions.MapPositiontoID[postype].ToString();
                    }
                }
            }


            editedjob.ModifiedDate = DateTime.Now;

            editedjob.StartDate = DateTime.Now;
            //string[] startdatearray = DateTime.Now.ToString().Split('/');
            // string startdate = new DateTime(Convert.ToInt32(startdatearray[2]), Convert.ToInt32(startdatearray[0]), Convert.ToInt32(startdatearray[1])).ToString("yyyy-MM-dd HH:mm:ss");
            string[] expdatearray = editedjob.ExpDate.Split('/');
            string expdt = new DateTime(Convert.ToInt32(expdatearray[2]), Convert.ToInt32(expdatearray[0]), Convert.ToInt32(expdatearray[1])).ToString("yyyy-MM-dd HH:mm:ss");

            editedjob.jobdata = Newtonsoft.Json.JsonConvert.SerializeObject(editedjob);
            editedjob.Publish = 1;
            editedjob.PayRate = editedjob.PayRate + "-" + editedjob.payratemax;
            bool stat = appclient.UpdateJob(editedjob.JobId, editedjob.CatId, editedjob.SubId, editedjob.JobName, editedjob.JobDesc, editedjob.PayRate, editedjob.PrefSkills, PositionTypeId, editedjob.State,
                editedjob.StartDate.Day.ToString(), editedjob.StartDate.Month.ToString(), editedjob.StartDate.Year.ToString(), expdatearray[1], expdatearray[0], expdatearray[2],
                Expcodeid, (int)editedjob.Publish, Session["companyid"].ToString(), "jobpost", editedjob.Email, editedjob.Phone, "", editedjob.jobdata, editedjob.additionaldetails);
            return RedirectToAction("show", "jobs", new { jobid = editedjob.JobId });
        }

        public ActionResult jobspage()
        {
            string jobid = Request.QueryString["jobid"].ToString();
            JobPosting jobpost = new JobPosting().GetJobPosting(jobid, Session["companyid"].ToString(), Session["usertoken"].ToString());
            return View(jobpost);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddApplicant(JObApplicationdata jobapplication, HttpPostedFileBase file)
        {
            string applicationid = "";
            string candidateid = Guid.NewGuid().ToString().Replace("-", "");

            Business.ApplicationService.AppServiceClient bd = new Business.ApplicationService.AppServiceClient();
            Business.CoreService.MyFile fileuploaded = new Business.CoreService.MyFile();
            Business.CoreService.IobjectServicesWebappVer2Client coreclient = new Business.CoreService.IobjectServicesWebappVer2Client();

            string customtoken = coreclient.Login("accounts@connectica.com", "password", "");
            AmazonFile af = new AmazonFile();
            fileuploaded = af.WriteFile(Request.Files[0].FileName, Request.Files[0].InputStream, candidateid, customtoken);
            coreclient.AddRelation(candidateid, fileuploaded.Id, "resume", customtoken);

            IList document = af.GetResourceTokens(null, candidateid, "resume", customtoken);

            string contentid = "";

            contentid = ((Business.CoreService.ResourceToken)document[0]).ContentId;

            string fileurl = af.GetFileUrl(contentid, customtoken);

            Business.ApplicantActions objApplicantActions = new ApplicantActions();
            try
            {
                applicationid = objApplicantActions.AddApplicant(Session["userid"].ToString(), Session["companyid"].ToString(),
                jobapplication.jobid, candidateid, "", contentid, "", jobapplication.data);
            }
            catch
            {
                applicationid = objApplicantActions.AddApplicant(candidateid, jobapplication.companyid,
                               jobapplication.jobid, candidateid, "", contentid, "", jobapplication.data);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCandidate(JObApplicationdata jobapplication, HttpPostedFileBase file)
        {
            string applicationid = "";
            string candidateid = Guid.NewGuid().ToString().Replace("-", "");

            Business.ApplicationService.AppServiceClient bd = new Business.ApplicationService.AppServiceClient();
            Business.CoreService.MyFile fileuploaded = new Business.CoreService.MyFile();
            Business.CoreService.IobjectServicesWebappVer2Client coreclient = new Business.CoreService.IobjectServicesWebappVer2Client();

            string customtoken = coreclient.Login("accounts@connectica.com", "password", "");
            AmazonFile af = new AmazonFile();
            fileuploaded = af.WriteFile(Request.Files[0].FileName, Request.Files[0].InputStream, candidateid, customtoken);
            coreclient.AddRelation(candidateid, fileuploaded.Id, "resume", customtoken);

            IList document = af.GetResourceTokens(null, candidateid, "resume", customtoken);

            string contentid = "";

            contentid = ((Business.CoreService.ResourceToken)document[0]).ContentId;

            string fileurl = af.GetFileUrl(contentid, customtoken);

            Business.ApplicantActions objApplicantActions = new ApplicantActions();
            try
            {
                applicationid = objApplicantActions.AddCandidate(Session["userid"].ToString(), Session["companyid"].ToString(),
                jobapplication.jobid, candidateid, "", contentid, "", jobapplication.data);
            }
            catch
            {
                applicationid = objApplicantActions.AddCandidate(candidateid, jobapplication.companyid,
                               jobapplication.jobid, candidateid, "", contentid, "", jobapplication.data);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string AddNotes(string applicationid, string subjecttype, string parentid, string description)
        {
            string commentid = "";
            string usertoken = Session["usertoken"].ToString();
            Business.ApplicationService.AppServiceClient bd = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.AppServiceJobportalResponse response = new Business.ApplicationService.AppServiceJobportalResponse();
            response = bd.AddNotes(applicationid, subjecttype, Session["userid"].ToString(), usertoken, description);
            return commentid;
        }

        [HttpPost]
        public MessageMaster CreateMessageMAster(string messagetext, string sendorid, string expirytime, string recipients, string messagetype, string subject, string downPlayTime,
            string sendertype, string recipienttype, string MasterParentId, string InvitationParentid)
        {
            string token = "";
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            token = appclient.Login("messagingmaster@gmail.com", "messagemaster");
            MessageMaster appmessagemaster = appclient.createmessagemaster(messagetext, appclient.GetUserObject(token).Id, expirytime, recipients, messagetype, subject, downPlayTime, sendertype, recipienttype,
                MasterParentId, InvitationParentid, token);
            return appmessagemaster;

        }

        public void UpdateApplicant(string applicationid, string jobid, string candidateid, string contactid, string resumeid, int status, string data)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            bool stat = appclient.UpdateApplicant(applicationid, Session["userid"].ToString(), Session["companyid"].ToString(), jobid, candidateid, contactid, resumeid, "", "", status, data);
        }

        public void UpdateApplicantStatus(string applicationid, string candidateid, string status)
        {
            int newapplicationstatus = 0;
            switch (status)
            {
                case "Unscreened":
                    newapplicationstatus = 1;
                    break;
                case "Screened":
                    newapplicationstatus = 2;
                    break;
                case "Telephonic":
                    newapplicationstatus = 3;
                    break;
                case "Face to Face":
                    newapplicationstatus = 4;
                    break;
                case "Offered & Accepted":
                    newapplicationstatus = 5;
                    break;
                case "Offered & Rejected":
                    newapplicationstatus = 6;
                    break;
                case "On Hold":
                    newapplicationstatus = 7;
                    break;
                case "Rejected":
                    newapplicationstatus = 8;
                    break;
            }

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            bool stat = appclient.UpdatestatusApplicant(applicationid, candidateid, candidateid, newapplicationstatus);
        }

        public ActionResult UpdateQuestionaire(string applicationid, string data)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.Updatequestionaire(applicationid, data);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult UpdateJobStatus(string jobid, string status)
        {
            int numericstatus = 0;

            try
            {
                numericstatus = Convert.ToInt32(status);
            }
            catch (Exception NumericConversionException)
            {
                string NumericConversionExceptionMessage = NumericConversionException.Message;
            }
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            appclient.Updatestatus(jobid, Session["companyid"].ToString(), numericstatus + 1);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public void UpdateNotes(int commentid, string subjectid, string subjecttype, string objecttype, string Des, int visibility)
        {
            Dictionary<string, string> dicProp = new Dictionary<string, string>();
            dicProp.Add("Key", "Value");
            Business.ApplicationService.AppServiceClient bd = new Business.ApplicationService.AppServiceClient();
            bd.UpdateNotes(Convert.ToInt32(commentid), subjectid, subjecttype, Session["userid"].ToString(), objecttype, Des, visibility, JsonConvert.SerializeObject(dicProp), Session["usertoken"].ToString());
            // return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public string GetCities(string tag)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            List<Business.ApplicationService.cardleycity> cities = appclient.Cities(tag, "20").ToList();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(cities);// serializer.Serialize(cities);
            return output;
        }

        [HttpPost]
        public string GetNotes(string parentid, string applicationid, string objecttype)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.AppNotes[] comments = appclient.GetNotes(applicationid, objecttype);
            return Newtonsoft.Json.JsonConvert.SerializeObject(AppComment.ConvertServiceAddressesToAppComments(comments.ToList()));
            //return AppComment.ConvertServiceAddressesToAppComments(comments.ToList());
        }

        [HttpPost]
        public Mesage[] GetAllMessageMaster(string since, string sendorid, string objecttype, string expiryTime, string downPlayTime)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            string token = "";
            token = appclient.Login("messagingmaster@gmail.com", "messagemaster");
            Mesage[] getmessages = appclient.getallmessages(since, sendorid, objecttype, expiryTime, downPlayTime, token);
            return getmessages;
        }

        public bool Deletejob(string jobid, string categoryid, string subcategoryid, string objectid)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            bool stat = appclient.DeleteJob(jobid, categoryid, subcategoryid, Session["companyid"].ToString());
            return stat;

        }

        public bool Deleteapplicant(string transactionid, string creatorid)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            bool stat = appclient.Deleteapplicant(transactionid, "");
            return stat;
        }

        public bool UpdateJob(string jobid, string categoryid, string subcategoryid, string jobname, string jobdescription, string payrate, string preferenceskills, string positiontype, string cityid,
                string startdate, string startmonth, string startyear, string enddate, string endmonth, string endyear, int expid, int publish, string objecttype, string email, string phone, string partnerslist, string data, string additionaldetails)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            bool stat = appclient.UpdateJob(jobid, categoryid, subcategoryid, jobname, jobdescription, payrate, preferenceskills, positiontype, cityid,
                startdate, startmonth, startyear, enddate, endmonth, endyear,
                expid, publish, Session["companyid"].ToString(), objecttype, email, phone, partnerslist, data, additionaldetails);
            return stat;

        }

        //public ActionResult linkedin()
        //{
        //    return View();
        //}


       
    }
}
