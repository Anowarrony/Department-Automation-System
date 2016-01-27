using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class ResultDisplaywModel
    {
        public double YearTermCgpa { get; set; }
        public double RunnignCgpa { get; set; }
        public double CurrentYearTermsTotalCredit { get; set; }
        public double TotalCompletedCredit { get; set; }
        public double TotalCreditRemaningCredit { get; set; }
        public string YearTerm { get; set; }
        public int YearTermId { get; set; }
    }
}