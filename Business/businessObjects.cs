using Business.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

    public enum registrationobjects
    {
        organization,
        user,
        mechant,
        vendor,
        supplier
    }

    public enum loginmodes
    {
        web,
        standalone,
        mobile,
        service
    }

    public enum paymentrecurrence
    {
        daily, //Daily
        week, //Weekly
        biweek, //Biweekly
        halfmonth,//Half Month
        month, //Monthly
        qyear, //Quarterly
        halfyear, //Half Yearly
        year, //Yearly
        none
    }

    public class AppInvoice
    {
        public string InvoiceId { get; set; }
        public string creatorid { get; set; }
        public string ownerid { get; set; }
        public string createddate { get; set; }
        public string fileurl { get; set; }
        public string status { get; set; }
        public bool isAttachedToTransaction { get; set; }
        public string data { get; set; }


    }

    public class PTServiceProvider
    {
        public string name { get; set; }
        public string id { get; set; }
        public string unid { get; set; }
    }

    public class AppTemplate
    {
        public string templateid { get; set; }
        public string type { get; set; }
        public string objecttype { get; set; }
        public string data { get; set; }
    }

    public class User
    {
        ApplicationService.AppServiceClient appclient = new ApplicationService.AppServiceClient();

        public string register(string userEmail, string password, string objecttype, string title, string imageurl, string firstname, string lastname)
        {
            string registereduserid = appclient.RegisterUser(userEmail, password, objecttype, title, loginmodes.web.ToString(), imageurl, firstname, lastname);
            return registereduserid;
        }

        public string RegisterCompany(string title, string email, string password, string fname, string lname)
        {
            string companyid = appclient.RegisterCompany(title, email, password, "web", "", fname, lname, "private", 0, "", "", "");
            return companyid;
        }

        public string login(string userEmail, string password)
        {
            string usertoken = "";
            try
            {
                usertoken = appclient.Login(userEmail, password);
            }
            catch
            {

            }
            return usertoken;
        }

        public AccessObject LoginCompany(string userEmail, string password)
        {
            try
            {
                ApplicationService.AccessObject objAccess = appclient.LoginCompany(userEmail, password);
                return objAccess;
            }
            catch
            {
                return new AccessObject();
            }
        }

        public void ChangePassword(string oldpassword, string newpassword, string accesstoken)
        {
            appclient.ChangePassword(oldpassword, newpassword, accesstoken);
        }
    }

    public class Organization
    {
        public string id { get; set; }
        public string name { get; set; }
        public string ownerid { get; set; }
        public string ownername { get; set; }
        public string status { get; set; }

        ApplicationService.AppServiceClient appclient = new ApplicationService.AppServiceClient();


        public string register(string userEmail, string password, string objecttype, string title, string imageurl, string firstname, string lastname)
        {
            string registereduserid = appclient.RegisterUser(userEmail, password, objecttype, title, loginmodes.web.ToString(), imageurl, firstname, lastname);
            return registereduserid;
        }


        public string RegisterCompany(string title, string email, string password, string fname, string lname)
        {
            string companyid = appclient.RegisterCompany(title, email, password, "web", "", fname, lname, "private", 0, "", "", "");
            return companyid;
        }

        public string login(string userEmail, string password)
        {
            string usertoken = "";
            try
            {
                usertoken = appclient.Login(userEmail, password);
            }
            catch
            {
            }
            return usertoken;
        }
    }

    public class AppCustomer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string companyid { get; set; }
        //public string companyname { get; set; }
        //public string companyEmail { get; set; }

        public string type { get; set; }
        public string objecttype { get; set; }

        public int status { get; set; }
        //public string properties { get; set; }
        public string data { get; set; }
        public string objectid { get; set; }
        public string createdby { get; set; }
        public string createdon { get; set; }
        public string modifiedby { get; set; }
        public string modifiedon { get; set; }


        public AppCustomer()
        {
            this.id = "";
            this.name = "";
            this.companyid = "";
            this.type = "";
            this.objecttype = "";
            this.status = 0;
            this.data = "";
            this.objectid = "";
            this.objecttype = "";
            this.createdby = "";
            this.createdon = "";
            this.modifiedby = "";
            this.modifiedon = "";
        }

        public static List<AppCustomer> ConvertServiceCustomerToAppCustomer(List<ApplicationService.customer> servicecustomers)
        {
            List<AppCustomer> tempcustomers = new List<AppCustomer>();

            foreach (ApplicationService.customer item in servicecustomers)
            {
                AppCustomer tempcustomer = new AppCustomer();

                tempcustomer.id = item.customerid;
                tempcustomer.name = item.name;
                tempcustomer.email = item.email;
                tempcustomer.companyid = item.companyid;
                tempcustomer.status = item.status;
                tempcustomer.data = item.data;
                tempcustomer.objectid = item.objectid;
                tempcustomer.createdby = item.createdby;
                tempcustomer.createdon = item.createddate;
                tempcustomer.modifiedby = item.modifiedby;
                tempcustomer.modifiedon = item.modifieddate;
                tempcustomers.Add(tempcustomer);
            }

            return tempcustomers;
        }

        public static AppCustomer ConvertServiceCustomerToAppCustomer(ApplicationService.customer serviceCustomer)
        {
            AppCustomer tempcustomer = new AppCustomer();

            tempcustomer.id = serviceCustomer.customerid;
            tempcustomer.name = serviceCustomer.name;
            tempcustomer.email = serviceCustomer.email;
            tempcustomer.companyid = serviceCustomer.companyid;
            tempcustomer.status = serviceCustomer.status;
            tempcustomer.data = serviceCustomer.data;
            tempcustomer.objectid = serviceCustomer.objectid;
            tempcustomer.createdby = serviceCustomer.createdby;
            tempcustomer.createdon = serviceCustomer.createddate;
            tempcustomer.modifiedby = serviceCustomer.modifiedby;
            tempcustomer.modifiedon = serviceCustomer.modifieddate;

            return tempcustomer;
        }
    }

    public class AppAddresss
    {
        public string id { get; set; }
        public string objectid { get; set; }
        public string companyid { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string landmark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string objecttype { get; set; }


        public static List<AppAddresss> ConvertServiceAddressesToAppAddresses(List<ApplicationService.address> addresses)
        {
            List<AppAddresss> tempaddresses = new List<AppAddresss>();

            foreach (ApplicationService.address item in addresses)
            {
                AppAddresss tempaddress = new AppAddresss();

                tempaddress.id = item.addressid;
                tempaddress.objectid = item.objectid;
                tempaddress.companyid = item.creatorid;
                tempaddress.line1 = item.addressline1;
                tempaddress.line2 = item.addressline2;
                tempaddress.landmark = item.landmark;
                tempaddress.city = item.city;
                tempaddress.state = item.state;
                tempaddress.country = item.country;
                tempaddress.zipcode = item.zipcode;
                tempaddress.objecttype = item.objecttype;
                tempaddresses.Add(tempaddress);
            }

            return tempaddresses;
        }

        public static AppAddresss ConvertServiceAddressToAppAddress(ApplicationService.address serviceAddress)
        {

            AppAddresss tempaddress = new AppAddresss();
            tempaddress.id = serviceAddress.addressid;
            tempaddress.objectid = serviceAddress.objectid;
            tempaddress.companyid = serviceAddress.creatorid;
            tempaddress.line1 = serviceAddress.addressline1;
            tempaddress.line2 = serviceAddress.addressline2;
            tempaddress.landmark = serviceAddress.landmark;
            tempaddress.city = serviceAddress.city;
            tempaddress.state = serviceAddress.state;
            tempaddress.country = serviceAddress.country;
            tempaddress.zipcode = serviceAddress.zipcode;
            tempaddress.objecttype = serviceAddress.objecttype;

            return tempaddress;
        }


    }

    public class AppContact
    {
        public string id { get; set; }
        public string objectid { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string secondaryemail { get; set; }
        public string Designation { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string type { get; set; }
        public string objecttype { get; set; }
        public string data { get; set; }
        public string createdby { get; set; }
        public string createdon { get; set; }
        public string creatorid { get; set; }
        public string modifiedby { get; set; }
        public string modifiedon { get; set; }

        public AppContact()
        {
            this.id = "";
            this.objectid = "";
            this.Title = "";
            this.FirstName = "";
            this.LastName = "";
            this.email = "";
            this.secondaryemail = "";
            this.phone = "";
            this.mobile = "";
            this.type = "";
            this.objecttype = "";
            this.data = "";
            this.createdby = "";
            this.createdon = "";
            this.creatorid = "";
            this.modifiedby = "";
            this.modifiedon = "";
        }

        public static AppContact ConvertServiceContactToAppContact(ApplicationService.contact servicecontact)
        {
            AppContact tempContact = new AppContact();

            tempContact.id = servicecontact.contactid;
            tempContact.objectid = servicecontact.objectid;
            tempContact.Title = servicecontact.title;
            tempContact.FirstName = servicecontact.firstname;
            tempContact.LastName = servicecontact.lastname;
            tempContact.email = servicecontact.emailid;
            tempContact.secondaryemail = servicecontact.secondaryemail;
            tempContact.phone = servicecontact.phone;
            tempContact.mobile = servicecontact.mobile;
            tempContact.type = servicecontact.type;
            tempContact.objecttype = servicecontact.objecttype;
            tempContact.data = servicecontact.data;
            tempContact.createdby = servicecontact.createdby;
            tempContact.createdon = servicecontact.createddate;
            tempContact.creatorid = servicecontact.creatorid;
            tempContact.modifiedby = servicecontact.modifiedby;
            tempContact.modifiedon = servicecontact.modifieddate;

            return tempContact;
        }

        public static List<AppContact> ConvertServiceContactsToAppContacts(List<ApplicationService.contact> servicecontacts)
        {
            List<AppContact> tempContacts = new List<AppContact>();

            foreach (ApplicationService.contact servicecontact in servicecontacts)
            {
                AppContact tempContact = new AppContact();

                tempContact.id = servicecontact.contactid;
                tempContact.objectid = servicecontact.objectid;
                tempContact.Title = servicecontact.title;
                tempContact.FirstName = servicecontact.firstname;
                tempContact.LastName = servicecontact.lastname;
                tempContact.email = servicecontact.emailid;
                tempContact.secondaryemail = servicecontact.secondaryemail;
                tempContact.phone = servicecontact.phone;
                tempContact.mobile = servicecontact.mobile;
                tempContact.type = servicecontact.type;
                tempContact.objecttype = servicecontact.objecttype;
                tempContact.data = servicecontact.data;
                tempContact.createdby = servicecontact.createdby;
                tempContact.createdon = servicecontact.createddate;
                tempContact.creatorid = servicecontact.creatorid;
                tempContact.modifiedby = servicecontact.modifiedby;
                tempContact.modifiedon = servicecontact.modifieddate;

                tempContacts.Add(tempContact);
            }
            return tempContacts;
        }
    }

    public class AppComment
    {
        public string id { get; set; }
        public string subjectid { get; set; }
        public string subjecttype { get; set; }
        public appuser parentid { get; set; }
        public string type { get; set; }
        public string objecttype { get; set; }
        public string status { get; set; }
        public string des { get; set; }
        public appuser ownerid { get; set; }
        public string appid { get; set; }
        public string networkid { get; set; }
        public appuser modifiedby { get; set; }
        public string modifieddate { get; set; }


        public static List<AppComment> ConvertServiceAddressesToAppComments(List<ApplicationService.AppNotes> comments)
        {
            List<AppComment> appcomments = new List<AppComment>();

            foreach (ApplicationService.AppNotes com in comments)
            {
                AppComment comment = new AppComment();
                comment.id = com.id;
                comment.subjectid = com.subjectid;
                comment.subjecttype = com.subjecttype;
                comment.parentid = com.parentid;
                comment.type = com.type;
                comment.objecttype = com.objecttype;
                comment.status = com.status;
                comment.des = com.des;
                comment.ownerid = com.ownerid;
                comment.appid = com.appid;
                comment.networkid = com.networkid;
                comment.modifiedby = com.modifiedby;
                comment.modifieddate = com.modifieddate;
                appcomments.Add(comment);
            }

            return appcomments;
        }

        public static AppComment ConvertServiceAddressToAppComment(ApplicationService.AppNotes serviceComments)
        {

            AppComment comment = new AppComment();
            comment.id = serviceComments.id;
            comment.subjectid = serviceComments.subjectid;
            comment.subjecttype = serviceComments.subjecttype;
            comment.parentid = serviceComments.parentid;
            comment.type = serviceComments.type;
            comment.objecttype = serviceComments.objecttype;
            comment.status = serviceComments.status;
            comment.des = serviceComments.des;
            comment.ownerid = serviceComments.ownerid;
            comment.appid = serviceComments.appid;
            comment.networkid = serviceComments.networkid;
            comment.modifiedby = serviceComments.modifiedby;
            comment.modifieddate = serviceComments.modifieddate;
            return comment;
        }
    }

    public class AppInvitation
    {
        public string CreatorId { get; set; }
        public string MessageTime { get; set; }
        public string ExpiryTime { get; set; }
        public string MessageText { get; set; }
        public string SendorId { get; set; }
        public string Title { get; set; }
        public string ObjectType { get; set; }
        public string Recipients { get; set; }
        public string mStatus { get; set; }
        public string MessageMasterId { get; set; }
        public string ModifiedTime { get; set; }
        public string prop { get; set; }


        //public class tagsbu
        //{
        //    public string read { get; set; }
        //    public string important { get; set; }
        //    public string attachments { get; set; }
        //}

        public static List<AppInvitation> ConvertServiceAddressToinvitation(List<ApplicationService.InvitationApp> invitationapp)
        {
            List<AppInvitation> invapp = new List<AppInvitation>();
            foreach (ApplicationService.InvitationApp invitation in invitationapp)
            {
                AppInvitation invapplication = new AppInvitation();
                invapplication.CreatorId = invitation.invitorid;
                invapplication.MessageTime = invitation.invitetime.ToString();
                invapplication.ExpiryTime = invitation.expirytime.ToString();
                invapplication.MessageText = invitation.invitationtext;
                invapplication.SendorId = invitation.sendorid;
                invapplication.Title = invitation.title;
                invapplication.ObjectType = invitation.objecttype;
                invapplication.Recipients = invitation.invitees;
                invapplication.mStatus = invitation.mstatus;
                invapplication.MessageMasterId = invitation.invitationmasterid;
                invapplication.ModifiedTime = invitation.modifiedtime.ToString();
                invapplication.prop = invitation.Prop;
                invapp.Add(invapplication);
            }

            return invapp;
        }


    }
}






