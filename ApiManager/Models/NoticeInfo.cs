using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models
{
    public class NoticeInfo
    {
        public string NoticeId { get; set; }
        public string PostedBy { get; set; }
        public string GroupId { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeDetails { get; set; }
        public string PostedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string NoticeStatus { get; set; }
        public string Remarks { get; set; }
    }
}
