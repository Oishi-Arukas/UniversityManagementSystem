using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
    }

    public class Day
    {
        public int DayId { get; set; }
        public string Days { get; set; }
    }

    public class RoomAllocation
    {
        public int RoomAllocationId { get; set; }
           [Required(ErrorMessage = "Please select department.")]
        public int DepartmentId { get; set; }
         [Required(ErrorMessage = "Please select room.")]
        public int RoomId { get; set; }
         [Required(ErrorMessage = "Please select day.")]
        public int DayId { get; set; }
         [Required(ErrorMessage = "Please select course.")]
        public int CourseId { get; set; }
         //[Required(ErrorMessage = "Please select time.")]
        [DataType(DataType.Time)]
       
        public DateTime FromTime { get; set; }
    
         //[Required(ErrorMessage = "Please select time.")]
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }
      
        public int RoomAllocationStatus { get; set; }
        
    }

}