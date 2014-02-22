using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using UI.Models;

namespace UI.Controllers
{
    public class CommunicationController : Controller
    {
        //
        // GET: /Communication/

        public ActionResult Index()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.InvitationApp[] invitationslist = appclient.GetInvitationmaster("accounts@connectica.com", "msgtype");
    
            List<UI.Models.mailmessage> mailmessages = new List<Models.mailmessage>();
            foreach (Business.ApplicationService.InvitationApp invitation in invitationslist)
            {
                UI.Models.mailmessage message = new Models.mailmessage();
              
                message.mailitemid = invitation.invitationmasterid;
                message.mailtime = invitation.invitetime.ToString();
                message.receipients = invitation.invitees;
                message.sender = invitation.sendorid;
                message.mailbrief = invitation.invitationtext;
                tags listmembersdata = JsonConvert.DeserializeObject<tags>(invitation.Prop);
                message.properties =listmembersdata;
                mailmessages.Add(message);
            }
            return View(mailmessages);
        }

     
        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Sent()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.InvitationApp[] invitationslist = appclient.GetInvitationmaster("accounts@connectica.com", "msgtype");
            List<UI.Models.mailmessage> mailmessages = new List<Models.mailmessage>();
            foreach (Business.ApplicationService.InvitationApp invitation in invitationslist)
            {
                UI.Models.mailmessage message = new Models.mailmessage();
                message.mailitemid = invitation.invitationmasterid;
                message.mailtime = invitation.invitetime.ToString();
                message.receipients = invitation.invitees;
                message.sender = invitation.sendorid;
                message.mailbrief = invitation.invitationtext;
              //  message.tags = invitation.Prop;
                mailmessages.Add(message);
            }
            return View(mailmessages);
        }

        public ActionResult Drafts()
        {
            return View();
        }

        public ActionResult Important()
        {
            return View();
        }

    }
}
