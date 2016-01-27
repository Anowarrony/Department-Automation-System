using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class BacklockRegistrationViewModel
    {
        [Required]
        public string[] Backlok { get; set; }
        public string YearTerm { get; set; }
    
        public string Code { get; set; }
    }
}