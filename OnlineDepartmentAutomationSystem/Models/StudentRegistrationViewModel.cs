using System;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace MvcApplication8.Models
{
    public class StudentRegistrationViewModel

    {
        [Required(ErrorMessage = "* Required")]
        public string Session { get; set; }
        [Required(ErrorMessage = "* Required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }
        public bool  Residensial{ get; set; }

        [Remote("CheckIsStudentIdExist", "Student", ErrorMessage = "StudentId Already Exist")]
        [Required(ErrorMessage = "* Required")]
        public string StudentId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Department { get; set; }
        public string HallName { get; set; }
 
      
    }
}