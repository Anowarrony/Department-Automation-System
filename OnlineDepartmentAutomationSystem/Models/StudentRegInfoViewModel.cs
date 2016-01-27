using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication8.Models
{
    public class StudentRegInfoViewModel
    {
        public string Id { get; set; }
        public string YearTerm { get; set; }
        public double TotlaCreditTaken { get; set; }
        public string RegisyeredCourse { get; set; }
        public string DepartmentName { get; set; }
        public string CourseTitle { get; set; }
        public double Credit { get; set; }
    }
}