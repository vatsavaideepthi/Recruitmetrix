using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AjaxDataController : Controller
    {

        Business.DataLayer dataLayer = new Business.DataLayer();
        UI.Models.CustomerModel customermodel = new Models.CustomerModel();

        #region Profile Data

        [HttpPost]
        public string GetDefaultTemplate()
        {
            string templateid = "";

            Business.ApplicationService.profile userprofile = dataLayer.GetUserProfile(Session["companyid"].ToString());
            templateid = userprofile.templateid;
            return templateid;

        }


        #endregion

        #region  Contact

        [HttpPost]
        public string AddContact(string objectid, string email, string title, string fname, string lname, string mobile)
        {
            string contactid = customermodel.AddContact(Session["companyid"].ToString(), objectid, title, fname, lname, email, mobile);
            return contactid;
        }

        [HttpPost]
        public string GetContact(string contactid, string objectid)
        {
            AppContact contact = dataLayer.GetContact(contactid, Session["companyid"].ToString(), objectid);
            return Newtonsoft.Json.JsonConvert.SerializeObject(contact);
        }

        [HttpPost]
        public string UpdateContact(string contactid, string companyid, string objectid, string title, string fname, string lname, string email, string mobile)
        {
            if (dataLayer.UpdateContact(contactid, companyid, objectid, title, fname, lname, email, "", "", mobile, "", "primary", "contact"))
                return contactid;
            else
                return "";
        }

        [HttpPost]
        public string DeleteContact(string companyid, string objectid, string contactid)
        {
            if (dataLayer.DeleteContact(companyid, objectid, contactid))
                return objectid;
            else
                return "";
        }

        #endregion

        #region Address

        [HttpPost]
        public string AddAddress(string objectid, string line1, string line2, string pincode)
        {
            string addressid = customermodel.AddAddress(Session["companyid"].ToString(), objectid, line1, line2, pincode);
            return addressid;
        }

        [HttpPost]
        public string GetAddress(string addressid,string objectid)
        {
            AppAddresss address = dataLayer.GetAddress(addressid, Session["companyid"].ToString(), objectid);
            return Newtonsoft.Json.JsonConvert.SerializeObject(address);
        }

        [HttpPost]
        public string UpdateAddress(string addressid, string companyid, string objectid, string line1, string line2, string city, string state, string zip, string country)
        {
            if (dataLayer.UpdateAddress(companyid, addressid, objectid, line1, line2, city, zip, "", state, country, "address"))
                return addressid;
            else
                return "";

        }


        [HttpPost]
        public string DeleteAddress(string companyid, string objectid, string addressid)
        {
            if (dataLayer.DeleteAddress(companyid, objectid, addressid))
                return objectid;
            else
                return "";

        }
        #endregion

        #region Customer

        [HttpPost]
        public string GetCustomerInfo(string customerid, string companyid)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(customermodel.GetCustomer(customerid, companyid, customerid));
        }


        [HttpPost]
        public string Create(string objectid, string name, string email)
        {
            string customerid = dataLayer.CreateCustomer(Session["companyid"].ToString(), objectid, name, email);
            return customerid;
        }



        [HttpGet]
        public AppCustomer[] GetAllCustomers(string companyid)
        {
            return dataLayer.GetAllCustomers(companyid);
        }

        [HttpPost]
        public string GetJsonCustomerlist(string tag)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dataLayer.GetAllCustomers(Session["companyid"].ToString()));
        }

        [HttpPost]
        public bool IsCustomerHasAddress(string addressid,string companyid, string customerid)
        {
            return customermodel.IsCustomerHasAddress(addressid,companyid, customerid);
        }

        [HttpPost]
        public bool IsCustomerHasContact(string companyid, string customerid)
        {
            return customermodel.IsCustomerHasContact(companyid, customerid);
        }


        #endregion

        #region Invoice

        [HttpPost]
        public string CreateInvoice(string invoiceobj)
        {
            dynamic invoiceobject = System.Web.Helpers.Json.Decode(invoiceobj);
            string xmldocument = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(invoiceobj, "invoice").InnerXml;
            xmldocument = xmldocument.Replace("  ", "");
            Business.DataLayer bd = new Business.DataLayer();
            string invoice = bd.GenerateInvoice(Session["userid"].ToString(), Session["companyid"].ToString(), "", Session["companyid"].ToString(), "invoice", "outbound", xmldocument);

            return invoice;
        }

        [HttpPost]
        public void UpdateInvoice(string invoiceid, string customerid, string invoiceobj, int status)
        {
            dynamic invoiceobject = System.Web.Helpers.Json.Decode(invoiceobj);
            string xmldocument = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(invoiceobj, "invoice").InnerXml;
            xmldocument = xmldocument.Replace("  ", "");
            Business.DataLayer bd = new Business.DataLayer();
            bd.UpdateInvoice(invoiceid, Session["userid"].ToString(), Session["companyid"].ToString(), customerid, Session["companyid"].ToString(), "invoice", "outbound", status, xmldocument);
        }


        [HttpPost]
        public string AddExternalInvoice(string invoiceobj)
        {
            dynamic invoiceobject = System.Web.Helpers.Json.Decode(invoiceobj);
            string xmldocument = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(invoiceobj, "invoice").InnerXml;
            xmldocument = xmldocument.Replace("  ", "");
            Business.DataLayer bd = new Business.DataLayer();
            string invoice = bd.GenerateInvoice(Session["userid"].ToString(), Session["companyid"].ToString(), "", Session["companyid"].ToString(), "invoice", "inbound", xmldocument);

            return invoice;
        }

        #endregion

        #region Invoice :Templates


        [HttpGet]
        public string GetInvoiceTemplate(string tempid)
        {
            Business.DataLayer bd = new Business.DataLayer();
            Business.AppTemplate invoicetemplate = bd.GetInvoiceTemplate(tempid, Session["companyid"].ToString());
            if (invoicetemplate.data == "" || invoicetemplate.data == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div><p>");
                sb.Append("<a href=\"/invoice\">Click to View All Invoices</a>");
                sb.Append("</p></div>");

                invoicetemplate.data = sb.ToString();
            }
            return invoicetemplate.data;
        }

        #endregion

        #region Transactions


        [HttpPost]
        public string CreateTransaction(string accountid, string userid, string subcategoryid, string categoryid, string description, string amount, int duemonth, int duedate, int paiddate, int paidmonth, int paidyear, paymentrecurrence recurrence)
        {
            string transactionid = "";
            dataLayer.createTransactionOverAccount(accountid, userid, subcategoryid, categoryid, description, amount, duemonth, duedate, paiddate, paidmonth, paidyear, paymentrecurrence.none);
            return transactionid;
        }

        [HttpPost]
        public void updateTransaction(string tid, string uid, string toaccid, string paymode, string subcatid, string catid, string desc, string paidamount, string budget, int duemonth, int duedate, int paiddate, int paidmonth, int paidyear, string recurrence)
        {
            string transactionid = "";
            dataLayer.UpdateTransaction(tid, uid, toaccid, paymode, subcatid, catid, desc, paidamount, budget, duemonth, duedate, paiddate, paidmonth, paidyear, Business.DataLayer.getRecurrence(recurrence));
        }

        [HttpPost]
        public string AddQuickPayment(string accountid, string userid, string subcategoryid, string categoryid, string description, string amount, int paiddate, int paidmonth, int paidyear,string paymode)
        {
            string transactionid = "";
            transactionid = dataLayer.addquickpayment(accountid, userid, subcategoryid, categoryid, description, amount, paiddate, paidmonth, paidyear, paymode);
            return transactionid;
        }
        #endregion

        #region Accounts

        [HttpPost]
        public void DeleteAccount(string accountid, string userid)
        {
            dataLayer.hidePaymentData(accountid, userid);
        }

        [HttpPost]
        public void UpdateAccount(string accountid, string accountname, string userid, string accounttype, string subcategoryid, string categoryid, string categoryname, string description, string providerid, string providername, string amount, int duemonth, int duedate, string recurrence)
        {
            dataLayer.ChangeSetupStatus(userid, true);
            dataLayer.updateUserPaymentData(accountid,
                                     accountname,
                                     userid,
                                     accounttype,
                                     subcategoryid,
                                     categoryid,
                                     categoryname,
                                     description,
                                     providerid,
                                     providername,
                                     amount,
                                     duemonth,
                                     duedate,
                                     recurrence, true, false);
            
        }

        [HttpPost]
        public string CreateNewAccount(string accountname, string userid, string accounttype, string subcategoryid,
            string categoryid, string categoryname, string providerid, string providername, string amount, int duedate)
        {
            string accountid = "";
           accountid = dataLayer.CreateNewAccount(accountname, userid, accounttype, subcategoryid, categoryid, categoryname, providerid, providername, amount, duedate);
            return accountid;
        }

        [HttpPost]
        public string GetAllAccounts(string usertoken)
        {
            string accounts = "";
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.accounts[] appaccounts = appclient.Getaccounts(usertoken);
            accounts = Newtonsoft.Json.JsonConvert.SerializeObject(appaccounts);
            return accounts;
        }


        #endregion

        #region miscellaneous

        [HttpPost]
        public string GetAllProviders(string unid)
        {

            List<Business.PTServiceProvider> providercollection = dataLayer.GetAllProviders(unid);
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(providercollection);// serializer.Serialize(cities);
            return output;
        }

        [HttpPost]
        public void ChangeSetupStatusTofalse()
        {
            dataLayer.ChangeSetupStatus(Session["userid"].ToString(), false);
        }


        #endregion

        #region TimeSheet

        

        #endregion


        #region Person

        [HttpPost]
        public List<AppContact> GetEmployees(string companyid, string userid)
        {
            return dataLayer.GetEmployees(companyid, userid);
        }

        [HttpPost]
        public string GetEmployeeInfo(string employeeid, string companyid, string objectype)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dataLayer.GetEmployeeInformation(employeeid, companyid, Session["userid"].ToString(), objectype));
        }


        [HttpPost]
        public string GetJsonEmployeelist()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dataLayer.GetEmployees(Session["companyid"].ToString(), Session["userid"].ToString()));
        }

        #endregion

        #region Messaging Service

        [HttpPost]
        public string GetMailingLists(string companyid,string usertoken)
        {
            string JsonMailLists = "";
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.maillist[] maillists= appclient.jsonMailinglists(companyid,usertoken);
            JsonMailLists = Newtonsoft.Json.JsonConvert.SerializeObject(maillists);
            return JsonMailLists;
        }
        
        #endregion
    }

}
