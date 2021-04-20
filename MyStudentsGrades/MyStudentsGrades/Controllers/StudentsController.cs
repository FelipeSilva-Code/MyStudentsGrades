using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStudentsGrades.Services;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;

        public StudentsController (StudentService studentService)
        {
            _studentService = studentService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create (int id)
        {
            Student student = new Student { ClassroomId = id };
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, bool addStudents)
        {
            if (!ModelState.IsValid)
                return View(student);

            await _studentService.InsertAsync(student);

            int classroomId = student.ClassroomId;

            if (addStudents)
                return RedirectToAction("Create", "Students", new { id = classroomId });
            else
                return RedirectToAction("CompleteInfo", "Classrooms", new { id = classroomId });
        }

        public async Task<IActionResult> Details (int? id)
        {
            try
            {
                var student = await _studentService.FindByIdAsync(id.Value);
                return View(student);
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
         
        }

        public async Task<IActionResult> Delete (int? id)
        {
            try
            {
                var student = await _studentService.FindByIdAsync(id.Value);
                return View(student);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                int classroomId = await _studentService.RemoveAsync(id);
                return RedirectToAction("CompleteInfo", "Classrooms", new { id = classroomId });
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
