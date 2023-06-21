using LessonRegistration.Data.Models;
using System.Diagnostics;

namespace LessonRegistration.DTO
{
    public class SemesterShort : PostgreEntity, IDto<Data.Models.Semester>
    {
        public int SemesterNumber { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public DepartmentShort? Department { get; set; }
        public SemesterShort() { }
        public SemesterShort(Data.Models.Semester semester)
        {
            this.Id = semester.Id;
            SemesterNumber = semester.SemesterNumber;
            this.Department = new DepartmentShort(semester.Department);
        }

        public virtual Data.Models.Semester ToModel()
        {
            return new Data.Models.Semester()
            {
                Id = this.Id,
                SemesterNumber = this.SemesterNumber,
                Department = this.Department?.ToModel()!
            };
        }
    }
}
