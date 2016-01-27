using System;
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class CourseRegistrationDateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd- MM-yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Last Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime LastDate { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }

        public double DaysLeft { get; set; }
    }
}