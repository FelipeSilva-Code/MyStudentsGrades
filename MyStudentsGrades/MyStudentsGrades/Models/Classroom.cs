using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStudentsGrades.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Display(Name = "School")]
        public string NameSchool { get; set; }

        [Display(Name = "Class")]
        public string ClassroomName { get; set; }

        [Display(Name = "School Subject")]
        public string SchoolSubject { get; set; }

        [Display(Name = "Required Media")]
        public double RequiredMedia { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        public Classroom ()
        { 
        }

        public Classroom(int id, string nameSchool, string classroomName, double requiredMedia)
        {
            Id = id;
            NameSchool = nameSchool;
            ClassroomName = classroomName;
            RequiredMedia = requiredMedia;
        }
  

    }
}
