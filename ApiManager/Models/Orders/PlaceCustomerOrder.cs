using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models.Orders
{
    public class PlaceCustomerOrder
    {
        public string MenuId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DeleveryDate { get; set; }
        public string DeleveryDayName { get; set; }
        public string Quantity { get; set; }
        public string AddressId { get; set; }
        public string DeleveryAddress { get; set; }
        public string ReceiverName { get; set; }
        public string UserRemarks { get; set; }
    }
}
