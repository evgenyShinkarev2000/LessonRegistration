using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data
{
    public class Institute : PostgreEntity
    {
        public string Name { get; set; } = default!;
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }
}
