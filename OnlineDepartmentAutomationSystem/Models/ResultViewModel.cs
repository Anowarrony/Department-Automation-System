using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication8.Models
{
    public class ResultViewModel

    {
        public string StudentId { get; set; }
        public double TotlaCreditTaken { get; set; }
        public double TotalCompletedCredit { get; set; }
        public double Cgpa { get; set; }
    }
}