using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UniversityCourseResultManagementSystemWebApp.BLL;
using UniversityCourseResultManagementSystemWebApp.Models;

namespace UniversityCourseResultManagementSystemWebApp.Controllers
{
    public class StudentController : Controller
    {
        private StudentRegisterManager _studentRegisterManager=new StudentRegisterManager();
        private DepartmentManager _departmentManager=new DepartmentManager();
        private CourseManager _courseManager = new CourseManager();
        private StudentEnrollManager _studentEnrollManager=new StudentEnrollManager();  
        private StudentResultManager _studentResultManager=new StudentResultManager();
            //
        // GET: /Student/
        [HttpGet]
        public ActionResult EnrollStudent()
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
            return View();
        }

        public ActionResult EnrollStudent(StudentEnroll studentEnroll)
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
            var courses = _courseManager.GetAllCourses();
            StudentEnroll student = _studentEnrollManager.GetStudentByStudentId(studentEnroll.StudentRegisterId);

            var courseList = courses.Where(a => a.DepartmentId == student.DepartmentId);
            ViewBag.Courses = new SelectList(courseList, "CourseId", "CourseName");
            int rowAffected = _studentEnrollManager.EnrollStudentInCourse(studentEnroll);
            if(ModelState.IsValid)
            if (rowAffected > 0)
            {
                {
                    ViewBag.ValidationMsg = "Successfull.";
                    ModelState.Clear();
                    return View(studentEnroll);
                }
            }
            if (rowAffected == -1)
            {
                ViewBag.ValidationMsg = "Please select course.";

                return View(studentEnroll);
            }
            ViewBag.ValidationMsg = "This course is already enrolled once to this student.";
           
            return View(studentEnroll);
        }

        public JsonResult GetStudentInfoByRegNumber(int studentId)
        {
            var student = _studentEnrollManager.GetStudentByStudentId(studentId);
            student.DepartmentName = _departmentManager.GetDepartmentNameById(student.DepartmentId);
           
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByDepartmentName(int studentId)
        {
            var courses = _courseManager.GetAllCourses();
            StudentEnroll student = _studentEnrollManager.GetStudentByStudentId(studentId);
           
            var courseList = courses.Where(a => a.DepartmentId == student.DepartmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveStudentResult()
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
            var gradeList = _studentResultManager.GetGradeList();
            ViewBag.Grades = new SelectList(gradeList, "GradeId", "GradeName");
            return View();
        }

        public ActionResult SaveStudentResult(StudentResultSave studentResultSave)
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
            var gradeList = _studentResultManager.GetGradeList();
            ViewBag.Grades = new SelectList(gradeList, "GradeId", "GradeName");
            var courses = _studentEnrollManager.GetEnrolledCoursesByStudentId(studentResultSave.StudentRegisterId);
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
            int rowAffected = _studentResultManager.SaveStudentResult(studentResultSave);
            if (ModelState.IsValid)
            {
                if (rowAffected > 0)
                {
                    studentResultSave.GradeId = 0;
                    studentResultSave.EnrollCourseId = 0;
                    ViewBag.ValidationMsg = "Successfull.";
                    ModelState.Clear();
                    return View(studentResultSave);
                }
            }
            if (rowAffected == -1)
            {
                ViewBag.ValidationMsg = "Please select course.";

                return View(studentResultSave);
            }
            ViewBag.ValidationMsg = "Saving failed.Try again.";
            return View(studentResultSave);
           
        }

        public JsonResult GetEnrolledCoursesByEnrollId(int studentId)
        {
            var courses = _studentEnrollManager.GetEnrolledCoursesByStudentId(studentId);
          
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult RegisterStudent()
        {
            var departmentList = _departmentManager.GetAllDepartments();

            ViewBag.Department = new SelectList(departmentList, "DepartmentId", "DepartmentName");
            StudentRegister student=new StudentRegister();
            student.RegDate = DateTime.Today;
            return View(student);
        }

        public ActionResult RegisterStudent(StudentRegister studentRegister)
        {
            var departmentList = _departmentManager.GetAllDepartments();

            ViewBag.Department = new SelectList(departmentList, "DepartmentId", "DepartmentName");
            int rowAffected = _studentRegisterManager.InsertStudent(studentRegister);
            ViewBag.StudentInfo = studentRegister;
            if(ModelState.IsValid)
            {
                if (rowAffected > 0)
                {
                    ViewBag.ValidationMsg = "Successfull.";
                    ModelState.Clear();
                    StudentRegister student = new StudentRegister();
                    student.RegDate = DateTime.Today;
                    return View(student);
                }
            }
            ViewBag.ValidationMsg = "This email already exist.Try a unique one.";
            return View(studentRegister);
        }

        [HttpGet]
        public ActionResult ShowResult()
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
            return View();
        }

        public ActionResult ShowResult(StudentResultShow studentResult)
        {
            var studentList = _studentRegisterManager.GetAllStudents();

            ViewBag.Students = new SelectList(studentList, "StudentId", "RegNumber");
           
            GeneratePdf(studentResult.StudentRegisterId);
            return View();
        }

        public JsonResult ShowTableResult(int studentId)
        {
            var courseResultList = _studentResultManager.GetResultByRegistrationId(studentId);
            ViewBag.EnrolledCourseResult = courseResultList;
            return Json(courseResultList, JsonRequestBehavior.AllowGet);
        }

        private void GeneratePdf(int studentId)
        {
            //try
            //{

                Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);

                string title = "ABC UNIVERSITY";
                string subTitle = "Grade Sheet";
                

              
                int columnNo = 3;

                PdfPTable tableLayout = new PdfPTable(columnNo);
                tableLayout.HorizontalAlignment = 1;

               

                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                pdfDoc.Open();
               
                Font fTitle = new Font(Font.FontFamily.HELVETICA, 50.0f, Font.NORMAL, BaseColor.BLACK);
                Font fSub = new Font(Font.FontFamily.TIMES_ROMAN, 35.0f, Font.UNDERLINE, BaseColor.BLACK);
                Paragraph para=new Paragraph(title,fTitle);
                para.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(para);
                pdfDoc.Add(new Paragraph(" "));
                Paragraph paraSub = new Paragraph(subTitle, fSub);
                paraSub.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(paraSub);
                pdfDoc.Add(new Paragraph(" \n\n"));
                pdfDoc.Add(new Paragraph(" \n\n"));
             
                StudentEnroll student = _studentEnrollManager.GetStudentByStudentId(studentId);
                student.DepartmentName = _departmentManager.GetDepartmentNameById(student.DepartmentId);
                var studentList = _studentRegisterManager.GetAllStudents();
                StudentRegister aStudent = studentList.Where(a => a.StudentId == studentId).FirstOrDefault();
                //string name = student.StudentName;
                Font infoFont = new Font(Font.FontFamily.TIMES_ROMAN,15.0f, Font.ITALIC, BaseColor.BLACK);
                //Paragraph nameParagraph = new Paragraph("Student Name:  " + name, infoFont);
                pdfDoc.Add(new Paragraph("Student Name:  " + student.StudentName,infoFont));
                pdfDoc.Add(new Paragraph("Reg. Number:  " + aStudent.RegNumber,infoFont));

                pdfDoc.Add(new Paragraph("Department:  " + student.DepartmentName,infoFont));

                pdfDoc.Add(new Paragraph("Email:  " + student.StudentEmail,infoFont));

                pdfDoc.Add(new Paragraph(" \n\n"));

                pdfDoc.Add(new Paragraph(" \n\n"));
                //pdfDoc.Add(tableLayout);
                pdfDoc.Add(Add_Content_To_PDF(tableLayout, studentId));

                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=GradeSheet.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);

                Response.End();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);

            //}
        }


   

     protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout,int studentId)  
     {  
  
        float[] headers = {30,40,25}; //Header Widths  
        tableLayout.SetWidths(headers); //Set the pdf headers  
        tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
        tableLayout.HeaderRows = 1;  
        //Add Title to the PDF file at the top  
  
    
  
  
  
        //tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0)))) {  
        //    Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER  
        //});  
  
  
        ////Add header  
        AddCellToHeader(tableLayout, "Course Code");
        AddCellToHeader(tableLayout, "Course Name");
        AddCellToHeader(tableLayout, "Grade");  
       List<StudentResultShow> studentResult =new List<StudentResultShow>();
     
        ////Add body  
       studentResult = _studentResultManager.GetResultByRegistrationId(studentId);
       foreach (StudentResultShow emp in studentResult)   
        {
            AddCellToBody(tableLayout, emp.CourseCode);
            AddCellToBody(tableLayout, emp.CourseName);
            AddCellToBody(tableLayout, emp.GradeName);  
        
  
        }  
  
        return tableLayout;  
    }
  
    private static void AddCellToHeader(PdfPTable tableLayout, string cellText)  
  {  
  
    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 14, 1, iTextSharp.text.BaseColor.BLACK)))  
    {
        HorizontalAlignment = Element.ALIGN_CENTER,
        Padding = 5,
        BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)  
    });  
   }  

     private static void AddCellToBody(PdfPTable tableLayout, string cellText)  
    {  
    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 1, iTextSharp.text.BaseColor.BLACK)))  
     {  
        HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)  
    });  
   }  


	}
}