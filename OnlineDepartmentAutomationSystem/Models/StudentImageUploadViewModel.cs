using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using OnlineDepartmentAutomationSystem.Models;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class StudentImageUploadViewModel
    {
        
        [ValidateImage]
        public HttpPostedFileBase File { get; set; }

    }

    public class ValidateImage : ValidateFileAttribute
    {
        public override bool IsValid(object value)
        {
            string[] AllowedFileExtensions = new string[] { ".jpg" };
            var file = (HttpPostedFileBase) value;
            if (file==null)
            {
                ErrorMessage= "* Please Upload Your Image";
                return false;
            }
            if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }


            return true;
        }
    }
}