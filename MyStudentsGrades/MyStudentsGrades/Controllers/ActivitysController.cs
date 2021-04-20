using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyStudentsGrades.Controllers
{
    public class ActivitysController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
