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

        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "School")]
        public string NameSchool { get; set; }

        [StringLength(60, MinimumLength = 2, ErrorMessage = "{0} size should be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Class")]
        public string ClassroomName { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "School Subject")]
        public string SchoolSubject { get; set; }


        [Range(0, 10, ErrorMessage = "{0} must be from {1} to {2}")]
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Average Required")]
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
