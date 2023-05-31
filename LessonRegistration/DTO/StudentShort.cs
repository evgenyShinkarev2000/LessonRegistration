using LessonRegistration.Data;

namespace LessonRegistration.DTO
{
    public class StudentShort : PostgreEntity, IDto<Data.Student>
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Patronymic { get; set; }
        public DTO.SemesterShort? Semester { get; set; }
        public float AverageGrade { get; set; }
        public DTO.DepartmentShort? Department { get; set; }
        public DTO.InstituteShort? Institute { get; set; }

        public StudentShort() { }
        public StudentShort(Data.Student student)
        {
            FirstName = student.FirstName;
            SecondName = student.SecondName;
            Patronymic = student.Patronymic;
            Semester = new DTO.SemesterShort(student.Semester);
            AverageGrade = student.AverageGrade;
        }

        public virtual Data.Student ToModel()
        {
            return new Data.Student()
            {
                FirstName = FirstName!,
                SecondName = SecondName!,
                Patronymic = Patronymic!,
                Semester = Semester?.ToModel()!,
                AverageGrade = AverageGrade,
            };
        }
    }
}
