using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStudentsGrades.Services;
using MyStudentsGrades.Models;
using MyStudentsGrades.Models.ViewModels;
using MyStudentsGrades.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

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
                return RedirectToAction("Create", "Students", new { id = classroom.Id });
            else
                return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            { 
                return View(await _classroomService.FindByIdAsync(id.Value));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Delete (int? id)
        {
            try
            {
                return View(await _classroomService.FindByIdAsync(id.Value));
            }
            catch(ApplicationException e)
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
                await _classroomService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Edit (int? id)
        {
            try
            {
                return View(await _classroomService.FindByIdAsync(id.Value));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Classroom classroom)
        {
            if (!ModelState.IsValid)
                return View(classroom);

            if (id != classroom.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                await _classroomService.UpdateAsync(classroom);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> CompleteInfo(int? id)
        {
            try
            {
                var classroom = await _classroomService.FindByIdAsync(id);

                var totalGrades = await _classroomService.GetTotalGradesAsync(classroom);

                return View(totalGrades);
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
