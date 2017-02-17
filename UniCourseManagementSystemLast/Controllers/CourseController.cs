using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystemWebApp.BLL;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.Controllers
{
    public class CourseController : Controller
    {
        private CourseManager _courseManager = new CourseManager();
        private DepartmentManager _departmentManager = new DepartmentManager();
        private  SemesterManager _semesterManager=new SemesterManager();
        [HttpGet]
        public ActionResult InsertCourse()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            var semesters = _semesterManager.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters, "SemesterId", "SemesterName");
            return View(new Course());
        }

        public ActionResult InsertCourse(Course course)
        {

            var departments = _departmentManager.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            var semesters = _semesterManager.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters, "SemesterId", "SemesterName");

            if (ModelState.IsValid)
            {

                var rowAffected = _courseManager.InsertCourse(course);
                if (rowAffected > 0)
                {
                    ViewBag.ValidationMsg = "Successful!";
                    ModelState.Clear();
                    return View(new Course());
                }
            }
          
           
             ViewBag.ValidationMsg = "Course code or name already exists.Please try again.";
             return View(course);
            
        }

        public ActionResult ViewCourseStatics()
        {
            var departments = _departmentManager.GetAllDepartments();

            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            int departmentId = 0;
            var courseStatics = _courseManager.GetCourseStatics(departmentId);
            ViewBag.CourseStatics = courseStatics;
            return View();
        }

        public JsonResult ShowCourseAssignedTable(int departmentId)
        {
            var courseStatics = _courseManager.GetCourseStatics(departmentId);
            return Json(courseStatics, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UnassignCourse()
        {
          
            return View();
        }

        public ActionResult UnassignCourse(string something)
        {
            int rowAffected = _courseManager.UnassignCourses();
            if (rowAffected > 0)
            {
                ViewBag.ValidationMsg = "Successful!";
                return View();
            }
            ViewBag.ValidationMsg = "UnSuccessful!";
            return View();
        }
	}
}