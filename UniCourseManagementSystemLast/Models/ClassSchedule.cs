using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystemWebApp.Models
{
    public class ClassSchedule
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string RoomNumber { get; set; }
        public string DayName { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string ScheduleInfo { get; set; }
        public int? AllocationStatus { get; set; }

    }
}