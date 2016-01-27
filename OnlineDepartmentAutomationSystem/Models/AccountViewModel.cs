using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public byte[] Image { get; set; }
    }
}