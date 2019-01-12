using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Models.Orders
{
    public class MenuItems
    {
        public string MenuId { get; set; }
        public string TitleBn { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionBn { get; set; }
        public string DescriptionEn { get; set; }
        public string Tag { get; set; }
        public string RegularPrice { get; set; }
        public string Discount { get; set; }
        public string Image { get; set; }
        public string MenuStatus { get; set; }
    }
}
