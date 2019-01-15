using BanglaKhabarAdmin.Models.ResponseModels;
using BanglaKhabarAdmin.Models.SecurityModels;
using BanglaKhabarAdmin.Models.UserModels;
using BanglaKhabarAdmin.NetworkModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaKhabarAdmin.Controllers
{
    public class SecurityController : Controller
    {
        ApiRequest apiRequest = new ApiRequest();
        // GET: Security
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserLogin(string EmailPhone, string Password)
        {
            try
            {
                LoginEntity loginEntity = new LoginEntity();
                loginEntity.Email = EmailPhone;
                loginEntity.Password = Password;
                loginEntity.UserType = "admin";
                loginEntity.TerminalId = "webadmin";

                var res = apiRequest.HttpPostRequest(loginEntity, "api/Security/UserLogin");
                // response = response.Replace("null", "0"); 
                string response = res.ToString();
                var responseModel = JsonConvert.DeserializeObject<ResponseMessage>(response);

                if (responseModel.MessageCode == "Y")
                {
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(responseModel.Content.ToString());


                    if (userInfo.UserType.ToString() == "admin")
                    {
                        Session["SessionUserId"] = userInfo.UserId;
                        Session["SessionUserEmail"] = userInfo.Email;
                        Session["SessionUserFirstName"] = userInfo.FirstName;
                        Session["SessionUserFullName"] = userInfo.FullName;
                        Session["SessionUserType"] = userInfo.UserType;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["msgAlert"] = "NotAuthorized";
                        TempData["msgAlertDetails"] = responseModel.Message.ToString();
                        Session["SessionUserId"] = null;
                        Session["SessionUserEmail"] = null;
                        Session["SessionUserFirstName"] = null;
                        Session["SessionUserFullName"] = null;
                        Session["SessionUserType"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                }


                else
                {
                    TempData["msgAlert"] = "N";
                    TempData["msgAlertDetails"] = responseModel.Message.ToString();
                    Session["SessionUserId"] = null;
                    Session["SessionUserEmail"] = null;
                    Session["SessionUserFirstName"] = null;
                    Session["SessionUserFullName"] = null;
                    Session["SessionUserType"] = null;
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = "Sorry, something wrong. Please wait and try after a few minutes.";
                Session["SessionUserId"] = null;
                Session["SessionUserEmail"] = null;
                Session["SessionUserFirstName"] = null;
                Session["SessionUserFullName"] = null;
                Session["SessionUserType"] = null;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            // Session["SessionUserEmail"] = null;
            Session.Contents.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

    }
}