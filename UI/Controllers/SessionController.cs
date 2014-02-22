using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class SessionController : Controller
    {
        private SessionObject SessionData()
        {
            SessionObject mysessionobject = new SessionObject();

            return mysessionobject;
        }


        public static SessionObject GetSessionData()
        {
            return new SessionController().SessionData();
        }

    }

    public class SessionObject
    {
        public string accesstoken { get; set; }
        public string companyid { get; set; }
        public string userid { get; set; }
        public string invoiceid { get; set; }
    }
}
