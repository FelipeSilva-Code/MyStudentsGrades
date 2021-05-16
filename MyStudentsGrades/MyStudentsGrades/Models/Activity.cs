using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models.Enuns;
using System.ComponentModel.DataAnnotations;

namespace MyStudentsGrades.Models
{
    public class Activity
    {
        public int Id { get; set; }
        
        [StringLength(60, MinimumLength = 2, ErrorMessage = "{0} size should be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }
        public Quarter Quarter { get; set; }
        public Classroom Classroom { get; set; }
        public int ClassroomId { get; set; }

        public Activity()
        {
        }

        public Activity(int id, string name, Classroom classroom, Quarter quarter)
        {
            Id = id;
            Name = name;
            Classroom = classroom;
            Quarter = quarter;
        }
    }
}
