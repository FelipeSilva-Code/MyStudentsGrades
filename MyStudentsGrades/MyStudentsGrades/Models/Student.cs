using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyStudentsGrades.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime BirthDate { get; set; }
        public Classroom Classroom { get; set; }
        public int ClassroomId { get; set; }

        public Student()
        {
        }

        public Student(int id, string name, int number, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Number = number;
            BirthDate = birthDate;
        }
    }
}
