using Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using UI.Controllers;

namespace UI.Models
{
    
    public class Person
    {
        public string UserName { get; set; }
        public int LuckyNumber { get; set; }
        public XDocument invoicedat { get; set; }
    }



    public class PaymentInvoice
    {
        public string invoicedata { get; set; }
        public string toaccountname { get; set; }
        public string toaccid { get; set; }
        public string createddate { get; set; }
        public string invstatus { get; set; }
        public string invoiceid { get; set; }

        public List<PaymentInvoice> GetAllInvoices(string ownerid, string creatorid)
        {
            Business.DataLayer bd = new Business.DataLayer();

            List<PaymentInvoice> invoices = new List<PaymentInvoice>();
            foreach (Business.AppInvoice appinv in bd.GetAlliInvoices(ownerid, creatorid,"",""))
            {
                PaymentInvoice tempinvoice = new PaymentInvoice();
                tempinvoice.invoiceid = appinv.InvoiceId;
                tempinvoice.invoicedata = appinv.data;
                tempinvoice.createddate = appinv.createddate;
                tempinvoice.invstatus = appinv.status;
                tempinvoice.toaccid = "";
                tempinvoice.toaccountname = "";
                invoices.Add(tempinvoice);
            }

            return invoices;
        }

        public List<PaymentInvoice> GetInboundInvoices(string ownerid, string creatorid, string usertoken)
        {
            Business.DataLayer bd = new Business.DataLayer();

            List<PaymentInvoice> invoices = new List<PaymentInvoice>();
            foreach (Business.AppInvoice appinv in bd.GetAlliInvoices(ownerid, creatorid, "invoice", "inbound"))
            {
                PaymentInvoice tempinvoice = new PaymentInvoice();
                tempinvoice.invoiceid = appinv.InvoiceId;
                tempinvoice.invoicedata = appinv.data;
                tempinvoice.createddate = appinv.createddate;
                tempinvoice.invstatus = appinv.status;
                tempinvoice.toaccid = "";
                tempinvoice.toaccountname = "";
                invoices.Add(tempinvoice);
            }

            return invoices;
        }

        public List<PaymentInvoice> GetOutboundInvoices(string ownerid, string creatorid)
        {
            Business.DataLayer bd = new Business.DataLayer();

            List<PaymentInvoice> invoices = new List<PaymentInvoice>();
            foreach (Business.AppInvoice appinv in bd.GetAlliInvoices(ownerid, creatorid, "invoice", "outbound"))
            {
                PaymentInvoice tempinvoice = new PaymentInvoice();
                tempinvoice.invoiceid = appinv.InvoiceId;
                tempinvoice.invoicedata = appinv.data;
                tempinvoice.createddate = appinv.createddate;
                tempinvoice.invstatus = appinv.status;
                tempinvoice.toaccid = "";
                tempinvoice.toaccountname = "";
                invoices.Add(tempinvoice);
            }

            return invoices;
        }

        public AppTemplate GetInvoiceemplate(string templateid, string ownerid)
        {
            Business.DataLayer bd = new Business.DataLayer();
            Business.AppTemplate invoicetemplate = bd.GetInvoiceTemplate(templateid, ownerid);
            return invoicetemplate;
        }

        public PaymentInvoice GetInvoice(string invoiceid, string ownerid, string creatorid)
        {
            Business.DataLayer bd = new Business.DataLayer();


            Business.AppInvoice appinv = bd.GetInvoice(invoiceid, ownerid, creatorid);
            PaymentInvoice tempinvoice = new PaymentInvoice();
            tempinvoice.invoicedata = appinv.data;
            tempinvoice.createddate = appinv.createddate;
            tempinvoice.invstatus = appinv.status;
            tempinvoice.toaccid = "";
            tempinvoice.toaccountname = "";
            return tempinvoice;
        }
    }
    
    public class CreateInvoice
    {
        public string templateid { get; set; }
        public string userid { get; set; }
        public string companyid { get; set; }
        public string templatedata { get; set; }
        public bool IsCustomerChoosen { get; set; }
        public string customerid { get; set; }


        public CreateInvoice GetInvoice(string companyid)
        {
            CreateInvoice cr = new CreateInvoice();

            cr.companyid = companyid;
            cr.templateid = ProfileModel.GetDefaultTemplate(companyid);
            cr.IsCustomerChoosen = false;
            cr.customerid = "";



            return cr;
        }
        public CreateInvoice GetInvoice(string companyid, string customerid)
        {
            CreateInvoice cr = new CreateInvoice();
            cr.companyid = companyid;
            cr.templateid = ProfileModel.GetDefaultTemplate(companyid);
            cr.IsCustomerChoosen = true;
            cr.customerid = customerid;
            return cr;
        }

    }

    public class MailInvoice
    {
        public string recipients { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string sendorid { get; set; }
        public string premailattachid { get; set; }



        public MailInvoice()
        {
            this.recipients = "";
            this.subject = "";
            this.message = "";
            this.sendorid = "";
            this.premailattachid = "";
        }
    }

    public class ExternalInvoice
    {
        public string invoiceid { get; set; }
        public string invoicemode { get; set; }
        public string receivertype { get; set; }
        public string receiverid { get; set; }
        public string invoiceamount { get; set; }
        public string invoicedate { get; set; }
        public string toaccountid { get; set; }
        public string fromaccountid { get; set; }
        public List<attachment> attachments { get; set; }


        public List<attachment> GetInvoiceAttachments(string invoiceid, string usertoken)
        {
            AmazonFile af = new AmazonFile();

            IList document = af.GetResourceTokens(null, invoiceid, "extinv", usertoken);
            List<attachment> attachments = new List<attachment>();
            foreach (Business.CoreService.ResourceToken li in document)
            {
                attachment tempattachment = new attachment();
                tempattachment.id = li.ContentId;
                tempattachment.path = af.GetFileUrl(li.ContentId, usertoken);
                tempattachment.title = li.Filename;
                tempattachment.type = li.ObjectType;
                attachments.Add(tempattachment);
            }

            return attachments;
        }
    }





}



