using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class BacklokDetailsViewModel
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public double Credit { get; set; }
        public string YearTerm { get; set; }
        public double TotalCredit { get; set; }
    }
}