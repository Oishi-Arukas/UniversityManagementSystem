using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class StudentResultManager
    {
        private StudentResultGateway _studentResultGateway=new StudentResultGateway();
        private StudentEnrollManager _studentEnrollManager=new StudentEnrollManager();

        public int SaveStudentResult(StudentResultSave studentResult)
        {
            if (studentResult.EnrollCourseId == 0)
            {
                return -1;
            }
            if (CheckIfCourseAlreadyGraded(studentResult))
            {
               return UpdateStudentResult(studentResult);

            }
            return _studentResultGateway.SaveStudentResult(studentResult);
        }

        public List<Grade> GetGradeList()
        {
            return _studentResultGateway.GetGradeList();
        }

        public bool CheckIfCourseAlreadyGraded(StudentResultSave studentResult)
        {
            return _studentResultGateway.CheckIfCourseAlreadyGraded(studentResult);
        }

        public int UpdateStudentResult(StudentResultSave studentResult)
        {
            return _studentResultGateway.UpdateStudentResult(studentResult);
        }


        public List<StudentResultShow> GetResultByRegistrationId(int studentRegisterId)
        {
            List<StudentResultShow> resultList = _studentResultGateway.GetResultByRegistrationId(studentRegisterId);
            List<Course> enrolledCourseList = _studentEnrollManager.GetEnrolledCoursesByStudentId(studentRegisterId);
            List<StudentResultShow> newResultList=new List<StudentResultShow>();
            StudentResultShow studentResult=null;
            foreach (Course course in enrolledCourseList)
            {
                bool flag = false;
                foreach (StudentResultShow resultShow in resultList)
                {
                    if (resultShow.CourseName==course.CourseName)
                    {
                        newResultList.Add(resultShow);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    studentResult=new StudentResultShow();
                    string grade = "Not Graded Yet";
                    studentResult.CourseCode = course.CourseCode;
                    studentResult.CourseName = course.CourseName;
                    studentResult.GradeName = grade;
                    newResultList.Add(studentResult);
                }
            }
            return newResultList;

        }

    }
}