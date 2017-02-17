using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please enter Department Code.")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be 2 to 7 letters.Try again.")]
        public string DepartmentCode { get; set; }
          [Required(ErrorMessage = "Please enter Department Name.")]
        public string DepartmentName { get; set; }


        public Department(int departmentId, string departmentCode, string departmentName)
        {
            DepartmentId = departmentId;
            DepartmentCode = departmentCode;
            DepartmentName = departmentName;
        }

        public Department()
        {

        }
    }
}