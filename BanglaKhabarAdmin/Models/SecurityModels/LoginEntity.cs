using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarAdmin.Models.SecurityModels
{
    public class LoginEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string TerminalId { get; set; }
    }
}