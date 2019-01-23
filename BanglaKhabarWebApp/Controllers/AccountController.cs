using ApiManager.Models;
using ApiManager.Models.BasicModels;
using ApiManager.Repository;
using BanglaKhabarWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanglaKhabarWebApp.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ApiRepository apiRepository = new ApiRepository();

        [HttpPost]
        [Route("SaveAddress")]
        public ResponseMessage SaveAddress(UserAddress userAddress) 
        {
            string reply = string.Empty;
            
            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.SaveUserAddress(userAddress, ref reply);
                if (result[0] == "Y")
                {

                    rM.MessageCode = "Y";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = result[1];
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
                rM.Message = "Server Error";
                rM.SystemMessage = ex.Message;
                rM.Content = "Server Error";
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("UpdateAddress")]
        public ResponseMessage UpdateUserAddress(UserAddress userAddress)
        {
            string reply = string.Empty;

            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.UpdateUserAddress(userAddress, ref reply);
                if (result[0] == "Y")
                {

                    rM.MessageCode = "Y";
                    rM.Message = result[1];
                    rM.SystemMessage = reply;
                    rM.Content = result[1];
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
                rM.Message = "Server Error";
                rM.SystemMessage = ex.Message;
                rM.Content = "Server Error";
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetAddress")]
        public ResponseMessage GetUserAddress(ParameterUserBasic obj)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<UserAddress> addressList = new List<UserAddress>();

            try
            {
                var dt = apiRepository.GetUserAddress(obj.UserId.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserAddress address = new UserAddress();

                        address.AddressId = dr["AddressId"].ToString().Trim();
                        address.UserId = dr["UserId"].ToString().Trim();
                        address.AddressTitle = dr["AddressTitle"].ToString().Trim();
                        address.StreetAddress = dr["StreetAddress"].ToString().Trim();
                        address.City = dr["City"].ToString().Trim();
                        address.PostCode = dr["PostCode"].ToString().Trim();
                        address.Country = dr["Country"].ToString().Trim();
                        address.Latitude = dr["Latitude"].ToString().Trim();
                        address.Longitude = dr["Longitude"].ToString().Trim();
                        address.AddressStatus = dr["AddressStatus"].ToString().Trim();

                        addressList.Add(address);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = addressList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No address found.";
                    rM.SystemMessage = reply;
                    rM.Content = addressList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = addressList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetAddressInfo")]
        public ResponseMessage GetAddressInfo(ParameterUserBasic obj)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            UserAddress address = new UserAddress();

            try
            {
                var dt = apiRepository.GetAddressInfo(obj.UserId.Trim(), obj.TerminalId.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {

                    address = new UserAddress();

                    address.AddressId = dt.Rows[0]["AddressId"].ToString().Trim();
                    address.UserId = dt.Rows[0]["UserId"].ToString().Trim();
                    address.AddressTitle = dt.Rows[0]["AddressTitle"].ToString().Trim();
                    address.StreetAddress = dt.Rows[0]["StreetAddress"].ToString().Trim();
                    address.City = dt.Rows[0]["City"].ToString().Trim();
                    address.PostCode = dt.Rows[0]["PostCode"].ToString().Trim();
                    address.Country = dt.Rows[0]["Country"].ToString().Trim();
                    address.Latitude = dt.Rows[0]["Latitude"].ToString().Trim();
                    address.Longitude = dt.Rows[0]["Longitude"].ToString().Trim();
                    address.AddressStatus = dt.Rows[0]["AddressStatus"].ToString().Trim();

                    
                
                rM.MessageCode = "Y";
                rM.Message = "";
                rM.SystemMessage = reply;
                rM.Content = address;
            }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No address found.";
                    rM.SystemMessage = reply;
                    rM.Content = address;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = address;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetAddressGroup")]
        public ResponseMessage GetAddressGroup(ParameterUserBasic obj)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<GroupInfo> addressList = new List<GroupInfo>();

            try
            {
                var dt = apiRepository.GetUserAddress(obj.UserId.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GroupInfo address = new GroupInfo();

                        address.GroupId = dr["AddressId"].ToString().Trim();
                        address.GroupTitle = dr["AddressTitle"].ToString().Trim();
                        address.GroupAddress = dr["StreetAddress"].ToString().Trim()+", "+ dr["City"].ToString().Trim() + ", Post Code: " + dr["PostCode"].ToString().Trim();
                        address.GroupDetails = dr["StreetAddress"].ToString().Trim() + ", " + dr["City"].ToString().Trim() + ", Post Code: " + dr["PostCode"].ToString().Trim();
                        address.GroupStatus = dr["AddressStatus"].ToString().Trim();

                        addressList.Add(address);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = addressList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No address found.";
                    rM.SystemMessage = reply;
                    rM.Content = addressList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = addressList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetAllAdmins")]
        public ResponseMessage GetAllAdmins(ParameterUserBasic objId)
        {
            string id = objId.UserId.Trim();

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<UserInformation> userInfoList = new List<UserInformation>();

            try
            {
                var dt = apiRepository.GetAllAdmins(ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserInformation userInfo = new UserInformation();

                        userInfo.UserId = dr["UserId"].ToString();
                        userInfo.FirstName = dr["FirstName"].ToString();
                        userInfo.LastName = dr["LastName"].ToString();
                        userInfo.NickName = dr["NickName"].ToString();
                        userInfo.FullName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString() + " (" + dr["NickName"].ToString() + ") ";
                        userInfo.BirthDate = dr["BirthDate"].ToString();
                        userInfo.Gender = dr["Gender"].ToString();
                        userInfo.Email = dr["Email"].ToString();
                        userInfo.Phone = dr["Phone"].ToString();
                        userInfo.UserType = dr["UserType"].ToString();
                        userInfo.UserStatus = dr["UserStatus"].ToString();

                        userInfoList.Add(userInfo);

                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = userInfoList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No notice found.";
                    rM.SystemMessage = reply;
                    rM.Content = userInfoList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = userInfoList;
                return rM;
            }


            return rM;
        }

    }
}
