using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;

namespace UI.Models
{
    public class ProfileModel
    {


        public static Business.ApplicationService.profile GetUserProfile(string ownerid)
        {
            DataLayer bd = new DataLayer();
            Business.ApplicationService.profile objProfile = new Business.ApplicationService.profile();
            objProfile = bd.GetUserProfile(ownerid);
            return objProfile;
        }

        public static bool GetProfilesetupstatus(string companyid)
        {
            DataLayer bd = new DataLayer();
            return bd.GetSetupStatus(companyid);
        }

        public static string GetDefaultTemplate(string companyid)
        {
            string templateid = "";
            DataLayer dataLayer = new DataLayer();
            Business.ApplicationService.profile userprofile = dataLayer.GetUserProfile(companyid);
            templateid = userprofile.templateid;
            return templateid;
        }


        public bool ChangePassword(UI.Models.ProfileModelObject.ChangePassword changepassworddata,string accesstoken)
        {
            bool passwordchangestatus = false;
            Business.DataLayer datalayer = new DataLayer();
            datalayer.ChangePassword(changepassworddata.newpassword, changepassworddata.AuthToken, accesstoken);

            return passwordchangestatus;
        }
    }
}