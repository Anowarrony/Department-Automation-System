using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace OnlineDepartmentAutomationSystem.Models
{
    public class DepartmentModels
    {

    }
    public class StudentResultDetails : SudentResultViewModel
    {

    }
     


    [Table("StudentInformation")]
    public  class StudentInformation
    {[Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public int SessionId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
       [DisplayName("Present Address")]
        public string PresentAddress { get; set; }
        [DisplayName("Parmanent Address")]
        public string ParmanentAddress { get; set; }
        public byte[] StudentImage { get; set; }
        public int? HallId { get; set; }
    }
     [Table("Teacher")]
    public  class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string PresentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Image { get; set; }
        public string Residential { get; set; }
    }
    [Table("AdminLoginTable")]
    public  class AdminLoginTable
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    [Table("Backlog")]
    public  class Backlog
    {
        [Key]
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string StudentId { get; set; }
        public int YearTermId { get; set; }
    }

    [Table("RegistrationDate")]
    public class RegistrationDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DepartmentId { get; set; }
        public int? DaysLeft { get; set; }
        public int? AllowReg { get; set; }
    }

    [Table("Grade")]
    public class Grade
    {
 
        [Key]
        public int GradeId
        { get; set; }
        public double GradePoint { get; set; }
        public string LetterGrade { get; set; }
  
    }
    [Table("BacklokRegistration")]
    public  class BacklokRegistration
    {
        [Key]
        public int BacklokRegId { get; set; }
        public string CourseCode { get; set; }
        public string StudentId { get; set; }
    }
    [Table("Course")]
    public  class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CoursTitle { get; set; }
        public double Credit { get; set; }
        public int YearTermId { get; set; }
        public int DepartmentId { get; set; }

    }
    [Table("CurrentSemister")]
    public  class CurrentSemister
    {
        [Key]
        public int CurrentSemId { get; set; }
        public int SessionId { get; set; }
        public int DepartmentId { get; set; }
        public int YearTermId { get; set; }
    }
    [Table("Day")]
    public  class Day
    {
        [Key]
        public int DayId { get; set; }
        public string DayName { get; set; }
    }

    
    [Table("Hall")]
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        public string HallName { get; set; }
    }
    [Table("Department")]
    public  class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public int FacultyId { get; set; }
        public string DepartmentShortName { get; set; }
        public string DepartmentFullName { get; set; }
    }
    [Table("Faculty")]
    public  class Faculty
    {
        [Key]
        public int FacultyId { get; set; }
        public string Name { get; set; }
    }
    [Table("Login")]
    public  class Login
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UniversityId { get; set; }
    }

    [Table("RegistrationDetail")]
        public  class RegistrationDetail
        {
            [Key]
        public int RegistrationDetailId { get; set; }
        public int YearTermId { get; set; }
        public string StudentId { get; set; }
        public double TotalCreditTaken { get; set; }
        public int DeptId { get; set; }
        public string Residential { get; set; }
       [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegisteredDate { get; set; }
    }
    [Table("Result")]
        public  class Result
        {
            [Key]
            public int ResultId { get; set; }
            public string CourseCode { get; set; }
            public string StudentId { get; set; }
            public double? Cgpa { get; set; }
            public int YearTermId { get; set; }
            public int GradeId { get; set; }
        }

   
    [Table("StudentFee")]
    public class StudentFee
    {
        [Key]
        public int FeesId { get; set; }
        public int AdmissionFee { get; set; }
        public int CreditHourFee { get; set; }
        public int StudentTrustFund { get; set; }
        public int HallRent { get; set; }
        public int CentralLibraryFee { get; set; }
        public int SeminarLibraryFee { get; set; }
        public int NonResidentialTransfortFee { get; set; }
        public int ResidentialTransfortFee { get; set; }
        public int BacklogFeePerCourse { get; set; }
        public int LateFee { get; set; }
        public int? Others { get; set; }
    }




    [Table("Routin")]
        public  class Routin
        {
            [Key]
            public int RoutinId { get; set; }
            public int DayId { get; set; }
            public string ClassRoomNo { get; set; }
            public string TeacherId { get; set; }
            public string CourseCode { get; set; }
            public string Time { get; set; }
            public int Duration { get; set; }
            public int DepartmentId { get; set; }
        }
    [Table("Session")]
        public  class Session
        {
            [Key]
            public int SessionId { get; set; }
            public string SessionName { get; set; }
        }

    [Table("TeacherInformation")]
        public  class TeacherInformation
        {
            [Key]
            public int Id { get; set; }
            public string TeasherId { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public int DepartmentId { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Nationality { get; set; }
            public string Religion { get; set; }
            public string PresentAddress { get; set; }
            public string ParmanentAddress { get; set; }
            public string MobileNumber { get; set; }
            public byte[] Image { get; set; }
            public string Residential { get; set; }
        }
    [Table("TeacherLogin")]
        public  class TeacherLogin
        {
            [Key]
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string MobileNumber { get; set; }
        }

    [Table("GpaTbl")]
    public class GpaTbl
    {
        [Key]
        public int GpaId { get; set; }
        public string Gpa { get; set; }
    }
    [Table("YearTerm")]
        public  class YearTerm
        {
            [Key]
            public int Id { get; set; }
            public string YearTermName { get; set; }
        }
    [Table("StudentResultDetails")]
    public class SudentResultViewModel
    {
        [Key]
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string StudentId { get; set; }
        public string YearTermName { get; set; }
        public double? TotalCompletedCredit { get; set; }
        public double? TotalGpa { get; set; }
        public double? Cgpa { get; set; }
    }

    
    public class DeptAutDbContext : DbContext
    {
      
        public DbSet<AdminLoginTable> AdminLoginTables { get; set; }
        public DbSet<Backlog> Backloks { get; set; }
        public DbSet<BacklokRegistration> BacklokRegistrations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CurrentSemister> CurrentSemisters { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<RegistrationDetail> RegistrationDetails { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Routin> Routins { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<StudentInformation> StudentInformations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherInformation> TeacherInformations { get; set; }
        public DbSet<TeacherLogin> TeacherLogins { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<GpaTbl> GpaTbls { get; set; }
        public DbSet<YearTerm> YearTerms { get; set; }
        public DbSet<SudentResultViewModel> ResultViewModels { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<RegistrationDate> RegistrationDates { get; set; }
        
    }
}