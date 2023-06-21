using System;
using System.Collections.Generic;

namespace LessonRegistration.Data.Models
{
    public class Semester : PostgreEntity
    {
        public int SemesterNumber { get; set; }
        public Department Department { get; set; } = default!;
        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
