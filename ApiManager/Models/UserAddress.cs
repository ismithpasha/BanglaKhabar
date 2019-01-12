using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models
{
    public class UserAddress
    {
        public string AddressId { get; set; }
        public string UserId { get; set; }
        public string AddressTitle { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
        public string AddressStatus { get; set; } 

    }
}
