using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication8.Models
{
    public class StudentTeacherLoginModel
    {
        [Required(ErrorMessage = "* Required!")]
        public string StudentUniId { get; set; }
        [Required(ErrorMessage = "* Required!")]
        public string Email { get; set; }
    }
 
}