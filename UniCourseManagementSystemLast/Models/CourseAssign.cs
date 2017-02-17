using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class CourseAssign
    {
        public int Id { get; set; }
         [Required(ErrorMessage = "Please select department.")]
        public int DepartmentId { get; set; }
            [Required(ErrorMessage = "Please select teacher.")]
        public int TeacherId { get; set; }
          
           [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double? CreditTaken { get; set; }
         
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double? RemainingCredit { get; set; }
            [Required(ErrorMessage = "Please select course.")]
        public int CourseId { get; set; }
         
        public string CourseName { get; set; }
        
        public double? CourseCredit { get; set; }
    }
}