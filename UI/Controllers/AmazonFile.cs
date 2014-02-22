using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Business.CoreService;
using System.Collections;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class AmazonFile
    {

        IobjectServicesWebappVer2Client objClient = new IobjectServicesWebappVer2Client();

        //Parent Id means - For messages(MessageMasterId) - For Files (UserId (or) FolderId)
        public MyFile WriteFile(string FileName, Stream FileContent, string ParentId, string UserToken)
        {
            
            //Create token using webservices 
            //FileInfo f = new FileInfo(filepath);
            ResourceToken objToken = objClient.GetWriteFile("File", FileName,"", UserToken);

            //Uploading the File to Amazon Url
            UploadFile(FileContent, objToken.Url, objClient.GetMimeType(FileName));

            //Set status in web services
            objClient.SetWriteSuccess(objToken.ContentId, UserToken);

            //Updating content info in webservices
            FileInfo objInfo = new FileInfo(FileName);
            Dictionary<string, string> DicProperties = new Dictionary<string, string>();
            DicProperties.Add("Name", objInfo.Name);
            //*****object type should be set based on file content type....*//
            MyFile file = objClient.AddFile(objToken.ContentId, FileName, ParentId, "pdf", JsonConvert.SerializeObject(DicProperties), UserToken);
            return file;
        }

        //public MyFile WriteVideoFile(string FileName, Stream FileContent, string Parentid, string UserToken)
        //{
        //    Video createdVideo;
        //    try
        //    {
        //        Video newVideo = new Video();


        //        newVideo.Title = FileName;
        //        newVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));
        //        newVideo.Keywords = FileName;
        //        newVideo.Description = "My description";
        //        newVideo.YouTubeEntry.Private = false;
        //        newVideo.Tags.Add(new MediaCategory("mydevtag, anotherdevtag",
        //          YouTubeNameTable.DeveloperTagSchema));

        //        newVideo.YouTubeEntry.Location = new GeoRssWhere(37, -122);
        //        // alternatively, you could just specify a descriptive string
        //        // newVideo.YouTubeEntry.setYouTubeExtension("location", "Mountain View, CA");

        //        newVideo.YouTubeEntry.MediaSource = new MediaFileSource(FileContent, "test file",
        //          "video/quicktime");
        //        createdVideo = CreateRequest().Upload(newVideo);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    ResourceToken objToken = objClient.CreateVideoFile("File", FileName,"", createdVideo.VideoId, UserToken);
        //    objClient.SetWriteSuccess(objToken.ContentId, UserToken);
        //    FileInfo objInfo = new FileInfo(FileName);
        //    Dictionary<string, string> DicProperties = new Dictionary<string, string>();
        //    DicProperties.Add("Name", objInfo.Name);
        //    MyFile file = objClient.AddFile(objToken.ContentId, FileName, Parentid, Enums.contenttype.video.ToString(),JsonConvert.SerializeObject(DicProperties), UserToken);
        //    return file;
        //}

        //Here Response Means Pass the "Page.Response"\
        //chk 
        //public void ReadFile(string RelationId, string UserToken, HttpResponse Response)
        //{
        //    ResourceToken RToken = objClient.GetReadFile(RelationId, UserToken);
        //    DownloadFile(RToken.Url, RToken.Filename, Response);
        //}

        private bool UploadFile(Stream fileStream, string Url,string contentType)
        {
            try
            {
                //Create weClient
                WebClient c = new WebClient();

                //add header
                c.Headers.Add("Content-Type",contentType);
                //Create Streams
                Stream outs = c.OpenWrite(Url, "PUT");
                Stream ins = fileStream;

                CopyStream(ins, outs);

                ins.Close();
                outs.Flush();
                outs.Close();

                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }

        }

        //chk
        ////Here Response Means Pass the "Page.Response"
        //public void DownloadFile(string url, string fileName, HttpResponse Response)
        //{
        //    //Create a stream for the file
        //    Stream stream = null;

        //    //This controls how many bytes to read at a time and send to the client
        //    int bytesToRead = 10000;

        //    // Buffer to read bytes in chunk size specified above
        //    byte[] buffer = new Byte[bytesToRead];

        //    // The number of bytes read
        //    try
        //    {

        //        //Create a WebRequest to get the file
        //        HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

        //        //Create a response for this request
        //        HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

        //        if (fileReq.ContentLength > 0)
        //            fileResp.ContentLength = fileReq.ContentLength;

        //        //Get the Stream returned from the response
        //        stream = fileResp.GetResponseStream();

        //        //Indicate the type of data being sent
        //        Response.ContentType = "application/octet-stream";
        //        Response.BufferOutput = true;
        //        //Name the file 
        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
        //        Response.AddHeader("Content-Length", fileResp.ContentLength.ToString());



        //        int length;
        //        do
        //        {
        //            // Verify that the client is connected.
        //            if (Response.IsClientConnected)
        //            {
        //                // Read data into the buffer.
        //                length = stream.Read(buffer, 0, bytesToRead);

        //                // and write it out to the response's output stream
        //                Response.OutputStream.Write(buffer, 0, length);

        //                // Flush the data
        //                Response.Flush();


        //                //Clear the buffer
        //                buffer = new Byte[bytesToRead];
        //            }
        //            else
        //            {
        //                // cancel the download if client has disconnected
        //                length = -1;
        //            }
        //        } while (length > 0); //Repeat until no data is read

        //        Response.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Clear();
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //        {
        //            //Close the input stream
        //            stream.Close();
        //        }
        //    }

        //}

        //Here Type means - For messages(Attachment) - For Files (File)
        //Parent Id means - For messages(MessageMasterId) - For Files (UserId (or) FolderId)
        public string GetFileUrl(string relId, string UserToken)
        {
            string fileurl = "";

            try
            {
                ResourceToken RToken = objClient.GetReadFile(relId, UserToken);
                fileurl= RToken.Url;
            }
            catch(Exception fileurlnotavailble)
            {
                string exceptionmessage = fileurlnotavailble.Message;
            }
            return fileurl;
        }


        //Parent Id means - For messages(MessageMasterId) - For Files (UserId (or) FolderId)
        public ResourceToken GetFileName(string relId, string UserToken)
        {
            ResourceToken RToken = objClient.GetReadFile(relId, UserToken);
            return RToken;
        }

        public IList GetResourceTokens(string since, string parentId, string objType, string userToken)
        {
            IList ResourceToken = new ArrayList();
            Relation[] arrRel = objClient.GetChildren(since, objType, parentId, userToken);
            foreach (Relation item in arrRel)
            {
                try
                {
                    ResourceToken RToken = objClient.GetReadFile(item.Id, userToken);

                    ResourceToken.Add(RToken);
                }
                catch(Exception photonotfound) {
                    string photonotfoundexceptiodetail = photonotfound.Message;
                }
            }
            return ResourceToken;
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

       
        public IList GetResourceTokensForFolders(string since, string parentId, string objType, string userToken)
        {
            MyFolder[] arrFolder = objClient.GetMyFolders(since, parentId, objType, userToken);
            return arrFolder.ToList();
        }

        //public YouTubeRequest CreateRequest()
        //{
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("example app", "AI39si7jQY5Bd6hNDeXn7DhWp3Fpyf_xs5Q7K2VhZAheGI05_Nis7DDi97NONfMAgyjmJm6cAxQpJ4TzxZaOkne1vzjZ7t1HCQ", "mediendigitalnetworks08@gmail.com", "Test123@1");
        //    return new YouTubeRequest(settings);
        //}

        public ResourceToken WriteFileSimple(string FileName, string UserToken)
        {
            //Create token using webservices 
            //FileInfo f = new FileInfo(filepath);
            ResourceToken objToken = objClient.GetWriteFileSimple("File", FileName,"", UserToken);
            return objToken;

        }

        public bool UpdateStatus(string ContentId, string userToken)
        {
            //Set status in web services
            objClient.SetWriteSuccess(ContentId, userToken);
            return true;
        }

        public MyFile AddFile(string ContentId, string FileName, string parentid,string UserToken)
        {
            //Updating content info in webservices

            Dictionary<string, string> DicProperties = new Dictionary<string, string>();
            DicProperties.Add("Name", FileName);
            MyFile file = objClient.AddFile(ContentId, FileName, parentid, null, JsonConvert.SerializeObject(DicProperties), UserToken);
            return file;

        }
 
    }
}