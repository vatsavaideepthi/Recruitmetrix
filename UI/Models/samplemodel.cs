using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.testmodel
{
    public class timesheet
    {
        public string id { get; set; }
        public string title { get; set; }
        public string createdon { get; set; }
        public string ownerid { get; set; }
        public string ownername { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string data { get; set; }
        
        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("self")]
        public Self Self { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("items")]
        public Items Items { get; set; }
    }

    public class Client
    {

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class Company
    {

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class Item
    {

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("workhours")]
        public string Workhours { get; set; }

        [JsonProperty("overtime")]
        public string Overtime { get; set; }

        [JsonProperty("tasks")]
        public Tasks Tasks { get; set; }
    }

    public class Items
    {

        [JsonProperty("item")]
        public Item[] Item { get; set; }
    }

    public class Metadata
    {

        [JsonProperty("totalhours")]
        public string Totalhours { get; set; }

        [JsonProperty("overtimehours")]
        public string Overtimehours { get; set; }
    }

    public class Self
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Task
    {

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("hours")]
        public string Hours { get; set; }
    }

    public class Tasks
    {

        [JsonProperty("task")]
        public Task[] Task { get; set; }
    }
}