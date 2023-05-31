using LessonRegistration.Data;

namespace LessonRegistration.DTO
{
    public class DepartmentShort : PostgreEntity, IDto<Data.Department>
    {
        public string? Name { get; set; }
        public DTO.InstituteShort? Institute { get; set; }
        public DepartmentShort() { }

        public DepartmentShort(Data.Department department)
        {
            Id = department.Id;
            Name = department.Name;
            Institute = new DTO.InstituteShort(department.Institute);
        }

        public virtual Data.Department ToModel()
        {
            return new Data.Department() { 
                Id = this.Id,
                Name = this.Name!,
                Institute = this.Institute?.ToModel()!
            };
        }
    }
}
