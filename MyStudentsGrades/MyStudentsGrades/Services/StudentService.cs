using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Services
{
    public class StudentService
    {

        private readonly MyStudentsGradesContext _context;

        public StudentService (MyStudentsGradesContext context)
        {
            _context = context;
        }

       public async Task InsertAsync (Student student)
        {
            if (student.Id != 0)
                student.Id = 0;

            _context.Add(student);

            await _context.SaveChangesAsync();
        }
    }
}
