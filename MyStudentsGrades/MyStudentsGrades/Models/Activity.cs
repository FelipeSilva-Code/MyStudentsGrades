using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models.Enuns;

namespace MyStudentsGrades.Models
{
    public class Activity
    {
        public int Id { get; set; }
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
