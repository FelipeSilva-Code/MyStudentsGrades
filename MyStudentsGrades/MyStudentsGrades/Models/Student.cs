using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyStudentsGrades.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [StringLength(60, MinimumLength = 1, ErrorMessage = "{0} size should be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }
        public int Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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
