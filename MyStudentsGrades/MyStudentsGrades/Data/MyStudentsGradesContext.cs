using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyStudentsGrades.Models
{
    public class MyStudentsGradesContext : DbContext
    {
        public MyStudentsGradesContext(DbContextOptions<MyStudentsGradesContext> options)
            : base(options)
        {
        }

        public DbSet<Classroom> Classroom { get; set; }

    }
}