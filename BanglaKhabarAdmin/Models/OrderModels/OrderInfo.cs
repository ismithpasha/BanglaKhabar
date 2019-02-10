using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarAdmin.Models.OrderModels
{
    public class OrderInfo
    {
        public string OrderId { get; set; }
        public string MenuId { get; set; }
        public string CustomerId { get; set; }
        public string TitleBn { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionBn { get; set; }
        public string DescriptionEn { get; set; }
        public string RegularPrice { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string OrderTime { get; set; }
        public string DeleveryDate { get; set; }
        public string DeleveryDayName { get; set; }
        public string Quantity { get; set; }
        public string AddressId { get; set; }
        public string DeleveryAddress { get; set; }
        public string ReceiverName { get; set; }
        public string UserRemarks { get; set; }
        public string AdminRemarks { get; set; }
        public string OrderStatus { get; set; }
        public string IsChangeAble { get; set; }

        public string UserStatus { get; set; }
    }
}