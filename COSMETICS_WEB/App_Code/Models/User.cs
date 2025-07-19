using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } 
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; } 
    }
}