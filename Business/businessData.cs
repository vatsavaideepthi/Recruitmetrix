using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.CoreService;
using Business.ApplicationService;

namespace Business
{
    public class DataLayer
    {

        ApplicationService.AppServiceClient appclient = new ApplicationService.AppServiceClient();


        #region Core Service Get Methods

        public ApplicationService.accounts[] getUserPaymentData(string usertoken)
        {
            ApplicationService.accounts[] useraccoundata = appclient.Getaccounts(usertoken);
            return useraccoundata;
        }

        #endregion

        #region User Related Methods



        #endregion

        #region Organization Related Methods



        #endregion

        #region User/Organization Account Setup Related Methods

        #region Accounts


        public void addUserPaymentdata(string accountname, string userid, string uniqueid, string accounttype, string subcategoryid, string categoryid, string categoryname, string description, string providerid, string providername, string amount, int duemonth, int duedate, string recurrence, bool global, bool initial)
        {
            try
            {
                if (getRecurrence(recurrence).ToString() != "none")
                    appclient.Create_account(accountname, userid, uniqueid, accounttype, subcategoryid, categoryid, categoryname, description, providerid, providername, Convert.ToDecimal(amount), duemonth, getRecurrence(recurrence).ToString(), "1", duedate, global, initial);
                else
                    throw new Exception("recurrence is not valid");
            }
            catch
            {

            }
        }

        public string CreateNewAccount(string accountname, string userid, string accounttype, string subcategoryid, string categoryid, string categoryname, string providerid, string providername, string amount, int duedate)
        {
            string accountid = "";
            try
            {
                accountid = appclient.Create_account(accountname, userid, categoryid, accounttype, subcategoryid, categoryid, categoryname, "", providerid, providername, Convert.ToDecimal(amount), 0, getRecurrence("month").ToString(), "1", duedate, true, true);
            }
            catch
            {

            }

            return accountid;
        }

        public void updateUserPaymentData(string accountid, string accountname, string userid, string accounttype, string subcategoryid, string categoryid, string categoryname, string description, string providerid, string providername, string amount, int duemonth, int duedate, string recurrence, bool global, bool initial)
        {
            try
            {
                if (getRecurrence(recurrence).ToString() != "none")
                {
                    appclient.Updateaccount(accountid, accountname, userid, accounttype, subcategoryid, categoryid, categoryname, description, providerid, providername, Convert.ToDecimal(amount), duemonth, getRecurrence(recurrence).ToString(), "1", duedate, global, initial);
                    createTransactionOverAccount(accountid, userid, subcategoryid, categoryid, description, amount, duemonth, duedate, 0, 0, 0, getRecurrence(recurrence));
                }
                else
                {
                    throw new Exception("recurrence is not valid");
                }
            }
            catch
            {

            }
        }

        public void hidePaymentData(string accountid, string userid)
        {
            try
            {
                appclient.Delete_account(accountid, userid);
            }
            catch
            {

            }
        }

        public string createUserAccount(string accountname, string userid, string accounttype, string subcatid, string catid, string catname, string desc, string providerid, string providername, string amount, int duemonth, string recurrence, int duedate, bool global, bool intial)
        {
            string accountid = appclient.Create_account(accountname, userid, "", accounttype, subcatid, catid, catname, desc, providerid, providername, Convert.ToDecimal(amount), duemonth, recurrence, "1", duedate, global, intial);
            return accountid;
        }

        #endregion

        #region List Data

        public void createUserList(CoreService.MList categoryitem, CoreService.MList[] subcategoryitems, string userid)
        {
            //Importing CategoryItem into Users App List
            try
            {
                appclient.Createlist(categoryitem.Id, categoryitem.Title, categoryitem.Type, userid, "00000000000000000000000000000000", "00000000000000000000000000000000", userid, "", categoryitem.ObjectType, categoryitem.Indexval, "", "");
            }
            catch { }

            //Retriving Newly Created Category List Item 

            ApplicationService.list1[] newcatglist = appclient.Getlists(categoryitem.Id, "1", "100");

            //Importing Sub CaegoryItems into User App List
            foreach (CoreService.MList subitem in subcategoryitems)
            {
                try
                {
                    appclient.Createlist(subitem.Id, subitem.Title, subitem.Type, newcatglist[0].listid, "00000000000000000000000000000000", "00000000000000000000000000000000", userid, "", subitem.ObjectType, subitem.Indexval, "", "");
                }
                catch
                {

                }
            }
        }

        #endregion

        #endregion

        #region Profile Related Methods

        public bool GetSetupStatus(string companyid)
        {
            ApplicationService.profile profile = appclient.Getprofile(companyid);
            return profile.setup;

        }

        public bool ChangeSetupStatus(string companyid, bool status)
        {
            try
            {
                ApplicationService.profile profile = appclient.Getprofile(companyid);
                appclient.ChangeSetup(profile.profileid, status);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ApplicationService.profile GetUserProfile(string ownerid)
        {
            return appclient.Getprofile(ownerid);
        }

        public ApplicationService.financial GetuserFinancial(string profileid, string ownerid)
        {
            ApplicationService.financial finance = appclient.Getfinancial(profileid, ownerid);
            return finance;
        }

        public void ChangePassword(string newpassword, string authkey, string accesstoken)
        {
            //Here the Service will be Consumed to change the password of the user...
        }

        #endregion

        #region Service Providers

        public List<PTServiceProvider> GetAllProviders(string unid)
        {
            ApplicationService.serviceprovider[] providers = appclient.Getserviceproviders(unid);
            List<PTServiceProvider> providerresult = new List<PTServiceProvider>();
            foreach (ApplicationService.serviceprovider provider in providers)
            {
                PTServiceProvider tempprovider = new PTServiceProvider();
                tempprovider.name = provider.providername;
                tempprovider.id = provider.providerid;
                tempprovider.unid = provider.unid;

                providerresult.Add(tempprovider);
            }
            return providerresult;
        }

        #endregion

        #region Helper Methods

        public static Business.paymentrecurrence getRecurrence(string recurrence)
        {
            Business.paymentrecurrence rec = new Business.paymentrecurrence();
            switch (recurrence.ToLower().ToString())
            {
                case "daily":
                case "day":
                    rec = Business.paymentrecurrence.daily;
                    break;
                case "weekly":
                case "week":
                    rec = Business.paymentrecurrence.week;
                    break;
                case "biweekly":
                case "biweek":
                    rec = Business.paymentrecurrence.biweek;
                    break;
                case "halfmonthly":
                case "halfmonth":
                    rec = Business.paymentrecurrence.halfmonth;
                    break;
                case "monthly":
                case "month":
                    rec = Business.paymentrecurrence.month;
                    break;
                case "quarterly":
                case "quarter":
                    rec = Business.paymentrecurrence.qyear;
                    break;
                case "halfyearly":
                case "halfyear":
                    rec = Business.paymentrecurrence.halfyear;
                    break;
                case "yearly":
                case "year":
                    rec = Business.paymentrecurrence.year;
                    break;
                default:
                    rec = paymentrecurrence.none;
                    break;

            }
            return rec;
        }

        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru, int days)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(days))
                yield return day;
        }

        #endregion

        #region Transactions

        public void createTransactionOverAccount(string accountid, string userid, string subcategoryid, string categoryid, string description, string amount, int duemonth, int duedate, int paiddate, int paidmonth, int paidyear, paymentrecurrence recurrence)
        {

            ApplicationService.financial fin = GetuserFinancial(GetUserProfile(userid).profileid, userid);

            DateTime finstartdate = new DateTime(DateTime.Now.Year, fin.accmonthstart, fin.accdatestart);
            DateTime finenddate = new DateTime(DateTime.Now.Year, fin.accmonthend, fin.accdateend);
            switch (recurrence)
            {
                case paymentrecurrence.daily:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 1))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.week:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 7))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.biweek:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 14))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.halfmonth:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 15))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.month:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 30))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.qyear:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 91))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.halfyear:
                    foreach (DateTime day in EachDay(finstartdate, finenddate, 182))
                    {
                        appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), day.Month, day.Day, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    }
                    break;
                case paymentrecurrence.year:
                    appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), duemonth, duedate, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    break;
                case paymentrecurrence.none:
                    appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), duemonth, duedate, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), "");
                    break;
                default:
                    break;

            }

        }


        public string addquickpayment(string accountid, string userid, string subcategoryid, string categoryid, string description, string amount, int paiddate, int paidmonth, int paidyear, string paymode)
        {
            string transactionid = "";
            transactionid = appclient.Create_transaction(userid, accountid, "", "", description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), 0, 0, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, Convert.ToDecimal(0.00), Convert.ToDecimal(amount), paymentrecurrence.none.ToString());
            appclient.Update_transaction(transactionid, userid, accountid, "", paymode, description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), 0, 0, paiddate, paidmonth, paidyear, "", subcategoryid, categoryid, true, Convert.ToDecimal(amount), Convert.ToDecimal(amount), paymentrecurrence.none.ToString());
            return transactionid;
        }


        public void UpdateTransaction(string transactionid, string userid, string toaccountid, string paymentmode, string subcategoryid, string categoryid, string description, string paidamount, string budget, int duemonth, int duedate, int paiddate, int paidmonth, int paidyear, paymentrecurrence recurrence)
        {

            appclient.Update_transaction(transactionid, userid, toaccountid, "", paymentmode, description, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                duemonth, duedate, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", subcategoryid, categoryid, true, Convert.ToDecimal(paidamount), Convert.ToDecimal(budget), recurrence.ToString());
        }


        #endregion

        #region Invoice

        public string GenerateInvoice(string creatorid, string ownerid, string toaccid, string fromaccid, string type, string objecttype, string invoicedata)
        {
            string invoiceid = appclient.CreateInvoicewithoutupload(creatorid, ownerid, toaccid, fromaccid, type, objecttype, invoicedata);

            return invoiceid;
        }

        public bool UpdateInvoice(string invoiceid, string creatorid, string ownerid, string toaccid, string fromaccid, string type, string objecttype, int status, string invoicedata)
        {
            bool updstatus = appclient.UpdateInvoice(invoiceid, creatorid, ownerid, toaccid, fromaccid, type, objecttype, status, invoicedata);
            return updstatus;
        }

        public AppInvoice GetInvoice(string invoiceid, string ownerid, string creatorid)
        {
            AppInvoice tempinvoice = new AppInvoice();

            try
            {
                ApplicationService.AppServiceClient client = new ApplicationService.AppServiceClient();
                ApplicationService.invoice inv = client.GetInvoice(ownerid, invoiceid, creatorid);
                tempinvoice.data = inv.invoicedata;
                tempinvoice.createddate = inv.createddate;
                tempinvoice.creatorid = inv.creatorid;
                tempinvoice.fileurl = inv.fileid;
                tempinvoice.InvoiceId = inv.invoiceid;
                tempinvoice.status = inv.status.ToString();
            }
            catch
            {

            }
            return tempinvoice;
        }

        public List<AppInvoice> GetAlliInvoices(string ownerid, string creatorid, string type, string objecttype)
        {
            List<AppInvoice> InvoiceCollection = new List<AppInvoice>();

            ApplicationService.AppServiceClient client = new ApplicationService.AppServiceClient();
            ApplicationService.invoice[] invoices = client.GetAllInvoice(ownerid, creatorid, objecttype, type);


            foreach (ApplicationService.invoice inv in invoices)
            {
                AppInvoice tempinvoice = new AppInvoice();

                tempinvoice.data = inv.invoicedata;
                tempinvoice.createddate = inv.createddate;
                tempinvoice.creatorid = inv.creatorid;
                tempinvoice.fileurl = inv.fileid;
                tempinvoice.InvoiceId = inv.invoiceid;
                tempinvoice.status = inv.status.ToString();
                InvoiceCollection.Add(tempinvoice);
            }


            return InvoiceCollection;
        }

        public void UpdateInvoice(string InvoiceId, string ownerid, string usertoken)
        {
            //Consume Update Invoice service
        }

        public void DeleteInvoice(string invoiceid, string ownerid, string creatorid, string usertoken)
        {
            //Consume the Delete Invoice Service
        }

        public void DeleteTransactionInvoice(string transactionid, string invoiceid, string usertoken)
        {
            //Delete invoice based on the given Transactionid
        }

        #endregion

        #region Templates


        public void GetMyTemplates()
        {
            ApplicationService.template[] templates = appclient.GetAllTemplate("36d11942d81611e2977cd8d385a14cb9", "invoice", "layout");
        }

        public AppTemplate GetInvoiceTemplate(string templateid, string ownerid)
        {
            ApplicationService.template templates = appclient.GetTemplate(templateid, ownerid);

            AppTemplate temptemplate = new AppTemplate();
            try
            {
                temptemplate.data = templates.data;
                temptemplate.type = "invoice";
                temptemplate.objecttype = "layout";
                temptemplate.templateid = templates.templateid;
            }
            catch
            {

            }
            return temptemplate;
        }

        #endregion

        #region customers

        public AppCustomer GetCustomer(string customerid, string companyid, string objectid)
        {
            ApplicationService.customer customer = appclient.GetCustomer(companyid, customerid, objectid);
            return AppCustomer.ConvertServiceCustomerToAppCustomer(customer);
        }

        public string CreateCustomer(string compid, string objectid, string custname, string custemail)
        {
            string customerid = appclient.CreateCustomer(compid, objectid, custname, custemail, 0, compid, compid);
            return customerid;
        }

        public bool UpdateCustomer(string compid, string customerid, string objectid, string custname, string custemail, int status)
        {
            bool updateStatus = appclient.UpdateCustomer(compid, customerid, objectid, custname, custemail, "", "", status, false, "", compid);
            return updateStatus;
        }

        public bool DeleteCustomer(string companyid, string customerid, string objectid)
        {
            bool deletestatus = appclient.DeleteCustomer(companyid, customerid, objectid);
            return deletestatus;
        }

        public AppCustomer[] GetAllCustomers(string companyid)
        {
            ApplicationService.customer[] customers = appclient.GetAllCustomer(companyid, "", "");
            return AppCustomer.ConvertServiceCustomerToAppCustomer(customers.ToList()).ToArray();
        }

        #endregion

        #region Address

        public string CreateAddress(string companyid, string objectid, string line1, string line2, string city, string zipcode, string landmark, string state, string country, string type, string objtype)
        {
            string addressid = appclient.CreateAddress(companyid, objectid, line1, line2, city, zipcode, landmark, state, country, "", type, objtype, companyid);
            return addressid;
        }

        public AppAddresss GetAddress(string addressid, string companyid, string objectid)
        {
            ApplicationService.address address = new ApplicationService.address();
            AppAddresss returnaddress = new AppAddresss();
            try
            {
                address = appclient.GetAddres(companyid, objectid, addressid, "address");
                returnaddress = AppAddresss.ConvertServiceAddressToAppAddress(address);
            }
            catch
            {

            }
            return returnaddress;
        }

        public List<AppAddresss> GetAddresses(string companyid, string objectid)
        {
            ApplicationService.address[] addresses = appclient.GetAddress(companyid, objectid);

            return AppAddresss.ConvertServiceAddressesToAppAddresses(addresses.ToList());
        }

        public bool DeleteAddress(string companyid, string objectid, string addressid)
        {
            return appclient.DeleteAddress(companyid, objectid, addressid);
        }

        public bool UpdateAddress(string companyid, string addressid, string objectid, string line1, string line2, string city, string zipcode, string landmark, string state, string country, string objtype)
        {
            return appclient.UpdateAddress(companyid, addressid, objectid, line1, line2, city, zipcode, landmark, state, country, "", "primary", objtype, companyid);
        }

        #endregion

        #region Contact

        public string CreateContact(string companyid, string objectid, string title, string fname, string lname, string email, string phone, string secondaryemail, string mobile, string data, string type, string objtype, string contactgroup)
        {
            string servicecontactid = appclient.CreateContact(objectid, companyid, "uniqueid", "", title, fname, lname, email, phone, secondaryemail, mobile, data, "", companyid, "");
            return servicecontactid;
        }
        public AppContact GetContact(string contactid, string companyid, string objectid)
        {
            ApplicationService.contact servicecontact = new ApplicationService.contact();
            AppContact returncontact = new AppContact();
            try
            {
                servicecontact = appclient.GetContact(contactid, objectid, companyid);
                returncontact = AppContact.ConvertServiceContactToAppContact(servicecontact);
            }
            catch
            {

            }
            return returncontact;
        }

        public List<AppContact> GetContacts(string companyid, string objectid)
        {
            ApplicationService.contact[] servicecontacts = appclient.GetContacts(objectid, companyid, "1", "100");
            return AppContact.ConvertServiceContactsToAppContacts(servicecontacts.ToList());
        }

        public bool UpdateContact(string contactid, string companyid, string objectid, string title, string fname, string lname, string email, string phone, string secondaryemail, string mobile, string data, string type, string objtype)
        {
            return appclient.UpdateContact(contactid, objectid, companyid, "uniqueid", "", title, fname, lname, email, phone, secondaryemail, mobile, data, type, objtype, "", companyid);
        }

        public bool DeleteContact(string companyid, string objectid, string contactid)
        {
            return appclient.DeleteContact(companyid, objectid, contactid);
        }

        #endregion

        #region timesheet

        public string CreateTimeSheet(string ownerid, string creatorid, string employeeid, string employeename, string clientid, string clientname, string startdate, string enddate, string data)
        {
            string timesheetid = "";

            timesheetid = appclient.CreateTimesheet(ownerid, creatorid, employeeid, employeename, clientid, clientname, 0, startdate, enddate, data);

            return timesheetid;
        }

        public bool UpdateTimeSheet(string timesheetid, string ownerid, string creatorid, string employeeid, string employeename, string clientid, string clientname, string startdate, string enddate, string data)
        {
            bool stats = appclient.UpdateTimesheets(timesheetid, ownerid, creatorid, employeeid, employeename, clientid, clientname, 0, startdate, enddate, data);

            return stats;
        }

        #endregion

        #region Person

        public string CreateEmployee(string companyid, string userid, AppContact contact)
        {
            string employeeid = appclient.CreateEmployee(companyid, userid, "", contact.Designation, contact.Title, contact.FirstName, contact.LastName,
                                      contact.email, contact.phone, contact.secondaryemail, contact.mobile, contact.data, "",
                                      userid);
            return employeeid;
        }


        public List<AppContact> GetEmployees(string companyid, string creatorid)
        {

            ApplicationService.contact[] ServiceEmployees = appclient.GetEmployees(companyid, creatorid, "", "");
            List<ApplicationService.contact> serviceemployeecollection = ServiceEmployees.ToList();
            List<AppContact> employees = AppContact.ConvertServiceContactsToAppContacts(serviceemployeecollection);

            return employees;
        }


        public AppContact GetEmployeeInformation(string employeeid, string companyid, string creatorid, string objectype)
        {
            AppContact employeeContact = new AppContact();
            ApplicationService.contact con = appclient.GetEmployee(employeeid, companyid, creatorid);
            employeeContact = AppContact.ConvertServiceContactToAppContact(con);
            return employeeContact;
        }

        #endregion

        #region Message Service

        public void CreateMailinglist(string listname, string companyid, string usertoken)
        {
            ApplicationService.AppRestResponse Objresponse = appclient.CreateMailingList(listname, companyid, usertoken);
        }

        public void Addlistmember(string listid, string address, string name, string description, string usertoken)
        {
            ApplicationService.AppRestResponse Objresponse = appclient.AddListMember(listid, address, name, description, usertoken);
        }

        public ApplicationService.maillist[] GetMailingLists(string companyid, string usertoken)
        {
            try
            {
                maillist[] maillists = appclient.GetMailinglists(companyid, usertoken);
                return maillists;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ApplicationService.maillist GetMailinglist(string listid, string companyid, string usertoken)
        {
            ApplicationService.maillist maililists = appclient.GetMailingList(listid, companyid, usertoken);
            return maililists;
        }

        public ApplicationService.AppRestResponse deletemailinglist(string maillistid, string userid, string parentid)
        {
            ApplicationService.AppRestResponse status = appclient.RemoveMailingList(maillistid, userid, parentid);
            return status;
        }


        #endregion

        #region Notes

        public Business.ApplicationService.AppServiceJobportalResponse AddNotes(string subjectid, string subjecttype, string parentid, string usertoken, string description)
        {
            Business.ApplicationService.AppServiceJobportalResponse response = new Business.ApplicationService.AppServiceJobportalResponse();
            try
            {
                response = appclient.AddNotes(subjectid, subjecttype, parentid, usertoken, description);
            }
            catch
            {

            }
            return response;
        }

        public Business.ApplicationService.AppServiceJobportalResponse UpdateNotes(string commentid, string subjectid, string subjecttype, string parentid, string objecttype, string Des, int visibility, string prop, string usertoken)
        {
            Business.ApplicationService.AppServiceJobportalResponse response = new Business.ApplicationService.AppServiceJobportalResponse();
            try
            {
                response = appclient.UpdateNotes(Convert.ToInt32(commentid), subjectid, subjecttype, parentid, objecttype, Des, visibility, prop, usertoken);
            }
            catch
            {
            }
            return response;
        }

        public List<AppComment> GetNotes(string subjectid, string objecttype)
        {
            ApplicationService.AppNotes[] comments = appclient.GetNotes(subjectid, objecttype);
            return AppComment.ConvertServiceAddressesToAppComments(comments.ToList());
        }

        #endregion

        #region MessageMaster

        public Business.ApplicationService.MessageMaster CreateMessageMaster(string messagetext, string sendorid, string expirytime, string recipients, string messagetype, string subject, string downPlayTime,
            string sendertype, string recipienttype, string MasterParentId, string InvitationParentid, string usertoken)
        {
            Business.ApplicationService.MessageMaster msg = appclient.createmessagemaster(messagetext, sendorid, expirytime, recipients, messagetype, subject, downPlayTime, sendertype, recipienttype, MasterParentId, InvitationParentid, usertoken);
            return msg;
        }

        public List<AppInvitation> GetInvitationMaster(string sendorid, string objecttype)
        {
            ApplicationService.InvitationApp[] invitationmaster = appclient.GetInvitationmaster(sendorid, objecttype);
            return AppInvitation.ConvertServiceAddressToinvitation(invitationmaster.ToList());

        }

        #endregion

        public bool Deletejob(string jobid, string categoryid, string subcategoryid, string objectid)
        {
            bool status = appclient.DeleteJob(jobid, categoryid, subcategoryid, objectid);
            return status;
        }

        public bool Deleteapplicant(string transactionid, string creatorid)
        {
            bool status = appclient.Deleteapplicant(transactionid, creatorid);
            return status;
        }

        public bool updatejob(string jobid, string categoryid, string subcategoryid, string jobname, string jobdescription, string payrate, string preferenceskills, string positiontype, string cityid,
                string startdate, string startmonth, string startyear, string enddate, string endmonth, string endyear, int expid, int publish, string objectid, string objecttype, string email, string phone, string partnerslist, string data, string additionaldetails)
        {
            bool status = appclient.UpdateJob(jobid, categoryid, subcategoryid, jobname, jobdescription, payrate, preferenceskills, positiontype, cityid,
                startdate, startmonth, startyear, enddate, endmonth, endyear,
                expid, publish, objectid, objecttype, email, phone, partnerslist, data, additionaldetails);
            return status;

        }




        #region Customers

        #region Supplier


        public string CreateSupplier(string companyid, string creatorid, string name, string email)
        {
            string supplierID = "";
            try
            {
                supplierID = appclient.CreateSupplier(companyid, companyid, name, email, 0, "", creatorid);
            }
            catch (Exception createSupplierException)
            {
                string createSupplierExceptionMessage = createSupplierException.Message;
            }

            return supplierID;
        }

        public List<customer> GetAllSuppliers(string companyid)
        {
            List<customer> Suppliers = new List<customer>();
            try
            {

                Suppliers = appclient.GetAllSupplier(companyid, "", "").ToList();
            }
            catch (Exception getallSuppliersException)
            {
                string getAllSuppliersExceptionMessage = getallSuppliersException.Message;
            }

            return Suppliers;
        }

        public customer GetSupplier(string companyid, string supplierid, string creatorid)
        {
            customer Supplier = new customer();

            try
            {
                Supplier = appclient.GetSupplier(companyid, supplierid, creatorid);
            }
            catch (Exception getSupplierException)
            {
                string getSupplierExceptionMessage = getSupplierException.Message;
            }
            return Supplier;
        }

        public bool UpdateSupplier(string companyid, string supplierid, string creatorid, string name, string email)
        {
            bool UpdateSupplierStatus = false;

            try
            {
                appclient.UpdateSupplier(companyid, supplierid, companyid, name, email, "", "supplier", 0, true, "", creatorid);
            }

            catch (Exception updateSupplierException)
            {
                string updateSupplierExceptionMessage = updateSupplierException.Message;
            }

            return UpdateSupplierStatus;
        }

        /// <summary>
        /// Delete the exisiting supplier
        /// </summary>
        /// <param name="companyid">CompanyId / Owner ID</param>
        /// <param name="supplierid">Supplierid</param>
        /// <param name="creatorid">Creatorid who creaeted the Supplier</param>
        /// <returns>Returns the Delete Status Of Supplier</returns>
        public bool DeleteSupplier(string companyid, string supplierid, string creatorid)
        {
            bool suppplierDeleteStatus = false;

            try
            {
                suppplierDeleteStatus = appclient.DeleteSupplier(companyid, supplierid, creatorid);
            }
            catch (Exception deleteSupplierException)
            {
                string exceptiondetails = deleteSupplierException.Message;
            }
            return suppplierDeleteStatus;
        }

        #endregion


        #endregion

    }


}
