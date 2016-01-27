using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class AccountAddModel
    {
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
       
        [DisplayName("Username")]
        [Required(ErrorMessage = "* Required")]
        [Remote("CheckIsUsernameExist", "Admin", ErrorMessage = "This Username Already in Use")]
        
     
        public string Username { get; set; }
        [Required(ErrorMessage = "* Required")]
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("UserId")]
        [Required(ErrorMessage = "* Required")]
        [Remote("CheckIsIdExist", "Admin", ErrorMessage = "This ID Already Exist")]
       
        public string UserId { get; set; }
    }
  
}