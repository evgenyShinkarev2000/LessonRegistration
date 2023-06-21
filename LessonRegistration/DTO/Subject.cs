using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.DTO
{
    public class Subject : DTO.SubjectShort
    {
        public IEnumerable<DTO.StudentShort>? RegisteredStudents { get; set; }
        public IEnumerable<DTO.SemesterShort>? UsedInSemesters { get; set; }
        public Subject() { }
        public Subject(Data.Models.Subject subject) : base(subject)
        {
            this.RegisteredStudents = subject.RegisteredStudents.Select(r => new DTO.StudentShort(r));
            this.UsedInSemesters = subject.UsedInSemesters.Select(u => new SemesterShort(u));
        }
    }
}
