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


    }
}
