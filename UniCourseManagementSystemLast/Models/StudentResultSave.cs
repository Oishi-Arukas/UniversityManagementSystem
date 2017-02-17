using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class StudentResultSave
    {
        public  int StudentResultId { get; set; }
              [Required(ErrorMessage = "Please select student reg. number.")]
        public int StudentRegisterId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
              [Required(ErrorMessage = "Please select course.")]
        public int EnrollCourseId { get; set; }
              [Required(ErrorMessage = "Please select grade.")]
        public int GradeId { get; set; }
    }
}