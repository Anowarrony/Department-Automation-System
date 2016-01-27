using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "*Username is required!")]
        public string Username { get; set; }
    [Required(ErrorMessage = "*Password is required!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "* Required!")]
    public string Role { get; set; }

    }

}