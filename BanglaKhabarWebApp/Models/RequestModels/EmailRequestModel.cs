using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarWebApp.Models.RequestModels
{
    public class EmailRequestModel
    {
        public string Email { get; set; }
        public string TerminalId { get; set; }
        public string Remarks { get; set; }
    }
}