using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class PortalAddress
    {

        #region Constructors

        public PortalAddress()
        {

        }

        //This is used to store the infn of the address data while retrieving
        public PortalAddress(DataRow AddressRow)
        {
            try
            {
                if (AddressRow.Table.Rows.Count > 0)
                {
                    //m_nAddressId = CommonFunctions.StringToInt(AddressRow["iAddressid"].ToString());
                    AddressId = CommonFunctions.StringToInt(AddressRow["iAddressid"].ToString());
                    AddressType = "Primary";
                    AddressLine1 = AddressRow["vAddress1"].ToString();
                    AddressLine2 = AddressRow["vAddress2"].ToString();
                    City = AddressRow["iCityId"].ToString();
                    State = AddressRow["vStateName"].ToString();
                    Country = "United States";
                    Zipcode = AddressRow["vZipcode"].ToString();


                }

            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region Private members

        protected int m_nAddressId = 0;
        private string m_eAddressType = string.Empty;
        private int m_nObjectId = 0;
        private TypeOfOwner m_strObjectType = TypeOfOwner.None;
        private string m_strAddressLine1 = string.Empty;
        private string m_strAddressLine2 = string.Empty;
        private string m_strAddressLine3 = string.Empty;
        private string m_strState = string.Empty;
        private string m_strCity = string.Empty;
        private string m_strLocation = string.Empty;
        private string m_strCountry = string.Empty;
        private int m_strStateId = 0;
        private int m_strCountryId = 0;
        private string m_nZipcode = string.Empty;

        #endregion

        #region Properties

        public int AddressId
        {
            get { return m_nAddressId; }
            set { m_nAddressId = value; }
        }

        public string AddressType
        {
            get { return m_eAddressType; }
            set { m_eAddressType = value; }
        }
        public int ObjectId
        {
            get { return m_nObjectId; }
            set { m_nObjectId = value; }
        }
        public TypeOfOwner ObjectType
        {
            get { return m_strObjectType; }
            set { m_strObjectType = value; }
        }

        [StringLength(100)]
        public string AddressLine1
        {
            get { return m_strAddressLine1; }
            set { m_strAddressLine1 = value; }
        }

        [StringLength(100)]
        public string AddressLine2
        {
            get { return m_strAddressLine2; }
            set { m_strAddressLine2 = value; }
        }

        [StringLength(100)]
        public string AddressLine3
        {
            get { return m_strAddressLine3; }
            set { m_strAddressLine3 = value; }
        }

        [StringLength(50)]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        [StringLength(50)]
        public string City
        {
            get { return m_strCity; }
            set { m_strCity = value; }
        }

        [StringLength(50)]
        public string Location
        {
            get { return m_strLocation; }
            set { m_strLocation = value; }
        }

        [StringLength(50)]
        public string Country
        {
            get { return m_strCountry; }
            set { m_strCountry = value; }
        }

        [StringLength(10)]
        public string Zipcode
        {
            get { return m_nZipcode; }
            set { m_nZipcode = value; }
        }

        #endregion
    }

    public class PortalContact
    {
        #region Private members

        public string id = "";
        public int bearerid = 0;
        public string m_eContact { get; set; }
        public string title { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string officephone { get; set; }
        public string mobilephone { get; set; }
        public string fax { get; set; }
        public string emailaddress { get; set; }
        public string secondaryemail { get; set; }
        public string createby { get; set; }
        public DateTime createddate;
        public string modifiedby { get; set; }
        public DateTime modifieddate;
        public string m_strDayPhone = string.Empty;
        public string objecttype { get; set; }
        public string Companyname { get; set; }
        public string designation { get; set; }
        public appNotes contactnotes { get; set; }
        #endregion




        public PortalContact()
        {
            this.middlename = "";
            this.Companyname = "";
        }


    }


    public class portaluser
    {
        public string usermail { get; set; }
        public string userpassword { get; set; }
        public string usertitle { get; set; }
    }

    public class Applicantdetails
    {
        public string candidateid { get; set; }
        public string contactid { get; set; }
        public string createddate { get; set; }
        public Business.ApplicationService.appuser creatorid { get; set; }
        public contactcore person { get; set; }
        public applicantaddress address { get; set; }
        public applicantdetailedinfo detailedinfo { get; set; }
        public appjob job { get; set; }
        public appsupplier supplier { get; set; }
        public string data { get; set; }
        public string jobid { get; set; }
        public string modifieddate { get; set; }
        public string resumeid { get; set; }
        public string transactionid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public Applicantdetails()
        {
            this.candidateid = "";
            this.contactid = "";
            this.createddate = "";
            this.creatorid = new Business.ApplicationService.appuser();
            this.person = new contactcore();
            this.address = new applicantaddress();
            this.detailedinfo=new applicantdetailedinfo();
            this.job = new appjob();
            this.supplier = new appsupplier();
            this.data = "";
            this.jobid = "";
            this.modifieddate = "";
            this.resumeid = "";
            transactionid = "";
            firstname = "";
            lastname = "";
        }
    }

    public class contactcore
    {
        public string email { get; set; }
        public string contact { get; set; }
        public string companyname { get; set; }
        public contactcore()
        {
            this.email = "";
            this.contact = "";
            this.companyname = "";
        }
    }
    //public contactcore getbhargavcontact()
    //{
    //    return new contactcore { email = "bvlbhargav@gmail.com" };
    //}

    public class applicantaddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string cityid { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public applicantaddress()
        {
            this.line1 = "";
            this.line2 = "";
            this.city = "";
            this.cityid = "";
            this.state = "";
            this.country = "";
        }
    }

    public class appjob
    {
        public string experience { get; set; }
        public appcategory category { get; set; }
        public appsubcategory subcategory { get; set; }
        public appskills skills { get; set; }
        public appjob()
        {
            this.experience = "";
            this.category = new appcategory();
            this.subcategory = new appsubcategory();
            this.skills = new appskills();
        }
    }

    public class appskills
    {

        public string primary { get; set; }

        public appskills()
        {
            this.primary = "";
        }
    }

    public class appcategory
    {

        public string id { get; set; }
        public string name { get; set; }

        public appcategory()
        {
            this.id = "";
            this.name = "";
        }
    }

    public class appsubcategory
    {

        public string id { get; set; }
        public string name { get; set; }

        public appsubcategory()
        {
            this.id = "";
            this.name = "";
        }
    }


    public class applicantdetailedinfo
    {
        public string visa { get; set; }
        public string payrate { get; set; }
        public string salary { get; set; }
        public string availability { get; set; }
        public string relocation { get; set; }
        public string location { get; set; }
        public string travel { get; set; }
        public string addinfo { get; set; }

   
        public applicantdetailedinfo()
        {
            this.visa = "";
            this.payrate = "";
            this.salary = "";
            this.availability = "";
            this.relocation = "";
            this.location = "";
            this.travel = "";
            this.addinfo = "";
        }



    }

    public class appsupplier
    {
        public string companyname { get; set; }
        public contactcore person { get; set; }
        public applicantaddress address { get; set; }
        public appsupplier()
        {
            this.companyname = "";
            this.person = new contactcore();
            this.address = new applicantaddress();
        }
    }


    public class UserViewModel
    {
        public List<menuitem> actions { get; set; }
        public List<Business.ApplicationService.appuser> UsersCollection { get; set; }

        public UserViewModel()
        {
            this.actions = new List<menuitem>();
            this.UsersCollection = new List<Business.ApplicationService.appuser>();
        }
    }

}