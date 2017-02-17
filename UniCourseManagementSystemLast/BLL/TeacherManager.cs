using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class TeacherManager
    {
        private TeacherGateway _teacherGateway=new TeacherGateway();

        public int InsertTeacher(Teacher teacher)
        {
            if (IsEmailExist(teacher))
            {
               
                    return _teacherGateway.InsertTeacher(teacher);
                
               
            }
          
            return 0;
        }

        public List<Designation> GetDesignations()
        {
            return _teacherGateway.GetDesignations();
        }

       

        public bool IsEmailExist(Teacher teacher)
        {
            return _teacherGateway.IsEmailExist(teacher);
        }

        public List<Teacher> GetTeachersByDepartmentId(string departmentName)
        {
            return _teacherGateway.GetTeachersByDepartmentId(departmentName);
        }

        public List<Teacher> GetTeachers()
        {
            return _teacherGateway.GetTeachers();
        }

        public int InsertCourseAssign(CourseAssign courseAssign)
        {
            if (courseAssign.TeacherId == 0 || courseAssign.CourseId == 0)
            {
                return -1;
            }
            int status=IsCourseAssigned(courseAssign);
            if (status==0)
            {
                courseAssign.RemainingCredit = courseAssign.RemainingCredit - courseAssign.CourseCredit;
                return _teacherGateway.InsertCourseAssign(courseAssign);
            }
            return 0;
        }

        public int IsCourseAssigned(CourseAssign courseAssign)
        {
           return _teacherGateway.IsCourseAssigned(courseAssign);
        }
    }
}