using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStudentsGrades.Models;
using MyStudentsGrades.Services.Exceptions;
using MyStudentsGrades.Models.ViewModels;

namespace MyStudentsGrades.Services
{
    public class ClassroomService
    {
        private readonly MyStudentsGradesContext _context;

        public ClassroomService(MyStudentsGradesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classroom>> FindAllAsync()
        {
            var classrooms = await _context.Classroom.ToListAsync();

            foreach (var item in classrooms)
            {
                item.Activities = await _context.Activity.Where(x => x.ClassroomId == item.Id).ToListAsync();
                item.Students = await _context.Student.Where(x => x.ClassroomId == item.Id).ToListAsync();
            }
            return classrooms;
        }

        public async Task<Classroom> FindByIdAsync(int? id)
        {
            if (id == null)
                throw new Exception("Id not provided");

            var classroom = await _context.Classroom.FirstOrDefaultAsync(x => x.Id == id);

            if (classroom == null)
                throw new NotFoundException("Id not found");

            classroom.Activities = await _context.Activity.Where(x => x.ClassroomId == classroom.Id).ToListAsync();
            classroom.Students = await _context.Student.Where(x => x.ClassroomId == classroom.Id).ToListAsync();

            classroom.Students = classroom.Students.OrderBy(x => x.Name).ToList();
            return classroom;
        }

        public async Task InsertAsync(Classroom classroom)
        {
            _context.Add(classroom);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var classroom = await FindByIdAsync(id);
                _context.Classroom.Remove(classroom);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete classroom because it has students and/or activities");
            }
        }

        public async Task UpdateAsync(Classroom classroom)
        {
            if (!await _context.Classroom.AnyAsync(x => x.Id == classroom.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Classroom.Update(classroom);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task<List<TotalGradeFormViewModel>> GetTotalGradesAsync(Classroom classroom)
        {
            List<Student> students = classroom.Students.ToList();
            List<Activity> activities = classroom.Activities.ToList();
            List<TotalGradeFormViewModel> totalGrades = new List<TotalGradeFormViewModel>();

            //Check if have at least one student in that classroom
            if (students.Count == 0)
            {
                TotalGradeFormViewModel tot = new TotalGradeFormViewModel();
                tot.Classroom = classroom;
                tot.Media = 0;

                foreach(Activity item in activities)
                {
                    GradePerActivity gradePer = new GradePerActivity();
                    gradePer.Activity = item;
                    tot.GradePerActivities.Add(gradePer);
                }
               
                totalGrades.Add(tot);

            }
            else
            {
                foreach (Student item in students)
                {
                    TotalGradeFormViewModel tot = new TotalGradeFormViewModel();
                    tot.Classroom = classroom;
                    tot.Student = item;
                    double total = 0;

                    foreach (Activity act in activities)
                    {
                        GradePerActivity gradePer = new GradePerActivity();
                        gradePer.Activity = act;
                        Grade grade = await _context.Grade.FirstOrDefaultAsync(x => x.StudentId == item.Id
                                                                            && x.ActivityId == act.Id);
                        if (grade == null)
                            gradePer.Grade = 0;
                        else
                            gradePer.Grade = grade.StudentGrade;

                        total += gradePer.Grade;
                        tot.GradePerActivities.Add(gradePer);
                    }

                    double media = total / activities.Count();

                    if (activities.Count == 0)
                        tot.Media = 0;
                    else
                        tot.Media = Math.Round(media, 2);

                    totalGrades.Add(tot);
                }
            }



            return totalGrades;
        }
    }
}
