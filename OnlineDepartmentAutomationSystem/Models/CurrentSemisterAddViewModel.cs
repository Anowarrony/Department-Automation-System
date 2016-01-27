
using System.ComponentModel.DataAnnotations;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class CurrentSemisterAddViewModel
    {

        [Required(ErrorMessage = "* Required")]
        public string Session { get; set; }
         [Required(ErrorMessage = "* Required")]
        public string Department { get; set; }
         [Required(ErrorMessage = "* Required")]
        public string YearTerm { get; set; }

    }

    public class CurrentSemisterViewModel : CurrentSemisterAddViewModel
    {
        public int Id { get; set; }
    }
}