using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class BacklokRegisterModel
    {
            [Required(ErrorMessage = "*Required!")]
            public string BackLoks { get; set; }
    
    }
}