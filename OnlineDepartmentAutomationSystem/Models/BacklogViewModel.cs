using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class BacklogViewModel
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public double Credit { get; set; }
        public string YearTermName { get; set; }

    }
}