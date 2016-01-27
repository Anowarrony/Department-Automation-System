
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class CourseAddModel

    {
        [DisplayName("Course Code")]
        [Remote("CheckIsCourseCodeExist","Admin",ErrorMessage = "* Course code already exist")]
        [Required(ErrorMessage = "* Required!")]
        public string CourseCode { get; set; }
        [DisplayName("Course Title")]
        [Required(ErrorMessage = "* Required!")]
        public string CourseTitle { get; set; }
       [DisplayName("Credit")]
        public double Credit { get; set; }
        [DisplayName("Year Term")]
        [Required(ErrorMessage = "* Required!")]
        public string YearTerm { get; set; }
        [Required(ErrorMessage = "* Required!")]
        [DisplayName("Department Name")]
        public string Department { get; set; }
    }
}