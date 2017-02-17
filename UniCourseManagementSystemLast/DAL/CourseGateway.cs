using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class CourseGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;


        public int InsertCourse(Course course)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            course.CourseStatus = 0;
            string query = "INSERT INTO Courses VALUES('" + course.CourseCode + "','" + course.CourseName + "','" + course.CourseCredit + "','" + course.CourseDescription + "','" + course.DepartmentId + "','" + course.SemesterId + "','" + course.CourseStatus + "')";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }


        public bool IsCourseExist(Course course)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.ConnectionString = connectionString;

            string query = "SELECT * FROM Courses WHERE CourseName = '" + course.CourseName + "'or CourseCode='" + course.CourseCode + "'";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
           
            if (reader.HasRows)
            {
                
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }

        public List<Course> GetAllCourses()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Courses  ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                Course course=new Course();
                course.CourseName = reader["CourseName"].ToString();
                
                course.CourseId = Convert.ToInt32(reader["CourseId"]);
                course.DepartmentId = (int) reader["DepartmentId"];
                course.CourseCode = reader["CourseCode"].ToString();
                course.CourseCredit = (double)reader["CourseCredit"];
                course.SemesterId = (int)reader["SemesterId"];
                courses.Add(course);
            }
            reader.Close();
            connection.Close();
            return courses;
        }

        public Course GetCourseNameByCourseId(Course courseId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Courses where CourseId='"+courseId.CourseId+"' ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            Course course = null;
            while (reader.Read())
            {
                course = new Course();
                course.CourseName = reader["CourseName"].ToString();
                course.CourseCode = reader["CourseCode"].ToString();
                course.CourseId = Convert.ToInt32(reader["CourseId"]);
               
               
            }
            reader.Close();
            connection.Close();
            return course;
        }

       

        public List<CourseStatics> GetCourseStatics(int departmentId)
        {
            //int status = 1;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select CourseCode,CourseName,Semester,TeacherName,CourseStatus from CourseStatics where DeptId='" + departmentId + "' and CourseStatus='"+1+"'";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<CourseStatics> courseStaticList = new List<CourseStatics>();
            while (reader.Read())
            {
                CourseStatics courseStatics = new CourseStatics();
                courseStatics.CourseName = reader["CourseName"].ToString();
                courseStatics.Semester = reader["Semester"].ToString();
                courseStatics.CourseStatus = (int)reader["CourseStatus"];
                courseStatics.CourseCode = reader["CourseCode"].ToString();
                courseStatics.TeacherName = reader["TeacherName"].ToString();
                courseStaticList.Add(courseStatics);
            }
            reader.Close();
            connection.Close();
            return courseStaticList;
        }

        public List<CourseStatics> GetAllCourseStatics()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select distinct CourseCode,CourseName,Semester,TeacherName,CourseStatus from CourseStatics  ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<CourseStatics> courseStaticList = new List<CourseStatics>();
            while (reader.Read())
            {
                CourseStatics courseStatics = new CourseStatics();
                courseStatics.CourseName = reader["CourseName"].ToString();
                courseStatics.Semester = reader["Semester"].ToString();
                courseStatics.CourseStatus = reader["CourseStatus"] == DBNull.Value
                    ? (int?)null
                    : (int)reader["CourseStatus"]; 
                courseStatics.CourseCode = reader["CourseCode"].ToString();
                courseStatics.TeacherName = reader["TeacherName"].ToString();
                courseStaticList.Add(courseStatics);
            }
            reader.Close();
            connection.Close();
            return courseStaticList;
        }

        public int UnassignCourse()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //int status = 0;

            string query = "Update Courses set  CourseStatus='"+0+"'";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            command.CommandText = "Update Teachers set RemainingCredit=CreditTaken";
            command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }


        
    }
}