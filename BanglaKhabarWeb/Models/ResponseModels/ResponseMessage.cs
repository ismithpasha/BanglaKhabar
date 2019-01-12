using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarWeb.Models.ResponseModels
{
    public class ResponseMessage
    {
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string SystemMessage { get; set; }
        public object Content { get; set; }
    }
}