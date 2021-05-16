using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;
using Microsoft.EntityFrameworkCore;
using MyStudentsGrades.Models.ViewModels;

namespace MyStudentsGrades.Services
{
    public class GradeService
    {
        private readonly MyStudentsGradesContext _context;

        public GradeService (MyStudentsGradesContext context)
        {
            _context = context;
        }

        public IEnumerable<Grade> FindById (int idActivity)
        {
            var grades =  _context.Grade
                .Include(x => x.Activity)
                .Include(x => x.Student)
                .Where(x => x.ActivityId == idActivity);

            return grades;
        }

        public async Task<List<GradeFormViewModel>> GetGradesAsync(Activity activity, Classroom classroom)
        {
            List<GradeFormViewModel> gradeFormViewModels = new List<GradeFormViewModel>();

            List<Student> students = classroom.Students.ToList();
            foreach (Student item in students)
            {
                GradeFormViewModel gradeFormViewModel = new GradeFormViewModel();

                //Student Data
                gradeFormViewModel.StudentId = item.Id;
                gradeFormViewModel.StudentName = item.Name;
                gradeFormViewModel.StudentNumber = item.Number;

                //Activity Data
                gradeFormViewModel.ActivityName = activity.Name;
                gradeFormViewModel.ActivityQuarter = activity.Quarter.ToString();
                gradeFormViewModel.ActivityId = activity.Id;

                //Classroom Data
                gradeFormViewModel.ClassroomName = classroom.ClassroomName;
                gradeFormViewModel.ClassroomId = classroom.Id;

                //Grade Data
                Grade grade = await _context.Grade.FirstOrDefaultAsync(x => x.StudentId == item.Id 
                                                                        && x.ActivityId == activity.Id);
                if (grade == null)
                {
                    gradeFormViewModel.GradeId = 0;
                    gradeFormViewModel.Observation = "";
                    gradeFormViewModel.Grade = 0;
                }
                else
                {
                    gradeFormViewModel.GradeId = grade.Id;
                    gradeFormViewModel.Observation = grade.Observation;
                    gradeFormViewModel.Grade = grade.StudentGrade;
                }
                
                gradeFormViewModels.Add(gradeFormViewModel);
            }

            return gradeFormViewModels.OrderBy(x => x.StudentName).ToList();
        }

        public async Task ManageGrades (List<GradeFormViewModel> gradeForms)
        {
            foreach(GradeFormViewModel item in gradeForms)
            {
                Grade grade = TransformToGrade(item);

                if (grade.Id == 0)
                    await InsertAsync(grade);
                else
                    await UpdateAsync(grade);
            }
        }

        public Grade TransformToGrade (GradeFormViewModel gradeForm)
        {
            Grade grade = new Grade
            {
                ActivityId = gradeForm.ActivityId,
                Observation = gradeForm.Observation,
                StudentGrade = gradeForm.Grade,
                StudentId = gradeForm.StudentId,
                Id = gradeForm.GradeId
            };
            return grade;
        }

        public async Task UpdateAsync (Grade grade)
        {
            _context.Grade.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync (Grade grade)
        {
            try
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
