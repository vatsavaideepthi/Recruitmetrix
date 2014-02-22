using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    class StaticDataBank
    {
        public static Dictionary<string, string> GetExperienceList()
        {
            Dictionary<string, string> experiencelist = new Dictionary<string, string>();

            experiencelist.Add("0 to 2", "1");
            experiencelist.Add("2 to 5", "2");
            experiencelist.Add("5 to 8", "3");
            experiencelist.Add("8 to 12", "4");
            experiencelist.Add("12 to 20", "5");
            experiencelist.Add("20 and above", "6");
            return experiencelist;
        }

        public static Dictionary<string, string> GetPositionTypeList()
        {
            Dictionary<string, string> positiontypeslist = new Dictionary<string, string>();

            positiontypeslist.Add("Contract - Corp-to-Corp", "1");
            positiontypeslist.Add("Contract - Independent", "2");
            positiontypeslist.Add("Contract - W2", "3");
            positiontypeslist.Add("Contract to Hire - Corp-to-Corp", "4");
            positiontypeslist.Add("Contract to Hire - Independent", "5");
            positiontypeslist.Add("Contract to Hire - W2", "6");
            positiontypeslist.Add("Full-time", "7");
            positiontypeslist.Add("Part-time", "8");
            return positiontypeslist;
        }


        public static Dictionary<string, Dictionary<string, string>> GetAllStaticData()
        {
            Dictionary<string, Dictionary<string, string>> StaticDataCollection = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> States = new Dictionary<string, string>();
            Dictionary<string, string> Industrys = new Dictionary<string, string>();
            Dictionary<string, string> JobCategories = new Dictionary<string, string>();
            Dictionary<string, string> Positiontypes = new Dictionary<string, string>();
            Dictionary<string, string> ExperienceList = new Dictionary<string, string>();
            Dictionary<string, string> Statuses = new Dictionary<string, string>();
            Dictionary<string, string> Qualification = new Dictionary<string, string>();
            Dictionary<string, string> Visalist = new Dictionary<string, string>();
            Dictionary<string, string> Travel = new Dictionary<string, string>();

            //Adding Data to States List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("state", ExperienceList);

            //Adding Data to Industry List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("industry", ExperienceList);

            //Adding Data to Job Category List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("jobcatg", ExperienceList);

            //Adding Data to Position Types List
            Positiontypes = GetPositionTypeList();
            StaticDataCollection.Add("postype", Positiontypes);

            //Adding Data to Experience List
            ExperienceList = GetExperienceList();
            StaticDataCollection.Add("exp", ExperienceList);

            //Adding Data to Statuses List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("status", ExperienceList);

            //Adding Data to Qualifications List
           // ExperienceList = GetExperienceList();
            StaticDataCollection.Add("qualf", ExperienceList);

            //Adding Data to Visas List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("visa", ExperienceList);

            //Adding Data to Travel List
            //ExperienceList = GetExperienceList();
            StaticDataCollection.Add("travel", ExperienceList);

            return StaticDataCollection;

        }
    }
}
