using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DentalClinicAPP
{
    public class MyWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public MyWebRequest(string url)
        {
            // Create a request using a URL that can receive a post.

            request = WebRequest.Create(url);
        }

        public MyWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public MyWebRequest(string url, string method, string data)
            : this(url, method)
        {

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            //    request.ContentLength = byteArray.Length;

            // Get the request stream.

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }

        public MyWebRequest(string url, string method, string data, string Token_Type, string Access_Token)
          : this(url, method)
        {

            // Create POST data and convert it to a byte array.

            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            // request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", Token_Type + " " + Access_Token);
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }


        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();


            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
        //public ApiClient(string url)
        //{
        //    // Initialize the request
        //    request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "GET"; // أو "POST" حسب متطلبات API
        //                            // إعدادات إضافية للطلب إذا لزم الأمر
        //}
        //public dynamic GetLogin()
        //{
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    //ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
        //    //{
        //    //    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
        //    //};
        //    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        //    try
        //    {
        //        using (WebResponse response = request.GetResponse())
        //        {
        //            this.Status = ((HttpWebResponse)response).StatusDescription;

        //            using (Stream dataStream = response.GetResponseStream())
        //            using (StreamReader reader = new StreamReader(dataStream))
        //            {
        //                string responseFromServer = reader.ReadToEnd();
        //                dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
        //                return obj;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        throw;
        //    }
        //}
        public async Task<dynamic> GetLoginAsync()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            string url = "https://id.eta.gov.eg/connect/token";
            string method = "POST"; // 
            string data = "grant_type=client_credentials&Client_id=44627186-a9af-444c-8666-e87d0590584e&Client_secret=d8fc20eb-a5b2-4ec4-b13e-37930cd1b567";
            // Get the original response.
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(data);
                await streamWriter.FlushAsync();
            }

            using (WebResponse response = await request.GetResponseAsync())
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream))
            {
                string responseFromServer = await reader.ReadToEndAsync();
                dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
                return obj;
            }


            // LoginReturnObj = JsonConvert.DeserializeObject( responseFromServer);

            // Clean up the streams.
            //reader.Close();
            //dataStream.Close();
            //response.Close();

            //return obj;
        }
    }
}
