using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStudentsGrades.Models;
using MyStudentsGrades.Services;
using MyStudentsGrades.Models.ViewModels;

namespace MyStudentsGrades.Controllers
{
    public class GradesController : Controller
    {

        private readonly GradeService _gradeService;
        private readonly ClassroomService _classroomService;
        private readonly ActivityService _activityService;

        public GradesController (GradeService gradeService, ClassroomService classroomService, ActivityService activityService)
        {
            _gradeService = gradeService;
            _classroomService = classroomService;
            _activityService = activityService;
        }


        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var activity = await _activityService.FindByIdAsync(id);
                var classroomId = activity.ClassroomId;
                var classroom = await _classroomService.FindByIdAsync(classroomId);

                var gradeForm = await _gradeService.GetGradesAsync(activity, classroom);

                return View(gradeForm);
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (List<GradeFormViewModel> grades)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(grades);

                if (grades.Count == 0)
                {
                    return RedirectToAction("Index", "Classrooms");
                }

                if (grades != null)
                {
                    await _gradeService.ManageGrades(grades);
                }

                int classroomId = grades.FirstOrDefault().ClassroomId;

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
