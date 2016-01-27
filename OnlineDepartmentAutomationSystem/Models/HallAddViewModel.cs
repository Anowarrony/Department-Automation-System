

using System.ComponentModel.DataAnnotations;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class HallAddViewModel
    {
        [Display(Name = "Hall Name")]
        [Required(ErrorMessage = "This field is required!")]
        public string HallName { get; set; }
    }

    public class HallDetailsModel : HallAddViewModel
    {
        public int HallId { get; set; }
    }
}