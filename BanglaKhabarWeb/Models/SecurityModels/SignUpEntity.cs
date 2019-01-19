﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaKhabarWeb.Models.SecurityModels
{
    public class SignUpEntity
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string UserStatus { get; set; }
    }
}