using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BanglaKhabarAdmin.NetworkModels
{
    public class ApiRequest
    {
        private string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public Object HttpPostRequest(Object obj, string apiUrl)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_baseUrl + apiUrl);
                var jsonString = JsonConvert.SerializeObject(obj);
                var data = Encoding.ASCII.GetBytes(jsonString);

                request.Method = "POST";
                request.ContentType = "application/json"; // request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var resObj = JsonConvert.DeserializeObject<Object>(responseString);
                return resObj;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
            return null;
        }

    }
}