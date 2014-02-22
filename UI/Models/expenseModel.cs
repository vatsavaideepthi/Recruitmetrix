using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
   

    public class expense
    {
        public string id { get; set; }
        public string title { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string subcategoryid { get; set; }
        public string subcategoryname { get; set; }
        public string amount { get; set; }
        public string spenton { get; set; }
        public string payabletoaccount { get; set; }
        public string paidbyaccount { get; set; }
        public string paymentmode { get; set; }
        public string isrecurred { get; set; }
        public string recurrence { get; set; }
        public string description { get; set; }

    }
}