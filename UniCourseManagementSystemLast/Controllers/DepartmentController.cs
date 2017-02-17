using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystemWebApp.BLL;
using UniversityCourseResultManagementSystemWebApp.Models;
using System.Web.Helpers;

namespace UniversityCourseResultManagementSystemWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentManager _departmentManager=new DepartmentManager();
        [HttpGet]
        public ActionResult InsertDepartment()
        {
            return View(new Department());
        }
        public ActionResult InsertDepartment(Department department)
        {

            if (ModelState.IsValid)
            {
                var rowAffected = _departmentManager.InsertDepartment(department);
                if (rowAffected > 0)
                {
                    ViewBag.ValidationMsg = "Successful!";
                    ModelState.Clear();
                    return View(new Department());
                }
            }
           
           

            ViewBag.ValidationMsg = "Department code or name already exist.Please try again.";
            return View(department);
            
           
        }

        public ActionResult ShowAllDepartment()
        {
            List<Department> departments = _departmentManager.GetAllDepartments();
            ViewBag.Show = departments;
            return View();
        }


        
	}
}