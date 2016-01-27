using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentMetadata
    {
  
        [Required(ErrorMessage = "* Required!")]
        public string StudentUniversityId { get; set; }
        [Required(ErrorMessage = "* Required!")]
        [MinLength(4, ErrorMessage = "At least 4 characters required!")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [DisplayName("Name :")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Required!")]
        public string Department { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Session { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Nationality { get; set; }
        [Required]
        public string Phone { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email")]
        
        [Required(ErrorMessage = "* Required!")]
        public string Email { get; set; }

      
        [Required(ErrorMessage = "* Required!")]
        public string PresentAddress { get; set; }
        [Required(ErrorMessage = "* Required!")]
        public string ParmanentAddress { get; set; }


        public byte[] Image { get; set; }

        [ValidateFile]
        public HttpPostedFileBase File { get; set; }

        public string HallName { get; set; }
    }

    public class StudentDetailsViewModel : StudentMetadata
    {
        
        public int StudentId { get; set; }
        public string Faculty { get; set; }
        public string Residentiality { get; set; }
    }

    public class StudentUpdateModel 
    {
       
    
        [Required(ErrorMessage = "* Required!")]
        public string Phone { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email")]

        [Required(ErrorMessage = "* Required!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "* Required!")]
        public string PresentAddress { get; set; }

        public byte[] Image { get; set; }
    
        public string HallName { get; set; }
      

    }
    public class TeacherMetaDataClass
    {   [Required(ErrorMessage = "* Required")]
        [MinLength(4, ErrorMessage = "At least 4 characters required!")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [DisplayName("Firstname :")]
        public string Firstname { get; set; }
       [MinLength(4, ErrorMessage = "At least 4 characters required!")]
       [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
       [DisplayName("Lastname :")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "* Required")]
        public int DepartmentId { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Religion { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string PresentAddress { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string ParmanentAddress { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string MobileNumber { get; set; }
       
        public byte[] Image { get; set; }
    }
   

    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 3; //3 MB
            string[] AllowedFileExtensions = new string[] { ".jpg",".jpeg" ,".JPEG"};

            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                ErrorMessage = "* Please Upload an Image.";
                return false;
            }

            if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }

            if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = "Your Photo is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }


            return true;
        }

    }
}