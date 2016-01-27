using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MvcApplication8.Models;
using OnlineDepartmentAutomationSystem.Models;

namespace OnlineDepartmentAutomationSystem.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        readonly DeptAutDbContext _db = new DeptAutDbContext();

     
        public JsonResult CheckIsStudentIdExist(string studentId)
        {

            return Json(!_db.RegistrationDetails.Any(x => x.StudentId.Equals(studentId)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBacklokDetails()
        {
            double sum = 0;
            var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
            if (u == null) return Json(null);
            var authUsrId = u.UniversityId;
         
         
            var backloks = (from b in _db.Backloks
                join r in _db.BacklokRegistrations on b.CourseCode equals r.CourseCode into rr
                from rg in rr.DefaultIfEmpty()

                join c in _db.Courses on b.CourseCode equals c.CourseCode
                where b.StudentId.Equals(authUsrId)
                where rg.CourseCode == null
                select b.CourseCode).ToList();
            var backlockViewModels=new List<BacklokDetailsViewModel>();
        
            foreach (var i in backloks)
            {

                var mo = new BacklokDetailsViewModel();
                var v = _db.Courses.SingleOrDefault(x => x.CourseCode == i);
                if (v != null)
                {
                    var credit =v.Credit;
                                  
                    mo.CourseTitle= v.CoursTitle;
                    mo.Credit= v.Credit;
                    var yearTrmId = v.YearTermId;
                    var c = _db.YearTerms.SingleOrDefault(x => x.Id == yearTrmId);
                    if (c != null)
                        mo.YearTerm= c.YearTermName;
                    mo.CourseCode = i;
                
                    sum = sum + credit;
                }
                mo.TotalCredit = sum;
                backlockViewModels.Add(mo);
            }


            return Json(backlockViewModels, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLatestRegisteredBacklokDetails()
        {
            var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
            if (u == null) return Json(null);
            var authUsrId = u.UniversityId;


            var lastRegBackloks = (_db.BacklokRegistrations.Join(_db.Courses, b => b.CourseCode, c => c.CourseCode,
                (b, c) => new {b, c})
                .Join(_db.YearTerms, @t => @t.c.YearTermId, yt => yt.Id, (@t, yt) => new {@t, yt})
                .Where(@t => @t.@t.b.StudentId.Equals(authUsrId))
                .Select(@t => new BacklokRegDetViewModel()
                {
                    YearTerm = @t.yt.YearTermName,
                    CourseCode = @t.@t.b.CourseCode,
                    CourseTitle = @t.@t.c.CoursTitle,
                    Credit = @t.@t.c.Credit
                })).ToList();

            var backlockViewModels = lastRegBackloks.Select(i => new BacklokDetailsViewModel
            {
                CourseCode = i.CourseCode,
                CourseTitle = i.CourseTitle,
                Credit = i.Credit,
                YearTerm = i.YearTerm
            }).ToList();

            return Json(backlockViewModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
          
            var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var authUsrId = u.UniversityId;

                var authUserId = _db.Logins.Single(m => m.Username.Equals(User.Identity.Name)).UniversityId;
                ViewData["profileInfo"] = (from s in _db.StudentInformations
                    join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                    join f in _db.Faculties on s.FacultyId equals f.FacultyId
                    join ss in _db.Sessions on s.SessionId equals ss.SessionId
                    where s.StudentId.Equals(authUserId)
                    select new StudentAccountInormation
                    {
                        Name = s.Name,
                        Id = s.StudentId,
                        Email = s.Email,
                        Gender = s.Gender,
                        Nationality = s.Nationality,
                        Department = d.DepartmentFullName,
                        Faculty = f.Name,
                        Session = ss.SessionName,
                        Phonenumber = s.Phone,
                        PresentAddress = s.PresentAddress,
                        ParmanentAddress = s.ParmanentAddress

                    }).ToList();
                
                
                   
                
                
                

                ViewData["backloks"] = (from b in _db.Backloks
                    join r in _db.BacklokRegistrations on b.CourseCode equals r.CourseCode into rr
                    from rg in rr.DefaultIfEmpty()

                    join c in _db.Courses on b.CourseCode equals c.CourseCode
                    where b.StudentId.Equals(authUsrId)
                    where rg.CourseCode == null
                    select b.CourseCode).ToList();

                ViewData["lastRegBackloks"] = (from b in _db.BacklokRegistrations
                    join c in _db.Courses on b.CourseCode equals c.CourseCode
                    join yt in _db.YearTerms on c.YearTermId equals yt.Id



                    where b.StudentId.Equals(authUsrId)


                    select new BacklokRegDetViewModel() { YearTerm = yt.YearTermName, CourseCode = b.CourseCode, CourseTitle = c.CoursTitle, Credit = c.Credit }).ToList();

              

               var data = from c in _db.Courses
                                      join r in _db.Results on c.CourseCode equals r.CourseCode
                                      join s in _db.StudentInformations on r.StudentId equals s.StudentId
                                      join dd in _db.Departments on s.DepartmentId equals dd.DepartmentId
                                      join y in _db.YearTerms on c.YearTermId equals y.Id
                                      join g in _db.Grades on r.GradeId equals g.GradeId
                                      where r.StudentId.Equals(authUserId)
                                      group new { r, dd, s, y, g, c } by new { r.StudentId, DepartmentName = dd.DepartmentShortName, s.Name, y.YearTermName } into gr
                                      select new ResultDisplaywModel
                                      {
                                    CurrentYearTermsTotalCredit=gr.Sum(x=>x.c.Credit),
                                          YearTerm = gr.Key.YearTermName,
                                          YearTermCgpa = gr.Sum(m => m.g.GradePoint * m.c.Credit) / gr.Sum(m => m.c.Credit)
                                      };

                ViewData["studentResult"] = data;
               ViewBag.count = data.Count();


                var v = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authUserId));
                if (v != null)
                {

                    var authStuDepartmentId = v.DepartmentId;
                    var startDate =
                         _db.RegistrationDates.SingleOrDefault(m => m.DepartmentId.Equals(authStuDepartmentId));
                    if (startDate != null)
                    {




                        var srtd = startDate.StartDate.Day.ToString();
                        var srtm = startDate.StartDate.Month.ToString();
                        var srty = startDate.StartDate.Year.ToString();

                        var endd = startDate.EndDate.Day.ToString();
                        var endm = startDate.EndDate.Month.ToString();
                        var endy = startDate.EndDate.Year.ToString();

                        var curtd = DateTime.Now.Day.ToString();
                        var curtm = DateTime.Now.Month.ToString();
                        var curty = DateTime.Now.Year.ToString();

                        ViewBag.startdate = startDate.StartDate;
                        ViewBag.lastDate = startDate.EndDate;
                        ViewBag.daysLeft =
                            (Convert.ToDateTime(startDate.EndDate.ToShortDateString()) -
                             Convert.ToDateTime(DateTime.Now.ToShortDateString())).TotalDays;
                        if (Convert.ToInt32(curtd) >= Convert.ToInt32(srtd) && Convert.ToInt32(curtm) >= Convert.ToInt32(srtm) && Convert.ToInt32(curty) >= Convert.ToInt32(srty) && Convert.ToInt32(curtd) <= Convert.ToInt32(endd) && Convert.ToInt32(curtm) <= Convert.ToInt32(endm) && Convert.ToInt32(curty) <= Convert.ToInt32(endy))
                        {

                            var checkIsAuthStudentIsRegisteredForCurrentSemister =
                                _db.RegistrationDetails.Any(m => m.StudentId.Equals(authUserId));
                            if (!checkIsAuthStudentIsRegisteredForCurrentSemister)
                            {
                                ViewBag.AlertForRegistrationId = 1;
                            }

                          

                        }

                    }



                }

            }

            ViewData["feeDetails"] = _db.StudentFees.Select(m => new StudentFeeViewModel()
            {
                Id = m.FeesId,
                AdmissionFee = m.AdmissionFee,
                CreditHourFee = m.CreditHourFee
            ,
                StudentTrustFund = m.StudentTrustFund,
                HallRent = m.HallRent,
                CentralLibraryFee = m.CentralLibraryFee,
                SeminarLibraryFee = m.SeminarLibraryFee,
                NonResidentialTransfortFee = m.NonResidentialTransfortFee,
                ResidentialTransfortFee = m.ResidentialTransfortFee,
                LateFee = m.LateFee,
                Others = m.Others,
                BacklogFeePerCourse = m.BacklogFeePerCourse
            });
            ViewBag.hide = 1;
            return View();
        }

        public ActionResult UpdateAccount()
        {
            var ua = new UpdateAccount();
            var u = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                string authId = u.UniversityId;
                var v = _db.StudentInformations.FirstOrDefault(x => x.StudentId.Equals(authId));
                if (v != null)
                    ua.Email = v.Email;
                var w = _db.StudentInformations.FirstOrDefault(x => x.StudentId.Equals(authId));
                if (w != null)
                    ua.PresentAddress = w.PresentAddress;
                var c = _db.StudentInformations.FirstOrDefault(x => x.StudentId.Equals(authId));
                if (c != null)
                    ua.PhoneNumber = c.Phone;
            }

            return View(ua);
        }
        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccount ua)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var u = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));
                    if (u != null)
                    {
                        var authId = u.UniversityId;

                        var si = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authId));
                        if (si != null)
                        {
                            si.Email = ua.Email;
                            si.PresentAddress = ua.PresentAddress;
                            si.Phone = ua.PhoneNumber;
                            _db.Entry(si).State = EntityState.Modified;
                        }
                    }
                    _db.SaveChanges();
                    ViewBag.successNotificationId = 1;
                }
                catch (Exception e)
                {

                    Response.Write(e.Message);
                }
            }
            return View();
        }

        public ViewResult GetCurrentRegInfo()
        {
            var u = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var id = u.UniversityId;
                ViewData["RegInfo"] =
                    _db.RegistrationDetails.Join(_db.YearTerms, rd => rd.YearTermId, yt => yt.Id,
                        (rd, yt) => new {rd, yt})
                        .Join(_db.Departments, @t => @t.rd.DeptId, de => de.DepartmentId, (@t, de) => new {@t, de})
                        .Where(@t => @t.@t.rd.StudentId.Equals(id))
                        .Select(@t => new StudentRegInfoViewModel()
                        {
                            Id = @t.@t.rd.StudentId,
                            YearTerm = @t.@t.yt.YearTermName,
                            TotlaCreditTaken = @t.@t.rd.TotalCreditTaken,
                            DepartmentName = @t.de.DepartmentShortName
                        });

                ViewData["coursesOnCurrentSemister"] =
                    _db.Courses.Join(_db.RegistrationDetails, c => c.YearTermId, rd => rd.YearTermId,
                        (c, rd) => new {c, rd})
                        .Where(@t => @t.rd.StudentId.Equals(id))
                        .Select(@t => new StudentRegInfoViewModel()
                        {
                            CourseTitle = @t.c.CoursTitle,
                            RegisyeredCourse = @t.c.CourseCode,
                            Credit = @t.c.Credit
                        });
            }


            return View();
        }


        private string GetAuthStudentId( )
        {

           return _db.Logins.Single(user => user.Username.Equals(User.Identity.Name)).UniversityId;
        }
       
        

        public ViewResult RegisterStudentCourse()
        {
            try


            {

              
                ViewBag.halls = _db.Halls.Select(m => m.HallName).ToList();
            var u = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var authUserId = u.UniversityId;
                var v = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authUserId));
                if (v != null)
                {
                  
                    var authStuDepartmentId = v.DepartmentId;
                   var regDate =
                        _db.RegistrationDates.SingleOrDefault(m => m.DepartmentId.Equals(authStuDepartmentId));

                    if (regDate != null)
                    {
                        var dateDifference = new DateDifference(DateTime.Now, regDate.EndDate);

                        var dateDiffBetStartDateAndCurrentDate = new DateDifference(DateTime.Now, regDate.StartDate);
                        if (dateDiffBetStartDateAndCurrentDate.Days==0)
                        {
                            var registrationDate =
                               _db.RegistrationDates.Single(x => x.DepartmentId.Equals(authStuDepartmentId));
                            registrationDate.AllowReg = 1;
                            _db.Entry(registrationDate).State = EntityState.Modified;
                            _db.SaveChanges();
                        }


                        var dayesLeft = dateDifference.Days ;
                        var isRegAllow = _db.RegistrationDates.Any(m => m.AllowReg.Value.Equals(1));
                        if (isRegAllow)
                        {
                            var checkIsAuthStudentIsRegisteredForCurrentSemister =
                                        _db.RegistrationDetails.Any(m => m.StudentId.Equals(authUserId));

                            if (!checkIsAuthStudentIsRegisteredForCurrentSemister)
                            {
                                ViewBag.AllowRegistrationProcessId = 1;
                            }
                            else
                            {
                                ViewBag.NotAllowRegistrationProcessId = 1;
                            }
                        }
                       
                        if (dayesLeft == 0)
                        {
                            var registrationDate =
                                _db.RegistrationDates.Single(x => x.DepartmentId.Equals(authStuDepartmentId));
                            registrationDate.DaysLeft = dayesLeft;
                            _db.Entry(registrationDate).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        var getDaysLeft =
                            _db.RegistrationDates.Single(x => x.DepartmentId.Equals(authStuDepartmentId)).DaysLeft;
                        if (getDaysLeft!=null)
                        {
                            if (dayesLeft == 1 && getDaysLeft.Value==0)
                        {
                            ViewBag.RegistrationDateExpioredNotificationId = 1;
                        } 
                        }
                       
           
                    }
                   
              
                
                    
                }
            }

            }
            catch (Exception e)
            {
                
               
            }
          
    
         DisplayDepartmentSelectList();
         DisplaySessionSelectList();
       
             return View();
        }
        [HttpPost]
        public ViewResult RegisterStudentCourse(StudentRegistrationViewModel model)
        {
            var u = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var authUserId = u.UniversityId;

                ViewBag.halls = _db.Halls.Select(m => m.HallName).ToList();
       
                DisplayDepartmentSelectList();
                DisplaySessionSelectList();
                if (ModelState.IsValid)
                {
                    var v = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authUserId));
                    if (v != null)
                    {
                        var sesId =
                            v.SessionId;
                        var dpId =
                            v.DepartmentId;

                        if (sesId == Convert.ToInt32(model.Session))
                        {
                            if (dpId == Convert.ToInt32(model.Department))
                            {
                                if (authUserId == model.StudentId)
                                {
                                    if (model.Residensial)
                                    {
                                        if (model.HallName != null)
                                        {
                                            var c =
                                                _db.CurrentSemisters.SingleOrDefault(
                                                    m => m.SessionId.Equals(sesId) && m.DepartmentId.Equals(dpId));
                                            if (c != null)
                                            {
                                                var ytId = c.YearTermId;
                                                var checkIsalreadyRegistered =
                                                    _db.RegistrationDetails.Any(
                                                        m =>
                                                            m.StudentId.Equals(model.StudentId) &&
                                                            m.YearTermId.Equals(ytId));
                                                if (!checkIsalreadyRegistered)
                                                {
                                                    var z = _db.YearTerms.SingleOrDefault(m => m.Id.Equals(ytId));
                                                    if (z != null)
                                                    {
                                                        var ytrName = z.YearTermName;

                                                        var totalCredit = _db.Courses.Where(
                                                            m => m.YearTermId.Equals(ytId))
                                                            .Sum(m => m.Credit);
                                                        var rgd = new RegistrationDetail
                                                        {
                                                            YearTermId = ytId,
                                                            DeptId = Convert.ToInt32(model.Department),
                                                            StudentId = model.StudentId,
                                                            Residential = model.HallName,
                                                            TotalCreditTaken = totalCredit,
                                                            RegisteredDate = DateTime.Now
                                                        };
                                                        _db.RegistrationDetails.Add(rgd);
                                                        _db.SaveChanges();
                                                        ViewBag.successMsg = "You have Successfully Registered for " +
                                                                             ytrName;
                                                    }
                                                    ViewBag.successDialogId = 1;
                                                }
                                                else
                                                {
                                                    ModelState.AddModelError("",
                                                        "* It seems you already have registered!  ");
                                                }
                                            }
                                            
                                        }
                                        else
                                        {
                                            ViewBag.hallDropdownShowId = 1;
                                            ModelState.AddModelError("",
                                                "Please select hallname where you are staying.If you are not staying in hall then uncheck again.");
                                        }
                                    }
                                    else
                                    {
                                        var c =
                                            v.HallId;
                                        try
                                        {
                                            if (c == null)
                                            {
                                                var p =
                                                    _db.CurrentSemisters.SingleOrDefault(m => m.SessionId.Equals(sesId));
                                                if (p != null)
                                                {
                                                    var ytId = p.YearTermId;

                                                    var totalCredit = _db.Courses.Where(m => m.YearTermId.Equals(ytId))
                                                        .Sum(m => m.Credit);
                                                    var rgd = new RegistrationDetail
                                                    {
                                                        YearTermId = ytId,
                                                        DeptId = Convert.ToInt32(model.Department),
                                                        StudentId = model.StudentId,
                                                        TotalCreditTaken = totalCredit,
                                                        RegisteredDate = DateTime.Now
                                                    };

                                                    _db.RegistrationDetails.Add(rgd);
                                                }
                                                _db.SaveChanges();
                                                ViewBag.successMsg = "Successfully Registered";

                                                ViewBag.successDialogId = 1;
                                            }
                                            else
                                            {
                                                ViewBag.AllowRegistrationProcessId = 1;
                                                ModelState.AddModelError("",
                                                    "It seems you are a staying in hall . So you need to check Residential button.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ModelState.AddModelError("", ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    ViewBag.stdIdErrMsg = "It seems you entered a wrong Id.Please enter your valid Id";
                                }
                            }
                            else
                            {
                                ViewBag.DptErrorMessage = "It seems that your have chosen a wrong department";
                            }
                        }

                        else
                        {
                            ViewBag.sessErMsg =
                                "It seems your entered session is not yours.Please choose your session correctly";
                        }
                    }
                   
                }
                
            }
            return View();
        }

        private void DisplayDepartmentSelectList()
        {
            var departments = _db.Departments.Select(c => new
            {
                DeptId = c.DepartmentId,

                Department = c.DepartmentShortName.Equals(null)?c.DepartmentFullName:c.DepartmentShortName
            }).ToList();
            ViewBag.departments = new SelectList(departments, "DeptId", "Department");

        }

        private void DisplaySessionSelectList()
        {
            var sessions = _db.Sessions.Select(c => new
            {
                Id = c.SessionId,

                Session = c.SessionName
            }).ToList();
            ViewBag.sessions = new SelectList(sessions, "Id", "Session");

        }

        public ViewResult GetYearTerms()
        { 
              var authStudentId = GetAuthStudentId();
            var authStudentdepartmentId =
             _db.StudentInformations.Single(m => m.StudentId.Equals(authStudentId)).DepartmentId;

            ViewBag.YearTermsList = (from yt in _db.YearTerms
                join c in _db.Courses on yt.Id equals c.YearTermId

                where c.DepartmentId.Equals(authStudentdepartmentId


                    )
                select new YearTermViewModel {YearTerm = yt.YearTermName}).Distinct();
            
            
            
            return View();
        }

        public ActionResult GetCourseDetailsByAuthStudentDepartment(string yearTerm)
     {
         var authStudentId = GetAuthStudentId();
         var authStudentdepartmentId =
             _db.StudentInformations.Single(m => m.StudentId.Equals(authStudentId)).DepartmentId;

         ViewData["CourseDetailsByAuthStudentDepartmen"] = (from c in _db.Courses
             join yt in _db.YearTerms on c.YearTermId equals yt.Id
             where c.DepartmentId.Equals(authStudentdepartmentId)
             where yt.YearTermName.Equals(yearTerm)
             select new CourseViewModel
             {
                 CourseCode = c.CourseCode,
                 CourseTitle = c.CoursTitle,
                 Credit = c.Credit
                
             });
            ViewBag.yearTerm = yearTerm;
        
         
         

         return View(
             );

     }
        public ActionResult GetRegistrationForm()
        {

            var v = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));

            if (v != null)
            {
                var authStudentId = v.UniversityId;
                var c = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authStudentId));
                if (c != null)
                    ViewBag.studentImage =
                        c.StudentImage;
                var u = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authStudentId));
                if (u != null)
                {
                    var w = _db.Sessions.SingleOrDefault(m => m.SessionId.Equals(u.SessionId));
                    if (w != null)
                    {
                        try
                        {
                            ViewData["TotalCredits"]=(

                                from co in _db.Courses
                                join d in _db.Departments on co.DepartmentId equals d.DepartmentId
                                join si in _db.StudentInformations on d.DepartmentId equals si.DepartmentId
                                join se in _db.Sessions on si.SessionId equals se.SessionId
                                join yt in _db.YearTerms on co.YearTermId equals yt.Id
                                join cs in _db.CurrentSemisters on yt.Id equals cs.YearTermId
                                where si.StudentId == authStudentId
                                where cs.SessionId == w.SessionId
                                select co.Credit).Sum();






                            ViewData["regDetails"] =(

                                from si in _db.StudentInformations
                                join s in _db.Sessions on si.SessionId equals s.SessionId

                                join d in _db.Departments on si.DepartmentId equals d.DepartmentId

                                join cs in _db.CurrentSemisters on s.SessionId equals cs.SessionId
                                 join y in _db.YearTerms on cs.YearTermId equals y.Id
                             
                              where si.StudentId .Equals(authStudentId)
                                    where cs.SessionId.Equals(w.SessionId)
                                               
                                             select new RegistrationDetailsViewModel()
                                    {
                                        Session = s.SessionName,
                                        YearTerm = y.YearTermName,
                                        Department = d.DepartmentFullName,
                                       
                                        Name = si.Name,
                                        Roll = si.StudentId
                                      
                                    }).ToList();
                            var h = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authStudentId));
                            ;
                            if (
                           h!=null)
                            {
                                var hallId =h.HallId;
                                ViewBag.hallName = _db.Halls.Find(hallId).HallName;
                            }
                            
                                //(from si in _db.StudentInformations
                                //    join d in _db.Departments on si.DepartmentId equals d.DepartmentId
                                //    join s in _db.Sessions on si.SessionId equals s.SessionId
                                //    join rd in _db.RegistrationDetails on si.StudentId equals rd.StudentId
                                //    join cs in _db.CurrentSemisters on si.SessionId equals cs.SessionId
                                //    join yt in _db.YearTerms on cs.YearTermId equals yt.Id
                                //    join h in _db.Halls on si.HallId equals h.Id
                                //    where si.StudentId == authStudentId
                                //    where cs.SessionId == w.SessionId
                                //    where yt.Id ==1
                                //    select new RegistrationDetailsViewModel()
                                //    {
                                //        Session = s.SessionName,
                                //        YearTerm = yt.YearTermName,
                                //        Department = d.DepartmentShortName,
                                //        Hall = h.HallName,
                                //        Name = si.Name,
                                //        Roll = si.StudentId,
                                //        Credit = rd.TotalCreditTaken
                                //    }).ToList();

                            var getStudentFees = _db.StudentFees.ToList();
                            foreach (var studentFee in getStudentFees)
                            {
                                ViewBag.admissionFee = studentFee.AdmissionFee;
                                ViewBag.creditHour = studentFee.CreditHourFee;
                                ViewBag.studentTrustFund = studentFee.StudentTrustFund;
                                ViewBag.hallRent = studentFee.HallRent;
                                ViewBag.centralLibraryFee = studentFee.CentralLibraryFee;
                                ViewBag.seminarLibraryFee = studentFee.SeminarLibraryFee;
                                ViewBag.nonResidentialTransfortFee = studentFee.NonResidentialTransfortFee;
                                ViewBag.residentialTransfortFee = studentFee.ResidentialTransfortFee;
                                ViewBag.lateFee = studentFee.LateFee;
                                ViewBag.others = studentFee.Others;
                            }

                            var checkIsAuthStudentHasBacklog =
                                _db.BacklokRegistrations.Any(m => m.StudentId.Equals(authStudentId));
                            if (checkIsAuthStudentHasBacklog)
                            {
                                if (_db.BacklokRegistrations != null)
                                {
                                    var backlogFeePerCourse = _db.StudentFees.Select(m => m.BacklogFeePerCourse).Max();
                                    var count =
                                        _db.BacklokRegistrations.Where(m => m.StudentId.Equals(authStudentId))
                                            .Select(m => m.CourseCode)
                                            .Count();

                                    ViewBag.backlogCredits = count*backlogFeePerCourse;
                                }
                               
                            }
                         
                        }
                        catch (Exception exception)
                        {
                        }

                        
                    }

                   
                }
           
            }
            return View();
        }
        public ActionResult Print()
        {
            var v = _db.Logins.SingleOrDefault(m => m.Username.Equals(User.Identity.Name));

            if (v != null)
            {
                var authStudentId = v.UniversityId;
                var u = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authStudentId));
                if (u != null)
                {
                    var w = _db.Sessions.SingleOrDefault(m => m.SessionId.Equals(u.SessionId));
                    if (w != null)
                    {
                        ViewData["regDetails"] =
                            (from si in _db.StudentInformations
                                join d in _db.Departments on si.DepartmentId equals d.DepartmentId
                                join s in _db.Sessions on si.SessionId equals s.SessionId
                                join rd in _db.RegistrationDetails on si.StudentId equals rd.StudentId
                                join cs in _db.CurrentSemisters on si.SessionId equals cs.SessionId
                                join yt in _db.YearTerms on cs.YearTermId equals yt.Id
                                join h in _db.Halls on si.HallId equals h.Id
                                where si.StudentId == authStudentId
                                where cs.SessionId == w.SessionId
                                select new RegistrationDetailsViewModel()
                                {
                                    Session = s.SessionName,
                                    YearTerm = yt.YearTermName,
                                    Department = d.DepartmentShortName,
                                    Hall = h.HallName,
                                    Name = si.Name,
                                    Roll = si.StudentId,
                                    Credit = rd.TotalCreditTaken
                                }).ToList();

                        var getStudentFees = _db.StudentFees.ToList();
                        foreach (var studentFee in getStudentFees)
                        {
                            ViewBag.admissionFee = studentFee.AdmissionFee;
                            ViewBag.creditHour = studentFee.CreditHourFee;
                            ViewBag.studentTrustFund = studentFee.StudentTrustFund;
                            ViewBag.hallRent = studentFee.HallRent;
                            ViewBag.centralLibraryFee = studentFee.CentralLibraryFee;
                            ViewBag.seminarLibraryFee = studentFee.SeminarLibraryFee;
                            ViewBag.nonResidentialTransfortFee = studentFee.NonResidentialTransfortFee;
                            ViewBag.residentialTransfortFee = studentFee.ResidentialTransfortFee;
                            ViewBag.lateFee = studentFee.LateFee;
                            ViewBag.others = studentFee.Others;
                        }

                        var checkIsAuthStudentHasBacklog =
                            _db.BacklokRegistrations.Any(m => m.StudentId.Equals(authStudentId));
                        if (!checkIsAuthStudentHasBacklog) return View("GetRegistrationForm");
                        if (_db.BacklokRegistrations == null) return View("GetRegistrationForm");
                        var backlogFeePerCourse = _db.StudentFees.Select(m => m.BacklogFeePerCourse).Max();
                        var count =
                            _db.BacklokRegistrations.Where(m => m.StudentId.Equals(authStudentId))
                                .Select(m => m.CourseCode)
                                .Count();

                        ViewBag.backlogCredits = count*backlogFeePerCourse;


                        return PartialView("_PrintView");
                    }


                 
                }
               
            }
            return View("GetRegistrationForm");
        }
        public ActionResult RegisterBacklocCourse(int id)
        {
            var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var authUsrId = u.UniversityId;

        


                var v = _db.YearTerms.SingleOrDefault(m => m.Id.Equals(id));
                if (v != null)
                    ViewBag.ytrName = v.YearTermName;

                ViewData["backloks"] = (from b in _db.Backloks
                    join r in _db.BacklokRegistrations on b.CourseCode equals r.CourseCode into rr
                    from rg in rr.DefaultIfEmpty()

                    join c in _db.Courses on b.CourseCode equals c.CourseCode
                    where c.YearTermId.Equals(id)
                    where b.StudentId.Equals(authUsrId)
                    where rg.CourseCode == null
                    select b.CourseCode).ToList();
            }


            foreach (var c in (IEnumerable)ViewData["backloks"])
                    {

                        ViewBag.noBacklomsg = c;

                    }
             

                ViewBag.dpt = _db.Departments.Select(x => x.DepartmentShortName);


            

            return View();
        }


        public ActionResult Load()
        {
            ViewData["op"] = 1;
      
            try
            {
                DisplayYearTermSelectList();
                ViewBag.m = _db.Backloks.Where(x => x.StudentId.Equals(null)).Select(n => n.CourseCode);
                ViewBag.yearTrm = _db.YearTerms.Select(m => m.YearTermName);
                ViewBag.dpt = _db.Departments.Select(x => x.DepartmentShortName);
             

            }
            catch (Exception e)
            {

                Response.Write(e.Message);
            }
           
            return View();


        }
        [HttpPost]
        public ActionResult Load(YearTermViewModel yt)
        {
            ViewData["op"] = 1;
   

            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.m = _db.Backloks.Where(x => x.StudentId.Equals(null)).Select(n => n.CourseCode);
                    ViewBag.yearTrm = _db.YearTerms.Select(m => m.YearTermName);
                    ViewBag.dpt = _db.Departments.Select(x => x.DepartmentShortName);
                   
                    TempData["YtrId"] = yt.YearTerm;

                    return RedirectToAction("RegisterBacklocCourse", "Student", new { id = yt.YearTerm });


       


                }
                catch (Exception e)
                {

                    Response.Write(e.Message);
                }

            }
            DisplayYearTermSelectList();
            return View();


        }

        private void DisplayYearTermSelectList()
        {
            var yearTerm = _db.YearTerms.Select(c => new
            {
                CategoryId = c.Id,

                YearTerm = c.YearTermName
            }).ToList();

            ViewBag.yearTrms = new SelectList(yearTerm, "CategoryId", "YearTerm");
           
        }

        public JsonResult DisplayCat(string yearTerm)
        {
            var u = _db.YearTerms.FirstOrDefault(m => m.YearTermName.Equals(yearTerm));
            if (u == null) return Json(ViewBag.list, JsonRequestBehavior.AllowGet);
            var ytId = u.Id;


            ViewBag.list = _db.Backloks.Where(m => m.YearTermId.Equals(ytId)).Select(m => m.YearTermId);


            return Json(ViewBag.list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RegisterBacklocCourse(BacklokRegisterModel model, string[] backLoks,int id)
        {
            var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
            if (u != null)
            {
                var authUsrId = u.UniversityId;

                ViewBag.l = 1;
                ViewData["op"] = 2;

           
                if (backLoks == null)
                {
                    ViewData["reqErrorMsg"] = "* Please Select Your Backloks";

                }
                else
                {
                    var b = new BacklokRegistration();
          
                    double sum = 0;
                    IEnumerable<string> regbackloks = backLoks;

                    ViewData["regbackloks"] = regbackloks;

                    var backlockRegistrationViewModels = new List<BacklokRegDetViewModel>();

                    foreach (var backLok in backLoks)
                    {

                        b.CourseCode = backLok;
                        b.StudentId = authUsrId;
                        _db.BacklokRegistrations.Add(b);
                        _db.SaveChanges();

                        var v = _db.Courses.SingleOrDefault(m => m.CourseCode.Equals(backLok));
                        if (v != null)
                        {
                            var courseTitle = v.CoursTitle;

                            var credit = v.Credit;
                            sum = sum + credit;


                            var mo = new BacklokRegDetViewModel
                            {
                                CourseCode = backLok,
                                CourseTitle = courseTitle,
                                Credit = credit
                            };
                            backlockRegistrationViewModels.Add(mo);
                        }
                    }
                    IEnumerable<BacklokRegDetViewModel> c = backlockRegistrationViewModels;
                    ViewData["regBacDetails"] = c;
                    ViewBag.totalCredits = sum;
                    ViewData["successMsg"] = 1;

                }

          
                ViewData["backloks"] = (from b in _db.Backloks
                    join r in _db.BacklokRegistrations on b.CourseCode equals r.CourseCode into rr
                    from rg in rr.DefaultIfEmpty()

                    join c in _db.Courses on b.CourseCode equals c.CourseCode
                    where c.YearTermId.Equals(id)
                    where b.StudentId.Equals(authUsrId)
                    where rg.CourseCode == null
                    select b.CourseCode).ToList();
            }

            foreach (var c in (IEnumerable)ViewData["backloks"])
            {

                ViewBag.noBacklomsg = c;

            }








            return View();
        }
        public ActionResult UpdateStudentImage()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UpdateStudentImage(StudentImageUploadViewModel model)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                var u = _db.Logins.SingleOrDefault(x => x.Username.Equals(User.Identity.Name));
                if (u != null)
                {
                    var authUsrId = u.UniversityId;

                    var data = new byte[model.File.ContentLength];

                    model.File.InputStream.Read(data, 0, model.File.ContentLength);

                    var s = _db.StudentInformations.SingleOrDefault(m => m.StudentId.Equals(authUsrId));
                    if (s != null)
                    {
                        s.StudentImage = data;
                        _db.Entry(s).State = EntityState.Modified;
                    }
                }
                _db.SaveChanges();

                ViewBag.successMsg = "Image Updated Successfully";
                ViewBag.successDialogId = 1;
            }
            catch (Exception e)
            {

                Response.Write(e.Message);
            }
            return View();
        }

        public ActionResult GetTranscript(int id = 0)
        {
         
    
           var authStudentId = _db.Logins.Single(m => m.Username.Equals(User.Identity.Name)).UniversityId;
            ViewData["ExmainationDetails"] = from c in _db.Courses
                join r in _db.Results on c.CourseCode equals r.CourseCode
                join g in _db.Grades on r.GradeId equals g.GradeId
                join y in _db.YearTerms on r.YearTermId equals y.Id
                join s in _db.StudentInformations on r.StudentId equals s.StudentId
                join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                where r.YearTermId.Equals(id)
                where r.StudentId.Equals(authStudentId)
                select new ExaminationDetailsViewMode
                {
                    CourseCode = c.CourseCode,
                    CourseTitle = c.CoursTitle,
                    Credit = c.Credit,
                    GradePoint = g.GradePoint,
                    LetterGrade = g.LetterGrade

                };

            ViewBag.studentName = User.Identity.Name.ToUpper();
            var deptId=_db.StudentInformations.Single(m => m.StudentId.Equals(authStudentId)).DepartmentId;
            ViewBag.deptName = _db.Departments.Single(m => m.DepartmentId.Equals(deptId)).DepartmentShortName;          
            var sessionId= _db.StudentInformations.Single(m => m.StudentId.Equals(authStudentId)).SessionId;
            ViewBag.sessionId = _db.Sessions.Single(m => m.SessionId.Equals(sessionId)).SessionName;
          ViewBag.yearTerm = _db.YearTerms.Single(m => m.Id.Equals(id)).YearTermName;
            ViewBag.studentRoll = authStudentId;
            var yearTerm = _db.YearTerms.Single(m => m.Id.Equals(id)).YearTermName;
       


            ViewBag.TermGPA = (from c in _db.Courses
                join r in _db.Results on c.CourseCode equals r.CourseCode
                join s in _db.StudentInformations on r.StudentId equals s.StudentId
                join dd in _db.Departments on s.DepartmentId equals dd.DepartmentId
                join y in _db.YearTerms on c.YearTermId equals y.Id
                join g in _db.Grades on r.GradeId equals g.GradeId
                where r.StudentId.Equals(authStudentId)
                where y.YearTermName.Equals(yearTerm)
                group new {r, dd, s, y, g, c} by new {r.StudentId, DepartmentName = dd.DepartmentShortName, s.Name, y.YearTermName}
                into gr
                select gr.Sum(x => x.g.GradePoint*x.c.Credit)/gr.Sum(x => x.c.Credit));
            
            
            
            
            return View();
        }

        
    }

   
}
