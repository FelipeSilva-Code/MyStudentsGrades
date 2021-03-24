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

        public ClassroomsController (ClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _classroomService.FindAllAsync());
        }
    }
}
