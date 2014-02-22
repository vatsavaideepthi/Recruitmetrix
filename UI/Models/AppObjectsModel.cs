using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    namespace LoginModel
    {
        public class User
        {
            public string userEmail { get; set; }
            public string password { get; set; }
            public bool isLoginValid { get; set; }
            public string Message { get; set; }


            public User()
            {
                this.userEmail = "";
                this.password = "";
                this.isLoginValid = false;

            }

            public Business.ApplicationService.AccessObject Login(User logindata)
            {
                Business.User businessuser = new Business.User();
                Business.ApplicationService.AccessObject accessobj = businessuser.LoginCompany(logindata.userEmail, logindata.password);
                return accessobj;
            }
        }
    }

    namespace RegistrationModel
    {


        public class Signup
        {
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public bool isSignupSuccess { get; set; }
        }

        public class User
        {
            public string title { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public EnumObjects.regacctype accounttype { get; private set; }

            public User()
            {
                email = "";
                password = "";
                accounttype = EnumObjects.regacctype.single;
            }

            public string Signup(User usersignupdata)
            {
                try
                {
                    Business.User serviceuser = new Business.User();
                    string companyid = serviceuser.RegisterCompany(usersignupdata.title, usersignupdata.email, usersignupdata.password, "", "");
                    if (companyid != "")
                    {
                        //Registering User is successfull .
                        return companyid;
                    }
                    else
                    {
                        //Registering User Failed 
                        return "";
                    }
                }
                catch
                {
                    //Registering User Failed
                    return "";
                }
            }

        }

        public class Organization
        {
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public EnumObjects.regacctype accountype { get; private set; }

            public Organization()
            {
                name = "";
                email = "";
                password = "";
                accountype = EnumObjects.regacctype.group;
            }

            public string Signup(Organization OrgSignupdata)
            {
                try
                {
                    Business.Organization serviceOrganization = new Business.Organization();
                    string userid = serviceOrganization.RegisterCompany(OrgSignupdata.name, OrgSignupdata.email, OrgSignupdata.password, "", "");
                    if (userid != "")
                    {
                        //Registering User is successfull .
                        return userid;
                    }
                    else
                    {
                        //Registering User Failed 
                        return "";
                    }
                }
                catch
                {
                    //Registering User Failed
                    return "";
                }
            }
        }
    }


    namespace ProfileModelObject
    {
        public class ChangePassword
        {
            public string AuthToken { get; set; }
            public string newpassword { get; set; }
            public string ipaddress { get; set; }
        }
    }

    namespace MessagingObjectModel
    {
        public class JObsBulkMail
        {
            public string jobs { get; set; }
            public string recipients { get; set; }
            public string recipientcc { get; set; }
            public string recipientbcc { get; set; }
            [System.Web.Mvc.AllowHtml]
            public string messagebody { get; set; }
            public string mailsubject { get; set; }
        }


        public class forward
        {
            public string applicants { get; set; }
            public string recipientid { get; set; }
            public string recipientcc { get; set; }
            public string recipientbcc { get; set; }
            [System.Web.Mvc.AllowHtml]
            public string messagebody { get; set; }
            public string mailsubject { get; set; }
        }
    }


}