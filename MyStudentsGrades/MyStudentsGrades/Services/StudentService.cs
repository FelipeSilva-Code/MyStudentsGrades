using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Student> FindByIdAsync (int? id)
        {
            if (id == null)
                throw new Exception("Id not provided.");

            var student = await _context.Student.Include(x => x.Classroom).FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
                throw new Exception("Id not found.");

            return student;
        }


    }
}
