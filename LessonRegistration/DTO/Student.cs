using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.DTO
{
    public class Student : StudentShort
    {
        public IEnumerable<DTO.SubjectShort>? SelectedSubject { get; set; }
        public Student() { }
        public Student(Data.Models.Student student) : base(student)
        {
            this.SelectedSubject = student.SelectedSubject.Select(s => new SubjectShort(s));
        }
    }
}
