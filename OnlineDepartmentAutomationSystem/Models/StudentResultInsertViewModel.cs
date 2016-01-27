
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentResultInsertViewModel 
    {
       
         [Required]
        [DisplayName("Student ID")]
        public string StudentId { get; set; }
        [Required]
        [DisplayName("Year Term")]
        public int YearTerm { get; set; }
     
        [DisplayName("Department Name")]
        [Required]
        public string DepartmentName { get; set; }

        public List<string> CourseCode { get; set; }
        [DisplayName("Course Code")]
        public List<double> Gpa { get; set; }

    }
    public abstract class BaseResult
    {
       [DisplayName("Course Code")]
       [Required]
       public List<string> CourseCode { get; set; }
          [DisplayName("Course Code")]
       public List<string> Gpa { get; set; }
    }
    public class PartialResult 
    {
        public string CourseCode { get; set; }
        public double Gpa { get; set; }

    }

    public class ResultList
    {
        public string CourseCode { get; set; }
        public int Gpa { get; set; }
        //public List<string> CourseCode { get; set; }
        //public List<double> Gpa { get; set; }
        //public string StudentId { get; set; }
   
        //public int YearTerm { get; set; }
    }


    public class ResultList2
    {
        public string CourseCode { get; set; }

        public int MatchId { get; set; }
        //public string StudentId { get; set; }

        //public int YearTerm { get; set; }
    }
    public class ResultList3
    {
  
        public int Gpa { get; set; }
        public int MatchId { get; set; }
        //public string StudentId { get; set; }

        //public int YearTerm { get; set; }
    }
}