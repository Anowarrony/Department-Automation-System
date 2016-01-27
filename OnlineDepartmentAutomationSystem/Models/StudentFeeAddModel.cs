

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentFeeAddModel


    {

   
        
        [Required(ErrorMessage = "Required")]
        [DisplayName("Admission Fee")]
        public int AdmissionFee { get; set; }
        [Display(Name = "Credit Hour Fee")]
        [Required(ErrorMessage = "Required")]
        public int CreditHourFee { get; set; }
        [Display(Name = "Student Trust Fund")]
        [Required(ErrorMessage = "Required")]
        public int StudentTrustFund { get; set; }
        [Display(Name = "Hall Rent")]
        [Required(ErrorMessage = "Required")]
        public int HallRent { get; set; }
        [Display(Name = "Central Library Fee")]
        [Required(ErrorMessage = "Required")]
        public int CentralLibraryFee { get; set; }
        [Display(Name = "Seminar Library Fee")]
        [Required(ErrorMessage = "Required")]
        public int SeminarLibraryFee { get; set; }
        [Display(Name = "NonResidential Transfort Fee")]
        [Required(ErrorMessage = "Required")]
        public int NonResidentialTransfortFee { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Residential Transfort Fee")]
        public int ResidentialTransfortFee { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Late Fee")]
        public int LateFee { get; set; }
        [Display(Name = "Others")]
        public int? Others { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Backlog Fee ")]
        public int BacklogFeePerCourse { get; set; }
    }

    public class StudentFeeViewModel : StudentFeeAddModel { public int Id { get; set; } }

    public class StudentFeeUpdateModel :StudentFeeAddModel
    {
        
    }

}