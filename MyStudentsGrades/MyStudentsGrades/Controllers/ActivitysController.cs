using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStudentsGrades.Models;
using MyStudentsGrades.Services;

namespace MyStudentsGrades.Controllers
{
    public class ActivitysController : Controller
    {
        private readonly ActivityService _activityService;

        public ActivitysController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            var activity = new Activity() { ClassroomId = id };
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Activity activity, bool addActivities)
        {
            if (!ModelState.IsValid)
                return View(activity);

            await _activityService.InsertAsync(activity);
            
            int classroomId = activity.ClassroomId;

            if (addActivities)
                return RedirectToAction("Create", "Activitys", new { id = classroomId });
            else
                return RedirectToAction("CompleteInfo", "Classrooms", new { id = classroomId });

        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var activity = await _activityService.FindByIdAsync(id.Value);
                return View(activity);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var activity = await _activityService.FindByIdAsync(id.Value);
                return View(activity);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Activity activity)
        {
            if (!ModelState.IsValid)
                return View(activity);

            try
            {
                if (id != activity.Id)
                    throw new Exception("Id mismatch.");

                int classroomId = activity.ClassroomId;

                await _activityService.UpdateAsync(activity);
                return RedirectToAction("CompleteInfo", "Classrooms", new { id = classroomId });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var activity = await _activityService.FindByIdAsync(id.Value);
                return View(activity);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int classroomId = await _activityService.RemoveAsync(id);
                return RedirectToAction("CompleteInfo", "Classrooms", new { id = classroomId });
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
