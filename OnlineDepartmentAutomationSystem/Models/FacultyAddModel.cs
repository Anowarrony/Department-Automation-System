

using System.ComponentModel.DataAnnotations;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class FacultyAddModel
    {[Required(ErrorMessage = "This field is required!")]
        [MinLength(3,ErrorMessage = "Minimum 3 chracters required!")]
        public string Faculty { get; set; }
    }
}