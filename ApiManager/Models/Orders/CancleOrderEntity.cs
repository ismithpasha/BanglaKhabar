using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models.Orders
{
    public class CancleOrderEntity
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string Remarks { get; set; }
    }
}
