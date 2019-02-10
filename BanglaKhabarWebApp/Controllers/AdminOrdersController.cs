using ApiManager.Models.BasicModels;
using ApiManager.Models.Orders;
using ApiManager.ModelsAdmin.OrdersAdmin;
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
    [RoutePrefix("web/Orders")]
    public class AdminOrdersController : ApiController
    {
        private AdminRepository adminRepository = new AdminRepository(); 

        [HttpPost]
        [Route("AllOrders")]
        public ResponseMessage GetOrderListByUser(ParameterUserBasic obj)
        {
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<AdminOrderInfo> orderList = new List<AdminOrderInfo>();

            try
            {
                TimeZoneInfo timeZoneInfo;
                DateTime currentDateTime;
                //Set the time zone information to Central Asia Standard Time
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
                //Get date and time in US Mountain Standard Time 
                currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                

                var dt = adminRepository.GetAllOrderList(obj.UserId.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdminOrderInfo orderInfo = new AdminOrderInfo();
                        orderInfo.OrderId = dr["OrderId"].ToString().Trim();
                        orderInfo.MenuId = dr["MenuId"].ToString().Trim();
                        orderInfo.CustomerId = dr["CustomerId"].ToString().Trim();
                        orderInfo.FirstName = dr["FirstName"].ToString();
                        orderInfo.LastName = dr["LastName"].ToString();
                        orderInfo.NickName = dr["NickName"].ToString();
                        orderInfo.BirthDate = dr["BirthDate"].ToString();
                        orderInfo.Gender = dr["Gender"].ToString();
                        orderInfo.Email = dr["Email"].ToString();
                        orderInfo.Phone = dr["Phone"].ToString();
                        orderInfo.TitleBn = dr["TitleBn"].ToString().Trim();
                        orderInfo.TitleEn = dr["TitleEn"].ToString().Trim();
                        orderInfo.DescriptionBn = dr["DescriptionBn"].ToString().Trim();
                        orderInfo.DescriptionEn = dr["DescriptionEn"].ToString().Trim();
                        orderInfo.RegularPrice = dr["RegularPrice"].ToString().Trim();
                        orderInfo.Discount = dr["Discount"].ToString().Trim();
                        double price = (Convert.ToDouble(orderInfo.RegularPrice) - (Convert.ToDouble(orderInfo.RegularPrice) * Convert.ToDouble(orderInfo.Discount) / 100));
                        orderInfo.Price = price.ToString();
                        orderInfo.Image = dr["Image"].ToString().Trim();
                        orderInfo.OrderTime = dr["OrderTime"].ToString().Trim();
                        orderInfo.DeleveryDate = dr["DeleveryDate"].ToString().Trim();

                        DateTime orderDate = Convert.ToDateTime(dr["DeleveryDate"].ToString().Trim());
                        if (currentDateTime < orderDate)
                        {
                            orderInfo.IsChangeAble = "Y";
                        }
                        else
                        {
                            orderInfo.IsChangeAble = "N";
                        }

                        orderInfo.DeleveryDayName = dr["DeleveryDayName"].ToString().Trim();
                        orderInfo.Quantity = dr["Quantity"].ToString().Trim();
                        orderInfo.AddressId = dr["AddressId"].ToString().Trim();
                        orderInfo.DeleveryAddress = dr["DeleveryAddress"].ToString().Trim();
                        orderInfo.ReceiverName = dr["ReceiverName"].ToString().Trim();
                        orderInfo.UserRemarks = dr["UserRemarks"].ToString().Trim();
                        orderInfo.AdminRemarks = dr["AdminRemarks"].ToString().Trim();
                        orderInfo.OrderStatus = dr["OrderStatus"].ToString().Trim();
                        orderInfo.UserStatus = dr["UserStatus"].ToString().Trim();
                        orderList.Add(orderInfo);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = orderList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No order found.";
                    rM.SystemMessage = reply;
                    rM.Content = orderList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = orderList;
                return rM;
            }


            return rM;
        }


    }
}
