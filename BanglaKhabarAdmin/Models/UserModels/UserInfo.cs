using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarAdmin.Models.UserModels
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string UserType { get; set; }
        public string UserStatus { get; set; }
    }
}