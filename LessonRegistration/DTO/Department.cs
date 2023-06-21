using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace LessonRegistration.DTO
{
    public class Department : DepartmentShort
    {
        public IEnumerable<DTO.SemesterShort>? Semesters { get; set; }

        public Department() { }

        public Department(Data.Models.Department department) : base(department)
        {
            Semesters = department.Semesters.Select(s => new DTO.SemesterShort(s));
        }
    }
}
