using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using OnlineDepartmentAutomationSystem.Models;
using Login=OnlineDepartmentAutomationSystem.Models.Login;

namespace OnlineDepartmentAutomationSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly DeptAutDbContext _db = new DeptAutDbContext();

        public JsonResult CheckIsUsernameExist(string username)
        {

            return Json(!_db.Logins.Any(x => x.Username.Equals(username)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSemisters()
        {
            var getCurrentSemisters =
                _db.CurrentSemisters.Join(_db.Departments, cs => cs.DepartmentId, d => d.DepartmentId,
                    (cs, d) => new { cs, d })
                    .Join(_db.Sessions, @t => @t.cs.SessionId, s => s.SessionId, (@t, s) => new { @t, s })
                    .Join(_db.YearTerms, @t => @t.@t.cs.YearTermId, y => y.Id, (@t, y) => new CurrentSemisterViewModel
                    {
                        Id = @t.@t.cs.CurrentSemId,
                        Department = @t.@t.d.DepartmentShortName
                        ,
                        Session = @t.s.SessionName,
                        YearTerm = y.YearTermName
                    });

            return Json(getCurrentSemisters, JsonRequestBehavior.AllowGet);
        }


        public void DeleteBaclog(int id)
        {
            try
            {
                var b = _db.Backloks.Find(id);
                _db.Entry(b).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception exception)
            {
                Response.Write(exception.Message);
            }
        }
        public ActionResult DeleteCourse(int id)
        {
            var course = _db.Courses.Find(id);
            _db.Entry(course).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddCourse");
        }
        public ActionResult DeleteCurrentSemister(int id)
        {
            var currentSemister = _db.CurrentSemisters.Find(id);
            _db.Entry(currentSemister).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddCurrentSemister");
        }
        public ActionResult AddCurrentSemister()
        {
            SemisterDetailsList();
            DisplayDepartmentSelectList();
            DisplaySessionList();
            DisplayYearTermSelectList();
            return View();
        }

        private void DisplayYearTermSelectList()
        {
            var yearTerms = _db.YearTerms.Select(c => new
            {
                ytId = c.Id,

                ytName = c.YearTermName
            }).ToList();

            ViewBag.yearTerms = new SelectList(yearTerms, "ytId", "ytName");

        }

        private void DisplaySessionList()
        {
            var sessions = _db.Sessions.Select(c => new
            {
                sessId = c.SessionId,

                sessName = c.SessionName
            }).ToList();

            ViewBag.sessions = new SelectList(sessions, "sessId", "sessName");

        }

        public ActionResult GetBacklogRegistrationDetails()
        {
            ViewData["bacRegDetails"] = (from b in _db.BacklokRegistrations
                                         join s in _db.StudentInformations on b.StudentId equals s.StudentId
                                         join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                         join c in _db.Courses on b.CourseCode equals c.CourseCode
                                         join y in _db.YearTerms on c.YearTermId equals y.Id
                                         select new RegisteredBacklogDetailsModel
                                         {
                                             Id = b.BacklokRegId
                                             ,
                                             Roll = b.StudentId,
                                             Image = s.StudentImage,
                                             Name = s.Name,
                                             Department = d.DepartmentShortName,
                                             CourseCode = b.CourseCode,
                                             CourseTitle = c.CoursTitle,
                                             YearTerm = y.YearTermName
                                         }).ToList().OrderBy(m => m.Department);
            return View();

        }

        [HttpPost]
        public ActionResult AddCurrentSemister(CurrentSemisterAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var departmentId = Convert.ToInt32(model.Department);
                var sessionId = Convert.ToInt32(model.Session);
                var yearTermId = Convert.ToInt32(model.YearTerm);
                var checkIsSemisterExist =
                    _db.CurrentSemisters.Any(
                        m =>
                            m.DepartmentId.Equals(departmentId) && m.SessionId.Equals(sessionId)
                        );


                if (!checkIsSemisterExist)
                {
                    var currentSemister = new CurrentSemister
                    {
                        DepartmentId = departmentId,
                        SessionId = sessionId,
                        YearTermId = yearTermId
                    };
                    _db.CurrentSemisters.Add(currentSemister);
                    _db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.successMessageShowId = 1;
                }
                else
                {
                    ModelState.AddModelError("",
                        "It seems there is already a record for this semister and you are trying to add duplicate one.");
                }
              
         
               
            }
          
            DisplayDepartmentSelectList();
            DisplaySessionList();
            DisplayYearTermSelectList();
            SemisterDetailsList();
            return View();
        }

        private void SemisterDetailsList()
        {
            ViewData["currentSemisterDetails"] = (from c in _db.CurrentSemisters
                                                  join s in _db.Sessions on c.SessionId equals s.SessionId
                                                  join d in _db.Departments on c.DepartmentId equals d.DepartmentId
                                                  join y in _db.YearTerms on c.YearTermId equals y.Id
                                                  select new CurrentSemisterDetailsModel
                                                  {
                                                      Id = c.CurrentSemId,
                                                      Session = s.SessionName,
                                                      Department = d.DepartmentShortName,
                                                      YearTerm = y.YearTermName
                                                  }).ToList().OrderBy(m => m.Department);
        }

        public JsonResult GetBacklog()
        {
            var getBacklogs =
                _db.Backloks.Join(_db.Courses, b => b.CourseCode, c => c.CourseCode, (b, c) => new { b, c })
                    .Join(_db.YearTerms, @t => @t.b.YearTermId, y => y.Id, (@t, y) => new BacklogViewModel
                    {
                        Id = @t.b.Id
                        ,
                        StudentId = @t.b.StudentId,
                        CourseCode = @t.b.CourseCode,
                        CourseTitle = @t.c.CoursTitle,
                        Credit = @t.c.Credit,
                        YearTermName = y.YearTermName
                    });

            return Json(getBacklogs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddStudentBacklog()
        {
            DisplayDepartmentSelectList();
            DisplayYearTermSelectList();
            return View();
        }
        [HttpPost]
        public ActionResult AddStudentBacklog(BacklogAddModel model, IEnumerable<string> courseCode)
        {
            if (ModelState.IsValid)
            {

                if (courseCode != null)
                {

                    foreach (var backlog in courseCode.Select(c => new Backlog
                    {
                        StudentId = model.StudentId,
                        CourseCode = c,
                        YearTermId = Convert.ToInt32(model.YearTerm)
                    }))
                    {
                        if (!IsAnyCurrentCourseCodeExistInResultTable(backlog.CourseCode, backlog.StudentId, backlog.YearTermId))
                        {
                            if (IsBacklogAlreadyExistInBacklogTable(backlog.CourseCode, backlog.StudentId, backlog.YearTermId))
                            {
                                ViewBag.ErrorMsg = "Course Code " + backlog.CourseCode + " already exist";

                            }
                            else
                            {
                                _db.Backloks.Add(backlog);
                                _db.SaveChanges();
                                ViewBag.successMessageShowId = 1;
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMsg = "Course Code " + backlog.CourseCode + " has been found in result table.You cann't insert this course in backlog table";

                        }
                      
                      
                    }
                    ModelState.Clear();



                }
                else
                {
                    ViewBag.errorMsg = "Please select Course Code";
                }




            }
            else
            {
                ViewBag.yearTermerrorMsg = "Required";
            }
   DisplayYearTermSelectList();
            DisplayDepartmentSelectList();
            ModelState.Clear();
            return View(model);
        }

        private bool IsAnyCurrentCourseCodeExistInResultTable(string courseCode, string studentId, int yearTermId)
        {
            return _db.Results.Where(m => m.StudentId.Equals(studentId) && m.YearTermId.Equals(yearTermId)).Any(y => y.CourseCode.Equals(courseCode));
       
        }

        private bool IsBacklogAlreadyExistInBacklogTable(string courseCode,string studentId,int yearTermId)
        {

            return _db.Backloks.Where(m=>m.StudentId.Equals(studentId)&&m.YearTermId.Equals(yearTermId)).Any(y => y.CourseCode.Equals(courseCode));
       
        }

        public JsonResult GetAccounts()
        {
            var getAccounts =
                _db.Logins.Join(_db.StudentInformations, l => l.UniversityId, s => s.StudentId, (l, s) => new { l, s })
                    .Join(_db.Departments, @t => @t.s.DepartmentId, d => d.DepartmentId, (@t, d) => new AccountViewModel
                    {
                        Id = @t.l.Id,
                        Username = @t.l.Username,
                        Password = @t.l.Password,
                        UserId = @t.l.UniversityId
                    });


            return Json(getAccounts, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckIsIdExist(string id)
        {

            return Json(!_db.Logins.Any(x => x.UniversityId.Equals(id)), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddAccount()
        {
            ViewData["getAccounts"] =
                _db.Logins.Join(_db.StudentInformations, l => l.UniversityId, s => s.StudentId, (l, s) => new { l, s })
                    .Join(_db.Departments, @t => @t.s.DepartmentId, d => d.DepartmentId,
                        (@t, d) => new AccountViewModel
                        {
                            Image = @t.s.StudentImage,
                            Username = @t.l.Username,
                            Password = @t.l.Password,
                            UserId = @t.l.UniversityId
                        });


            return View();
        }
        [HttpPost]
        public ActionResult AddAccount(AccountAddModel accountAddModel)
        {
            if (ModelState.IsValid)
            {
                var checkIsIdExist = _db.Logins.SingleOrDefault(m => m.UniversityId.Equals(accountAddModel.UserId));
                if (checkIsIdExist == null)
                {
                    var login = new Login
                    {
                        Username = accountAddModel.Username,
                        Password = accountAddModel.Password,
                        UniversityId = accountAddModel.UserId
                    };
                    _db.Logins.Add(login);
                    _db.SaveChanges();
                    ViewBag.successMessageShowId = 1;
                }
                else
                {
                    ModelState.AddModelError("", "ID " + accountAddModel.UserId + " already exist.");
                }

            }
            return View();
        }

        public ViewResult GetDepartments()
        {
            ViewData["Departments"] = _db.Departments.Select(x => new DepartmentViewModel{DepartmentId = x.DepartmentId,Department = x.DepartmentFullName});
          
            return View();
        }
       
        public ActionResult AddNewStudent()
        {
            DisplayStudentsDetails();
            DisplaySessionList();
            DisplayDepartmentSelectList();
            DisplayHallList();
            return View();
        }

        private void DisplayHallList()
        {
            var halls = _db.Halls.Select(c => new
            {
                HallId = c.Id,

                HalName = c.HallName
            }).ToList();

            ViewBag.halls = new SelectList(halls, "HallId", "HalName");
        }


        private void DisplayStudentsDetails()
        {
            ViewData["studentDetails"] = from s in _db.StudentInformations
                                         join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                         join h in _db.Halls on s.HallId equals h.Id
                                         join f in _db.Faculties on s.FacultyId equals f.FacultyId
                                         join ss in _db.Sessions on s.SessionId equals ss.SessionId
                                         select new StudentDetailsViewModel
                                         {
                                             StudentId = s.Id,
                                             Image = s.StudentImage,
                                             Name = s.Name,
                                             StudentUniversityId = s.StudentId,
                                             Faculty = f.Name,
                                             Department = d.DepartmentFullName,
                                             Session = ss.SessionName,
                                             Phone = s.Phone,
                                             Email = s.Email,
                                             HallName = h.HallName

                                         };

        }

        public ActionResult CheckIsStudentIdExist(string studentId)
        {
            return Json(!_db.StudentInformations.Any(m => m.StudentId.Equals(studentId)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddNewStudent(StudentMetadata stm, string search)
        {
             DisplayStudentsDetails();
            DisplaySessionList();
            DisplayDepartmentSelectList();
            DisplayHallList();
            if (ModelState.IsValid)
            {
                try
                {

                    var checkIsStudentIdExist = _db.StudentInformations.Any(m => m.StudentId.Equals(stm.StudentUniversityId));
                    if (!checkIsStudentIdExist)
                    {
                        var sti = new StudentInformation();

                        ViewBag.d = _db.Departments.Select(m => m.DepartmentShortName);
                        var data = new byte[stm.File.ContentLength];
                        stm.File.InputStream.Read(data, 0, stm.File.ContentLength);
                        stm.Image = data;
                        sti.StudentImage = stm.Image;
                        var dptId = Convert.ToInt32(stm.Department);
                        var facId = _db.Departments.Find(dptId).FacultyId;
                        var hallId = Convert.ToInt32(stm.HallName);

                        sti.DepartmentId = Convert.ToInt32(stm.Department);
                        sti.Name = stm.Name;
                        sti.StudentId = stm.StudentUniversityId;
                        sti.Phone = stm.Phone;
                        sti.Email = stm.Email;
                        sti.PresentAddress = stm.PresentAddress;
                        sti.ParmanentAddress = stm.ParmanentAddress;
                        sti.FacultyId = facId;
                        sti.SessionId = Convert.ToInt32(stm.Session);
                        sti.Gender = stm.Gender;
                        sti.Nationality = stm.Nationality;
                        sti.HallId = hallId;
                        _db.StudentInformations.Add(sti);
                        _db.SaveChanges();
                        ViewData["ReqMsg"] = 3;
                        ViewBag.successMsg = " New Student Added Successfully..";
                        ViewBag.check = 1;
                        Response.Write(hallId);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Student ID " + stm.StudentUniversityId + " already exist!");
                    }








                }
                catch (Exception e)
                {

                    ViewData["ReqMsg"] = 3;



                }
            }
            else
            {
                ViewBag.ddd = 3;
                ViewBag.imgReq = "Upload a photo";
            }


            ViewBag.d = _db.Departments.Select(m => m.DepartmentShortName);



            return View();
        }

        public ActionResult AddHall()
        {
            GetHalls();
            return View();
        }

        private void GetHalls()
        {
            ViewBag.hallDetails =
                _db.Halls.Select(m => new HallDetailsModel { HallName = m.HallName, HallId = m.Id });
        }

        [HttpPost]
        public ActionResult AddHall(HallAddViewModel objHallAddViewModel)
        {
            GetHalls();
            if (ModelState.IsValid)
            {
                if (!IsHallExist(objHallAddViewModel.HallName))
                {
                    var objHall = new Hall { HallName = objHallAddViewModel.HallName };
                    _db.Halls.Add(objHall);
                    _db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.addSuccessId = 1;
                }
                else
                {
                    ModelState.AddModelError("", "This Hall already exist in database.");
                }

            }
            return View();
        }

        public ViewResult DeleteHall(int id)
        {
            try
            {
                if (!IsCurrentHallReferenceToAnyTable(id))
                {

                    var h = _db.Halls.SingleOrDefault(m => m.Id.Equals(id));
                    if (h != null)
                    {
                        ViewBag.hall = h.HallName;
                    }
                }
                else
                {
                    ViewBag.deleteErrorMsg = "You can delete this hall because this used in another table.";
                }


            }
            catch (Exception e)
            {


            }

            return View();
        }

        private bool IsCurrentHallReferenceToAnyTable(int hallId)
        {
            return _db.StudentInformations.Any(x => x.HallId.Value.Equals(hallId));
        }

        [HttpPost]
        public RedirectToRouteResult DeleteHall(int id = 0, int i = 0)
        {
            var objToDelete = _db.Halls.Find(id);
            _db.Entry(objToDelete).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddHall", "Admin");
        }
        private bool IsHallExist(string hall)
        {
            return _db.Halls.Any(m => m.HallName.Equals(hall));
        }

     

        public ActionResult GetStudentsDetailsByDepartment(int departmentId)
        {
            ViewData["studentDetails"] = from s in _db.StudentInformations
                                         join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                         join ss in _db.Sessions on s.SessionId equals ss.SessionId
                                         where d.DepartmentId.Equals(departmentId)
                                         select new StudentDetailsViewModel
                                         {

                                             StudentId = s.Id,
                                             Name = s.Name,
                                             Image = s.StudentImage,
                                             StudentUniversityId = s.StudentId,
                                             Department = d.DepartmentFullName,
                                             Session = ss.SessionName

                                         };
            



            return View();
        }
        [HttpPost]
        public ActionResult GetStudentsDetailsByDepartment(StudentSearchModel model, int departmentId)
        {
            try
            {
                ViewData["studentDetails"] = from s in _db.StudentInformations
                                             join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                             join ss in _db.Sessions on s.SessionId equals ss.SessionId
                                             where d.DepartmentId.Equals(departmentId)
                                             where s.StudentId.Equals(model.SearchTerm)
                                             select new StudentDetailsViewModel
                                             {

                                                 StudentId = s.Id,
                                                 Name = s.Name,
                                                 Image = s.StudentImage,
                                                 StudentUniversityId = s.StudentId,
                                                 Department = d.DepartmentFullName,
                                                 Session = ss.SessionName

                                             };
              

            }
            catch (Exception e)
            {

                Response.Write(e.Message);
            }
         
            return View();
        }

        public ActionResult DisplayAdminHomePage()
        {

            try
            {


             
                    ViewData["Departments"] = _db.Departments.Select(x => new DepartmentViewModel { DepartmentId = x.DepartmentId, Department = x.DepartmentFullName });
          

                    var regDateIds =
                  _db.RegistrationDates.Select(m => m.Id);

                    foreach (var id in regDateIds)
                    {

                        var startDate = _db.RegistrationDates.Find(id).StartDate;
                        var sd = startDate.Day;
                        var sm = startDate.Month;
                        var sy = startDate.Year;
                        var cd = DateTime.Now.Day;
                        var cm = DateTime.Now.Month;
                        var cy = DateTime.Now.Year;

                        var dateDifference =
                            Convert.ToDateTime(_db.RegistrationDates.Find(id).EndDate.ToShortDateString()) -
                            Convert.ToDateTime(DateTime.Now.ToShortDateString());

                        var dayesLeft = dateDifference.Days;
                        if (dayesLeft == 0)
                        {
                            var registrationDate =
                                _db.RegistrationDates.Find(id);
                            registrationDate.DaysLeft = dayesLeft;
                            _db.Entry(registrationDate).State = EntityState.Modified;

                        }
                        var getDaysLeft =
                           _db.RegistrationDates.Find(id).DaysLeft;
                        if (getDaysLeft != null)
                        {

                        }
                        else
                        {
                            if (sd <= cd && sm <= cm && sy <= cy)
                            {
                                ViewBag.displayDaysLeftId = 1;
                            }

                        }


                    }

                    _db.SaveChanges();

                    ViewData["getCourseRegDateDetails"] = (from r in _db.RegistrationDates
                                                           join d in _db.Departments on r.DepartmentId equals d.DepartmentId

                                                           select new CourseRegistrationDateViewModel
                                                           {
                                                               Id = r.Id,
                                                               Department = d.DepartmentShortName,
                                                               StartDate = r.StartDate,
                                                               LastDate = r.EndDate,

                                                           }).ToList();


                    var checkUser = _db.AdminLoginTables.Any(m => m.Username.Equals(User.Identity.Name));
                    if (checkUser)
                    {
                      



                        ViewData["feeDetails"] = _db.StudentFees.Select(m => new StudentFeeViewModel
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

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

               

            }
            catch (Exception e)
            {

                Response.Write(e.Message);
            }


            return View();
        }
     


        public ActionResult AddDepartment()
        {

            DisplayDepartmentDetails();

            DisplayFacultySelectList();
            return View();
        }

        private void DisplayFacultySelectList()
        {
            var faculties = _db.Faculties.Select(c => new
            {
                FacId = c.FacultyId,

                Faculty = c.Name
            }).ToList();

            ViewBag.faculties = new SelectList(faculties, "FacId", "Faculty");
        }

        private void DisplayDepartmentDetails()
        {
            ViewData["Depts"] = (from d in _db.Departments
                                 join f in _db.Faculties on d.FacultyId equals f.FacultyId

                                 select new DepartViewModel { DepartmentShortName = d.DepartmentShortName, DeptId = d.DepartmentId, Faculty = f.Name, DepartmentFullName = d.DepartmentFullName }).OrderBy(m => m.DepartmentShortName).ToList();

        }

        [HttpPost]
        public ActionResult AddDepartment(DepartAddViewModel d)
        {
           
            if (ModelState.IsValid)
            {
                if (_db.Departments.SingleOrDefault(m => m.DepartmentShortName.Equals(d.DepartmentShortName)) == null)
                {
                    var dpt = new Department { FacultyId = Convert.ToInt32(d.Faculty), DepartmentShortName = d.DepartmentShortName, DepartmentFullName = d.DepartmentFullName };
                    _db.Departments.Add(dpt);
                    _db.SaveChanges();
                    ViewBag.msg = "New Department has been Added";

                    ViewBag.empt = 1;
                }

                else
                {
                    ViewBag.msg = "New Department already Exist";

                }
            }
            DisplayFacultySelectList();
            DisplayDepartmentDetails();


            return View();
        }
        public ActionResult AddCourse()
        {


            DisplayDepartmentSelectList();      
            DisplayYearTermSelectList();
            DisplayCourseDetails();

            return View();
        }

        private void DisplayCourseDetails()
        {
            ViewData["courses"] = from c in _db.Courses
                                  join d in _db.Departments on c.DepartmentId equals d.DepartmentId


                                  select
                                      new CourseViewModel
                                      {
                                          Department = d.DepartmentShortName ?? d.DepartmentFullName,
                                          CourseCode = c.CourseCode,
                                          CourseTitle = c.CoursTitle,
                                          Credit = c.Credit,
                                          YearTermId = c.YearTermId,
                                          Id = c.CourseId
                                      };
        }

        public ActionResult CheckIsCourseCodeExist(string code)
        {
            var courseCode = _db.Courses.Any(m => m.CourseCode.Equals(code));
            return Json(!courseCode, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddCourse(CourseAddModel model)
        {


            if (ModelState.IsValid)
            {
                var checkIsCourseCodeExist = _db.Courses.Any(m => m.CourseCode.Equals(model.CourseCode));
                if (!checkIsCourseCodeExist)
                {

                    var departmentId =
                        Convert.ToInt32(model.Department);
                        var course = new Course
                        {
                            CourseCode = model.CourseCode,
                            CoursTitle = model.CourseTitle,
                            Credit = model.Credit,
                            YearTermId = Convert.ToInt32(model.YearTerm),
                            DepartmentId = departmentId

                        };
                        _db.Courses.Add(course);
                        _db.SaveChanges();
                        ViewBag.successMsg = "New course has been added successfully.";
                        ViewBag.successDialogId = 1;
                    
                 
                }
                else
                {
                    ModelState.AddModelError("", "* Course code already exist");
                }
            }

          DisplayYearTermSelectList();
          DisplayDepartmentSelectList();
         DisplayCourseDetails();
            return View();
        }

        public ActionResult DeleteRegisteredBacklog(int id)
        {
            var regBacklog = _db.BacklokRegistrations.Find(id);
            _db.Entry(regBacklog).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("GetBacklogRegistrationDetails", "Admin");
        }

        public ActionResult DeleteDepartment(int id)
        {
            var department = _db.Departments.Find(id);
            _db.Entry(department).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("AddDepartment", "Admin");
        }

        [HttpPost]
        public ActionResult DeleteDepartment(int? id)
        {
            var department = _db.Departments.SingleOrDefault(m => m.DepartmentId.Equals(id.Value));
            _db.Entry(department).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddDepartment");
        }

        public ActionResult UpdateDepartment(int id = 0)
        {
            if (Request.IsAjaxRequest())
            {
                var d = new DepartUpdateViewModel();
                try
                {
                    var n = _db.Departments.SingleOrDefault(m => m.DepartmentId.Equals(id));
                    if (n != null)
                    {
                        d.DepartmentShortName = _db.Departments.Find(id).DepartmentShortName;
                        d.DepartmentFullName = _db.Departments.Find(id).DepartmentFullName;
                        var selectList = new SelectList(_db.Faculties, "FacultyId", "Name", n.FacultyId);
                        ViewData["faculties"] = selectList;
                    }


                }
                catch (Exception e)
                {
                    Response.Write(e.Message);

                }

                return PartialView("_UpdateDepartment", d);
            }
            return View("ProcessingError");
        }
        [HttpPost]
        public ActionResult UpdateDepartment(DepartUpdateViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var department = _db.Departments.Find(id);
                    department.DepartmentShortName = model.DepartmentShortName;
                    department.DepartmentFullName = model.DepartmentFullName;
                    department.FacultyId = Convert.ToInt32(model.Faculty);
                    _db.Entry(department).State = EntityState.Modified;
                    _db.SaveChanges();

                }
                catch (Exception e)
                {
                    Response.Write(e.Message);

                }


            }
            return RedirectToAction("AddDepartment", "Admin");

        }

        public ActionResult GetStudentDetails(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var singledetails = _db.StudentInformations.SingleOrDefault(m => m.Id == id);

                if (singledetails != null)
                {


                    return PartialView("_StudentDetails", singledetails);



                }

                return View("DisplayAdminHomePage");

            }
            else
            {
                return View("ProcessingError");
            }


        }


        public ActionResult DeleteStudent(int? id)
        {

            var u
                = _db.StudentInformations.SingleOrDefault(m => m.Id == id.Value);
            if (u != null)
            {
                string studentId = u.StudentId;
                var findStudentInLoginTable = _db.Logins.SingleOrDefault(m => m.UniversityId.Equals(studentId));
                if (findStudentInLoginTable != null)
                {
                    var deleteId = _db.Logins.Single(m => m.UniversityId.Equals(studentId)).Id;
                    var loginModel = _db.Logins.Find(deleteId);
                    _db.Entry(loginModel).State = EntityState.Deleted;
                    _db.SaveChanges();

                }

                var findStudentInBacklokTable = _db.Backloks.FirstOrDefault(m => m.StudentId.Equals(studentId));
                var backlokIdList = new List<int>();
                var resultIdList = new List<int>();
                var backlokRegistrationIdList = new List<int>();
                if (findStudentInBacklokTable != null)
                {
                    var deleteIds = _db.Backloks.Where(m => m.StudentId.Equals(studentId)).Select(m => m.Id);
                    backlokIdList.AddRange(deleteIds);
                }

                foreach (var i in backlokIdList)
                {
                    var backlokModel = _db.Backloks.Single(m => m.Id == i);
                    _db.Entry(backlokModel).State = EntityState.Deleted;
                    _db.SaveChanges();
                }


                var findStudentInResultable = _db.Results.FirstOrDefault(m => m.StudentId.Equals(studentId));

                if (findStudentInResultable != null)
                {
                    var deleteIds = _db.Results.Where(m => m.StudentId.Equals(studentId)).Select(m => m.ResultId);
                    resultIdList.AddRange(deleteIds);
                }
                foreach (var i in resultIdList)
                {
                    var resultModel = _db.Results.Find(i);
                    _db.Entry(resultModel).State = EntityState.Deleted;
                    _db.SaveChanges();
                }

                var findStudentInRegistrationtable = _db.RegistrationDetails.SingleOrDefault(m => m.StudentId.Equals(studentId));

                if (findStudentInRegistrationtable != null)
                {
                    var deleteId = _db.RegistrationDetails.Single(m => m.StudentId.Equals(studentId)).RegistrationDetailId;
                    var registrationDetailModel = _db.RegistrationDetails.Find(deleteId);
                    _db.Entry(registrationDetailModel).State = EntityState.Deleted;
                    _db.SaveChanges();


                }

                var findStudentInBacklokRegistrationtable = _db.BacklokRegistrations.FirstOrDefault(m => m.StudentId.Equals(studentId));

                if (findStudentInBacklokRegistrationtable != null)
                {
                    var deleteIds = _db.BacklokRegistrations.Where(m => m.StudentId.Equals(studentId)).Select(m => m.BacklokRegId);
                    backlokRegistrationIdList.AddRange(deleteIds);
                }

                foreach (var i in backlokRegistrationIdList)
                {
                    var backlockRegistrationModel = _db.BacklokRegistrations.Find(i);
                    _db.Entry(backlockRegistrationModel).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
            }

            var si = _db.StudentInformations.Find(id);
            _db.Entry(si).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddNewStudent");
        }




        public ActionResult UpdateStudent(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var hallName = "";

                ViewBag.halls = _db.Halls.Select(m => m.HallName).ToList();
                var supm = new StudentUpdateModel();
                var u = _db.StudentInformations.SingleOrDefault(m => m.Id.Equals(id));

                var hallId = u.HallId;
                var n = _db.Halls.SingleOrDefault(c => c.Id.Equals(hallId.Value));
                if (n != null)
                    hallName = n.HallName;
                if (hallName != "")
                {
                    ViewBag.hallListShowId = 1;

                }


                var information = _db.StudentInformations.Find(id);

                supm.PresentAddress = information.PresentAddress;
                supm.Phone = information.Phone;
                supm.Email = information.Email;
                supm.Image = information.StudentImage;

                var halls = _db.Halls.Select(c => new
                {
                    HallId = c.Id,

                    Hall = c.HallName
                }).ToList();


                var h = _db.Halls.FirstOrDefault(m => m.HallName.Equals(hallName));
                SelectList selectList;
                if (u.HallId > 0)
                {
                    selectList = h != null ? new SelectList(halls, "HallId", "Hall", h.Id) : new SelectList(halls, "HallId", "HallName");

                }
                else
                {
                    selectList = new SelectList(halls, "HallId", "Hall");
                }

                ViewData["Halls"] = selectList;


                return PartialView("_UpdateStudent", supm);
            }
            else
            {
                return View("ProcessingError");
            }




        }
        [HttpPost]
        public ActionResult UpdateStudent(StudentUpdateModel model, int? id)
        {

            var hallId = 0;
            var selectedHallId = 0;
            if (ModelState.IsValid)
            {

                var si = _db.StudentInformations.Find(id);
                if (model.HallName != null)
                {
                    hallId = Convert.ToInt32(model.HallName);
                    si.HallId = hallId;
                }
                si.Phone = model.Phone;
                si.Email = model.Email;


                si.PresentAddress = model.PresentAddress;

                _db.Entry(si).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DisplayAdminHomePage");

            }




            ViewBag.halls = _db.Halls.Select(m => m.HallName).ToList();
            var supm = new StudentUpdateModel();
            var information = _db.StudentInformations.Find(id);

            supm.PresentAddress = information.PresentAddress;
            supm.Phone = information.Phone;
            supm.Email = information.Email;
            supm.Image = information.StudentImage;

            if (model.HallName != "")
            {

                selectedHallId = hallId;
                ViewBag.hallListShowId = 1;
            }

            var halls = _db.Halls.Select(c => new
            {
                HallId = c.Id,

                Hall = c.HallName
            }).ToList();

            var selectList = new SelectList(halls, "HallId", "Hall", selectedHallId);

            ViewData["Halls"] = selectList;


            return PartialView("_UpdateStudent", supm);
        }

        public ViewResult GetStudentNamesAndIdsByDepartment(int departmentId)
        {
            ViewBag.StudentNamesAndIdsByDepartment =( from s in _db.StudentInformations
                join ses in _db.Sessions on s.SessionId equals ses.SessionId
            
            
                where s.DepartmentId.Equals(departmentId)
                select new StudentDetailsViewModel {StudentUniversityId = s.StudentId, Name = s.Name,Session = ses.SessionName}).ToList();

            return View();

        }
        public ViewResult GetStudentResult(string studentId)
        {
         DisplayDepartmentSelectList();
            var d = new StudentsResultDbContext();   
            
            ViewBag.yearTerms = new SelectList(_db.YearTerms, "YearTermName", "YearTermName");
            return View(d.StudentResultDetails.Where(x=>x.StudentId.Equals(studentId)).ToList());

        }
 
       
    
        public ActionResult GetStudentResultDetails(string id, string stId)
        {

            var t = _db.ResultViewModels.FirstOrDefault(m => m.YearTermName.Equals(id) && m.StudentId.Equals(stId));
            var ytCount = _db.ResultViewModels.Where(m => m.StudentId.Equals(stId)).Select(m => m.YearTermName).Count();



            var d = _db.ResultViewModels.Where(m => m.StudentId.Equals(stId)).Select(m => m.Cgpa).Sum();
            if (d == null) return PartialView("_GetStudentResultDetails", t);
            var sum = d.Value;

            ViewData["r"] = sum / ytCount;
            return PartialView("_GetStudentResultDetails", t);
        }

        public ActionResult InsertStudentResult()
        {
            try
            {
               
               
                DisplayDepartmentSelectList();
                DisplayYearTermSelectList();
        
                ViewBag.gpas = new SelectList(_db.Grades, "GradeId", "GradePoint");


            }
            catch (Exception)
            {


            }

            return View();
        }

        private void DisplayDepartmentSelectList()
        {
            IQueryable<DepartmentSelectList> l1 =
             _db.Departments.Where(m => m.DepartmentShortName == null)
                 .Select(m => new DepartmentSelectList { Department = m.DepartmentFullName, DepartmentId = m.DepartmentId });
            IQueryable<DepartmentSelectList> l2 =
          _db.Departments.Where(m => m.DepartmentShortName != null)
              .Select(m => new DepartmentSelectList { Department = m.DepartmentShortName, DepartmentId = m.DepartmentId });

            IEnumerable<DepartmentSelectList> l3 = l1.Union(l2);
            ViewBag.departments = new SelectList(l3, "DepartmentId", "Department");
          
        }

        [HttpPost]
        public ActionResult InsertStudentResult(StudentResultInsertViewModel model, List<int> gpa, List<string> courseCode)
        {
            ViewBag.Id = 1;
            if (gpa == null)
            {
                ViewBag.errorMsg = "Required";
            }
            if (courseCode == null)
            {
                ViewBag.errorMsg = "Required";
            }
            if (ModelState.IsValid)
            {

                try
                {
                    var rl2 = new List<ResultList2>();
                    var rl3 = new List<ResultList3>();

                    if (courseCode != null)
                    {
                        if (gpa != null)
                            rl3.AddRange(gpa.Select((g, index) => new ResultList3 { Gpa = g, MatchId = index }));

                        rl2.AddRange(courseCode.Select((c, index) => new ResultList2 { CourseCode = c, MatchId = index }));

                        var resultData = from r2 in rl2
                                         join r3 in rl3 on r2.MatchId equals r3.MatchId

                                         select new ResultList { CourseCode = r2.CourseCode, Gpa = r3.Gpa };

                        var rl2MaxId = rl2.Max(m => m.MatchId);
                        var rl3MaxId = rl3.Max(m => m.MatchId);

                        if (rl2MaxId == rl3MaxId)
                        {
                            foreach (var result in resultData.Select(resultList => new Result
                            {
                                CourseCode = resultList.CourseCode,

                                StudentId = model.StudentId,
                                YearTermId = model.YearTerm,
                                GradeId = resultList.Gpa
                            }))
                            {
                                _db.Results.Add(result);
                                _db.SaveChanges();
                                ViewBag.successMsgId = 1;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "It seems course code and gpa combination is wrong." + "\n" + "Number of course codes must be same to gpa.");

                        }




                    }
                }
                catch (Exception)
                {


                }
            }


       DisplayYearTermSelectList();
            DisplayDepartmentSelectList();
       
            ViewBag.gpas = new SelectList(_db.Grades, "GradeId", "GradePoint");

            return View();
        }
        public ActionResult AddStudentFee()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddStudentFee(StudentFeeAddModel model)
        {
            if (!ModelState.IsValid) return View();
            var studentFee = new StudentFee
            {
                AdmissionFee = model.AdmissionFee,
                BacklogFeePerCourse = model.BacklogFeePerCourse,
                CentralLibraryFee = model.CentralLibraryFee,
                CreditHourFee = model.CreditHourFee,
                HallRent = model.HallRent,
                LateFee = model.LateFee,
                NonResidentialTransfortFee = model.NonResidentialTransfortFee,
                ResidentialTransfortFee = model.ResidentialTransfortFee,
                SeminarLibraryFee = model.SeminarLibraryFee,
                StudentTrustFund = model.StudentTrustFund,
                Others = model.Others ?? 0

            };
            _db.StudentFees.Add(studentFee);
            ModelState.Clear();
            _db.SaveChanges();
            @ViewBag.successNotificationId = 1;
            return View();
        }

        public ActionResult DeleteStudentFee(int id)
        {
            var fee = _db.StudentFees.Find(id);
            _db.Entry(fee).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("DisplayAdminHomePage", "Admin");
        }
        public ActionResult UpdateStudentFee(int id)
        {
            var studentFee = _db.StudentFees.Find(id);
            var model = new StudentFeeUpdateModel
            {
                AdmissionFee = studentFee.AdmissionFee,
                CreditHourFee = studentFee.CreditHourFee,
                StudentTrustFund = studentFee.StudentTrustFund,
                HallRent = studentFee.HallRent,
                CentralLibraryFee = studentFee.CentralLibraryFee,
                SeminarLibraryFee = studentFee.SeminarLibraryFee,
                NonResidentialTransfortFee = studentFee.NonResidentialTransfortFee,
                ResidentialTransfortFee = studentFee.ResidentialTransfortFee,
                LateFee = studentFee.LateFee,
                Others = studentFee.Others,
                BacklogFeePerCourse = studentFee.BacklogFeePerCourse
            };
            return PartialView("_UpdateStudentFee", model);
        }
        [HttpPost]
        public ActionResult UpdateStudentFee(StudentFeeUpdateModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var studentFee = _db.StudentFees.Find(id);
                studentFee.AdmissionFee = model.AdmissionFee;
                studentFee.CreditHourFee = model.CreditHourFee;
                studentFee.StudentTrustFund = model.StudentTrustFund;
                studentFee.HallRent = model.HallRent;
                studentFee.CentralLibraryFee = model.CentralLibraryFee;
                studentFee.SeminarLibraryFee = model.SeminarLibraryFee;
                studentFee.NonResidentialTransfortFee = model.NonResidentialTransfortFee;
                studentFee.ResidentialTransfortFee = model.ResidentialTransfortFee;
                studentFee.LateFee = model.LateFee;
                studentFee.Others = model.Others;
                studentFee.BacklogFeePerCourse = model.BacklogFeePerCourse;

                _db.Entry(studentFee).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("DisplayAdminHomePage");
            }
            return PartialView("_UpdateStudentFee", model);
        }



        public JsonResult GetCourseCodes(int departmentName)
        {
            var corses =
                _db.Courses.Where(m => m.DepartmentId.Equals(departmentName)).Select(m => m.CourseCode).ToList();
            return Json(corses, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetCoursesByDepartmentAndYearTerm(int departmentName, int yearTerm)
        {
           
         
            var corses =
               _db.Courses.Where(m => m.DepartmentId.Equals(departmentName) && m.YearTermId.Equals(yearTerm)).Select(m => m.CourseCode).ToList();
          
            return Json(corses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourses(int yearTerm,int departmentId)
        {
            var corses =
               _db.Courses.Where(m => m.YearTermId.Equals(yearTerm)&&m.DepartmentId.Equals(departmentId)).Select(m => m.CourseCode).ToList();
            return Json(corses, JsonRequestBehavior.AllowGet);
        }

        public ViewResult GetResultDetails(string yearTerm, string studentId)
        {
            double runningCgpaForEachStudent = 0;
            var rdb = new StudentsResultDbContext();
            var yterId = _db.YearTerms.Single(m => m.YearTermName.Equals(yearTerm)).Id;

            ViewData["ExmainationDetails"] = from c in _db.Courses
                                             join r in _db.Results on c.CourseCode equals r.CourseCode
                                             join g in _db.Grades on r.GradeId equals g.GradeId
                                             join y in _db.YearTerms on r.YearTermId equals y.Id
                                             join s in _db.StudentInformations on r.StudentId equals s.StudentId
                                             join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                             where r.YearTermId.Equals(yterId)
                                             where r.StudentId.Equals(studentId)
                                             select new ExaminationDetailsViewMode
                                             {
                                                 CourseCode = c.CourseCode,
                                                 CourseTitle = c.CoursTitle,
                                                 Credit = c.Credit,
                                                 GradePoint = g.GradePoint,
                                                 LetterGrade = g.LetterGrade

                                             };

            ViewBag.studentName = _db.StudentInformations.Single(m => m.StudentId.Equals(studentId)).Name;
            ViewBag.studentID = studentId;

            var countYearTerm = rdb.StudentResultDetails.Where(m => m.StudentId.Equals(studentId)).Select(m => m.YearTermName).Count();
            
            var sum = rdb.StudentResultDetails.Where(m => m.StudentId.Equals(studentId)).Select(m => m.Cgpa).Sum();
            if (sum != null)
            {
                var totalCgpa = sum.Value;

                var runningTotal = totalCgpa / countYearTerm;
                runningCgpaForEachStudent += runningTotal;
            }

            ViewData["runningCGPA"] = runningCgpaForEachStudent;


            return View();
        }

        public JsonResult GetStudentNames(string n)
        {
            var data = _db.StudentInformations.Where(m => m.Name.StartsWith(n)).Select(m => m.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ViewResult AddCourseRegistrationDate()
        {
            DisplayDepartmentSelectList();
            return View();
        }
        [HttpPost]
        public ViewResult AddCourseRegistrationDate(RegistrationDateAddModel model)
        {

            if (ModelState.IsValid)
            {
                var departmentId = Convert.ToInt32(model.Department);
                var checkIsRecordExist =
                    _db.RegistrationDates.Any(m => m.DepartmentId.Equals(departmentId));
                if (!checkIsRecordExist)
                {
                    var registrationDateModel = new RegistrationDate
                    {
                        DepartmentId = departmentId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate
                    };
                    _db.RegistrationDates.Add(registrationDateModel);
                    _db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.successNotificationId = 1;
                }
                else
                {
                    ModelState.AddModelError("", "A record already exist for the selected department");
                }

            }
            DisplayDepartmentSelectList();
            return View();
        }

        public ActionResult DeleteCourseRegistrationDate(int id = 0)
        {
            var registrationDate = _db.RegistrationDates.Find(id);
            _db.Entry(registrationDate).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("DisplayAdminHomePage", "Admin");
        }

        public ViewResult AddFaculty()
        {
            ViewData["Faculties"] = _db.Faculties.Select(m => new FacltyViewModel { Faculty = m.Name, FacultyId = m.FacultyId }).ToList();


            return View();
        }
        [HttpPost]
        public ViewResult AddFaculty(FacultyAddModel facultyAddModel)
        {
            if (ModelState.IsValid)
            {
                var chechIsRecordExists = _db.Faculties.Any(x => x.Name.Equals(facultyAddModel.Faculty));

                if (!chechIsRecordExists)
                {
                    var faculty = new Faculty { Name = facultyAddModel.Faculty };
                    _db.Faculties.Add(faculty);
                    _db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "* Faculty already exist!");
                }

            }
            ViewData["Faculties"] = _db.Faculties.Select(m => new FacltyViewModel { Faculty = m.Name, FacultyId = m.FacultyId }).ToList();

            return View();
        }

        public RedirectToRouteResult DeleteFaculty(int id = 0)
        {
            var faculty = _db.Faculties.Find(id);
            _db.Entry(faculty).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddFaculty");
        }

        public ViewResult AddSession()
        {
            ViewData["Sessions"] = _db.Sessions.Select(m => new SessionViewModel { Session = m.SessionName, SessionId = m.SessionId }).ToList();

            return View();
        }
        [HttpPost]
        public ViewResult AddSession(SessionAddModel sessionAddModel)
        {
            if (ModelState.IsValid)
            {
                var chechIsRecordExists = _db.Sessions.Any(x => x.SessionName.Equals(sessionAddModel.Session));
                if (!chechIsRecordExists)
                {
                    var session = new Session { SessionName = sessionAddModel.Session };
                    _db.Sessions.Add(session);
                    _db.SaveChanges();
                }
                else
                {
                    ViewBag.errorId = 1;
                }

            }
            ModelState.Clear();
            ViewData["Sessions"] = _db.Sessions.Select(m => new SessionViewModel { Session = m.SessionName, SessionId = m.SessionId }).ToList();

            return View();
        }

        public RedirectToRouteResult DeleteSession(int id = 0)
        {
            var session = _db.Sessions.Find(id);
            _db.Entry(session).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("AddSession");
        }

        public ActionResult Test()
        {
         
            return View();
        }

    }
}
