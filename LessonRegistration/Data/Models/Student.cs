using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data.Models
{
    public class Student : Person
    {
        public Semester Semester { get; set; } = default!;
        public float AverageGrade { get; set; }
        public virtual ICollection<Subject> SelectedSubject { get; set; } = new HashSet<Subject>();
    }
}
