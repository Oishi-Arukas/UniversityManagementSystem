using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class CourseStatics
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string TeacherName { get; set; }
        public int? CourseStatus { get; set; }

        public CourseStatics(string courseCode, string courseName, string semester, string teacherName)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            Semester = semester;
            TeacherName = teacherName;
        }

        public CourseStatics()
        {
        }
    }
}