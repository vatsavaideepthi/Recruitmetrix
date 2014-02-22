using Business;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using UI.Controllers;

namespace UI.Models
{
    public class Job
    {

        #region Private Varibles

        private string m_nJobId = "";

        private string m_nJobRecrId = "";

        private string m_nRecrSelPartJob = "";

        private int m_nJobIDforPartnersList = 0;

        private string m_nCompanyId = string.Empty;

        private string m_nCatId = "";

        private string m_nSubId = "";

        private string m_strCatgName = string.Empty;

        private string m_strCompName = string.Empty;

        private string m_strSubCatgName = string.Empty;

        private string m_strJobName = string.Empty;

        private string m_strJobCode = string.Empty;

        private string m_strJobLength = string.Empty;

        private string m_strJobDesc = string.Empty;

        private string m_strPayRate = string.Empty;

        private IList m_strPositionType = new ArrayList();

        private string m_strPType = string.Empty; //for time being

        private int m_nExpId = 0;

        private string m_strExp = string.Empty;

        private string m_strSkills = string.Empty;

        private string m_strPrefSkills = string.Empty;

        private string m_strTravel = string.Empty;

        private string m_strCity = string.Empty;

        private string m_nStateId = "";

        private string m_strState = string.Empty;

        private DateTime m_dStartDate;

        private DateTime m_dExpiryDate;

        private string m_dExpDate; //for time bieng

        private string m_strVisibleStatus = "UnDefined";

        private int m_nPublish = 0; // (int)e_Publish.Inhouse; 2912

        private IList m_strPartnersList = new ArrayList();

        private DateTime m_ModifiedDate;

        private string m_strRecrId = string.Empty;

        private string m_strRecrName = string.Empty;

        private string m_strJobStatus = string.Empty;


        //These 2 are related to job_post_nfo
        private string m_strPhone = string.Empty;

        private string m_strEmail = string.Empty;


        //Job Attach Candidates columns
        private DateTime m_dPostDate;
        private string m_strStatus;
        private string m_strCandidateId;

        public string additionaldetails { get; set; }

        public string payratemax { get; set; }
        public string payratemode { get; set; }
        public List<JobApplication> applications { get; set; }
        public string jobmailstatus { get; set; }
        public DateTime createddate { get; set; }
        public bool payratenegotaible { get; set; }
        public string jobdata { get; set; }
        #endregion

        #region Public Variables

        public string JobId
        {
            get { return m_nJobId; }
            set { m_nJobId = value; }
        }


        public int JobIDforPublisPartnersList
        {
            get { return m_nJobIDforPartnersList; }
            set { m_nJobIDforPartnersList = value; }
        }


        [StringLength(50)]
        public string CompanyId
        {
            get { return m_nCompanyId; }
            set { m_nCompanyId = value; }
        }

        [StringLength(50)]
        public string RecrId
        {
            get { return m_strRecrId; }
            set { m_strRecrId = value; }
        }

        public string RecrName
        {
            get { return m_strRecrName; }
            set { m_strRecrName = value; }
        }


        public string JobStatus
        {
            get { return m_strJobStatus; }
            set { m_strJobStatus = value; }
        }

        public string CatId
        {
            get { return m_nCatId; }
            set { m_nCatId = value; }
        }

        [StringLength(50)]
        [Required(ErrorMessage = "Category Name is required")]
        public string CatgName
        {
            get { return m_strCatgName; }
            set { m_strCatgName = value; }
        }

        [StringLength(50)]
        public string CompanyName
        {
            get { return m_strCompName; }
            set { m_strCompName = value; }
        }


        public string SubId
        {
            get { return m_nSubId; }
            set { m_nSubId = value; }
        }

        [StringLength(50)]
        public string SubCatgName
        {
            get { return m_strSubCatgName; }
            set { m_strSubCatgName = value; }
        }

        [StringLength(50)]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobName
        {
            get { return m_strJobName; }
            set { m_strJobName = value; }
        }

        [StringLength(50)]
        public string JobCode
        {
            get { return m_strJobCode; }
            set { m_strJobCode = value; }
        }

        [StringLength(50)]
        public string JobLength
        {
            get { return m_strJobLength; }
            set { m_strJobLength = value; }
        }

        [StringLength(50)]
        [Required(ErrorMessage = "Job Description is required")]
        public string JobDesc
        {
            get { return m_strJobDesc; }
            set { m_strJobDesc = value; }
        }

        public string PayRate
        {
            get { return m_strPayRate; }
            set { m_strPayRate = value; }
        }

        public IList PositionType
        {
            get { return m_strPositionType; }
            set { m_strPositionType = value; }
        }

        public string postype
        {
            get { return m_strPType; }
            set { m_strPType = value; }
        } //for time being

        public int ExpId
        {
            get { return m_nExpId; }
            set { m_nExpId = value; }
        }

        public string Exp
        {
            get { return m_strExp; }
            set { m_strExp = value; }
        }

        [StringLength(150)]
        public string Skills
        {
            get { return m_strSkills; }
            set { m_strSkills = value; }
        }

        [StringLength(150)]
        [Required(ErrorMessage = "Preferred skills are required")]
        public string PrefSkills
        {
            get { return m_strPrefSkills; }
            set { m_strPrefSkills = value; }
        }

        [StringLength(10)]
        public string Travel
        {
            get { return m_strTravel; }
            set { m_strTravel = value; }
        }

        [StringLength(10)]
        [Required(ErrorMessage = "City is required")]
        public string City
        {
            get { return m_strCity; }
            set { m_strCity = value; }
        }

        //public int StateId
        //{
        //    get { return m_nStateId; }
        //    set { m_nStateId = value; }
        //}

        [StringLength(50)]
        [Required(ErrorMessage = "State is required")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }


        public DateTime StartDate
        {
            get { return m_dStartDate; }
            set { m_dStartDate = value; }
        }


        [Required(ErrorMessage = "Expiry Date is required")]
        public DateTime ExpiryDate
        {
            get { return m_dExpiryDate; }
            set { m_dExpiryDate = value; }
        }

        public string ExpDate
        {
            get { return m_dExpDate; }
            set { m_dExpDate = value; }
        } //for time being


        [StringLength(15)]
        public string VisibleStatus
        {
            get { return m_strVisibleStatus; }
            set { m_strVisibleStatus = value; }
        }

        public int Publish
        {
            get { return m_nPublish; }
            set { m_nPublish = value; }
        }

        [StringLength(10)]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone
        {
            get { return m_strPhone; }
            set { m_strPhone = value; }
        }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [StringLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email address")]
        public string Email
        {
            get { return m_strEmail; }
            set { m_strEmail = value; }
        }

        public IList SelectedJobPartnersList
        {
            get { return m_strPartnersList; }
            set { m_strPartnersList = value; }
        }


        //Job attached to Mycandidates


        [Required(ErrorMessage = "Expiry Date is required")]
        public DateTime PostDate
        {
            get { return m_dExpiryDate; }
            set { m_dExpiryDate = value; }
        }

        [StringLength(50)]
        public string Status
        {
            get { return m_strStatus; }
            set { m_strStatus = value; }
        }

        [StringLength(50)]
        public string CandidateId
        {
            get { return m_strCandidateId; }
            set { m_strCandidateId = value; }
        }

        public DateTime ModifiedDate
        {
            get { return m_ModifiedDate; }
            set { m_ModifiedDate = value; }
        }

        #endregion

        #region Job Methods

        public List<Job> getJobDetailsByJobId(string intJobId, string companyid)
        {
            CommonFunctions cf = new CommonFunctions();
            try
            {
                Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
                Business.ApplicationService.jobs sjob = new Business.ApplicationService.jobs();
                sjob = appclient.GetJob(intJobId, "", "", companyid);

                Job tempjob = new Job();

                tempjob.JobId = sjob.jobid;
                tempjob.JobIDforPublisPartnersList = 0;
                tempjob.JobName = sjob.jobname;
                tempjob.Exp = CommonFunctions.MapIDtoExperience[sjob.expid.ToString()].ToString();
                tempjob.PayRate = sjob.payrate;
                tempjob.PrefSkills = sjob.preferenceskills;
                tempjob.Status = sjob.status;
                tempjob.State = sjob.cityid;
                tempjob.JobLength = sjob.additionaldetails;
                tempjob.Email = sjob.email;
                tempjob.Phone = sjob.phone;
                tempjob.jobdata = sjob.data;
                UI.Models.Job Objob = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.Models.Job>(sjob.data);
                tempjob.payratemax = Objob.payratemax;
                tempjob.payratemode = Objob.payratemode;
                tempjob.Travel = Objob.Travel;
                tempjob.payratenegotaible = Objob.payratenegotaible;


                try
                {
                    tempjob.ExpiryDate = new DateTime(Convert.ToInt32(sjob.endyear), Convert.ToInt32(sjob.endmonth), Convert.ToInt32(sjob.enddate));
                }
                catch (Exception enddatexception)
                {

                    string strdatexceptioninfo = enddatexception.Message;
                }
                try
                {
                    tempjob.StartDate = new DateTime(Convert.ToInt32(sjob.startyear), Convert.ToInt32(sjob.startmonth), Convert.ToInt32(sjob.startdate));
                }
                catch (Exception strdatexception)
                {

                    string strdatexceptioninfo = strdatexception.Message;
                }
                tempjob.jobmailstatus = sjob.emailstatus;
                try
                {
                    tempjob.createddate = DateTime.Parse(sjob.createddate);
                }
                catch (Exception createddatexception)
                {

                    string strdatexceptioninfo = createddatexception.Message;
                }
                tempjob.JobDesc = sjob.jobdescription;
                tempjob.additionaldetails = sjob.additionaldetails;
                try
                {
                    string[] jobpositioncodearray = sjob.positiontype.Split(',');
                    foreach (string positioncode in jobpositioncodearray)
                    {
                        if (tempjob.postype == "")
                        {
                            tempjob.postype = CommonFunctions.MapIDtoPosition[positioncode].ToString();
                            tempjob.PositionType.Add(CommonFunctions.MapIDtoPosition[positioncode].ToString());
                        }
                        else
                        {
                            tempjob.postype = tempjob.postype + "," +CommonFunctions.MapIDtoPosition[positioncode].ToString();
                            tempjob.PositionType.Add(CommonFunctions.MapIDtoPosition[positioncode].ToString());
                        }
                    }
                    
                }
                catch { tempjob.postype = CommonFunctions.MapIDtoPosition[sjob.positiontype].ToString(); }


                List<Job> jobslist = new List<Job>();

                jobslist.Add(tempjob);
                //}

                return jobslist;

            }

            catch { throw; }

        }

        public string InsertJobDetials(Job job)
        {
            try
            {
                Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();

                int CompID = 1;
                int CatId = 1;
                int stateId = 1;
                int Expcodeid = 1;
                Expcodeid = Convert.ToInt32(CommonFunctions.MapExperiencetoID[job.Exp].ToString());

                string[] postypearray = job.postype.Split(',');
                string PositionTypeId = "";
                if (postypearray.Length > 0)
                {
                    foreach (string postype in postypearray)
                    {
                        if (PositionTypeId == "")
                        { PositionTypeId = CommonFunctions.MapPositiontoID[postype].ToString(); }
                        else
                        {
                            PositionTypeId = PositionTypeId + "," + CommonFunctions.MapPositiontoID[postype].ToString();
                        }
                    }
                }
                job.StartDate = DateTime.Now;
                job.ModifiedDate = DateTime.Now;

                string[] expdatearray = job.ExpDate.Split('/');


                string expdt = new DateTime(Convert.ToInt32(expdatearray[2]), Convert.ToInt32(expdatearray[0]), Convert.ToInt32(expdatearray[1])).ToString("yyyy-MM-dd HH:mm:ss");

                job.Publish = 1; //inhouse

                job.jobdata = Newtonsoft.Json.JsonConvert.SerializeObject(job);
                appclient.CreateJob("", "", job.JobName, job.JobDesc, job.PayRate, job.PrefSkills, PositionTypeId, job.City, job.StartDate.Day.ToString(), job.StartDate.Month.ToString(), job.StartDate.Year.ToString(), Convert.ToInt32(expdatearray[1]).ToString(), Convert.ToInt32(expdatearray[0]).ToString(), Convert.ToInt32(expdatearray[2]).ToString(), Expcodeid, (int)job.Publish, System.Web.HttpContext.Current.Session["companyid"].ToString(), "jobpost", job.Email, job.Phone, "", job.jobdata, job.additionaldetails);

                return m_nJobId;
            }

            catch { throw; }

        } //with mv

        public List<Job> getAllInhouseorPublishActiveorInactiveJobsList() //for time being using static userid
        {
            string executionprocess = "";
            CommonFunctions cf = new CommonFunctions();
            executionprocess += "created commonfunctions Object";
            try
            {
                List<Business.ApplicationService.jobs> servicejobslist = new List<Business.ApplicationService.jobs>();
                executionprocess += "service jobs list object";
                try
                {
                    Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
                    executionprocess += "appclient object";
                    executionprocess += "; companyid =" + System.Web.HttpContext.Current.Session["companyid"].ToString();
                    Business.ApplicationService.jobs[] serviceJobs = appclient.GetJobs("", "", System.Web.HttpContext.Current.Session["companyid"].ToString());
                    executionprocess += "service jobs array";
                    servicejobslist = serviceJobs.ToList();
                    executionprocess += "service jobs array to list done";
                }
                catch
                {
                    throw (new Exception(executionprocess + "exception at service jobs list"));
                }
                List<Job> jobslist = new List<Job>();
                executionprocess += "service joblist object";

                foreach (Business.ApplicationService.jobs sjob in servicejobslist)
                {
                    Job tempjob = new Job();
                    executionprocess += "service tempjob object";

                    tempjob.JobId = sjob.jobid;
                    tempjob.JobIDforPublisPartnersList = 0;
                    tempjob.JobName = sjob.jobname;
                    tempjob.Exp = CommonFunctions.MapIDtoExperience[sjob.expid.ToString()].ToString();
                    tempjob.PayRate = sjob.payrate;
                    tempjob.PrefSkills = sjob.preferenceskills;
                    tempjob.Status = sjob.status;
                    tempjob.jobdata = sjob.data;

                    UI.Models.Job Objob = Newtonsoft.Json.JsonConvert.DeserializeObject<UI.Models.Job>(sjob.data);
                    tempjob.payratemax = Objob.payratemax;
                    tempjob.payratemode = Objob.payratemode;
                    tempjob.Travel = Objob.Travel;
                    tempjob.payratenegotaible = Objob.payratenegotaible;

                    tempjob.State = sjob.cityid;
                    executionprocess += "assigning values to temp job";
                    try
                    {
                        try
                        {
                            tempjob.ExpiryDate = new DateTime(Convert.ToInt32(sjob.endyear), Convert.ToInt32(sjob.endmonth), Convert.ToInt32(sjob.enddate));
                        }
                        catch
                        {
                            tempjob.ExpiryDate = new DateTime(Convert.ToInt32(sjob.endyear), Convert.ToInt32(sjob.endmonth), Convert.ToInt32(sjob.enddate));
                        }
                    }
                    catch (Exception enddatexception)
                    {

                        string strdatexceptioninfo = executionprocess + enddatexception.Message;
                    }

                    try
                    {
                        try
                        {
                            tempjob.StartDate = new DateTime(Convert.ToInt32(sjob.startyear), Convert.ToInt32(sjob.startmonth), Convert.ToInt32(sjob.startdate));
                        }
                        catch
                        {
                            tempjob.StartDate = new DateTime(Convert.ToInt32(sjob.startyear), Convert.ToInt32(sjob.startmonth), Convert.ToInt32(sjob.startdate));
                        }
                    }
                    catch (Exception strdatexception)
                    {

                        string strdatexceptioninfo = executionprocess + strdatexception.Message;
                    }
                    tempjob.jobmailstatus = sjob.emailstatus;

                    try
                    {
                        try
                        {
                            tempjob.createddate = DateTime.ParseExact(sjob.createddate, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            tempjob.createddate = DateTime.Parse(sjob.createddate);
                        }
                    }
                    catch (Exception createddatexception)
                    {

                        string strdatexceptioninfo = executionprocess + createddatexception.Message;
                    }
                    executionprocess += "time conversions done";
                    tempjob.JobDesc = sjob.jobdescription;
                    tempjob.additionaldetails = sjob.additionaldetails;
                    executionprocess += "desc and additional details added";
                    try
                    {
                        string[] positiontypecodearray = sjob.positiontype.Split(',');
                        if (positiontypecodearray.Length > 0)
                        {
                            foreach (string positioncode in positiontypecodearray)
                            {
                                if (tempjob.postype == null || tempjob.postype == "")
                                {
                                    tempjob.postype = CommonFunctions.MapIDtoPosition[positioncode].ToString();
                                }
                                else
                                {
                                    tempjob.postype = tempjob.postype + "," + CommonFunctions.MapIDtoPosition[positioncode].ToString();
                                }
                            }
                        }
                    }
                    catch { tempjob.postype = CommonFunctions.MapIDtoPosition[sjob.positiontype].ToString();  }

                    try
                    {
                        executionprocess += "Before retrieving job applications object ;  ";
                        List<JobApplication> applications = GetJobApplicants(sjob.jobid, System.Web.HttpContext.Current.Session["companyid"].ToString());
                        executionprocess += "job applications object";
                        tempjob.applications = applications;
                        executionprocess += "assigning applicationa to job done";
                        jobslist.Add(tempjob);
                        executionprocess += "assigning job to job list done";
                    }
                    catch
                    {
                        throw (new Exception(executionprocess + "exception at jobs applications list"));
                    }
                }
                return jobslist.OrderByDescending(x => x.createddate).ToList();
            }
            catch
            {
                throw (new Exception(executionprocess + "exception at top level"));
            }

        }

        public List<JobApplication> GetJobApplicants(string jobid, string companyid)
        {
            List<JobApplication> applications = new List<JobApplication>();

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            List<Business.ApplicationService.applicant> serviceapplicants = appclient.GetApplicantsinjob(System.Web.HttpContext.Current.Session["userid"].ToString(), companyid, jobid, "","applicant").ToList();

            foreach (Business.ApplicationService.applicant serviceapplicant in serviceapplicants)
            {
                JobApplication tempjobapplication = new JobApplication();
                tempjobapplication.applicationid = serviceapplicant.transactionid;

                portalcandidate tempcandidate = new portalcandidate();
                tempcandidate.id = serviceapplicant.candidateid;
                tempcandidate.resumeid = serviceapplicant.resumeid;
                if (tempcandidate.resumeid == "" || tempcandidate.resumeid == null)
                {

                    tempcandidate.resumeurl = "";
                }
                else
                {
                    try
                    {
                        AmazonFile af = new AmazonFile();
                        tempcandidate.resumeurl = af.GetFileUrl(serviceapplicant.resumeid, System.Web.HttpContext.Current.Session["usertoken"].ToString());
                    }
                    catch
                    {

                    }
                }

                tempcandidate.contactid = serviceapplicant.contactid;
                tempcandidate.data = serviceapplicant.data;

                tempjobapplication.candidate = tempcandidate;

                try
                {
                    tempjobapplication.applicationstatus = serviceapplicant.status.ToString();
                }
                catch
                {
                    tempjobapplication.applicationstatus = "0";
                }



                applications.Add(tempjobapplication);
            }
            return applications;
        }

        public Job getPublicJobById(string intJobId)
        {
            CommonFunctions cf = new CommonFunctions();
            try
            {
                Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
                Business.ApplicationService.jobs sjob = new Business.ApplicationService.jobs();
                sjob = appclient.GetJob(intJobId, "", "", "");

                Job tempjob = new Job();
                tempjob.JobId = sjob.jobid;
                tempjob.JobIDforPublisPartnersList = 0;
                tempjob.JobName = sjob.jobname;
                tempjob.Exp = CommonFunctions.MapIDtoExperience[sjob.expid.ToString()].ToString();
                tempjob.PayRate = sjob.payrate;
                tempjob.PrefSkills = sjob.preferenceskills;
                tempjob.Status = sjob.status;
                tempjob.State = sjob.cityid;
                tempjob.JobLength = sjob.additionaldetails;
                tempjob.Email = sjob.email;
                tempjob.Phone = sjob.phone;
                tempjob.jobdata = sjob.data;
                tempjob.CompanyId = sjob.objectid;
                try
                {
                    tempjob.ExpiryDate = new DateTime(Convert.ToInt32(sjob.endyear), Convert.ToInt32(sjob.endmonth), Convert.ToInt32(sjob.enddate));
                }
                catch (Exception enddatexception)
                {

                    string strdatexceptioninfo = enddatexception.Message;
                }
                try
                {
                    tempjob.StartDate = new DateTime(Convert.ToInt32(sjob.startyear), Convert.ToInt32(sjob.startmonth), Convert.ToInt32(sjob.startdate));
                }
                catch (Exception strdatexception)
                {

                    string strdatexceptioninfo = strdatexception.Message;
                }
                tempjob.jobmailstatus = sjob.emailstatus;
                try
                {
                    tempjob.createddate = DateTime.Parse(sjob.createddate);
                }
                catch (Exception createddatexception)
                {

                    string strdatexceptioninfo = createddatexception.Message;
                }
                tempjob.JobDesc = sjob.jobdescription;
                tempjob.additionaldetails = sjob.additionaldetails;
                try
                {
                    tempjob.postype = CommonFunctions.MapIDtoPosition[sjob.positiontype].ToString();
                }
                catch { }

                return tempjob;

            }

            catch { throw; }

        }

        #endregion

    }

    public class JobPosting
    {
        public Job postedjob { get; set; }
        public List<JobApplication> applications { get; set; }

        public JobPosting GetJobPosting(string jobid, string companyid, string accesstoken)
        {
            JobPosting appliedjob = new JobPosting();
            List<Job> jobinfo = new Job().getJobDetailsByJobId(jobid, companyid);
            appliedjob.postedjob = jobinfo[0];
            List<JobApplication> applications = new List<JobApplication>();
            Business.ApplicantActions ObjApplicantActions = new ApplicantActions();
            List<Business.ApplicationService.applicant> candidatesCollection = ObjApplicantActions.GetApplicantsinjob(System.Web.HttpContext.Current.Session["userid"].ToString(), companyid, jobid,"applicant");
            foreach (Business.ApplicationService.applicant dr in candidatesCollection)
            {
                JobApplication tempjobapplication = new JobApplication();
                tempjobapplication.applicationid = dr.transactionid.ToString();
                portalcandidate tempcandidate = new portalcandidate();
                tempcandidate.id = dr.candidateid.ToString();
                tempcandidate.resumeid = dr.resumeid.ToString();
                if (tempcandidate.resumeid == "" || tempcandidate.resumeid == null)
                {
                    tempcandidate.resumeurl = "";
                }
                else
                {
                    AmazonFile af = new AmazonFile();
                    tempcandidate.resumeurl = af.GetFileUrl(dr.resumeid.ToString(), accesstoken);
                }
                tempcandidate.contactid = dr.contactid.ToString();
                tempcandidate.data = dr.data.ToString();
                tempjobapplication.candidate = tempcandidate;
                tempjobapplication.appliedon = dr.createddate.ToString();
                try
                {
                    tempjobapplication.applicationstatus = dr.status.ToString();
                }
                catch
                {
                    tempjobapplication.applicationstatus = "0";
                }
                applications.Add(tempjobapplication);
            }
            appliedjob.applications = applications;
            return appliedjob;
        }
    }
}