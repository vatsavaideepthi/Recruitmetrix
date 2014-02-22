using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business
{
    public enum TypeOfAddress
    {
        None,
        Invalid,
        Primary = 1,
        ServiceCenter = 3,
        Billing = 2,
        Other = 4
    }

    public enum TypeOfOwner
    {
        None = 0,
        Company,
        Recruiter,
        Candidate
    }

    public enum TypeOfStatus
    {
        None,
        Invalid,
        Active,
        InActive
    }

    public enum TypeOfContact
    {
        None,
        Invalid,
        Primary = 1,
        Technical = 2,
        Billing = 3,
        Other = 4
    }

    public enum TypeofTitle
    {
        Unknown,
        Mr,
        Mrs,
        Dr
    }

    public enum ContactError
    {
        DbError,
        Unknown,
        NoError
    }

    public enum AddressError
    {
        DbError,
        Unknown,
        NoError
    }

    public enum EmployeeError
    {
        DbError,
        Unknown,
        NoError
    }

    public enum e_Publish
    {
        Inhouse = 1,
        Portal = 2,//EVery one in the Portal
        Network = 3, //EVery one in the network
        Partners = 4 //Selected Partners
    }

    public enum TypeOfAvailiability
    {
        SelectAvailiability = 0,
        Immediate = 1,
        OnetoTwoweek = 2,
        TwotoFourWeeks = 3,
        Above4weeks = 4
        ////2-4 Weeks = 3, 
        ////4+ Weeks = 4 
    }


    public class CommonFunctions
    {

        public CommonFunctions()
        {
            GetListData("state", "223");
            //GetAllStates( );
            GetListData("industry", string.Empty);
            //GetAllIndustries();
            GetListData("jobcatg", string.Empty);
            //GetJobCategories();
            GetListData("postype", string.Empty);
            //GetPositions();
            GetListData("exp", string.Empty);
            //GetExperience();

            GetListData("visa", string.Empty);
            //GetVisas();
            GetListData("qualf", string.Empty);
            //GetQulaifications();
            GetListData("status", string.Empty);
            //GetAllStatus(); 
            GetListData("travel", string.Empty);

            //GetExperienceWithCode();





            //  GetQuestions();         

            // //  GetFolders();
            //   // GetAllSubCategories();
            //// GetStatesWithCode();   
        }

        public IList m_listStaticData = new ArrayList();

        public static Hashtable MapNameToId = new Hashtable();
        public static Hashtable MapIdToName = new Hashtable();
        public static Hashtable MapTypeName = new Hashtable();

        #region Client Module Variables

        public static Hashtable MapStateNameToId = new Hashtable();
        public static Hashtable MapIdToStateName = new Hashtable();

        public static IList m_listStateNames = new ArrayList();


        public static Hashtable MapIndustryNameToId = new Hashtable();
        public static Hashtable MapIdToIndustryName = new Hashtable();

        public static IList m_listIndustryNames = new ArrayList();


        public static Hashtable MapCompanyNameToId = new Hashtable();
        public static Hashtable MapCompanyIdToName = new Hashtable();

        public static IList m_listCompanyNames = new ArrayList();

        #endregion




        public static Hashtable MapCatgNametoID = new Hashtable();
        public static Hashtable MapCatgIDtoName = new Hashtable();

        public static IList m_listJobCategoires = new ArrayList();

        public static Hashtable MapSubCatgNametoID = new Hashtable();
        public static Hashtable MapSubCatgIDtoName = new Hashtable();

        public static IList m_listJobSubCategories = new ArrayList();

        public static Hashtable MapPositiontoID = new Hashtable();
        public static Hashtable MapIDtoPosition = new Hashtable();

        public static IList m_listJobPosition = new ArrayList();

        public static Hashtable MapExperiencetoID = new Hashtable();
        public static Hashtable MapIDtoExperience = new Hashtable();

        public static IList m_listJobExperience = new ArrayList();

        public static Hashtable MapClientNametoID = new Hashtable();
        public static Hashtable MapIDtoClientName = new Hashtable();

        public static IList m_listClients = new ArrayList();


        public static Hashtable MapPartnerNametoID = new Hashtable();
        public static Hashtable MapPartnerIDtoName = new Hashtable();

        public static IList m_listPartners = new ArrayList();

        public static Hashtable MapStateNameToCode = new Hashtable();
        public static Hashtable MapStateCodeToName = new Hashtable();

        public static IList m_listStateCodeNames = new ArrayList();

        public static Hashtable MapExperiencetoCode = new Hashtable();
        public static Hashtable MapExpCodetoExperience = new Hashtable();

        public static IList m_listExperienceCode = new ArrayList();

        public static Hashtable MapStatusNametoID = new Hashtable();
        public static Hashtable MapStatusIDtoName = new Hashtable();

        public static IList m_listStatusNames = new ArrayList();

        public static Hashtable MapTravelNametoID = new Hashtable();
        public static Hashtable MapIDtoTravelName = new Hashtable();

        public static IList m_listTravelNames = new ArrayList();

        public static Dictionary<int, Dictionary<int, string>> DicSubCat = new Dictionary<int, Dictionary<int, string>>();




        #region Candidate Module

        public static Hashtable MapQualftoID = new Hashtable();
        public static Hashtable MapIdtoQualf = new Hashtable();

        public static IList m_listQualification = new ArrayList();

        public static Hashtable MapVisatoID = new Hashtable();
        public static Hashtable MapIdtoVisa = new Hashtable();

        public static IList m_listVisas = new ArrayList();


        #endregion

        #region Recruiters Module

        public static Hashtable MapQuestiontoID = new Hashtable();
        public static Hashtable MapIdtoQuestion = new Hashtable();

        public static IList m_listQuestions = new ArrayList();


        #endregion


        //private IList GetExperienceWithCode()
        //{
        //    try
        //    {
        //        if (m_listExperienceCode.Count != 0)
        //            return m_listExperienceCode;

        //        //GetJobExperienceTableAdapter expAdapter = new GetJobExperienceTableAdapter();
        //        //DataTable dtExp = expAdapter.GetJobExperience();
        //        Dictionary<string, string> Experiencelist = Business.StaticDataBank.GetExperienceList();

        //        //StaticDataTableAdapters.StaticDataCollectionTableAdapter expAdapter = new StaticDataTableAdapters.StaticDataCollectionTableAdapter();
        //        //DataTable dtExp = expAdapter.GetStaticData("exp", string.Empty);

        //        foreach (KeyValuePair<string,string> drExp in Experiencelist)
        //        {
        //            //string exp = drExp["vExperience"].ToString();
        //            //string expcode = drExp["vexpcode"].ToString();

        //            string exp = drExp.Key.ToString();
        //            string expcode = drExp.Value.ToString();

        //            MapExpCodetoExperience.Add(expcode, exp);
        //            MapExperiencetoCode.Add(exp, expcode);

        //            m_listExperienceCode.Add(exp);

        //        }

        //        return m_listExperienceCode;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public IList GetListData(string strType, string strParentId)
        {

            try
            {
                //if (m_listStaticData.Count != 0)
                //    return m_listStaticData;

                m_listStaticData.Clear();

                if (strType == "state")
                {
                    if (m_listStateNames.Count != 0)
                        return m_listStateNames;
                }
                else if (strType == "industry")
                {
                    if (m_listIndustryNames.Count != 0)
                        return m_listIndustryNames;
                }
                else if (strType == "jobcatg")
                {
                    if (m_listJobCategoires.Count != 0)
                        return m_listJobCategoires;
                }
                else if (strType == "postype")
                {
                    if (m_listJobPosition.Count != 0)
                        return m_listJobPosition;
                }
                else if (strType == "exp")
                {
                    if (m_listJobExperience.Count != 0)
                        return m_listJobExperience;
                }
                else if (strType == "status")
                {
                    if (m_listStatusNames.Count != 0)
                        return m_listStatusNames;
                }
                else if (strType == "qualf")
                {
                    if (m_listQualification.Count != 0)
                        return m_listQualification;
                }
                else if (strType == "visa")
                {
                    if (m_listVisas.Count != 0)
                        return m_listVisas;
                }

                else if (strType == "travel")
                {
                    if (m_listTravelNames.Count != 0)
                        return m_listTravelNames;
                }
                //StaticDataTableAdapters.StaticDataCollectionTableAdapter posAdapter = new StaticDataTableAdapters.StaticDataCollectionTableAdapter();

                //DataTable dtPos = posAdapter.GetStaticData(strType, strParentId);

                Dictionary<string, Dictionary<string, string>> staticData = Business.StaticDataBank.GetAllStaticData();


                foreach (KeyValuePair<string, Dictionary<string, string>> staticlist in staticData)
                {

                    if (staticlist.Key == "state")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string stateName = listitem.Key.ToString();
                            string stateId = listitem.Value.ToString();

                            MapIdToStateName.Add(stateId, stateName);
                            MapStateNameToId.Add(stateName, stateId);

                            m_listStateNames.Add(stateName);

                            m_listStaticData.Add(stateName);
                        }
                    }
                    else if (staticlist.Key == "industry")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string industryName = listitem.Key.ToString();
                            string industryId = listitem.Value.ToString();

                            MapIdToIndustryName.Add(industryId, industryName);
                            MapIndustryNameToId.Add(industryName, industryId);

                            m_listIndustryNames.Add(industryName);

                            m_listStaticData.Add(industryName);
                        }
                    }
                    //else if (staticlist.Key == "jobcatg")
                    //{
                    //    foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                    //    {
                    //        string jobcatgname = listitem.Key.ToString();
                    //        string jobcatgid = listitem.Value.ToString();

                    //        MapCatgIDtoName.Add(jobcatgid, jobcatgname);
                    //        MapCatgNametoID.Add(jobcatgname, jobcatgid);

                    //        GetSubCatgListData("subcatg", jobcatgid);

                    //        m_listJobCategoires.Add(jobcatgname);

                    //        m_listStaticData.Add(jobcatgname);
                    //    }
                    //}
                    else if (staticlist.Key == "postype")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string jobpos = listitem.Key.ToString();
                            string jobposid = listitem.Value.ToString();

                            MapIDtoPosition.Add(jobposid, jobpos);
                            MapPositiontoID.Add(jobpos, jobposid);

                            m_listJobPosition.Add(jobpos);

                            m_listStaticData.Add(jobpos);
                        }
                    }
                    else if (staticlist.Key == "exp")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string jobexp = listitem.Key.ToString();
                            string jobexpid = listitem.Value.ToString();

                            MapIDtoExperience.Add(jobexpid, jobexp);
                            MapExperiencetoID.Add(jobexp, jobexpid);

                            m_listJobExperience.Add(jobexp);

                            m_listStaticData.Add(jobexp);
                        }
                    }
                    else if (staticlist.Key == "status")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string Name = listitem.Key.ToString();
                            string Id = listitem.Value.ToString();

                            MapStatusIDtoName.Add(Id, Name);
                            MapStatusNametoID.Add(Name, Id);

                            m_listStatusNames.Add(Name);

                            m_listStaticData.Add(Name);
                        }
                    }
                    else if (staticlist.Key == "qualf")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string qualf = listitem.Key.ToString();
                            string qualid = listitem.Value.ToString();

                            MapQualftoID.Add(qualf, qualid);
                            MapIdtoQualf.Add(qualid, qualf);

                            m_listQualification.Add(qualf);

                            m_listStaticData.Add(qualf);
                        }
                    }
                    else if (staticlist.Key == "visa")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string visa = listitem.Key.ToString();
                            string visaid = listitem.Value.ToString();

                            MapVisatoID.Add(visa, visaid);
                            MapIdtoVisa.Add(visaid, visa);

                            m_listVisas.Add(visa);

                            m_listStaticData.Add(visa);
                        }
                    }
                    else if (staticlist.Key == "travel")
                    {
                        foreach (KeyValuePair<string, string> listitem in staticlist.Value)
                        {
                            string travel = listitem.Key.ToString();
                            string travelid = listitem.Value.ToString();

                            MapTravelNametoID.Add(travel, travelid);
                            MapIDtoTravelName.Add(travelid, travel);

                            m_listTravelNames.Add(travel);

                            m_listStaticData.Add(travel);
                        }
                    }


                } //foreach

                return m_listStaticData;
            }
            catch
            {
                return null;
            }
        }


        //public IList GetSubCatgListData(string strType, string strParentId)
        //{

        //    try
        //    {
        //        Dictionary<int, string> objDicSubCateg = new Dictionary<int, string>();
        //        if (DicSubCat.ContainsKey(int.Parse(strParentId)))
        //            return null;

        //        StaticDataTableAdapters.StaticDataCollectionTableAdapter posAdapter = new StaticDataTableAdapters.StaticDataCollectionTableAdapter();
        //        DataTable dtPos = posAdapter.GetStaticData(strType, strParentId);

        //        foreach (DataRow drPos in dtPos.Rows)
        //        {
        //            string strTitle = drPos["vTitle"].ToString();
        //            string strListId = drPos["iListId"].ToString();


        //            //MapSubCatgIDtoName.Add(strListId, strTitle);
        //            //MapSubCatgNametoID.Add(strTitle, strListId);

        //            objDicSubCateg.Add(int.Parse(strListId), strTitle);
        //            // m_listStaticData.Add(strTitle);


        //        }
        //        DicSubCat.Add(int.Parse(strParentId), objDicSubCateg);
        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static int StringToInt(string strValue)
        {
            int IntegerValue = int.Parse(strValue == "" ? "0" : strValue);
            return IntegerValue;
        }

        public static string FirstCharToUpper(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public static string FirstCharToUppr(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }




        static public int intFoldertoMycands = 0;

    }
}
