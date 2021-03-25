using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStudentsGrades.Services;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly ClassroomService _classroomService;

        public ClassroomsController(ClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomService.FindAllAsync();
            return View(classrooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom classroom, bool addStudents)
        {
            if (!ModelState.IsValid)
                return View(classroom);

            await _classroomService.InsertAsync(classroom);

            if (addStudents)
                return RedirectToAction("Students", nameof(Create));
            else
                return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            { 
                return View(await _classroomService.FindById(id.Value));
            }
            catch (ApplicationException e)
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
