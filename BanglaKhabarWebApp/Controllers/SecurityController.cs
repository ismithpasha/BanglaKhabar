using ApiManager.ApiModels;
using ApiManager.Models;
using ApiManager.Models.BasicModels;
using ApiManager.Repository;
using ApiManager.Security;
using BanglaKhabarWebApp.Models;
using BanglaKhabarWebApp.Models.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanglaKhabarWebApp.Controllers
{
    [RoutePrefix("api/Security")]
    public class SecurityController : ApiController
    {
        private ApiRepository apiRepository = new ApiRepository();
        Encrypt objEncrypt = new Encrypt();

        [HttpPost]
        [Route("UserLogin")]
        public ResponseMessage UserLogin(LoginInfo loginInfo)
        {
            string reply = string.Empty;
            UserInformation userInfo = new UserInformation();
            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.UserLogIn(loginInfo, ref reply);
                if (result[0] == "Y")
                {
                    try
                    {
                        var dt = apiRepository.GetUserInfo(loginInfo.Email, ref reply);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            userInfo.UserId = dt.Rows[0]["UserId"].ToString();
                            userInfo.FirstName = dt.Rows[0]["FirstName"].ToString();
                            userInfo.LastName = dt.Rows[0]["LastName"].ToString();
                            userInfo.NickName = dt.Rows[0]["NickName"].ToString();
                            userInfo.FullName = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString() + " " + dt.Rows[0]["NickName"].ToString();
                            userInfo.BirthDate = dt.Rows[0]["BirthDate"].ToString();
                            userInfo.Gender = dt.Rows[0]["Gender"].ToString();
                            userInfo.Email = dt.Rows[0]["Email"].ToString();
                            userInfo.Phone = dt.Rows[0]["Phone"].ToString();
                            userInfo.UserType = dt.Rows[0]["UserType"].ToString();
                            userInfo.UserStatus = dt.Rows[0]["UserStatus"].ToString();
                        }

                        rM.MessageCode = "Y";
                        rM.Message = result[1];
                        rM.SystemMessage = reply;
                        rM.Content = userInfo;

                        return rM;


                    }
                    catch (Exception ex)
                    {
                        rM.MessageCode = "N";
                        rM.Message = "Login failed due to Server Error";
                        rM.SystemMessage = ex.Message;
                        rM.Content = userInfo;
                        return rM;
                    }

                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = userInfo;
                    return rM;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "Login failed due to Server Error";
                rM.SystemMessage = ex.Message;
                rM.Content = userInfo;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("UserSignUp")]
        public ResponseMessage UserSignUp(UserInfo userInfo)
        {
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.UserSignUp(userInfo, ref reply);
                if (result[0] == "Y")
                {

                    rM.MessageCode = "Y";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = "User registration successful";
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = result[1];
                }
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = "Failed to register new user due to server problem.";
                return rM;
            }


            return rM;
        }

    }
}

//        [HttpPost]
//        [Route("UpdateToken")]
//        public ResponseMessage UpdateToken(ApplicationModel applicationModel)
//        {
//            ResponseMessage rM = new ResponseMessage();
//            try
//            {
//                string query = "SELECT TokenStatus FROM `firebasetoken` WHERE UserId='" + applicationModel.UserId + "'";
//                var responseString = apiRequest.HttpPostRequestPhp(query, "viewData.php");
//                //  var responseString = Encoding.Default.GetString(res).Replace("[","").Replace("]","");
//                var resObj = JsonConvert.DeserializeObject<Object>(responseString.Replace("[", "").Replace("]", ""));

//                if (resObj.ToString() == "N")
//                {

//                    string insQ = "INSERT INTO `firebasetoken` (`TokenId`, `UserId`, `FcmToken`, `RequestTime`, `TokenStatus`) VALUES (NULL, '" + applicationModel.UserId + "', '" + applicationModel.TerminalId + "', '" + DateTime.Now.ToString("yyyy-mm-dd h:mm tt") + "', 'Y');";

//                    var resString = apiRequest.HttpPostRequestPhp(insQ, "saveData.php");
//                    //  var responseString = Encoding.Default.GetString(res).Replace("[","").Replace("]","");
//                    var reObj = JsonConvert.DeserializeObject<Object>(resString.Replace("[", "").Replace("]", ""));

//                    if (reObj.ToString() == "Y")
//                    {
//                        rM.MessageCode = "Y";
//                        rM.Message = "Notification Activated";
//                        rM.SystemMessage = "";
//                        rM.Content = "";
//                    }
//                    else
//                    {
//                        rM.MessageCode = "Y";
//                        rM.Message = "Notification Activation Failed";
//                        rM.SystemMessage = "";
//                        rM.Content = "";
//                    }

//                }
//                else
//                {
//                    string insQ = "UPDATE `firebasetoken` SET `FcmToken` = '" + applicationModel.TerminalId + "', `TokenStatus` = '" + applicationModel.Remarks + "', `RequestTime` = '" + DateTime.Now.ToString("yyyy-mm-dd h:mm tt") + "' WHERE `firebasetoken`.`UserId` = '" + applicationModel.UserId + "';    ";

//                    var resString = apiRequest.HttpPostRequestPhp(insQ, "saveData.php");
//                    //  var responseString = Encoding.Default.GetString(res).Replace("[","").Replace("]","");
//                    var reObj = JsonConvert.DeserializeObject<Object>(resString.Replace("[", "").Replace("]", ""));

//                    if (reObj.ToString() == "Y")
//                    {
//                        rM.MessageCode = "Y";
//                        rM.Message = "Notification Status Updated";
//                        rM.SystemMessage = "";
//                        rM.Content = "";
//                    }
//                    else
//                    {
//                        rM.MessageCode = "Y";
//                        rM.Message = "Notification Status Update Failed";
//                        rM.SystemMessage = "";
//                        rM.Content = "";
//                    }


//                }

//            }
//            catch (Exception ex)
//            {
//                rM.MessageCode = "N";
//                rM.Message = "Sorry! System Error.";
//                rM.SystemMessage = ex.Message;
//                rM.Content = ex.Message;
//                return rM;
//            }

//            return rM;
//        }
