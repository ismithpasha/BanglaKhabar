using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models
{
    public class UserToVendorMessage
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageDetails { get; set; }
        public string TerminalId { get; set; }
    }
}
