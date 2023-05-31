using LessonRegistration.Data;
using System.Diagnostics;

namespace LessonRegistration.DTO
{
    public class SemesterShort : PostgreEntity, IDto<Data.Semester>
    {
        public int SemesterNumber { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public DepartmentShort? Department { get; set; }
        public SemesterShort() { }
        public SemesterShort(Data.Semester semester)
        {
            this.Id = semester.Id;
            SemesterNumber = semester.SemesterNumber;
            this.Department = new DepartmentShort(semester.Department);
        }

        public virtual Data.Semester ToModel()
        {
            return new Data.Semester()
            {
                Id = this.Id,
                SemesterNumber = this.SemesterNumber,
                Department = this.Department?.ToModel()!
            };
        }
    }
}
