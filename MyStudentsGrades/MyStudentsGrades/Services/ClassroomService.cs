using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStudentsGrades.Models;
namespace MyStudentsGrades.Services
{
    public class ClassroomService
    {
        private readonly MyStudentsGradesContext _context;

        public ClassroomService (MyStudentsGradesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classroom>> FindAllAsync ()
        {
            var classrooms = await _context.Classroom.ToListAsync();

            foreach (var item in classrooms)
            {
                item.Activities = await _context.Activity.Where(x => x.ClassroomId == item.Id).ToListAsync();
                item.Students = await _context.Student.Where(x => x.ClassroomId == item.Id).ToListAsync();
            }
            return classrooms;
        }
    }
}
