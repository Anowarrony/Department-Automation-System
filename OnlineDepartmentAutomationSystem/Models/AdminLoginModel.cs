﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class AdminLoginModel
    {
        [Required(ErrorMessage = "* Required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "* Required!")]
        public string Password { get; set; }
    }

}