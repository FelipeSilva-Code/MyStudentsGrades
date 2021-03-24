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
            var clasrooms = await _context.Classroom.ToListAsync();
            return clasrooms;
        }
    }
}
