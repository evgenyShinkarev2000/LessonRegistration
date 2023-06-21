using LessonRegistration.Data.Models;

namespace LessonRegistration.DTO
{
    public class DepartmentShort : PostgreEntity, IDto<Data.Models.Department>
    {
        public string? Name { get; set; }
        public DTO.InstituteShort? Institute { get; set; }
        public DepartmentShort() { }

        public DepartmentShort(Data.Models.Department department)
        {
            Id = department.Id;
            Name = department.Name;
            Institute = new DTO.InstituteShort(department.Institute);
        }

        public virtual Data.Models.Department ToModel()
        {
            return new Data.Models.Department() { 
                Id = this.Id,
                Name = this.Name!,
                Institute = this.Institute?.ToModel()!
            };
        }
    }
}
