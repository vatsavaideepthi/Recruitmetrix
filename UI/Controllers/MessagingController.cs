using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class MessagingController : Controller
    {
        //
        // GET: /Messaging/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(UI.Models.MessagingObjectModel.JObsBulkMail maildata)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            string recipientstring = "";
            string[] recipients = maildata.recipients.Split(',');
            foreach (string str in recipients)
            {
                if (str.ToLower() != "undefined" && str.Length == 32)
                {
                    if (recipientstring == "")
                    {
                        recipientstring = str;
                    }
                    else
                    {
                        recipientstring = recipientstring + "," + str;
                    }
                }
            }
            Business.CoreService.IobjectServicesWebappVer2Client client = new Business.CoreService.IobjectServicesWebappVer2Client();

            List<Business.ApplicationService.mailattachment> attachments = new List<Business.ApplicationService.mailattachment>();
            string companyemail = "";
            try
            {
                companyemail = Session["companyemail"].ToString();
            }
            catch
            {
                companyemail = "mailer@" + Session["companyname"].ToString() + ".com";
            }
            Business.ApplicationService.AppRestResponse response = appclient.SendComplexMessage(recipientstring, companyemail, "", maildata.messagebody.ToString(), maildata.mailsubject, attachments.ToArray(), Session["usertoken"].ToString());
            string[] jobs = maildata.jobs.Split(';');

            foreach (string singlejob in jobs)
            {
                if (singlejob.Length == 32)
                {
                    appclient.Updateemailstatus(singlejob, 1);
                }
            }


            return Redirect(Request.UrlReferrer.ToString());

        }



        public string relaymailmessage(string mailrecipients,string subject,string messagebody)
        {
            string mailresponseid = "";

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            string recipientstring = "";
            string[] recipients = mailrecipients.Split(',');
            foreach (string str in recipients)
            {
                if (str.ToLower() != "undefined" && str.Length >0)
                {
                    if (str.ToLower().Length == 32)
                    {
                        if (recipientstring == "")
                        {
                            recipientstring = str;
                        }
                        else
                        {
                            recipientstring = recipientstring + "," + str;
                        }
                    }
                    else
                    {
                        if (recipientstring == "")
                        {
                            recipientstring = str;
                        }
                        else
                        {
                            recipientstring = recipientstring + "," + str;
                        }
                    }
                }
            }


            Business.CoreService.IobjectServicesWebappVer2Client client = new Business.CoreService.IobjectServicesWebappVer2Client();
            List<Business.ApplicationService.mailattachment> attachments = new List<Business.ApplicationService.mailattachment>();
            string companyemail = "";
            try
            {
                companyemail = Session["companyemail"].ToString();
            }
            catch
            {
                companyemail = "mailer@" + Session["companyname"].ToString() + ".com";
            }

            Business.ApplicationService.AppRestResponse response = appclient.SendComplexMessage(recipientstring, companyemail, messagebody.ToString(), messagebody.ToString(),subject, attachments.ToArray(), Session["usertoken"].ToString());
            return response.StatusCode;

        }


        [HttpPost]
        public ActionResult forward(UI.Models.MessagingObjectModel.forward data)
        {
            string mailresponseid = "";
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            string recipientstring = "";
            string[] recipients = data.recipientid.Split(',');
            foreach (string str in recipients)
            {
                if (str.ToLower() != "undefined" && str.Length > 0)
                {
                    if (str.ToLower().Length == 32)
                    {
                        if (recipientstring == "")
                        {
                            recipientstring = str;
                        }
                        else
                        {
                            recipientstring = recipientstring + ";" + str;
                        }
                    }
                    else
                    {
                        if (recipientstring == "")
                        {
                            recipientstring = str;
                        }
                        else
                        {
                            recipientstring = recipientstring + ";" + str;
                        }
                    }
                }
            }
            Business.CoreService.IobjectServicesWebappVer2Client client = new Business.CoreService.IobjectServicesWebappVer2Client();
            List<Business.ApplicationService.mailattachment> attachments = new List<Business.ApplicationService.mailattachment>();
            string companyemail = "";
            try
            {
                companyemail = Session["companyemail"].ToString();
            }
            catch
            {
                companyemail = "mailer@" + Session["companyname"].ToString() + ".com";
            }

            Business.ApplicationService.AppRestResponse response = appclient.SendComplexMessage(recipientstring, companyemail, "", data.messagebody.ToString(), data.mailsubject, attachments.ToArray(), Session["usertoken"].ToString());

            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}
