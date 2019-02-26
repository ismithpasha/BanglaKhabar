using ApiManager.ApiModels;
using ApiManager.Models;
using ApiManager.Models.BasicModels;
using ApiManager.Models.Orders;
using ApiManager.Repository;
using BanglaKhabarWebApp.Models;
using BanglaKhabarWebApp.Models.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanglaKhabarWebApp.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private ApiRepository apiRepository = new ApiRepository();

        [HttpPost]
        [Route("GetMenu")]
        public ResponseMessage GetMenuItems(ParameterUserBasic obj)
        {
             
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<MenuItems> menuList = new List<MenuItems>();

            try
            {
                var dt = apiRepository.GetMenuItems(ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MenuItems menu = new MenuItems();

                        menu.MenuId = dr["MenuId"].ToString().Trim();
                        menu.TitleBn = dr["TitleBn"].ToString().Trim();
                        menu.TitleEn = dr["TitleEn"].ToString().Trim();
                        menu.DescriptionBn = dr["DescriptionBn"].ToString().Trim();
                        menu.DescriptionEn = dr["DescriptionEn"].ToString().Trim();
                        menu.Tag = dr["Tag"].ToString().Trim();
                        menu.RegularPrice = dr["RegularPrice"].ToString().Trim();
                        menu.Discount = dr["Discount"].ToString().Trim();
                        menu.Image = dr["Image"].ToString().Trim();
                        menu.MenuStatus = dr["MenuStatus"].ToString().Trim();

                        menuList.Add(menu);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menuList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetMenuList")]
        public ResponseMessage GetMenuList(ParameterMenuDetails obj)
        {
             
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<MenuItems> menuList = new List<MenuItems>();

            try
            {
                var dt = apiRepository.GetMenuDetails(obj.DayName.Trim(), obj.Tag.Trim(),ref reply); 
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MenuItems menu = new MenuItems();

                        menu.MenuId = dr["MenuId"].ToString().Trim();
                        menu.TitleBn = dr["TitleBn"].ToString().Trim();
                        menu.TitleEn = dr["TitleEn"].ToString().Trim();
                        menu.DescriptionBn = dr["DescriptionBn"].ToString().Trim();
                        menu.DescriptionEn = dr["DescriptionEn"].ToString().Trim();
                        menu.Tag = dr["Tag"].ToString().Trim();
                        menu.RegularPrice = dr["RegularPrice"].ToString().Trim();
                        menu.Discount = dr["Discount"].ToString().Trim();
                        menu.Image = dr["Image"].ToString().Trim();
                        menu.MenuStatus = dr["MenuStatus"].ToString().Trim();

                        menuList.Add(menu);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menuList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetMenuInfo")]
        public ResponseMessage GetMenuInfo(ParameterMenuDetails obj)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            MenuItems menu = new MenuItems();
            try
            {
                var dt = apiRepository.GetMenuDetails(obj.DayName.Trim(), obj.Tag.Trim(), ref reply);
              
                if (dt != null && dt.Rows.Count > 0)
                {
                     
                        menu.MenuId = dt.Rows[0]["MenuId"].ToString().Trim();
                        menu.TitleBn = dt.Rows[0]["TitleBn"].ToString().Trim();
                        menu.TitleEn = dt.Rows[0]["TitleEn"].ToString().Trim();
                        menu.DescriptionBn = dt.Rows[0]["DescriptionBn"].ToString().Trim();
                        menu.DescriptionEn = dt.Rows[0]["DescriptionEn"].ToString().Trim();
                        menu.Tag = dt.Rows[0]["Tag"].ToString().Trim();
                        menu.RegularPrice = dt.Rows[0]["RegularPrice"].ToString().Trim();
                        menu.Discount = dt.Rows[0]["Discount"].ToString().Trim();
                        menu.Image = dt.Rows[0]["Image"].ToString().Trim();
                        menu.MenuStatus = dt.Rows[0]["MenuStatus"].ToString().Trim();

                       
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menu;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menu;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menu;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetMenuDetails")]
        public ResponseMessage GetMenuItemDetails(ParameterMenuDetails obj)
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            MenuItems menu = new MenuItems();
            try
            {
                var dt = apiRepository.GetMenuItemDetails(obj.DayName.Trim(), ref reply);

                if (dt != null && dt.Rows.Count > 0)
                {

                    menu.MenuId = dt.Rows[0]["MenuId"].ToString().Trim();
                    menu.TitleBn = dt.Rows[0]["TitleBn"].ToString().Trim();
                    menu.TitleEn = dt.Rows[0]["TitleEn"].ToString().Trim();
                    menu.DescriptionBn = dt.Rows[0]["DescriptionBn"].ToString().Trim();
                    menu.DescriptionEn = dt.Rows[0]["DescriptionEn"].ToString().Trim();
                    menu.Tag = dt.Rows[0]["Tag"].ToString().Trim();
                    menu.RegularPrice = dt.Rows[0]["RegularPrice"].ToString().Trim();
                    menu.Discount = dt.Rows[0]["Discount"].ToString().Trim();
                    menu.Image = dt.Rows[0]["Image"].ToString().Trim();
                    menu.MenuStatus = dt.Rows[0]["MenuStatus"].ToString().Trim();


                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menu;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menu;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menu;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetTagList")]
        public ResponseMessage GetTagList(DayInfo obj) 
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<TagName> menuList = new List<TagName>();

            try
            {
                var dt = apiRepository.GetTagList(obj.DayName.Trim(),ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TagName tg = new TagName();
                        tg.Tag = dr["Tag"].ToString().Trim();
                        menuList.Add(tg);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menuList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("GetOrderListByUser")]
        public ResponseMessage GetOrderListByUser(ParameterUserBasic obj)
        {
             
            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<OrderInfo> orderList = new List<OrderInfo>();

            try
            {
                TimeZoneInfo timeZoneInfo;
                DateTime currentDateTime;
                //Set the time zone information to Central Asia Standard Time
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
                //Get date and time in US Mountain Standard Time 
                currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        
                

                var dt = apiRepository.GetOrderListByUser(obj.UserId.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderInfo orderInfo = new OrderInfo();
                        orderInfo.OrderId = dr["OrderId"].ToString().Trim();
                        orderInfo.MenuId = dr["MenuId"].ToString().Trim();
                        orderInfo.CustomerId = dr["CustomerId"].ToString().Trim();
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
                        if(currentDateTime<orderDate)
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


        [HttpPost]
        [Route("PlaceOrder")]
        public ResponseMessage PlaceCustomerOrder(PlaceCustomerOrder objOrder) 
        {
            string reply = string.Empty;

            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.PlaceCustomerOrder(objOrder, ref reply);
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
        [Route("GetAllOrders")]
        public ResponseMessage GetAllOrders(DayInfo obj) 
        {

            string reply = string.Empty;
            ResponseMessage rM = new ResponseMessage();
            List<TagName> menuList = new List<TagName>();

            try
            {
                var dt = apiRepository.GetTagList(obj.DayName.Trim(), ref reply);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TagName tg = new TagName();
                        tg.Tag = dr["Tag"].ToString().Trim();
                        menuList.Add(tg);
                    }
                    rM.MessageCode = "Y";
                    rM.Message = "";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                else
                {
                    rM.MessageCode = "N";
                    rM.Message = "No menu found.";
                    rM.SystemMessage = reply;
                    rM.Content = menuList;
                }
                return rM;
            }
            catch (Exception ex)
            {
                rM.MessageCode = "N";
                rM.Message = "System Error";
                rM.SystemMessage = ex.Message;
                rM.Content = menuList;
                return rM;
            }


            return rM;
        }

        [HttpPost]
        [Route("CancleOrderByUser")]
        public ResponseMessage CancleOrderByUser(CancleOrderEntity objOrder)
        {
            string reply = string.Empty;

            ResponseMessage rM = new ResponseMessage();
            try
            {
                var result = apiRepository.CancleOrderByUser(objOrder, ref reply);
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



    }
}
