using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class StudentResultShow
    {
        public int StudentRegisterId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string DepartmentName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string GradeName { get; set; }


        public StudentResultShow(string courseCode, string courseName, string gradeName)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            GradeName = gradeName;
        }

        public StudentResultShow()
        {

        }
    }
}