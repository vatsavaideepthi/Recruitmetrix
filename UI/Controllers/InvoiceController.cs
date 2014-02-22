using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using System.Collections;
using RazorPDF;
using System.Net.Mail;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.xml;
using System.Xml;
using iTextSharp.text.html;


namespace UI.Controllers
{

    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/



        [HttpGet]
        public ActionResult Index()
        {
            return View(new PaymentInvoice().GetAllInvoices(Session["companyid"].ToString(), Session["userid"].ToString()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            string customerid = "";
            if (!String.IsNullOrEmpty(Request.QueryString["cid"]))
                customerid = Request.QueryString["cid"].ToString();

            CreateInvoice crinv = new CreateInvoice();
            if (customerid != "")
            {
                crinv = new CreateInvoice().GetInvoice(Session["companyid"].ToString(), customerid);
                crinv.templatedata = GetInvoiceTemplate(crinv.templateid);
                return View(crinv);
            }
            else
            {
                crinv = new CreateInvoice().GetInvoice(Session["companyid"].ToString());
                crinv.templatedata = GetInvoiceTemplate(crinv.templateid);
                return View(crinv);
            }


        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(ExternalInvoice einv, HttpPostedFileBase file)
        {

            Business.ApplicationService.AppServiceClient bd = new Business.ApplicationService.AppServiceClient();
            Business.CoreService.MyFile fileuploaded = new Business.CoreService.MyFile();
            Business.CoreService.IobjectServicesWebappVer2Client coreclient = new Business.CoreService.IobjectServicesWebappVer2Client();
            AmazonFile af = new AmazonFile();
            fileuploaded = af.WriteFile(Request.Files[0].FileName, Request.Files[0].InputStream, Session["companyid"].ToString(), Session["usertoken"].ToString());
            coreclient.AddRelation(einv.invoiceid, fileuploaded.Id, "extinv", Session["usertoken"].ToString());

            IList document = af.GetResourceTokens(null, einv.invoiceid, "extinv", Session["usertoken"].ToString());
           string fileurl = "";
           foreach (Business.CoreService.ResourceToken li in document)
           {


               fileurl = af.GetFileUrl(li.ContentId, Session["usertoken"].ToString());
               
           }

            return RedirectToAction("inbound");
        }

        public ActionResult Inbound()
        {
            return View(new PaymentInvoice().GetInboundInvoices(Session["companyid"].ToString(), Session["userid"].ToString(),Session["usertoken"].ToString()));
        }

        public ActionResult Outbound()
        {
            return View(new PaymentInvoice().GetOutboundInvoices(Session["companyid"].ToString(), Session["userid"].ToString()));
        }


        public ActionResult View(string invoiceid)
        {
            Session.Add("invoiceid", Request.QueryString["invid"].ToString());
            return View();
        }

        public ActionResult Edit(string invoiceid, string templateid)
        {

            return View();
        }

        public ActionResult Send(MailInvoice minvoice)
        {

            PaymentInvoice tempinvoice = new PaymentInvoice().GetInvoice(Session["invoiceid"].ToString(), Session["companyid"].ToString(), Session["userid"].ToString());
            string data = tempinvoice.invoicedata;
            XDocument invoicedoc = new XDocument();
            try
            {
                invoicedoc = XDocument.Parse(data);
            }
            catch
            {

            }


            Business.DataLayer bd = new Business.DataLayer();
            bd.UpdateInvoice(Session["invoiceid"].ToString(), Session["userid"].ToString(), Session["companyid"].ToString(), "", Session["companyid"].ToString(), "invoice", "outbound", 5, invoicedoc.ToString());


            string resultstring = "";
            ViewData.Model = tempinvoice;
            ViewContext viewContext;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "download");
                viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                resultstring = sw.GetStringBuilder().ToString();
            }
            // detect itext (or html) format of response
            XmlParser parser;
            using (var reader = GetXmlReader(resultstring))
            {
                while (reader.Read() && reader.NodeType != XmlNodeType.Element)
                {
                    // no-op
                }

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "itext")
                    parser = new XmlParser();
                else
                    parser = new HtmlParser();
            }

            // Create a document processing context
            var document = new Document();
            document.Open();

            // associate output with response stream
            string fname = Guid.NewGuid().ToString().Replace("-", "");
            //var pdfWriter = PdfWriter.GetInstance(document, viewContext.HttpContext.Response.OutputStream);
            FileStream fs = new FileStream(Server.MapPath("/invoicebox/") + fname + ".pdf", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            var pdfWriter = PdfWriter.GetInstance(document, fs);
            pdfWriter.CloseStream = false;

            // this is as close as we can get to being "success" before writing output
            // so set the content type now
            viewContext.HttpContext.Response.ContentType = "application/pdf";

            // parse memory through document into output
            using (var reader = GetXmlReader(resultstring))
            {
                parser.Go(document, reader);
            }


            pdfWriter.Close();
            fs.Dispose();
            document.Close();





            MailMessage mail = new MailMessage();
            mail.To.Add(minvoice.recipients);
            mail.To.Add("bvlbhargav@gmail.com");
            mail.From = new MailAddress("acounts@palniinc.com");
            mail.Subject = minvoice.subject;

            //string url = "http://192.168.1.252/mexservices/sharingservices.svc/User/Confirm?email=" + HttpUtility.UrlEncode(email) + "&code=" + confirmationcode;
            string Body = minvoice.message;

            mail.Body = Body;

            mail.IsBodyHtml = true;


            mail.Attachments.Add(new Attachment(Server.MapPath("/invoicebox/" + fname + ".pdf")));


            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["smtphost"].ToString(); //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpusername"].ToString(), ConfigurationManager.AppSettings["smtppassword"].ToString());
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return Redirect(Request.UrlReferrer.ToString());
        }



        public ActionResult download()
        {

            PaymentInvoice tempinvoice = new PaymentInvoice().GetInvoice(Request.QueryString["tid"].ToString(), Session["companyid"].ToString(), Session["userid"].ToString());
            string data = tempinvoice.invoicedata;
            XDocument invoicedoc = new XDocument();
            try
            {
                invoicedoc = XDocument.Parse(data);
            }
            catch
            {

            }





            var pdf = new PdfResult(tempinvoice, "download");

            string filename = "attachment; filename=" + invoicedoc.Descendants("client").First().Descendants("compname").First().Value.ToString() + "-" + invoicedoc.Descendants("metadata").First().Descendants("id").First().Value.ToString() + ".pdf";

            pdf.ViewBag.Title = "Invoice";
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", filename);




            return pdf;

            // return View();
        }


        public ActionResult SaveAsPdf()
        {
            PaymentInvoice tempinvoice = new PaymentInvoice().GetInvoice(Request.QueryString["tid"].ToString(), Session["companyid"].ToString(), Session["userid"].ToString());
            string data = tempinvoice.invoicedata;
            XDocument invoicedoc = new XDocument();
            try
            {
                invoicedoc = XDocument.Parse(data);
            }
            catch
            {

            }
            string resultstring = "";
            ViewData.Model = tempinvoice;
            ViewContext viewContext;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "download");
                viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                resultstring = sw.GetStringBuilder().ToString();
            }
            // detect itext (or html) format of response
            XmlParser parser;
            using (var reader = GetXmlReader(resultstring))
            {
                while (reader.Read() && reader.NodeType != XmlNodeType.Element)
                {
                    // no-op
                }

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "itext")
                    parser = new XmlParser();
                else
                    parser = new HtmlParser();
            }

            // Create a document processing context
            var document = new Document();
            document.Open();

            // associate output with response stream
            //var pdfWriter = PdfWriter.GetInstance(document, viewContext.HttpContext.Response.OutputStream);
            FileStream fs = new FileStream(Server.MapPath("/invoicebox/") + Guid.NewGuid().ToString().Replace("-", "") + ".pdf", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            var pdfWriter = PdfWriter.GetInstance(document, fs);
            pdfWriter.CloseStream = false;

            // this is as close as we can get to being "success" before writing output
            // so set the content type now
            viewContext.HttpContext.Response.ContentType = "application/pdf";

            // parse memory through document into output
            using (var reader = GetXmlReader(resultstring))
            {
                parser.Go(document, reader);
            }


            pdfWriter.Close();
            fs.Dispose();
            document.Close();



            return Redirect(Request.UrlReferrer.ToString());
            //var pdfdata = new PdfResultnew(tempinvoice, "download");
            //Response.ClearContent();         
            //return pdfdata;
        }

        private static XmlTextReader GetXmlReader(string source)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(source);
            MemoryStream stream = new MemoryStream(byteArray);
            var xtr = new XmlTextReader(stream);
            xtr.WhitespaceHandling = WhitespaceHandling.None; // Helps iTextSharp parse 
            return xtr;
        }

        public string GetInvoice(string invid)
        {
            PaymentInvoice tempinvoice = new PaymentInvoice().GetInvoice(invid, Session["companyid"].ToString(), Session["userid"].ToString());
            string data = tempinvoice.invoicedata;
            return data;
        }

        public string GetInvoiceTemplate(string tempid)
        {
            Business.DataLayer bd = new Business.DataLayer();
            Business.AppTemplate invoicetemplate = bd.GetInvoiceTemplate(tempid, Session["companyid"].ToString());
            if (invoicetemplate.data == "" || invoicetemplate.data == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div><p>");
                sb.Append("<a href=\"/invoice\">Click to View All Invoices</a>");
                sb.Append("</p></div>");

                invoicetemplate.data = sb.ToString();
            }
            return invoicetemplate.data;
        }
       
    }

  




}



