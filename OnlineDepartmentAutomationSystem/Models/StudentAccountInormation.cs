using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentAccountInormation

    {
        public string Name { get; set; }
        public String Id { get; set; }
        public String Department { get; set; }
        public string Email { get; set; }
        public string Session { get; set; }
        public string Faculty { get; set; }
        public string Phonenumber { get; set; }
        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string Nationality { get; set; }
    }

    public class UpdateAccount
    {
        [Required(ErrorMessage = "* Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* PhoneNumber is required!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "* PresentAddress is required!")]
        public string PresentAddress { get; set; }
}
    
    public class TeacherProfileInfo : StudentAccountInormation
    {
        public string Religion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TeacherProfileUpdateViewModel
    {
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email")]        
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required!")]
        public string PresentAddress { get; set; }
      
        [Required(ErrorMessage = "* Required!")]
        public string Phonenumber { get; set; }

        public int TeacherUpdateId { get; set; }

    }
}