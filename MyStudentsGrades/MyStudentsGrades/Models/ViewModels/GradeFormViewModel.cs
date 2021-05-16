using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStudentsGrades.Models;
using System.ComponentModel.DataAnnotations;

namespace MyStudentsGrades.Models.ViewModels
{
    public class GradeFormViewModel
    {
        public int GradeId { get; set; }
        public int ActivityId { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public string ActivityName{ get; set; }
        public string ActivityQuarter { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} required")]
        public double Grade { get; set; }

        [Display(Name = "Name")]
        public string StudentName { get; set; }       
        
        [Display(Name = "Number")]
        public int StudentNumber { get; set; }
        public string Observation { get; set; }
    }


}
