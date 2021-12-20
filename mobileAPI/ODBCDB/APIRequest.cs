using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace mobileAPI.ODBCDB
{
    public class APIRequest
    {
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; }

        private static void AddAuthorization(HttpWebRequest request)
        {
            string username = "4d6b54317fcd078b6c2c658a57033b11";
            string password = "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";
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            request.Headers.Add("Authorization", "Basic " + encoded);
            //request.Headers.Add("access_token", "tF@#2@p121)^15T");
        }

        private static HttpWebRequest CreateWebRequest(string method, string url)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentLength = 0;
            request.Method = method;
            request.Timeout = 30000;
            AddAuthorization(request);

            return request;
        }

        private static void WriteBodyFormUrlEncoded(HttpWebRequest request, string body)
        {
            request.ContentType = "application/x-www-form-urlencoded";
            WriteBody(request, body);
        }

        private static void WriteBodyRowJson(HttpWebRequest request, string body)
        {
            request.ContentType = "application/json;charset=utf-8";
            WriteBody(request, body);
        }

        private static void WriteBody(HttpWebRequest request, string body)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(body);
            request.ContentLength = byteArray.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
            }
        }

        private static string GetWebResponse(HttpWebRequest request)
        {
            using (WebResponse httpResponse = request.GetResponse())
            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }

        }

        private static string GetParams(Dictionary<string, string> paramss)
        {
            List<String> keys = new List<String>(paramss.Keys);
            keys.Sort();
            String str = "";

            for (int i = 0; i < keys.Count; i++)
            {
                String key = keys[i];
                String value = paramss[key];
                str += key + '=' + value;

                if (i != keys.Count - 1) { str += '&'; }
            }
            return str;
        }

        public static string GetRequest(string url)
        {
            string result = "";

            try
            {
                HttpWebRequest request = CreateWebRequest("GET", url);
                result = GetWebResponse(request);
            }
            catch (Exception e)
            {
                //Console.WriteLine("[Error]GetRequest");
                //Console.WriteLine(e.Message);
                throw e;
            }

            return result;
        }

        public static string PostRequest(string url, string paramss)
        {
            var result = "";
            try
            {
                HttpWebRequest request = CreateWebRequest("POST", url + "?" + paramss);

                result = GetWebResponse(request);

            }
            catch (Exception e)
            {
                //Console.WriteLine("[Error]PostRequest");
                //Console.WriteLine(e.Message);
                throw e;
            }

            return result;
        }

        public static string PostRequest(string url, Dictionary<string, string> paramss = null)
        {
            var result = "";
            try
            {
                string paramsss = (paramss == null) ? "" : "?" + GetParams(paramss);
                HttpWebRequest request = CreateWebRequest("POST", url + paramsss);
                result = GetWebResponse(request);
            }
            catch (Exception e)
            {
                //Console.WriteLine("[Error]PostRequest");
                //Console.WriteLine(e.Message);
                throw e;
            }

            return result;
        }

        private static string PostRequestFromBody(string url, Dictionary<string, string> paramss, Dictionary<string, string> bodyFormUrlEncoded, string bodyRowJson)
        {
            var result = "";
            try
            {
                string paramsss = (paramss == null) ? "" : "?" + GetParams(paramss);
                HttpWebRequest request = CreateWebRequest("POST", url + paramsss);
                if (bodyFormUrlEncoded != null) { WriteBodyFormUrlEncoded(request, GetParams(bodyFormUrlEncoded)); }
                if (bodyRowJson != "") { WriteBodyRowJson(request, bodyRowJson); }
                result = GetWebResponse(request);
            }
            catch (Exception e)
            {
                //Console.WriteLine("[Error]PostRequest");
                //Console.WriteLine(e.Message);
                throw e;
            }

            return result;
        }

        public static string PostRequestFromBody(string url, string bodyRowJson)
        {
            return PostRequestFromBody(url, null, null, bodyRowJson);
        }

        public static string PostRequestFromBody(string url, Dictionary<string, string> bodyFormUrlEncoded)
        {
            return PostRequestFromBody(url, null, bodyFormUrlEncoded, "");
        }

        public static string PostRequestFromBody(string url, Dictionary<string, string> paramss, string bodyRowJson)
        {
            return PostRequestFromBody(url, paramss, null, bodyRowJson);
        }

        public static string PostRequestFromBody(string url, Dictionary<string, string> paramss, Dictionary<string, string> bodyFormUrlEncoded)
        {
            return PostRequestFromBody(url, paramss, bodyFormUrlEncoded, "");
        }
    }
}