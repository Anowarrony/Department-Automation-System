
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    
    public class DepartAddViewModel
    {
        [Display(Name = "Department Shortname")]
    
        public string DepartmentShortName { get; set; }
        [Display(Name = "Department Fullname")]
        [Required(ErrorMessage = "*Required")]
        public string DepartmentFullName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Faculty { get; set; }

    }

    public class DepartViewModel :DepartAddViewModel
    {
        public int DeptId { get; set; }
    }

    public class DepartUpdateViewModel : DepartAddViewModel
    {
        
    }
}