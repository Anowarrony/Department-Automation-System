using System;
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class GpaViewModel
    {
        public string Gpa { get; set; }
        public int Id { get; set; }
    }

    public class RegistrationDateAddModel
    {   [Display(Name = "Start Date")]
        [Required(ErrorMessage = "* Required")]

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Last Date")]
    [Required(ErrorMessage = "* Required")]
  
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Department { get; set; }
    }
}