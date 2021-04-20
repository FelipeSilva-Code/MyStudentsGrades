using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;
using Microsoft.EntityFrameworkCore;
using MyStudentsGrades.Services.Exceptions;

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

        public async Task<int> RemoveAsync (int? id)
        {
          

            var student = await FindByIdAsync(id);

            //Necessary because need to back to the page of this classroom
            int classroomId = student.ClassroomId;

            _context.Remove(student);
            await _context.SaveChangesAsync();

            return classroomId;
           
        }

        public async Task UpdateAsync (Student student)
        {
            if (!await _context.Student.AnyAsync(x => x.Id == student.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Student.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }


    }
}
