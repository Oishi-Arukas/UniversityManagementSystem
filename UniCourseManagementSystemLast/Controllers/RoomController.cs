using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystemWebApp.BLL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.Controllers
{
    public class RoomController : Controller
    {
        private RoomAllocationManager _roomAllocationManager=new RoomAllocationManager();
        private DepartmentManager _departmentManager = new DepartmentManager();
        private CourseManager _courseManager=new CourseManager();
        [HttpGet]
        public ActionResult AllocateRooms()
        {
            var departmentList = _departmentManager.GetAllDepartments();

            ViewBag.Department = new SelectList(departmentList, "DepartmentId", "DepartmentName");
            var days = _roomAllocationManager.GetAllDays();
            ViewBag.Days = new SelectList(days, "DayId", "Days");
            var rooms = _roomAllocationManager.GetAllRooms();

            ViewBag.Rooms = new SelectList(rooms, "RoomId", "RoomNumber");
          
            
            return View();
        }

        public ActionResult AllocateRooms(RoomAllocation roomAllocation)
        {
            var departmentList = _departmentManager.GetAllDepartments();

            ViewBag.Department = new SelectList(departmentList, "DepartmentId", "DepartmentName");
            var days = _roomAllocationManager.GetAllDays();
            ViewBag.Days = new SelectList(days, "DayId", "Days");
            var rooms = _roomAllocationManager.GetAllRooms();

            ViewBag.Rooms = new SelectList(rooms, "RoomId", "RoomNumber");
           
          
            int rowAffected = _roomAllocationManager.InsertRoomAllocation(roomAllocation);
            if(ModelState.IsValid)
            {
                if (rowAffected>0)
                {
                    ViewBag.ValidationMsg = "Successful!";
                    ModelState.Clear();
                    return View();
                }
            }
            var courses = _courseManager.GetAllCourses();
            var courseList = courses.Where(a => a.DepartmentId == roomAllocation.DepartmentId);
            ViewBag.Courses = new SelectList(courseList, "CourseId", "CourseName");
            if (rowAffected == -1)
            {
                ViewBag.ValidationMsg = "Please select course.";
                return View(roomAllocation);
            }
            if (rowAffected == -2)
            {
                ViewBag.ValidationMsg = "From time should be less than to time.Enter Correctly.";
                return View(roomAllocation);
            }
            ViewBag.ValidationMsg = "Room already booked at this time period.";
            return View(roomAllocation);
        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = _courseManager.GetAllCourses();
            var courseList = courses.Where(a => a.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewClassSchedule()
        {
            var departments = _departmentManager.GetAllDepartments();

            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
            //int departmentId = 0;
            //var courseStatics = _courseManager.GetCourseStatics(departmentId);
            //ViewBag.CourseStatics = courseStatics;
            return View();
        }

        public JsonResult ShowClassScheduleTable(int departmentId)
        {
            var schduleList = _roomAllocationManager.GetClassSchedulesByDepartmentId(departmentId);
            return Json(schduleList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UnallocateRooms()
        {
           
            return View();
        }
       
        public ActionResult UnallocateRooms(string something)
        {
            int rowAffected = _roomAllocationManager.UnallocateRooms();
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