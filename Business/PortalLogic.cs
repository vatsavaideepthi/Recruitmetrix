using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business
{

    public class ApplicantActions
    {
        public string AddApplicant(string creatorid, string ownerid,string jobid, string candidateid, string contactid, string resumeid,string resumeurl, string data)
        {

            string status = "";
            try
            {
                Business.ApplicationService.AppServiceClient Appclient = new ApplicationService.AppServiceClient();

                status = Appclient.CreateApplicant(creatorid,ownerid, "", candidateid, contactid, resumeid,resumeurl, 0, data);
                Appclient.AttachCandidate(creatorid, ownerid, jobid, candidateid, contactid, resumeid, resumeurl, 0, data);
            }
            catch (Exception e)
            {
                string exceptiondetails = e.Data.ToString();
            }
            return status;
        }

        public string AddCandidate(string creatorid, string ownerid, string jobid, string candidateid, string contactid, string resumeid, string resumeurl, string data)
        {

            string status = "";
            try
            {
                Business.ApplicationService.AppServiceClient Appclient = new ApplicationService.AppServiceClient();

                status = Appclient.CreateApplicant(creatorid, ownerid, "", candidateid, contactid, resumeid, resumeurl, 0, data);
                //Appclient.AttachCandidate(creatorid, ownerid, jobid, candidateid, contactid, resumeid, resumeurl, 0, data);
            }
            catch (Exception e)
            {
                string exceptiondetails = e.Data.ToString();
            }
            return status;
        }



        public List<Business.ApplicationService.applicant> GetApplicants(string userid,string companyid, string jobid)
        {
            List<Business.ApplicationService.applicant> applicantscollection = new List<ApplicationService.applicant>();
            Business.ApplicationService.AppServiceClient Appclient = new ApplicationService.AppServiceClient();
            Business.ApplicationService.applicant[] applicants = Appclient.GetApplicants("",companyid,jobid, "","master");
            try
            {
                applicantscollection = applicants.ToList();
            }
            catch(Exception applicantsEmptyException)
            {
                string ExceptionMessage = applicantsEmptyException.Message;
            }
            return applicantscollection;
        }

        public Business.ApplicationService.applicant GetApplicant(string applicantid,string companyid,string userid,string jobid,string candidateid)
        {
            Business.ApplicationService.AppServiceClient appclient = new ApplicationService.AppServiceClient();
            Business.ApplicationService.applicant applicant = new ApplicationService.applicant();
            try
            {
                applicant = appclient.GetApplicant(applicantid, companyid,userid, jobid, candidateid);
            }
            catch (Exception applicantNotFoundException) { string ExceptionMessage = applicantNotFoundException.Message; }

            return applicant;
        }

        public List<Business.ApplicationService.applicant> GetApplicantsinjob(string userid, string companyid, string jobid,string objecttype)
        {
            List<Business.ApplicationService.applicant> applicantscollection = new List<ApplicationService.applicant>();
            Business.ApplicationService.AppServiceClient Appclient = new ApplicationService.AppServiceClient();
            Business.ApplicationService.applicant[] applicants = Appclient.GetApplicantsinjob(userid, companyid, jobid, "",objecttype);
            try
            {
                applicantscollection = applicants.ToList();
            }
            catch (Exception applicantsEmptyException)
            {
                string ExceptionMessage = applicantsEmptyException.Message;
            }
            return applicantscollection;
        }
        //public void importmaillist()
        //{
        //    string strPath = string.Empty;
        //    string filename = string.Empty;

        //    //file upload path
        //    string path = fu.PostedFile.FileName;

        //    string UploadFileFolderPath = System.Configuration.ConfigurationManager.AppSettings["ExcelSheet"]; //folder shld exists in appln path

        //    if (fu.PostedFile.FileName.ToString().Trim().Length > 0)
        //    {

        //        if (!Directory.Exists(Server.MapPath(UploadFileFolderPath)))
        //        {
        //            Directory.CreateDirectory(Server.MapPath(UploadFileFolderPath));
        //        }

        //        filename = Path.GetFileName(fu.PostedFile.FileName);

        //        //Renaming already existing file with numbering

        //        string tempfileName = "";
        //        string savepath = Server.MapPath(UploadFileFolderPath + "/");
        //        string pathToCheck = savepath + filename;

        //        if (System.IO.File.Exists(pathToCheck))
        //        {
        //            //int counter = 2;
        //            //while (System.IO.File.Exists(pathToCheck))
        //            //{
        //            //    tempfileName = counter.ToString() + filename;
        //            //    pathToCheck = savepath + tempfileName;
        //            //    counter++;
        //            //}

        //            //filename = tempfileName;

        //            File.Delete(pathToCheck);

        //        }
        //        else
        //        {

        //        }


        //        fu.PostedFile.SaveAs(Server.MapPath(UploadFileFolderPath) + "/" + filename);

        //        // strPath= UploadFileFolderPath + "/" + filename;
        //    }

        //    strPath = Request.PhysicalApplicationPath + UploadFileFolderPath + "/" + filename;

        //    //Create connection string to Excel work book
        //    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=Excel 12.0;Persist Security Info=False";

        //    //Create Connection to Excel work book
        //    System.Data.OleDb.OleDbConnection excelConnection = new System.Data.OleDb.OleDbConnection(excelConnectionString);


        //    System.Data.OleDb.OleDbDataAdapter MyCommand = null;
        //    System.Data.DataSet DtSet = null;
        //    DataTable dt = new DataTable();


        //    string message = string.Empty;

        //    MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet2$]", excelConnection);
        //    DtSet = new System.Data.DataSet();
        //    MyCommand.Fill(DtSet, "[Sheet2$]");


        //    dt = DtSet.Tables[0];

        //}
    }


}
