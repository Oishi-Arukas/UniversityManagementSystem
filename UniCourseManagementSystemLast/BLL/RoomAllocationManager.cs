using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class RoomAllocationManager
    {
        private RoomAllocationGateway _roomAllocationGateway=new RoomAllocationGateway();
       
        public List<Day> GetAllDays()
        {
            return _roomAllocationGateway.GetAllDays();
        }

        public List<Room> GetAllRooms()
        {
            return _roomAllocationGateway.GetAllRooms();
        }

        public int InsertRoomAllocation(RoomAllocation roomAllocation)
        {
            if (roomAllocation.CourseId == 0)
            {
                return -1;
            }
            if (TimeRangeCheck(roomAllocation))
            {
                int status = IsRoomAllocated(roomAllocation);
                if (status == -1)
                {

                    return _roomAllocationGateway.InsertRoomAllocation(roomAllocation);
                }
                else
                {
                    if (status == 0)
                    {

                        return _roomAllocationGateway.InsertRoomAllocation(roomAllocation);
                    }
                }
                return 0;
            }
            return -2;

        }



        public bool TimeRangeCheck(RoomAllocation roomAllocation)
        {
            if (roomAllocation.FromTime > roomAllocation.ToTime)
            {
                return false;
            }
            return true;
        }

     

        public int IsRoomAllocated(RoomAllocation roomAllocation)
        {
            return _roomAllocationGateway.IsRoomAllocated(roomAllocation);
        }


        public List<ClassSchedule> GetClassSchedulesByDepartmentId(int departmentId)
        {
            List<ClassSchedule> schdeuleList = _roomAllocationGateway.GetClassSchedulesByDepartmentId(departmentId);
            List<ClassSchedule> newList = new List<ClassSchedule>();

            var query = schdeuleList.GroupBy(x => x.CourseCode)
              .Where(g => g.Count() >= 1).ToList();


            foreach (var aQuery in query)
            {
                ClassSchedule aSchedule = new ClassSchedule(); 

                var list = aQuery;
                foreach (ClassSchedule schedule in list)
                {


                    aSchedule.CourseCode = schedule.CourseCode;
                    aSchedule.CourseName = schedule.CourseName;
                    string day = schedule.DayName;
                    string roomNumber = schedule.RoomNumber;
                    string fromTime = schedule.FromTime != null ? schedule.FromTime.Value.ToString("hh:mm tt") : "n/a";
                    string toTime = schedule.ToTime != null ? schedule.ToTime.Value.ToString("hh:mm tt") : "n/a";
                    if (day == "" || roomNumber == "" || fromTime == "")
                    {
                        aSchedule.ScheduleInfo = "Not Scheduled Yet";
                    }
                    else
                    {
                        if(schedule.AllocationStatus==1)
                        {
                            if (aSchedule.ScheduleInfo == "Not Scheduled Yet")
                            {
                                aSchedule.ScheduleInfo = "R.No:" + roomNumber + "," + day + "," + fromTime + "-" + toTime + ";";
                            }
                            else
                            {
                                aSchedule.ScheduleInfo += "R.No:" + roomNumber + "," + day + "," + fromTime + "-" + toTime + ";";
                            }
                            
                        }
                        else if (schedule.AllocationStatus == 0)
                        {
                            aSchedule.ScheduleInfo = "Not Scheduled Yet";
                        }
                    }

                }
                newList.Add(aSchedule);


            }

            return newList;
        }

        public int UnallocateRooms()
        {
            return _roomAllocationGateway.UnallocateRooms();
        }
    }
}