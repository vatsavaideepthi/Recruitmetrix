using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class CustomerModel
    {
        Business.DataLayer dl = new Business.DataLayer();


        #region Customers

        public string CreateCustomer(string compid,string objectid, string name, string email)
        {
            return dl.CreateCustomer(compid,objectid, name, email);
        }

        public AppCustomer[] GetAllCustomers(string companyid)
        {
            return dl.GetAllCustomers(companyid);
        }

        public AppCustomer GetCustomer(string customerid,string companyid, string objectid)
        {
            return dl.GetCustomer(customerid,companyid, objectid);
        }

        public bool DeleteCustomer(string companyid, string customerid, string objectid)
        {
            return dl.DeleteCustomer(companyid, customerid, objectid);
        }

        public bool UpdateCustomer(string companyid, string customerid, string objectid, string customername, string customeremail, int status)
        {
            return dl.UpdateCustomer(companyid, customerid, objectid, customername, customeremail, status);
        }

        public string GetJSONCustomerList(string companyid, string tag)
        {
            string customerlist = Newtonsoft.Json.JsonConvert.SerializeObject((dl.GetAllCustomers(companyid).ToList()));
            return customerlist;
        }

        public List<CustomerData> GetCustomerCollection(string companyid)
        {
            List<CustomerData> collection = new List<CustomerData>();
           AppCustomer[] customers =  GetAllCustomers(companyid);

           foreach (AppCustomer item in customers)
           {
               CustomerData tempitem = new CustomerData();

               tempitem.customer = item;
               tempitem.address = GetAddress("",companyid, item.id);
               tempitem.contact = GetContact("",companyid, item.id);
               collection.Add(tempitem);
           }
           return collection;
        }

        public CustomerData GetCustomerCollection(string companyid,string customerid)
        {
           

                CustomerData tempitem = new CustomerData();
                tempitem = GetCustomerCollection(companyid).Where(m => m.customer.id == customerid).Single();
                return tempitem;
        }

        #endregion


        #region Customer : Address


        public string AddAddress(string companyid, string objectid, string line1, string line2, string pincode)
        {
            string Addressid = dl.CreateAddress(companyid, objectid, line1, line2, "", pincode, "", "", "", "primary", "address");
            return Addressid;
        }

        public void UpdateAddress()
        {

        }

        public void DeleteAddress()
        {

        }

        public void GetAddresses()
        {

        }

        public AppAddresss GetAddress(string addressid,string companyid, string objectid)
        {
            AppAddresss address = dl.GetAddress(addressid,companyid, objectid);
            return address;
        }


        public bool IsCustomerHasAddress(string addressid,string companyid, string customerid)
        {

            AppAddresss address = dl.GetAddress(addressid,companyid, customerid);
            if (address != null)
                return true;
            else
                return false;
        }
        #endregion


        #region Customer : Contact


        public string AddContact(string companyid, string objectid,string title,string fname,string lname, string email, string mobile)
        {
            string contactid = dl.CreateContact(companyid, objectid,title,fname,lname, email, "", "", mobile, "", "primary", "contact","");
            return contactid;
        }

        public void UpdateContact()
        {

        }

        public void DeleteContact()
        {

        }
        public void GetContacts()
        {

        }

        public AppContact GetContact(string contactid,string companyid, string objectid)
        {
            AppContact contact = dl.GetContact(contactid,companyid, objectid);
            return contact;
        }


        public bool IsCustomerHasContact(string companyid, string customerid)
        {

            AppContact contact = dl.GetContact("",companyid, customerid);
            if (contact != null)
                return true;
            else
                return false;
        }
        #endregion


        public class CustomerData
        {
            public AppCustomer customer { get; set; }
            public AppAddresss address { get; set; }
            public AppContact contact { get; set; }
        }
    }
}