
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class YearTermViewModel
    {
        [Required(ErrorMessage="* Required")]
        public string YearTerm { get; set; }

       
    }
}