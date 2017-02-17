using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.DAL
{
    public class RoomAllocationGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UCRMS"].ConnectionString;

        public List<Day> GetAllDays()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Days";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Day> days = new List<Day>();
            while (reader.Read())
            {
                Day day=new Day();
               
                day.Days = reader["Day"].ToString();
                day.DayId = Convert.ToInt32(reader["Id"]);
               
                days.Add(day);
            }
            reader.Close();
            connection.Close();
            return days;
        }


        public List<Room> GetAllRooms()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Rooms";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<Room> rooms = new List<Room>();
            while (reader.Read())
            {
                Room room=new Room();

                room.RoomNumber = reader["RoomNumber"].ToString();
                room.RoomId = Convert.ToInt32(reader["Id"]);

                rooms.Add(room);
            }
            reader.Close();
            connection.Close();
            return rooms;
        }


        public int InsertRoomAllocation(RoomAllocation roomAllocation)
        {
            SqlConnection connection = new SqlConnection(connectionString);


            //string query = "INSERT INTO RoomAllocation VALUES('" + roomAllocation.RoomId + "','" + roomAllocation.DayId + "','" + roomAllocation.CourseId + "','" + roomAllocation.FromTime.ToString("hh:mm") + "','" + roomAllocation.FromTimeSelected + "','" + roomAllocation.ToTime.ToString("hh:mm") + "','" + roomAllocation.ToTimeSelected + "','" + roomAllocation.DepartmentId + "')";
            //string status = "Located";
            string query = "INSERT INTO RoomAllocation(RoomId,DayId,CourseId,FromTime,ToTime,DeptId,RoomAllocationStatus) VALUES('" + roomAllocation.RoomId + "','" + roomAllocation.DayId + "','" + roomAllocation.CourseId + "','" + roomAllocation.FromTime.ToString("hh:mm tt") + "','" + roomAllocation.ToTime.ToString("hh:mm tt") + "','" + roomAllocation.DepartmentId + "',"+1+")";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public int IsRoomAllocated(RoomAllocation roomAllocation)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //string query = "SELECT * FROM RoomAllocation where RoomId='" + roomAllocation.RoomId + "'and DayId='" + roomAllocation.DayId + "'and (FromTime <'" + roomAllocation.ToTime.ToString("hh:mm") + "' and '"+roomAllocation.FromTime.ToString("hh:mm")+"'< ToTime)  and FromTimeSelected='" + roomAllocation.FromTimeSelected + "' and ToTimeSelected='" + roomAllocation.ToTimeSelected + "' ";
            string query = "SELECT * FROM RoomAllocation where RoomId='" + roomAllocation.RoomId + "'and DayId='" + roomAllocation.DayId + "'and (FromTime <'" + roomAllocation.ToTime.ToString("hh:mm tt") + "' and '" + roomAllocation.FromTime.ToString("hh:mm tt") + "'< ToTime) ";
            SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            int status = -1;
        
            List<int> roomAllocationId=new List<int>();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                RoomAllocation allocation = new RoomAllocation();
                while(reader.Read())

                {
                    status = (int)reader["RoomAllocationStatus"];
                    allocation.RoomAllocationId = (int) reader["RoomAllocationId"];
                  
                    roomAllocationId.Add(allocation.RoomAllocationId);
                }
            

            }
            reader.Close();
            foreach (int id in roomAllocationId)
            {
                command.CommandText = "Update RoomAllocation set  RoomAllocationStatus=" + 1 + " where RoomAllocationId='" + id + "'";
                command.ExecuteNonQuery();
            }
           
            connection.Close();
            return status;

        }

        public List<ClassSchedule> GetClassSchedulesByDepartmentId(int departmentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ClassSchedule where DepartmentId='" + departmentId + "'";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            List<ClassSchedule> scheduleList = new List<ClassSchedule>();
            while (reader.Read())
            {
                ClassSchedule schedule = new ClassSchedule();

                schedule.RoomNumber = reader["RoomNumber"].ToString();
                schedule.CourseCode = reader["CourseCode"].ToString();
                schedule.CourseName = reader["CourseName"].ToString();
                schedule.DayName = reader["Day"].ToString();

                schedule.FromTime = reader["FromTime"] == DBNull.Value
                    ? (DateTime?)null
                    : (DateTime)reader["FromTime"];
                schedule.ToTime = reader["ToTime"] == DBNull.Value
                    ? (DateTime?)null
                    : (DateTime)reader["ToTime"];
                schedule.AllocationStatus = reader["RoomAllocationStatus"] == DBNull.Value
                    ? (int?) null
                    : (int) reader["RoomAllocationStatus"];
                scheduleList.Add(schedule);
            }
            reader.Close();
            connection.Close();
            return scheduleList;
        }

        public int UnallocateRooms()
        {
            SqlConnection connection = new SqlConnection(connectionString);
          

            string query = "Update RoomAllocation set  RoomAllocationStatus=" + 0 + "";


            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }
    }
}