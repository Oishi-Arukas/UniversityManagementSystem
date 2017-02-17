using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class StudentRegister
    {
        public int StudentId { get; set; }
         [Required(ErrorMessage = "Please enter name.")]
        public string StudentName { get; set; }
         [Required(ErrorMessage = "Please enter email address.")]
         [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string StudentEmail { get; set; }
        public string StudentContact { get; set; }
        public string StudentAddress { get; set; }
        public DateTime RegDate { get; set; }
          [Required(ErrorMessage = "Please enter department.")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string RegNumber { get; set; }


    }
}