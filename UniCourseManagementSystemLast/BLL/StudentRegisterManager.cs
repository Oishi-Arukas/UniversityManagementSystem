using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class StudentRegisterManager
    {
        private StudentRegisterGateway _studentRegisterGateway=new StudentRegisterGateway();
        private DepartmentGateway _departmentGateway = new DepartmentGateway();
        public int InsertStudent(StudentRegister student)
        {
           
            if (IsEmailExist(student))
            {
                GenerateRegNumber(student);
                student.DepartmentName = _departmentGateway.GetDepartmentNameById(student.DepartmentId);
                return _studentRegisterGateway.InsretStudent(student);
            }
            return 0;
        }

      

        public List<StudentRegister> GetAllStudents()
        {
            return _studentRegisterGateway.GetAllStudents();
        }

        public List<StudentRegister> GetStudentsByDeptId(int departmentId)
        {
            return _studentRegisterGateway.GetStudentsByDeptId(departmentId);
        }


        public StudentRegister GenerateRegNumber(StudentRegister studentRegister)
        {
            string departmentCode = _departmentGateway.GetDepartmentCodeById(studentRegister.DepartmentId);
            string symbol = "-";
            string year = studentRegister.RegDate.ToString("yyyy");

            int yearCount = 0;
            List<StudentRegister> studentRegisters = GetStudentsByDeptId(studentRegister.DepartmentId);

            int count = studentRegisters.Count;
            if (count != 0)
            {
                foreach (StudentRegister register in studentRegisters)
                {
                    string regYear = register.RegDate.ToString("yyyy");
                    if (regYear == year)
                    {
                        yearCount++;


                    }


                }



            }

            yearCount++;
            string regNumber = departmentCode + symbol + year + symbol +
                                  yearCount.ToString().PadLeft(3, '0');

            studentRegister.RegNumber = regNumber;
            return studentRegister;
        }

        public bool IsEmailExist(StudentRegister student)
        {
            return _studentRegisterGateway.IsEmailExist(student);
        }
    }
}