using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
   public class Messaging
    {
       ApplicationService.AppServiceClient appclient = new ApplicationService.AppServiceClient();

        public Business.ApplicationService.MessageMaster CreateMessageMaster(string messagetext, string sendorid, string expirytime, string recipients, string messagetype, string subject, string downPlayTime,
            string sendertype, string recipienttype, string MasterParentId, string InvitationParentid, string usertoken)
        {
            Business.ApplicationService.MessageMaster msg = appclient.createmessagemaster(messagetext, sendorid, expirytime, recipients, messagetype, subject, downPlayTime, sendertype, recipienttype, MasterParentId, InvitationParentid, usertoken);
            return msg;
        }

        public Business.ApplicationService.Mesage[] GetAllMessage(string since, string sendorid, string objecttype, string expiryTime, string downPlayTime, string userToken)
        {
            Business.ApplicationService.Mesage[] getmsgs = appclient.getallmessages(since, sendorid, objecttype, expiryTime, downPlayTime, userToken);
            return getmsgs;
        }
       
    }
}
