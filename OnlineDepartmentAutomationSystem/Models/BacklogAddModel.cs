
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class BacklogAddModel
    {

        [Display(Name = "Student ID")]
        [Required(ErrorMessage = "* Required")]
        public string StudentId { get; set; }
        [Display(Name = "Year Term")]
        [Required(ErrorMessage = "* Required")]
        public string YearTerm { get; set; }
      [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}