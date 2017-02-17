using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class SemesterGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;


        public List<Semester> GetAllSemesters()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Semester";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Semester> semesters = new List<Semester>();
            while (reader.Read())
            {
                Semester semester = new Semester();
                semester.SemesterName = reader["Semester"].ToString();
               
                semester.SemesterId = Convert.ToInt32(reader["Id"]);
               
                semesters.Add(semester);
            }
            reader.Close();
            connection.Close();
            return semesters;
        }

        public string GetSemesterNameById(int semesterId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT Semester FROM Semester where Id='" + semesterId + "' ";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            string semester = "";
           
            while (reader.Read())
            {
               
                semester= reader["Semester"].ToString();

             

              
            }
            reader.Close();
            connection.Close();
            return semester;
        }
    }
}