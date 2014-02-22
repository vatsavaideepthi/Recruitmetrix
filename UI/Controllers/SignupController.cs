using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Models.RegistrationModel;

namespace UI.Controllers
{
    public class SignupController : Controller
    {
        //
        // GET: /Signup/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult User(User usersignupdata)
        {

            string userid = new User().Signup(usersignupdata);
            Signup userRegistrationStatus = new Signup();
            try
            {

                if (userid != "")
                {
                    //Registering User is successfull .
                    TempData["signupemail"] = usersignupdata.email;
                    TempData["isSignupSuccess"] = 1;
                    return RedirectToAction("Index", "Login");//Redirecting to the Login Page
                }
                else
                {
                    TempData["signupemail"] = usersignupdata.email;
                    TempData["isSignupSuccess"] = 0;

                    return RedirectToAction("Index", "Signup"); //Registering User Failed 
                }

            }
            catch
            {

                return RedirectToAction("Index", "Signup"); //Registering User Failed
            }
        }

        public ActionResult Organization(Organization orgSignupData)
        {
            string userid = new Organization().Signup(orgSignupData);
            try
            {

                if (userid != "")
                {
                    //Registering User is successfull .
                    TempData["signupemail"] = orgSignupData.email;
                    TempData["isSignupSuccess"] = 1;
                    return RedirectToAction("Index", "Login");//Redirecting to the Login Page
                }
                else
                {
                    TempData["signupemail"] = orgSignupData.email;
                    TempData["isSignupSuccess"] = 1;
                    return RedirectToAction("Index","Signup"); //Registering User Failed 
                }

            }
            catch
            {

                return RedirectToAction("Index", "Signup"); //Registering User Failed
            }
        }

    }
}
