using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.applicant[] applicants = appclient.GetApplicants("", Session["companyid"].ToString(), "", "","master");

            List<UI.Models.Applicantdetails> portalcontacts = new List<Models.Applicantdetails>();
            foreach (Business.ApplicationService.applicant sinapplicant in applicants)
            {
                UI.Models.Applicantdetails singleapplicant = new Models.Applicantdetails();
                try
                {
                    UI.Models.Applicantdetails Objapplicant = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.Models.Applicantdetails>(sinapplicant.data);

                    if (Objapplicant == null)
                    {
                        singleapplicant.firstname = "";
                        singleapplicant.lastname = "";
                        singleapplicant.person.email = "";
                    }
                    else
                    {
                        singleapplicant.firstname = Objapplicant.firstname;
                        singleapplicant.lastname = Objapplicant.lastname;
                        singleapplicant.person.email = Objapplicant.person.email;
                        singleapplicant.person.contact = Objapplicant.person.contact;
                        singleapplicant.person.companyname = Objapplicant.person.companyname;
                        singleapplicant.address.line1 = Objapplicant.address.line1;
                        singleapplicant.address.line2 = Objapplicant.address.line2;
                        singleapplicant.address.city = Objapplicant.address.city;
                        singleapplicant.address.cityid = Objapplicant.address.cityid;
                        singleapplicant.address.state = Objapplicant.address.state;
                        singleapplicant.address.country = Objapplicant.address.country;
                        singleapplicant.detailedinfo.visa = Objapplicant.detailedinfo.visa;
                        singleapplicant.detailedinfo.payrate = Objapplicant.detailedinfo.payrate;
                        singleapplicant.detailedinfo.salary = Objapplicant.detailedinfo.salary;
                        singleapplicant.detailedinfo.availability = Objapplicant.detailedinfo.availability;
                        singleapplicant.detailedinfo.relocation = Objapplicant.detailedinfo.relocation;
                        singleapplicant.detailedinfo.location = Objapplicant.detailedinfo.location;
                        singleapplicant.detailedinfo.travel = Objapplicant.detailedinfo.travel;
                        singleapplicant.detailedinfo.addinfo = Objapplicant.detailedinfo.addinfo;
                        singleapplicant.job.experience = Objapplicant.job.experience;
                        singleapplicant.job.skills.primary = Objapplicant.job.skills.primary;
                        singleapplicant.job.category.id = Objapplicant.job.category.id;
                        singleapplicant.job.category.name = Objapplicant.job.category.name;
                        singleapplicant.job.subcategory.id = Objapplicant.job.subcategory.id;
                        singleapplicant.job.subcategory.name = Objapplicant.job.subcategory.name;
                        singleapplicant.supplier.companyname = Objapplicant.supplier.companyname;
                        singleapplicant.supplier.person.email = Objapplicant.supplier.person.email;
                        singleapplicant.supplier.person.contact = Objapplicant.supplier.person.contact;
                        singleapplicant.supplier.person.companyname = Objapplicant.supplier.person.companyname;


                    }

                }
                catch
                {
                    singleapplicant.firstname = "";
                }
                singleapplicant.candidateid = sinapplicant.candidateid;
                singleapplicant.contactid = sinapplicant.contactid;
                singleapplicant.createddate = sinapplicant.createddate;
                singleapplicant.creatorid = sinapplicant.creatorid;
                singleapplicant.jobid = sinapplicant.jobid;

                portalcontacts.Add(singleapplicant);
            }
            return View(portalcontacts);

        }

        [HttpPost]
        public string Getapplicants()
        {
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.applicant[] applicants = appclient.GetApplicants("", Session["companyid"].ToString(), "", "", "master");
            return Newtonsoft.Json.JsonConvert.SerializeObject(applicants.ToList());
            //     List<UI.Models.Applicantdetails> portalapplicants = new List<Models.Applicantdetails>();
            //    string applicant = "";
            //    foreach (Business.ApplicationService.applicant sinapplicant in applicants)
            //    {
            //        UI.Models.Applicantdetails singleapplicant = new Models.Applicantdetails();
            //        if (sinapplicant.objecttype != "masterview")
            //        {
            //            singleapplicant.data = Newtonsoft.Json.JsonConvert.SerializeObject(applicants.ToList());
            //            portalapplicants.Add(singleapplicant);
            //        }
            //    }
            //    return portalapplicants.ToString();
            //}
       }

        [HttpPost]
        public string Addcandidates(string jobid,string candidateid)
        {
            string applicantid="";
            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();     
            Business.ApplicationService.applicant[] applicants = appclient.GetApplicants("", Session["companyid"].ToString(), "", candidateid, "master");
            foreach (Business.ApplicationService.applicant sinapplicant in applicants)
            {
                UI.Models.Applicantdetails singleapplicant = new Models.Applicantdetails();
                applicantid = appclient.AttachCandidate(Session["userid"].ToString(), Session["companyid"].ToString(), jobid, candidateid,
                         sinapplicant.contactid, sinapplicant.resumeid, sinapplicant.resumeurl, 0, sinapplicant.data);
                
            }
            return applicantid;
            
        }
    }
}
    

