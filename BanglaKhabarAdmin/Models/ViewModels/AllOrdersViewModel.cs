using BanglaKhabarAdmin.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarAdmin.Models.ViewModels
{
    public class AllOrdersViewModel
    {
         public List<OrderInfo> OrderList { get; set; } 
    }
}