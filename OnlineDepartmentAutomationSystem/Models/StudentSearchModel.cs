
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentSearchModel
    
    {
        [Required(ErrorMessage = "* Required!")]
        public string SearchTerm { get; set; }
    }
}