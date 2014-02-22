using Business;
using Business.CoreService;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using UI.Controllers;

namespace UI.Models
{
    public class portalcandidate
    {

        public string id { get; set; }
        public string resumeid { get; set; }
        public string resumeurl { get; set; }
        public string contactid { get; set; }
        public string data { get; set; }

       


        
        #region Constructors

        public portalcandidate()
        { }

        #endregion      

        #region DML Methods

        //public string CreateCandidate(string companyid)
        //{
        //    string candidateid = "";
        //    string jobid = "";
        //    string contactid= "";
        //    string resumeid = "";
        //    string data= "";
        //    string  tid = "";
            
        //    //Create Candidate
        //    Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
        //    candidateid = appclient.CreateApplicant(companyid, jobid, candidateid, contactid, resumeid, 0, data);
            
        //    //Business.CandidateTableAdapters.CandidateTableAdapter candidateadapter = new Business.CandidateTableAdapters.CandidateTableAdapter();


        //    //candidateadapter.AddCandidate(companyid, jobid, candidateid, contactid, resumeid, data, out tid);


        //    //Adding Candidate To Company Or Recruiter // Create Relation
        //    return candidateid;
        //}

        //public List<portalcandidate> GetMyCandidates(string companyid,string jobid)
        //{
        //    List<portalcandidate> candidates = new List<portalcandidate>();

        //    Business.ApplicantActions ObjApplicantActions = new ApplicantActions();
        //   List<Business.ApplicationService.applicant> candidatesCollection = ObjApplicantActions.GetApplicants(companyid, jobid);

        //   foreach (Business.ApplicationService.applicant dr in candidatesCollection)
        //   {
        //       portalcandidate tempcandidate = new portalcandidate();

        //       tempcandidate.id = dr.candidateid.ToString();
        //       tempcandidate.resumeid = dr.resumeid.ToString();
        //       tempcandidate.contactid = dr.contactid.ToString();
        //       tempcandidate.data = dr.data.ToString();
        //       candidates.Add(tempcandidate);
        //   }

        //    return candidates;
        //}

        //public List<portalcandidate> GetCandidateDetails(string candidateid, string usertoken)
        //{
        //    List<portalcandidate> candidates = new List<portalcandidate>();

        //    Business.CandidateTableAdapters.CandidateTableAdapter candidateadapter = new Business.CandidateTableAdapters.CandidateTableAdapter();

        //    candidateadapter.GetCandidate("", "", "", "");

        //    return candidates;
        //}
        #endregion

      

        
    }
    
    public class JobApplication
    {
        public string applicationid {get;set;}
        public portalcandidate candidate{get;set;}
        public string appliedon{get;set;}
        public string applicationstatus{get;set;}
    }

    public class JObApplicationdata
    {
        public string creatorid { get; set; }
        public string jobid { get; set; }
        public string candidateid { get; set; }
        public string contactid { get; set; }
        public string resumeid { get; set; }
        public string data { get; set; }
        public string companyid { get; set; }

    }
}