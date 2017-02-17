using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
          [Required(ErrorMessage = "Please enter name.")]
        public string TeacherName { get; set; }
        
        public string TeacherAddress { get; set; }
         [Required(ErrorMessage = "Please enter email address.")]
         [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string TeacherEmail { get; set; }
        public string TeacherContact { get; set; }
          [Required(ErrorMessage = "Please enter designation.")]
        public int DesignationId { get; set; }
          [Required(ErrorMessage = "Please enter department.")]
        public int DepartmentId { get; set; }
          [Required(ErrorMessage = "Please enter credit.")]
          [Range(0.0, 50.00, ErrorMessage = "Credit can not be negative.Try again.")]
        public double? CreditTaken { get; set; }
        public double? RemainingCredit { get; set; }
    }
}