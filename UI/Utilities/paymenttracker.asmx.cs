using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace UI.Utilities
{
    /// <summary>
    /// Summary description for paymenttracker
    /// </summary>
    [WebService(Namespace = "http://tracker.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class paymenttracker : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string getproviders(string unid,string tag)
        {
            Business.DataLayer bdata = new Business.DataLayer();
            List<Business.PTServiceProvider> providercollection =  bdata.GetAllProviders(unid);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(providercollection.Where(m=>m.name.StartsWith(tag)).ToList());// serializer.Serialize(cities);
            return output;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetCities(string tag)
        {


            DataSet ds = new DataSet();

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cardleymerchantnewConnectionString"].ConnectionString.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand("getcities", conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("tag", tag);
            adapter.Fill(ds);
            List<cardleycity> cities = new List<cardleycity>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cardleycity city = new cardleycity();
                city.id = dr["cityid"].ToString();
                city.name = dr["name"].ToString();
                city.statecode = dr["statecode"].ToString();
                city.code = dr["citycode"].ToString();
                cities.Add(city);
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(cities);// serializer.Serialize(cities);
            return output;
        }
    }

    public class cardleycity
    {
        public string name { get; set; }
        public string id { get; set; }
        public string statecode { get; set; }
        public string code { get; set; }
    }
}
