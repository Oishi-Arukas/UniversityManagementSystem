using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class TeacherGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;

        public int InsertTeacher(Teacher teacher)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "INSERT INTO Teachers VALUES('" + teacher.TeacherName + "','" + teacher.TeacherAddress + "','" + teacher.TeacherEmail + "','" + teacher.TeacherContact + "','" + teacher.DesignationId + "','" + teacher.DepartmentId + "','" + teacher.CreditTaken + "','" + teacher.CreditTaken + "')";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }


        public List<Designation> GetDesignations()
        {
             SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Designation";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Designation> designations = new List<Designation>();
            while (reader.Read())
            {
                Designation designation=new Designation();
                designation.Title = reader["Title"].ToString();
                
                designation.DesignationId= Convert.ToInt32(reader["Id"]);
              
                designations.Add(designation);
            }
            reader.Close();
            connection.Close();
            return designations;
        }


        public bool IsEmailExist(Teacher teacher)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT * FROM Teachers WHERE TeacherEmail = '" + teacher.TeacherEmail + "' ";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //Department aDepartment = null;
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

        public List<Teacher> GetTeachersByDepartmentId(string departmentName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Teachers t join Departments d on t.DeptId=d.DepartmentId where d.DepartmentName='"+departmentName+"'";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Teacher> teachers = new List<Teacher>();
            while (reader.Read())
            {
                Teacher teacher=new Teacher();
                teacher.TeacherName = reader["TeacherName"].ToString();
                teacher.CreditTaken = (double)reader["CreditTaken"];
                teacher.TeacherId = Convert.ToInt32(reader["Id"]);

                teachers.Add(teacher);
            }
            reader.Close();
            connection.Close();
            return teachers;
        }

        public List<Teacher> GetTeachers()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select * from Teachers ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Teacher> teachers = new List<Teacher>();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.TeacherName = reader["TeacherName"].ToString();
                //teacher.CreditTaken = (decimal)reader["CreditTaken"];
                teacher.TeacherId = Convert.ToInt32(reader["Id"]);
                teacher.DepartmentId = (int) reader["DeptId"];
                teacher.CreditTaken = (double) reader["CreditTaken"];
                teacher.RemainingCredit = (double) reader["RemainingCredit"];
                teachers.Add(teacher);
            }
            reader.Close();
            connection.Close();
            return teachers;
        }

        public int InsertCourseAssign(CourseAssign courseAssign)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //int status = 1;

            string query = "INSERT INTO CourseAssignTeacher VALUES('" + courseAssign.DepartmentId + "','" + courseAssign.TeacherId + "','" + courseAssign.CourseId + "')";

           
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            command.CommandText= "Update Teachers set RemainingCredit='" + courseAssign.RemainingCredit + "' where Id='" + courseAssign.TeacherId + "'";
            command.ExecuteNonQuery();
            command.CommandText = "Update  Courses set CourseStatus='" + 1 + "'where CourseId='" +courseAssign.CourseId + "'";
            command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public int IsCourseAssigned(CourseAssign courseAssign)
        {
            int status = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Select CourseStatus from Courses where CourseId='" + courseAssign.CourseId + "' ";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                status =(int) reader["CourseStatus"];
               
            }
            reader.Close();
            connection.Close();
            return status;
        }

        }
    }
