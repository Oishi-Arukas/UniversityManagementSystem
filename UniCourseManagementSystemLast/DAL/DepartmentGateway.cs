using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class DepartmentGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;

        public int InsertDepartment(Department department)
        {
            SqlConnection connection = new SqlConnection(connectionString);
           

            string query = "INSERT INTO Departments VALUES('" + department.DepartmentCode + "','" + department.DepartmentName + "')";
            

            SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }



        public Department IsDepartmentExist(Department department)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT * FROM Departments WHERE DepartmentCode = '" + department.DepartmentCode + "' or DepartmentName='"+department.DepartmentName+"'";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Department aDepartment = null;
            if (reader.HasRows)
            {

                 reader.Read();
                 int id = (int)reader["DepartmentId"];
                 string name = reader["DepartmentName"].ToString();
                 string code = reader["DepartmentCode"].ToString();
                 aDepartment=new Department(id,code,name);
                

            }
            reader.Close();
            connection.Close();
            return aDepartment;
        }



        public List<Department> GetAllDepartments()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Departments";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Department> departments = new List<Department>();
            while (reader.Read())
            {
                string name = reader["DepartmentName"].ToString();
                string code = reader["DepartmentCode"].ToString();
                int id = Convert.ToInt32(reader["DepartmentId"]);
                Department department = new Department(id,code,name);
                departments.Add(department);
            }
            reader.Close();
            connection.Close();
            return departments;
        }


        public string GetDepartmentNameById(int departmentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT DepartmentName FROM Departments WHERE DepartmentId = '" + departmentId + "' ";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
         
            string departmentName = "";
            if (reader.HasRows)
            {

                reader.Read();
               
                departmentName = reader["DepartmentName"].ToString();
             


            }
            reader.Close();
            connection.Close();
            return departmentName;
        }


        public string GetDepartmentCodeById(int departmentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT DepartmentCode FROM Departments WHERE DepartmentId = '" + departmentId + "' ";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            string departmentCode = "";
            if (reader.HasRows)
            {

                reader.Read();

                departmentCode = reader["DepartmentCode"].ToString();



            }
            reader.Close();
            connection.Close();
            return departmentCode;
        }
    }
}