using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStudentsGrades.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public string Observation { get; set; }
        public double StudentGrade { get; set; }

        public Grade()
        {
        }

        public Grade(int id, Activity activity, Student student, string observation)
        {
            Id = id;
            Activity = activity;
            Student = student;
            Observation = observation;
        }
    }
}
