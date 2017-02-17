using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class StudentEnrollGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;


        public int EnrollStudentInCourse(StudentEnroll studentEnroll)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "INSERT INTO EnrollStudent VALUES('" + studentEnroll.StudentRegisterId + "','" + studentEnroll.CourseId + "','" + studentEnroll.Enrolldate.ToString("yyyy-MM-dd") + "' )";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public StudentEnroll GetStudentByStudentId(int studentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from StudentsRegister where Id='" + studentId + "' ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            StudentEnroll student = null;
            while (reader.Read())
            {
                student = new StudentEnroll();
                student.StudentName = reader["StudentName"].ToString();
                student.StudentEmail = reader["StudentEmail"].ToString();

                student.DepartmentId = (int)reader["DeptId"];


            }
            reader.Close();
            connection.Close();
            return student;
        }

        public bool IsCourseAlreadyEnrolled(StudentEnroll studentEnroll)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM EnrollStudent where CourseId='" + studentEnroll.CourseId + "' and StudentId='"+studentEnroll.StudentRegisterId+"' ";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;

        }

        public List<Course> GetEnrolledCoursesByStudentId(int studentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select CourseId from EnrollStudent where StudentId='" + studentId + "' ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


           
            List<Course> courseIdList=new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
               
                
                course.CourseId = (int)reader["CourseId"];
                courseIdList.Add(course);

            }
            reader.Close();
            connection.Close();
            return courseIdList;
        }

    }
}