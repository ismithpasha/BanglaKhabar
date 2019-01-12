using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models.Orders
{
    public class CustomerOrder
    {
        public string OrderId { get; set; }
        public string MenuId { get; set; }
        public string CustomerId { get; set; }
        public string OrderTime { get; set; }
        public string DeleveryDate { get; set; }
        public string DeleveryDayName { get; set; }
        public string Quantity { get; set; }
        public string AddressId { get; set; }
        public string DeleveryAddress { get; set; }
        public string ReceiverName { get; set; }
        public string UserRemarks { get; set; }
        public string AdminRemarks { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedOn { get; set; }
        public string OrderStatus { get; set; }


    }
}
