using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.ApiModels
{
    public class ApiRequest
    {
        private string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private string _dbName = ConfigurationManager.AppSettings["DbName"];
        private string _dbUserName = ConfigurationManager.AppSettings["DbUserName"];
        private string _dbUserPass = ConfigurationManager.AppSettings["DbUserPass"];

        public Object HttpPostRequestDotNet(string query, string apiUrl) 
        {
            try  
            {
                RequestModels requestModels = new RequestModels();

                requestModels.Data = query;
                requestModels.User = _dbUserName;
                requestModels.Pass = _dbUserPass;
                requestModels.Db = _dbName;

                var request = (HttpWebRequest)WebRequest.Create(_baseUrl + apiUrl);
                //var jsonString = JsonConvert.SerializeObject(requestModels);
                string jsonString = "Data="+ query+ "&User="+_dbUserName+ "&Pass=" + _dbUserPass+ "&Db=" + _dbName;
                // var data = Encoding.ASCII.GetBytes(jsonString);
                var data = Encoding.UTF8.GetBytes(jsonString);
                request.Method = "POST";
                //request.ContentType = "application/json"; // request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/x-www-form-urlencoded";

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

        public string HttpPostRequestPhp(string query, string apiUrl)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["Data"] = query;
                    values["User"] = _dbUserName;
                    values["Pass"] = _dbUserPass;
                    values["Db"] = _dbName;

                    var response = client.UploadValues(_baseUrl + apiUrl, values);

                    //var responseString = Encoding.Default.GetString(response);
                    var responseString = Encoding.UTF8.GetString(response);
                    //var responseString = Encoding.Default.GetString(response).Replace("[[","[").Replace("]]","]");
                    //var resObj = JsonConvert.DeserializeObject<Object>(responseString);
                    //return resObj;
                    return responseString;
                }
            }
            catch(Exception ex)
            {

                throw;
            }
            
            return "N";
        }

    }
}