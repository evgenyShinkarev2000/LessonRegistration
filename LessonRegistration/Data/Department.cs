using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data
{
    public class Department : PostgreEntity
    {
        public string Name { get; set; } = default!;
        public Institute Institute { get; set; } = default!;
        public virtual ICollection<Semester> Semesters { get; set; } = new HashSet<Semester>();
    }
}
