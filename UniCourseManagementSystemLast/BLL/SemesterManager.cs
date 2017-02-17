using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class SemesterManager
    {
       private SemesterGateway _semesterGateway=new SemesterGateway();

        public List<Semester> GetAllSemesters()
        {
            return _semesterGateway.GetAllSemesters();
        }

        public string GetSemesterNameById(int semesterId)
        {
            return _semesterGateway.GetSemesterNameById(semesterId);
        }
    }
}