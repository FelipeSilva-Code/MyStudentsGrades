using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MyStudentsGrades.Models;
using MyStudentsGrades.Services.Exceptions;

namespace MyStudentsGrades.Services
{
    public class ActivityService
    {
        private readonly MyStudentsGradesContext _context;

        public ActivityService (MyStudentsGradesContext context)
        {
            _context = context;
        }

        public async Task<Activity> FindByIdAsync (int? id)
        {
            if (id == null)
                throw new Exception("Id not provided");

            var activity = await _context.Activity.Include(x => x.Classroom).FirstOrDefaultAsync(x => x.Id == id);

            if (activity == null)
                throw new NotFoundException("Id not found");

            return activity;
        }
        public async Task InsertAsync (Activity activity)
        {
            if (activity.Id != 0)
                activity.Id = 0;

            _context.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync (Activity activity)
        {
            if (!await _context.Activity.AnyAsync(x => x.Id == activity.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(activity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task<int> RemoveAsync (int? id)
        {
            var activity = await FindByIdAsync(id);

            int classroomId = activity.ClassroomId;

            _context.Remove(activity);
            await _context.SaveChangesAsync();

            return classroomId;
        }
    }
}
