using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class CourseManager
    {
        private CourseGateway _courseGateway=new CourseGateway();
        private SemesterGateway _semesterGateway=new SemesterGateway();
        public int InsertCourse(Course course)
        {
            if (IsCourseExist(course))
            {
                return 0;
            }
          
              
            return _courseGateway.InsertCourse(course);
                

        }

        public bool IsCourseExist(Course course)
        {
            return _courseGateway.IsCourseExist(course);
        }

       

       

       
        public List<Course> GetAllCourses()
        {
            return _courseGateway.GetAllCourses();
        }

        public List<CourseStatics> GetCourseStatics(int departmentId)
        {
            List<CourseStatics> courseStaticsList;
            List<Course> courses;
            List<CourseStatics> newCourseStatics;
            string assigned = "";
            if(departmentId!=0)
            {

                courseStaticsList = _courseGateway.GetCourseStatics(departmentId);
                courses  = GetAllCourses();
                var courseList = courses.Where(a => a.DepartmentId == departmentId);
                newCourseStatics = new List<CourseStatics>();
                CourseStatics aCourseStatics = null;
               
                foreach (Course course in courseList)
                {
                
                    bool flag = false;
                    foreach (CourseStatics courseStatics in courseStaticsList)
                    {
                        if (courseStatics.CourseCode==course.CourseCode)
                        {
                            if (courseStatics.CourseStatus==1)
                            {
                                newCourseStatics.Add(courseStatics);
                                flag = true;
                                break;
                            }
                      
                          
                        }
                    }
                    if (!flag)
                    {
                        string semester = _semesterGateway.GetSemesterNameById(course.SemesterId);
                        assigned= "Not Assigned Yet";
                        aCourseStatics=new CourseStatics(course.CourseCode,course.CourseName,semester,assigned);
                        newCourseStatics.Add(aCourseStatics);
                    }
                
                }
                return newCourseStatics;
            }
           courseStaticsList = _courseGateway.GetAllCourseStatics();
            //courses = GetAllCourses();
            newCourseStatics = new List<CourseStatics>();
            CourseStatics bCourseStatics = null;
            foreach (CourseStatics courseStatic in courseStaticsList)
            {
                if (courseStatic.CourseCode!="")


                {
                    if (courseStatic.TeacherName!="")
                    {
                        if (courseStatic.CourseStatus==1)
                        {
                            newCourseStatics.Add(courseStatic);
                        }
                        else
                        {
                            assigned = "Not Assigned Yet";
                            bCourseStatics = new CourseStatics(courseStatic.CourseCode, courseStatic.CourseName, courseStatic.Semester, assigned);
                            newCourseStatics.Add(bCourseStatics);
                        }
                       
                    }
                    else
                    {
                        assigned = "Not Assigned Yet";
                        bCourseStatics=new CourseStatics(courseStatic.CourseCode,courseStatic.CourseName,courseStatic.Semester,assigned);
                        newCourseStatics.Add(bCourseStatics);
                    }
                }
            }
            return newCourseStatics;
        }

        public int UnassignCourses()
        {
            return _courseGateway.UnassignCourse();
        }
    }
}