using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models
{
    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string TerminalId { get; set; }
    }
}
