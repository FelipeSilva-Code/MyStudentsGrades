using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Models.ViewModels
{
    public class TotalGradeFormViewModel
    {
        public Classroom Classroom { get; set; }
        public Student Student { get; set; }
        public List<GradePerActivity> GradePerActivities { get; set; } = new List<GradePerActivity>();
        public double Media { get; set; }
    }

    public class GradePerActivity
    {
        public Activity Activity { get; set; }
        public double Grade { get; set; }
    }
}
