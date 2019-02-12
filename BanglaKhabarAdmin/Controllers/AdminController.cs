using BanglaKhabarAdmin.Models.BasicModels;
using BanglaKhabarAdmin.Models.OrderModels;
using BanglaKhabarAdmin.Models.ResponseModels;
using BanglaKhabarAdmin.Models.ViewModels;
using BanglaKhabarAdmin.NetworkModels;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaKhabarAdmin.Controllers
{
    public class AdminController : Controller
    {
        ApiRequest apiRequest = new ApiRequest();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DailyOrders()
        { 
            return View(); 
        }

        public ActionResult AllOrders(int? page)
        {
            try
            {
                int pageSize = 5;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                List<OrderInfo> orderList = GetOrderList();
                IPagedList<OrderInfo> listOrderInfo = orderList.ToPagedList(pageIndex, pageSize);

                return View("AllOrders", listOrderInfo);
            }
            catch
            {
                return null;
            }
        }

        public ActionResult LoadAllOrders(int? page) 
        {
            try
            {
                int pageSize = 8;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                List<OrderInfo> orderList = GetOrderList();
                IPagedList<OrderInfo> listOrderInfo = orderList.ToPagedList(pageIndex, pageSize);

                return View("AllOrders", listOrderInfo);
            }
            catch
            {
                return null;
            }
           
        }

        public List<OrderInfo> GetOrderList() 
        {
            UserBasic objUser = new UserBasic();
            objUser.UserId = Session["SessionUserId"].ToString();
            objUser.Email = Session["SessionUserEmail"].ToString();
            objUser.TerminalId = "admin";


            List<OrderInfo> orderInfoList = new List<OrderInfo>(); 
            var response = apiRequest.HttpPostRequest(objUser, "web/Orders/AllOrders");
            var responseModel = JsonConvert.DeserializeObject<ResponseMessage>(response.ToString());

            if (responseModel.MessageCode == "Y")
            {
                orderInfoList = JsonConvert.DeserializeObject<List<OrderInfo>>(responseModel.Content.ToString());
                
                return orderInfoList;
            }
            else
            {
                TempData["msgAlert"] = "NoOrders";
                TempData["msgAlertDetails"] = "No order information found.";
                return orderInfoList;
            }

            return orderInfoList;

        }

    }
}