using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystemWebApp.DAL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.BLL
{
    public class DepartmentManager
    {
       private DepartmentGateway _departmentGateway=new DepartmentGateway();

        public int InsertDepartment(Department department)
        {
            Department aDepartment = IsDepartmentExist(department);
            if (aDepartment == null)
            {
                if(DepartmentCodeChecking(department))
                {
                    return _departmentGateway.InsertDepartment(department);
                }
                return -1;
            }
            return 0;
        }

        public Department IsDepartmentExist(Department department)
        {

           return _departmentGateway.IsDepartmentExist(department);
          
            
        }

        public bool DepartmentCodeChecking(Department department)
        {
            if (department.DepartmentCode.Length < 2 || department.DepartmentCode.Length > 7)
            {
                return false;
            }
            return true;
        }

        public List<Department> GetAllDepartments()
        {
            return _departmentGateway.GetAllDepartments();
        }

        public bool IfInputIsEmpty(Department department)
        {
            if (String.IsNullOrWhiteSpace(department.DepartmentCode) || String.IsNullOrWhiteSpace(department.DepartmentName))
            {
                return true;
            }
            return false;
        }

        public string GetDepartmentNameById(int departmentId)
        {
            return _departmentGateway.GetDepartmentNameById(departmentId);
        }

    }
}