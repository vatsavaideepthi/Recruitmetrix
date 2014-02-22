using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        [HttpGet]
        public ActionResult Index()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.contact[] contacts = appclient.GetContacts(Session["companyid"].ToString(), Session["companyid"].ToString(),"1","100");

            List<UI.Models.PortalContact> portalcontacts = new List<Models.PortalContact>();
            foreach (Business.ApplicationService.contact contact in contacts)
            {
                UI.Models.PortalContact singlecontact = new Models.PortalContact();
                try
                {
                    UI.Models.PortalContact ObjContact = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.Models.PortalContact>(contact.data);
                    if (ObjContact == null)
                    {
                        singlecontact.Companyname = "";
                        singlecontact.middlename = "";
                    }
                    else
                    {
                        singlecontact.Companyname = ObjContact.Companyname;
                        singlecontact.middlename = ObjContact.middlename;
                    }

                }
                catch 
                {
                    singlecontact.Companyname = "";
                    singlecontact.middlename = "";

                }
                singlecontact.id = contact.contactid;
                singlecontact.firstname = contact.firstname;
                singlecontact.lastname = contact.lastname;
                singlecontact.emailaddress = contact.emailid;
                singlecontact.mobilephone = contact.mobile;
                singlecontact.officephone = contact.phone;
                singlecontact.objecttype = contact.objecttype;
                singlecontact.designation = contact.designation;
                
                portalcontacts.Add(singlecontact);
            }
            return View(portalcontacts);
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Import()
        //{
        //    return View();
        //}

        public bool Edit(string contactid,string unid,string designation,string title,string firstname,string lastname,string emailaddress,string mobilephone,string secondaryemail,string phone,string companyname,string type,string objecttype,string contactgroup)
        {
            bool status = new bool();
            string comcompanyid = Session["companyid"].ToString();
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

            UI.Models.PortalContact editedcontact = new Models.PortalContact();
            editedcontact.Companyname = companyname;
            editedcontact.firstname = firstname;
            editedcontact.lastname = lastname;
            editedcontact.emailaddress = emailaddress;
            editedcontact.officephone = phone;
            editedcontact.mobilephone = mobilephone;
            
            status=appclient.UpdateContact(contactid,
                                    comcompanyid,
                                    comcompanyid,
                                    unid,
                                    designation,
                                    title,
                                    firstname,
                                    lastname,
                                    emailaddress,
                                    mobilephone,
                                    secondaryemail,
                                    phone, Newtonsoft.Json.JsonConvert.SerializeObject(editedcontact), type, objecttype, contactgroup, comcompanyid);
                                   
            return status;
        }

        public ActionResult contact(string contactid)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
           

            return View();
        }

        [HttpPost]
        public ActionResult Add(UI.Models.PortalContact contact)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            string jsoncontact = Newtonsoft.Json.JsonConvert.SerializeObject(contact);
            appclient.CreateContact(Session["companyid"].ToString(), Session["companyid"].ToString(), Guid.NewGuid().ToString().Replace("-",""), contact.designation, contact.title, contact.firstname, contact.lastname, contact.emailaddress, contact.officephone, contact.secondaryemail, contact.mobilephone, jsoncontact, contact.objecttype,"", Session["companyid"].ToString());
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Manage(string listid)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.contact[] contacts = appclient.GetContacts("9a73072573754e13a02381c9d931e5c4", "9a73072573754e13a02381c9d931e5c4","1","100");

            List<UI.Models.PortalContact> portalcontacts = new List<Models.PortalContact>();
            foreach (Business.ApplicationService.contact contact in contacts)
            {
                UI.Models.PortalContact tempcontact = new Models.PortalContact();

                tempcontact.id = contact.contactid;
                tempcontact.emailaddress = contact.emailid;

                portalcontacts.Add(tempcontact);
            }
            return View(portalcontacts);
        }


        [HttpGet]
        public ActionResult shared()
        {
           List<UI.Models.PortalContact> contacts = new List<Models.PortalContact>();
           return View(contacts);
        }

        [HttpGet]
        public ActionResult requests()
        {
            List<UI.Models.PortalContact> contacts = new List<Models.PortalContact>();
            return View(contacts);
        }

        public bool DeleteContact( string contactid,string companyid, string objectid)
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            bool status = appclient.DeleteContact(contactid,Session["companyid"].ToString(), Session["companyid"].ToString());
            return status;  
        }
    }
}
