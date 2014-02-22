using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{

    public class paymentrecord
    {
        string descHeader { get; set; }
        string descSubtitle { get; set; }
        DateTime duedate { get; set; }
        float budgetAmount { get; set; }
        float amount { get; set; }
        paymentmode paymentType { get; set; }
        bool isRecordDeleted { get; set; }
        bool isPaymentMade { get; set; }
        int categoryid { get; set; }

        public paymentrecord()
        {
            this.descHeader = "";
            this.descSubtitle = "";
            this.duedate = new DateTime(1, 1, 1);
            this.budgetAmount = (float)0;
            this.amount = (float)0;
            this.isRecordDeleted = false;
            this.paymentType = paymentmode.empty;
            this.isPaymentMade = false;
        }


    }


    public class paymentrecordset
    {
        string categoryId { get; set; }
        string categoryName { get; set; }
        List<paymentrecord> paymentRecords { get; set; }


        public paymentrecordset()
        {
            this.categoryId = "";
            this.categoryName = "";
            this.paymentRecords = new List<paymentrecord>();
        }

        public List<paymentrecordset> GetPaymentRecords()
        {
            List<paymentrecordset> collection = new List<paymentrecordset>();
            return collection;
        }
    }

    enum paymentmode
    {
        empty,
        check,
        cash,
        card,
        online

    }

    #region Accounts

    public class AccountgroupCollection
    {
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string userid { get; set; }
        public List<PTAccount> accounts { get; set; }

        public AccountgroupCollection()
        {
            this.categoryid = "";
            this.categoryname = "";
            this.userid = "";
            this.accounts = new List<PTAccount>();
        }
    }

    public class AccountCollection
    {
        public string requestedtime { get; set; }
        public List<AccountgroupCollection> accountcollection { get; set; }
    }

    public class PTAccount
    {
        public string accountId { get; set; }
        public string accountName { get; set; }
        public string userid { get; set; }
        public string uniqueid { get; set; }
        public string accounttype { get; set; }
        public string subcatid { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
        public string providerid { get; set; }
        public string providername { get; set; }
        public float amount { get; set; }
        public int duemonth { get; set; }
        public int duedate { get; set; }
        public string recurrence { get; set; }
        public string createddate { get; set; }
        public string modifieddate { get; set; }
        public string codes { get; set; }
        public bool deleted { get; set; }
        public bool global { get; set; }
        public bool initial { get; set; }


        public PTAccount()
        {
            this.accountId = "";
            this.accountName = "";
            this.userid = "";
            this.uniqueid = "";
            this.accounttype = "";
            this.subcatid = "";
            this.categoryid = "";
            this.categoryname = "";
            this.description = "";
            this.providerid = "";
            this.providername = "";
            this.amount = (float)0;
            this.duemonth = 0;
            this.duedate = 0;
            this.recurrence = "";
            this.createddate = "";
            this.modifieddate = "";
            this.codes = "0";
            this.deleted = false;
            this.global = true;
            this.initial = true;

        }



        public PTAccount CreateAccount(string accountname, string userid, string accounttype, string subcatid, string description, string providerid, string providername, float amount, string duedate, string recurrence)
        {
            PTAccount CreatedAccount = new PTAccount();

            return CreatedAccount;

        }

        public AccountCollection GetUserAccounts(string usertoken)
        {

            Business.ApplicationService.AppServiceClient appclient = new Business.ApplicationService.AppServiceClient();
            Business.ApplicationService.accounts[] appaccounts = appclient.Getaccounts(usertoken);

            AccountCollection AccountCollection = new Models.AccountCollection();


            List<AccountgroupCollection> accountgroupcollection = new List<AccountgroupCollection>();


            foreach (Business.ApplicationService.accounts item in appaccounts.GroupBy(p => p.categoryid).Select(g => g.First()))
            {
                AccountgroupCollection temgroup = new AccountgroupCollection();

                temgroup.categoryid = item.categoryid;
                temgroup.categoryname = item.categoryname;
                temgroup.userid = item.userid;
                List<Business.ApplicationService.accounts> resultaccounts = appaccounts.Where(p => p.categoryid == item.categoryid).ToList();
                foreach (Business.ApplicationService.accounts subitem in resultaccounts)
                {

                    PTAccount tempaccount = new PTAccount();
                    tempaccount.accountId = subitem.accountid;
                    tempaccount.accountName = subitem.accountname;
                    tempaccount.accounttype = subitem.accounttype;
                    tempaccount.amount = (float)Convert.ToDouble(subitem.amount);
                    tempaccount.uniqueid = subitem.unid;
                    tempaccount.categoryid = subitem.categoryid;
                    tempaccount.categoryname = subitem.categoryname;
                    tempaccount.createddate = subitem.createddate;
                    tempaccount.deleted = false;
                    tempaccount.description = subitem.description;
                    tempaccount.duemonth = subitem.duemonth;
                    tempaccount.duedate = subitem.duedate;
                    tempaccount.modifieddate = subitem.modifieddate;
                    tempaccount.providerid = subitem.providerid;
                    tempaccount.providername = subitem.providername;
                    tempaccount.recurrence = subitem.recurrence;
                    tempaccount.subcatid = subitem.subcategoryid;
                    tempaccount.userid = subitem.userid;
                    tempaccount.global = subitem.global;
                    tempaccount.initial = subitem.initial;
                    tempaccount.codes = subitem.codes;
                    temgroup.accounts.Add(tempaccount);

                }
                accountgroupcollection.Add(temgroup);
            }

            AccountCollection.requestedtime = DateTime.Now.ToString();
            AccountCollection.accountcollection = accountgroupcollection;
            return AccountCollection;
        }

    }

    #endregion

    #region Transactions
    public class TransactionItem
    {
        public string transactionid { get; set; }
        public string accountId { get; set; }
        public string accountName { get; set; }
        public string userid { get; set; }
        public string accounttype { get; set; }
        public string subcatid { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string toaccountid { get; set; }
        public string fromaccountid { get; set; }
        public string paymentmode { get; set; }
        public string description { get; set; }
        public string providerid { get; set; }
        public string providername { get; set; }
        public float budgetamount { get; set; }
        public float paidamount { get; set; }
        public int duemonth { get; set; }
        public int duedate { get; set; }
        public int paiddate { get; set; }
        public int paidmonth { get; set; }
        public int paidyear { get; set; }
        public string invoiceid { get; set; }
        public bool ispaid { get; set; }
        public string recurrence { get; set; }
        public string transactiondate { get; set; }
        public string createddate { get; set; }
        public string modifieddate { get; set; }
        public bool deleted { get; set; }

        public TransactionItem()
        {
            this.accountId = "";
            this.accountName = "";
            this.userid = "";
            this.accounttype = "";
            this.subcatid = "";
            this.categoryid = "";
            this.categoryname = "";
            this.toaccountid = "";
            this.fromaccountid = "";
            this.paymentmode = "";
            this.description = "";
            this.providerid = "";
            this.providername = "";
            this.budgetamount = (float)0;
            this.paidamount = (float)0;
            this.duemonth = 0;
            this.duedate = 0;
            this.paiddate = 0;
            this.paidmonth = 0;
            this.paidyear = 0;
            this.invoiceid = "";
            this.ispaid = false;
            this.recurrence = "";
            this.createddate = "";
            this.modifieddate = "";
            this.deleted = false;
        }
         Business.ApplicationService.AppServiceClient client = new Business.ApplicationService.AppServiceClient();
        public Business.ApplicationService.transactions[] getAllTransactionItems(string usertoken)
        {
            return client.GetAllTransactions(usertoken);
        }

        public TransactionCollection getTransactions(string usertoken)
        {
            TransactionCollection TransactionCollection = new TransactionCollection();
            
            Business.ApplicationService.transactions[] alltransactionscollection = getAllTransactionItems(usertoken);
            AccountCollection allaccounts = new PTAccount().GetUserAccounts(usertoken);
            int i = 1;
            foreach (Business.ApplicationService.transactions item in alltransactionscollection.GroupBy(p => p.categoryid).Select(g => g.First()))
            {
                AccountgroupCollection accgroup = allaccounts.accountcollection.Where(m => m.categoryid == item.categoryid).First();
                
                Transactiongroupcollection temptransactiongroupcollection = new Transactiongroupcollection();

               

                List<Business.ApplicationService.transactions> resulttransactions = alltransactionscollection.Where(p => p.categoryid == item.categoryid).ToList();
                foreach (Business.ApplicationService.transactions subitem in resulttransactions)
                {
                    try
                    {
                        PTAccount acc = accgroup.accounts.Where(n => n.accountId == subitem.toaccountid).First();
                        TransactionItem temptransactionItem = new TransactionItem();
                        temptransactionItem.transactionid = subitem.transactionid;
                        temptransactionItem.accountId = subitem.toaccountid;
                        temptransactionItem.accountName = acc.accountName;
                        temptransactionItem.accounttype = acc.accounttype;
                        temptransactionItem.budgetamount = (float)Convert.ToDouble(subitem.budget);
                        temptransactionItem.paidamount = (float)Convert.ToDouble(subitem.paidamount);
                        temptransactionItem.categoryid = subitem.categoryid;
                        temptransactionItem.categoryname = acc.categoryname;
                        temptransactionItem.toaccountid = subitem.toaccountid;
                        temptransactionItem.fromaccountid = subitem.fromaccountid;
                        temptransactionItem.paymentmode = subitem.paymentmode;
                        temptransactionItem.createddate = subitem.createddate;
                        temptransactionItem.deleted = false;
                        temptransactionItem.description = subitem.description;
                        temptransactionItem.duemonth = subitem.duemonth;
                        temptransactionItem.duedate = subitem.duedate;
                        temptransactionItem.paiddate = 0;// subitem.paiddate;
                        temptransactionItem.paidmonth = subitem.paidmonth;
                        temptransactionItem.paidyear = subitem.paidyear;
                        temptransactionItem.modifieddate = subitem.modifieddate;
                        temptransactionItem.providerid = acc.providerid;
                        temptransactionItem.providername = acc.providername;
                        temptransactionItem.recurrence = subitem.recurrence;
                        temptransactionItem.subcatid = subitem.subcategoryid;
                        temptransactionItem.userid = subitem.userid;
                        temptransactionItem.transactiondate = subitem.transactiondate;

                        temptransactiongroupcollection.groupitems.Add(temptransactionItem);
                    }
                    catch(Exception e)
                    {
                        string message = e.Message;
                    }
                    
                }

                if (temptransactiongroupcollection.groupitems.Count > 0)
                {
                    temptransactiongroupcollection.categoryid = item.categoryid;
                    temptransactiongroupcollection.categoryname = accgroup.categoryname;
                    TransactionCollection.groupcollection.Add(temptransactiongroupcollection);
                }
                else
                {
 
                }
                
            }
            return TransactionCollection;
        }

        public TransactionCollection getPayments(string usertoken)
        {
            TransactionCollection TransactionCollection = new TransactionCollection();

            Business.ApplicationService.transactions[] alltransactionscollection = getAllTransactionItems(usertoken);
            AccountCollection allaccounts = new PTAccount().GetUserAccounts(usertoken);
            int i = 1;
            foreach (Business.ApplicationService.transactions item in alltransactionscollection.GroupBy(p => p.categoryid).Select(g => g.First()))
            {
                AccountgroupCollection accgroup = allaccounts.accountcollection.Where(m => m.categoryid == item.categoryid).First();

                Transactiongroupcollection temptransactiongroupcollection = new Transactiongroupcollection();



                List<Business.ApplicationService.transactions> resulttransactions = alltransactionscollection.Where(p => p.categoryid == item.categoryid).ToList();
                foreach (Business.ApplicationService.transactions subitem in resulttransactions)
                {
                    try
                    {
                        if(!subitem.ispaid)
                        {
                        PTAccount acc = accgroup.accounts.Where(n => n.accountId == subitem.toaccountid).First();
                        TransactionItem temptransactionItem = new TransactionItem();
                        temptransactionItem.transactionid = subitem.transactionid;
                        temptransactionItem.accountId = subitem.toaccountid;
                        temptransactionItem.accountName = acc.accountName;
                        temptransactionItem.accounttype = acc.accounttype;
                        temptransactionItem.budgetamount = (float)Convert.ToDouble(subitem.budget);
                        temptransactionItem.paidamount = (float)Convert.ToDouble(subitem.paidamount);
                        temptransactionItem.categoryid = subitem.categoryid;
                        temptransactionItem.categoryname = acc.categoryname;
                        temptransactionItem.toaccountid = subitem.toaccountid;
                        temptransactionItem.fromaccountid = subitem.fromaccountid;
                        temptransactionItem.paymentmode = subitem.paymentmode;
                        temptransactionItem.createddate = subitem.createddate;
                        temptransactionItem.deleted = false;
                        temptransactionItem.description = subitem.description;
                        temptransactionItem.duemonth = subitem.duemonth;
                        temptransactionItem.duedate = subitem.duedate;
                        temptransactionItem.paiddate = 0;// subitem.paiddate;
                        temptransactionItem.paidmonth = subitem.paidmonth;
                        temptransactionItem.paidyear = subitem.paidyear;
                        temptransactionItem.modifieddate = subitem.modifieddate;
                        temptransactionItem.providerid = acc.providerid;
                        temptransactionItem.providername = acc.providername;
                        temptransactionItem.recurrence = subitem.recurrence;
                        temptransactionItem.subcatid = subitem.subcategoryid;
                        temptransactionItem.userid = subitem.userid;
                        temptransactionItem.transactiondate = subitem.transactiondate;

                        temptransactiongroupcollection.groupitems.Add(temptransactionItem);
                    }
                        else{}
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }

                }

                if (temptransactiongroupcollection.groupitems.Count > 0)
                {
                    temptransactiongroupcollection.categoryid = item.categoryid;
                    temptransactiongroupcollection.categoryname = accgroup.categoryname;
                    TransactionCollection.groupcollection.Add(temptransactiongroupcollection);
                }
                else
                {

                }

            }
            return TransactionCollection;
        }

        public TransactionCollection getPaymentHistory(string usertoken)
        {
            TransactionCollection TransactionCollection = new TransactionCollection();

            Business.ApplicationService.transactions[] alltransactionscollection = getAllTransactionItems(usertoken);
            AccountCollection allaccounts = new PTAccount().GetUserAccounts(usertoken);
            int i = 1;
            foreach (Business.ApplicationService.transactions item in alltransactionscollection.GroupBy(p => p.categoryid).Select(g => g.First()))
            {
                AccountgroupCollection accgroup = allaccounts.accountcollection.Where(m => m.categoryid == item.categoryid).First();

                Transactiongroupcollection temptransactiongroupcollection = new Transactiongroupcollection();



                List<Business.ApplicationService.transactions> resulttransactions = alltransactionscollection.Where(p => p.categoryid == item.categoryid).ToList();
                foreach (Business.ApplicationService.transactions subitem in resulttransactions)
                {
                    try
                    {
                        if (subitem.ispaid)
                        {
                            PTAccount acc = accgroup.accounts.Where(n => n.accountId == subitem.toaccountid).First();
                            TransactionItem temptransactionItem = new TransactionItem();
                            temptransactionItem.transactionid = subitem.transactionid;
                            temptransactionItem.accountId = subitem.toaccountid;
                            temptransactionItem.accountName = acc.accountName;
                            temptransactionItem.accounttype = acc.accounttype;
                            temptransactionItem.budgetamount = (float)Convert.ToDouble(subitem.budget);
                            temptransactionItem.paidamount = (float)Convert.ToDouble(subitem.paidamount);
                            temptransactionItem.categoryid = subitem.categoryid;
                            temptransactionItem.categoryname = acc.categoryname;
                            temptransactionItem.toaccountid = subitem.toaccountid;
                            temptransactionItem.fromaccountid = subitem.fromaccountid;
                            temptransactionItem.paymentmode = subitem.paymentmode;
                            temptransactionItem.createddate = subitem.createddate;
                            temptransactionItem.deleted = false;
                            temptransactionItem.description = subitem.description;
                            temptransactionItem.duemonth = subitem.duemonth;
                            temptransactionItem.duedate = subitem.duedate;
                            temptransactionItem.paiddate = 0;// subitem.paiddate;
                            temptransactionItem.paidmonth = subitem.paidmonth;
                            temptransactionItem.paidyear = subitem.paidyear;
                            temptransactionItem.modifieddate = subitem.modifieddate;
                            temptransactionItem.providerid = acc.providerid;
                            temptransactionItem.providername = acc.providername;
                            temptransactionItem.recurrence = subitem.recurrence;
                            temptransactionItem.subcatid = subitem.subcategoryid;
                            temptransactionItem.userid = subitem.userid;
                            temptransactionItem.transactiondate = subitem.transactiondate;

                            temptransactiongroupcollection.groupitems.Add(temptransactionItem);
                        }
                        else { }
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }

                }

                if (temptransactiongroupcollection.groupitems.Count > 0)
                {
                    temptransactiongroupcollection.categoryid = item.categoryid;
                    temptransactiongroupcollection.categoryname = accgroup.categoryname;
                    TransactionCollection.groupcollection.Add(temptransactiongroupcollection);
                }
                else
                {

                }

            }
            return TransactionCollection;
        }
    }


    public class Transactiongroupcollection
    {
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public List<TransactionItem> groupitems { get; set; }

        public Transactiongroupcollection()
        {
            this.categoryid = "";
            this.categoryname = "";
            this.groupitems = new List<TransactionItem>();
        }
    }

    public class TransactionCollection
    {
        public string requesttime { get; set; }
        public List<Transactiongroupcollection> groupcollection { get; set; }

        public TransactionCollection()
        {
            this.requesttime = DateTime.Now.ToString();
            this.groupcollection = new List<Transactiongroupcollection>();

        }
    }


    #endregion
}