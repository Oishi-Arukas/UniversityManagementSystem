using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class StudentEnrollManager
    {
        private StudentEnrollGateway _studentEnrollGateway=new StudentEnrollGateway();
        private CourseGateway _courseGateway=new CourseGateway();

        public int EnrollStudentInCourse(StudentEnroll studentEnroll)
        {
            if (studentEnroll.CourseId==0)
            {
                return -1;
            }
            if (IsCourseAlreadyAssigned(studentEnroll))
            {
                return _studentEnrollGateway.EnrollStudentInCourse(studentEnroll);
            }
            return 0;
        }

        public StudentEnroll GetStudentByStudentId(int studentId)
        {
           
            return _studentEnrollGateway.GetStudentByStudentId(studentId);
        }

        public List<Course> GetEnrolledCoursesByStudentId(int studentId)
        {

            List<Course> courseIdList= _studentEnrollGateway.GetEnrolledCoursesByStudentId(studentId);
            List<Course> courseList=new List<Course>();
            foreach (Course course in courseIdList)
            {
                Course aCourse = _courseGateway.GetCourseNameByCourseId(course);
                //aCourse.CourseId = aCourse.CourseId;
                //aCourse.CourseName = aCourse.CourseName;
                courseList.Add(aCourse);

            }
            return courseList;
        }

        public bool IsCourseAlreadyAssigned(StudentEnroll studentEnroll)
        {
            return _studentEnrollGateway.IsCourseAlreadyEnrolled(studentEnroll);
        }
    }
}