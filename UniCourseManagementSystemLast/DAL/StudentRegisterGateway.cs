using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class StudentRegisterGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;

        public int InsretStudent(StudentRegister student)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "INSERT INTO StudentsRegister VALUES('" + student.StudentName + "','" + student.StudentEmail + "','" + student.StudentContact + "','" + student.StudentAddress + "','" + student.RegDate.ToString("yyyy-MM-dd") + "','" + student.DepartmentId + "','" + student.RegNumber + "')";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

      
        public List<StudentRegister> GetAllStudents()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from StudentsRegister  ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


           List<StudentRegister> studenList=new List<StudentRegister>();
            while (reader.Read())
            {
                StudentRegister student = new StudentRegister();
                //student.StudentName = reader["StudentName"].ToString();
                //student.StudentEmail = reader["StudentEmail"].ToString();

                student.StudentId = (int)reader["Id"];
                student.RegNumber = reader["RegNumber"].ToString();
                studenList.Add(student);

            }
            reader.Close();
            connection.Close();
            return studenList;
        }

        public List<StudentRegister> GetStudentsByDeptId(int departmentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from StudentsRegister where DeptID='" + departmentId + "'  ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<StudentRegister> studenList = new List<StudentRegister>();
            while (reader.Read())
            {
                StudentRegister student = new StudentRegister();
                //student.StudentName = reader["StudentName"].ToString();
                //student.StudentEmail = reader["StudentEmail"].ToString();

                student.RegDate = (DateTime)reader["RegDate"];
                student.RegNumber = reader["RegNumber"].ToString();
                studenList.Add(student);

            }
            reader.Close();
            connection.Close();
            return studenList;
        }

        public bool IsEmailExist(StudentRegister student)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT * FROM StudentsRegister WHERE StudentEmail = '" + student.StudentEmail + "' ";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {


                reader.Close();
                connection.Close();
                return false;

            }
            reader.Close();
            connection.Close();
            return true;
        }


      
    }
}