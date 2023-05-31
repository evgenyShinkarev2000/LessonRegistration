using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data
{
    public class Subject : PostgreEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public virtual ICollection<Student> RegisteredStudents { get; set; } = new HashSet<Student>();
        public virtual ICollection<Semester> UsedInSemesters { get; set; } = new HashSet<Semester>();
    }
}
