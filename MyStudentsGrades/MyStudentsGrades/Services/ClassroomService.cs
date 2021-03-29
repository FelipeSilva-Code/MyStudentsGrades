using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStudentsGrades.Models;
using MyStudentsGrades.Services.Exceptions;

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

        public async Task<Classroom> FindByIdAsync (int? id)
        {
            if (id == null)
                throw new Exception("Id not provided");

            var classroom = await _context.Classroom.FirstOrDefaultAsync(x => x.Id == id);

            if (classroom == null)
                throw new NotFoundException("Id not found");

            classroom.Activities = await _context.Activity.Where(x => x.ClassroomId == classroom.Id).ToListAsync();
            classroom.Students = await _context.Student.Where(x => x.ClassroomId == classroom.Id).ToListAsync();

            return classroom;
        }

        public async Task InsertAsync (Classroom classroom)
        {
            _context.Add(classroom);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync (int id)
        {
            try
            {
                var classroom = await FindByIdAsync(id);
                _context.Classroom.Remove(classroom);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Can't delete classroom because it has students and/or activities");
            }
        }

        public async Task UpdateAsync (Classroom classroom)
        {
            if(!await _context.Classroom.AnyAsync(x => x.Id == classroom.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Classroom.Update(classroom);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
