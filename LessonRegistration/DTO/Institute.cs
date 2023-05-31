using LessonRegistration.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.DTO
{ 
    public class Institute : InstituteShort
    {
        public IEnumerable<DepartmentShort>? Departments { get; set; }
        public Institute() { }
        public Institute(Data.Institute institute) : base(institute)
        {
            Departments = institute.Departments.Select(d => new DepartmentShort(d));
        }
    }
}
