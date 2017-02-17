using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please enter code.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Code must be at least 5 letters.Try again.")]
        public string CourseCode { get; set; }
       [Required(ErrorMessage = "Please enter name.")]
        public string CourseName { get; set; }
       [Required(ErrorMessage = "Please enter credit.")]
       [Range(0.5, 5.0, ErrorMessage = "Credit range must be from 0.5 to 5.0.Try again.")]
        public double? CourseCredit { get; set; }
        public string CourseDescription { get; set; }
        [Required(ErrorMessage = "Please select department.")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select semester.")]
        public int SemesterId { get; set; }
        public int CourseStatus { get; set; }

    }
}