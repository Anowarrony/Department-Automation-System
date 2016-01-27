using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class CourseViewModel:CourseAddModel
    {
        public int YearTermId { get; set; }

        public int Id { get; set; }
    }
}