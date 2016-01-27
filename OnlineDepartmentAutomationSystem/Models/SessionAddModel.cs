

using System.ComponentModel.DataAnnotations;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class SessionAddModel
    {[Required(ErrorMessage = "This field is required!")]
        public string Session { get; set; }
    }
}