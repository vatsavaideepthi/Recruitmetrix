using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Models;
using UI.Models.LoginModel;

namespace UI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {

            //Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            ////List<Business.ApplicationService.cardleycity> cities = appclient.Cities("").ToList();
            //List<Business.ApplicationService.cardleycity> cities = new List<Business.ApplicationService.cardleycity>();
            //cities.Add(new Business.ApplicationService.cardleycity {id="1",code="code",name="country",statecode="1234" });
            //cities.Add(new Business.ApplicationService.cardleycity { id = "2", code = "2code", name = "2country", statecode = "21234" });
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string output = Newtonsoft.Json.JsonConvert.SerializeObject(cities);

            //HttpCookie cookie;
            //if (Request.Cookies["cities"] == null)
            //{
            //    cookie = new HttpCookie("cities");
            //    foreach (Business.ApplicationService.cardleycity city in cities)
            //    {
            //        cookie[city.name] = city.id;
            //    }
            //}
            //else
            //{
            //    cookie = Request.Cookies["cities"];
            //    foreach (Business.ApplicationService.cardleycity city in cities)
            //    {
            //        cookie[city.name] = city.id;
            //    }
            //}
            //cookie.Expires = DateTime.Now.AddDays(30);
            //Response.Cookies.Add(cookie);


            return View();
        }

        [HttpPost]
        public ActionResult Index(User logindata)
        {
            Business.ApplicationService.AccessObject accessobj = new User().Login(logindata);
            if (accessobj.accesstoken == "")
            {
                logindata.isLoginValid = false;
                return View(logindata);
            }
            else
            {
                Business.CoreService.IobjectServicesWebappVer2Client coreclient = new Business.CoreService.IobjectServicesWebappVer2Client();
                if (accessobj.accessid == null || accessobj.accessid == "")
                {
                    try
                    {
                        Business.CoreService.Relation[] relations = coreclient.GetParents("", accessobj.role, accessobj.userid, accessobj.accesstoken);
                        accessobj.accessid = relations[0].ParentId;
                        accessobj.companyname = "";
                    }
                    catch
                    {

                        UI.Models.LoginModel.User failedlogin = new User();

                        failedlogin.isLoginValid = false;
                        failedlogin.Message = "User Doesn't belongs to any Company";

                        return View("Index", failedlogin);
                    }
                }

                Business.CoreService.User u = coreclient.GetUserObject(accessobj.accesstoken);
                if (u.Email != null || u.Email != "")
                {
                    Session.Add("companyemail", u.Email.Trim());
                }
                else
                {
                    Session.Add("companyemail","mailer@"+accessobj.companyname+".com");
                }
                Session.Add("usertoken", accessobj.accesstoken);
                Session.Add("userid", accessobj.userid);
                Session.Add("title", accessobj.usertitle);
                Session.Add("companyid", accessobj.accessid);
                //Session.Add("profileid", ProfileModel.GetUserProfile(accessobj.accessid).profileid);
                Session.Add("userrole", accessobj.role);
                Session.Add("companyname", accessobj.companyname);
                Dictionary<string, string> route = Business.ApplicationUtility.AfterLoginRoute(Session["usertoken"].ToString());

                if (route.Keys.Count > 0)
                {
                    return RedirectToAction(route["action"].ToString(), route["controller"].ToString());
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                //if(ProfileModel.GetProfilesetupstatus(Session["companyid"].ToString()))
                //return RedirectToAction("Index", "expenses");
                //else
                //    return RedirectToAction("expenses", "setup");
            }
        }

        public SessionData GetSessionData()
        {

            SessionData sd = new SessionData();
            sd.accesstoken = System.Web.HttpContext.Current.Session["usertoken"].ToString();

            RedirectToActionPermanent("index", "login", null);
            return sd;
        }
    }
        public class SessionData
    {
        public string accesstoken { get; set; }

    }
}
