using BanglaKhabarWeb.Models.ResponseModels;
using BanglaKhabarWeb.Models.SecurityModels;
using BanglaKhabarWeb.Models.UserModels;
using BanglaKhabarWeb.NetworkModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaKhabarWeb.Controllers
{
 
    public class SecurityController : Controller
    {
        ApiRequest apiRequest = new ApiRequest();

        public ActionResult Index()
        {
            return RedirectToAction("Login", "Security");
        }

        public ActionResult Login() 
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }


        public ActionResult UserLogin(string EmailPhone, string Password)
        {
            try
            {
                LoginEntity loginEntity = new LoginEntity();
                loginEntity.Email = EmailPhone;
                loginEntity.Password = Password;
                loginEntity.UserType = "customer";
                loginEntity.TerminalId = "web";

                var res = apiRequest.HttpPostRequest(loginEntity, "api/Security/UserLogin");
                // response = response.Replace("null", "0"); 
                string response = res.ToString();
                var responseModel = JsonConvert.DeserializeObject<ResponseMessage>(response);

                if (responseModel.MessageCode == "Y")
                {
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(responseModel.Content.ToString());


                    if (userInfo.UserType.ToString() == "customer")
                    {
                        Session["SessionUserId"] = userInfo.UserId;
                        Session["SessionUserEmail"] = userInfo.Email;
                        Session["SessionUserFirstName"] = userInfo.FirstName;
                        Session["SessionUserFullName"] = userInfo.FullName;
                        Session["SessionUserType"] = userInfo.UserType;
                        Session["SessionUserGender"] = userInfo.Gender;

                        return RedirectToAction("Index", "User");
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
                        return RedirectToAction("Login", "Security");
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
                    return RedirectToAction("Login", "Security");

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
                return RedirectToAction("Login", "Security");
            }
            return RedirectToAction("Login", "Security");
        }

        public ActionResult UserSignUp(string FirstName, string LastName, string Phone, string Email, string Password, string RePassword, string Gender, string BirthDate)
        {
            try
            {
                if(Password!=RePassword)
                {
                    TempData["msgAlert"] = "N";
                    TempData["msgAlertDetails"] = "Password did not matched";
                    Session["SessionUserId"] = null;
                    Session["SessionUserEmail"] = null;
                    Session["SessionUserFirstName"] = null;
                    Session["SessionUserFullName"] = null;
                    Session["SessionUserType"] = null;
                    return RedirectToAction("Login", "Security");
                }
                SignUpEntity signUpEntity = new SignUpEntity();
                signUpEntity.FirstName = FirstName;
                signUpEntity.LastName = LastName;
                signUpEntity.Phone = Phone;
                signUpEntity.Email = Email;
                signUpEntity.Password = Password;
                signUpEntity.Gender = Gender;
                signUpEntity.BirthDate = BirthDate;
                signUpEntity.NickName = "";
                signUpEntity.UserId = "0";
                signUpEntity.UserType = "customer";
                signUpEntity.UserStatus = "Y";

                var res = apiRequest.HttpPostRequest(signUpEntity, "api/Security/UserSignUp");
                // response = response.Replace("null", "0"); 
                string response = res.ToString();
                var responseModel = JsonConvert.DeserializeObject<ResponseMessage>(response);

                if (responseModel.MessageCode == "Y")
                {               
                        TempData["msgAlert"] = "Y";
                        TempData["msgAlertDetails"] = responseModel.Message.ToString();
             
                    return RedirectToAction("Login", "Security");
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
                    return RedirectToAction("SignUp", "Security");

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
                return RedirectToAction("SignUp", "Security");
            }
            return RedirectToAction("SignUp", "Security");
        }

        public ActionResult Logout()
        {
            // Session["SessionUserEmail"] = null;
            Session.Contents.RemoveAll();
            return RedirectToAction("Index", "Home");
        }



    }
}
