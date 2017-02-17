using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class StudentResultGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;

        public int SaveStudentResult(StudentResultSave studentResult)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "INSERT INTO SaveStudentResult VALUES('" + studentResult.EnrollCourseId + "','" + studentResult.GradeId + "','" + studentResult.StudentRegisterId + "' )";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public List<Grade> GetGradeList()
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "Select * from Grades";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            List<Grade> grades = new List<Grade>();
            while (reader.Read())
            {
                Grade aGrade = new Grade();
                aGrade.GradeName = reader["Grade"].ToString();

                aGrade.GradeId = Convert.ToInt32(reader["Id"]);

                grades.Add(aGrade);
            }
            reader.Close();
            connection.Close();
            return grades;
        }

        public bool CheckIfCourseAlreadyGraded(StudentResultSave studentResult)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.ConnectionString = connectionString;

            string query = "SELECT * FROM SaveStudentResult WHERE CourseId = '" + studentResult.EnrollCourseId + "'and StudentId='" + studentResult.StudentRegisterId + "'";
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

        public int UpdateStudentResult(StudentResultSave studentResult)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "Update SaveStudentResult set GradeId='"+studentResult.GradeId+"' where StudentId='"+studentResult.StudentRegisterId+"'and CourseId='" + studentResult.EnrollCourseId + "'";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public List<StudentResultShow> GetResultByRegistrationId(int studentRegisterId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select CourseCode,CourseName,Grade from Showresult where Id='" + studentRegisterId + "' ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<StudentResultShow> resultList = new List<StudentResultShow>();
            while (reader.Read())
            {
                StudentResultShow result = new StudentResultShow();
                result.CourseName = reader["CourseName"].ToString();
                result.GradeName = reader["Grade"].ToString();

                result.CourseCode = reader["CourseCode"].ToString();

                resultList.Add(result);
            }
            reader.Close();
            connection.Close();
            return resultList;
        }
    }
}