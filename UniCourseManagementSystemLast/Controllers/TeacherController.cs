using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UniversityCourseResultManagementSystemWebApp.BLL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherManager _teacherManager=new TeacherManager();
        private DepartmentManager _departmentManager=new DepartmentManager();
        private CourseManager _courseManager=new CourseManager();
        [HttpGet]
        public ActionResult InsertTeacher()
        {
            var departments = _departmentManager.GetAllDepartments();
            var designations = _teacherManager.GetDesignations();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.Designations = new SelectList(designations, "DesignationId", "Title");
            return View(new Teacher());
        }

        public ActionResult InsertTeacher(Teacher teacher)
        {
            var departments = _departmentManager.GetAllDepartments();
            var designations = _teacherManager.GetDesignations();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.Designations = new SelectList(designations, "DesignationId", "Title");
            if (ModelState.IsValid)
            {
                var rowAffected = _teacherManager.InsertTeacher(teacher);
                if (rowAffected > 0)
                {
                    ViewBag.ValidationMsg = "Successful!";
                    ModelState.Clear();
                    return View(new Teacher());
                }
            }
            
           
            ViewBag.ValidationMsg = "This email already exist.Please try again";
            return View(teacher);
        }

        //public View 
        [HttpGet]
        public ActionResult CourseAssign()
        {
            var departments = _departmentManager.GetAllDepartments();

            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
        
            return View(new CourseAssign());
        }
        
        public ActionResult CourseAssign(CourseAssign courseAssign)
        {
           
            var departments = _departmentManager.GetAllDepartments();

            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            var courses = _courseManager.GetAllCourses();
            var courseList = courses.Where(a => a.DepartmentId == courseAssign.DepartmentId);
            var teachers = _teacherManager.GetTeachers();
            var teacherList = teachers.Where(a => a.DepartmentId == courseAssign.DepartmentId);
            ViewBag.Courses = new SelectList(courseList, "CourseId", "CourseCode");
            ViewBag.Teachers = new SelectList(teacherList, "TeacherId", "TeacherName");
          
           
            int rowAffected = _teacherManager.InsertCourseAssign(courseAssign);
            courseAssign.CourseName = null;
            courseAssign.CourseCredit = null;
            courseAssign.CourseId = 0;
            ModelState.Clear();
            if (rowAffected > 0)
            {
                    ViewBag.ValidationMsg = "Successfull.";
                    
                    return View(courseAssign);
            }
            if (rowAffected == -1)
            {
                ViewBag.ValidationMsg = "Please select teacher and course.";

                return View(courseAssign);
            }
            ViewBag.ValidationMsg = "This course is already assigned.";
       
            return View(courseAssign);
          
        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            var teachers = _teacherManager.GetTeachers();
            var teacherList = teachers.Where(a => a.DepartmentId == departmentId);
           
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = _courseManager.GetAllCourses();
            var courseList = courses.Where(a => a.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeachersByTeacherId(int teacherId)
        {
            var teachers = _teacherManager.GetTeachers();
            var teacherList = teachers.Where(a => a.TeacherId == teacherId).FirstOrDefault();
            //var teacherValues = new { CreditTaken = teacherList.CreditTaken, RemainingCredit = teacherList.RemainingCredit };
            return Json(teacherList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCoursesByCourseId(int courseId)
        {
            var courses = _courseManager.GetAllCourses();
            var courseList = courses.Where(a => a.CourseId == courseId).FirstOrDefault();
            //var courseValues = new { CourseName = courseList.CourseName, CourseCredit = courseList.CourseCredit };
            return Json(courseList, JsonRequestBehavior.AllowGet);

        }

       
	}
}