using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDepartmentAutomationSystem.Models
{
    public class RegisterMetaDataClass
    {
        
        [Required(ErrorMessage = "* Required! ")]
        public string StudentID { get; set; }
    }
    //[MetadataType(typeof(RegisterMetaDataClass))]
    //public partial class RegistrationDetail
    //{
         
    //}

    public class BacklockViewModel
    {
        public IEnumerable<SelectListItem> BacklocItems { get; set; }
        public IEnumerable<string> SelectedBacklokItems { get; set; }
    }

    //public class BacklockRegistration
    //{
    //    public string StudentId { get; set; }
    //}
    //[MetadataType(typeof(BacklockRegistration))]
    public partial class BacklocRegTable
    {

    }
}