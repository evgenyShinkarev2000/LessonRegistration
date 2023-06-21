
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.DTO
{
    public class Semester : SemesterShort
    {
        public IEnumerable<DTO.SubjectShort>? Subjects { get; set; }
        public IEnumerable<DTO.StudentShort>? Students { get; set; }
        public Semester() { }

        public Semester(Data.Models.Semester semester) : base(semester)
        {
            this.Subjects = semester.Subjects.Select(s => new SubjectShort(s));
            this.Students = semester.Students.Select(s => new StudentShort(s));
        }
    }
}
