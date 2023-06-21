using LessonRegistration.Data.Models;

namespace LessonRegistration.DTO
{
    public class InstituteShort : PostgreEntity, IDto<Data.Models.Institute>
    {
        public string? Name { get; set; }
        public InstituteShort() { }
        public InstituteShort(Data.Models.Institute institute)
        {
            Id = institute.Id;
            Name = institute.Name;
        }

        public virtual Data.Models.Institute ToModel()
        {
            return new Data.Models.Institute() { Name = this.Name!, Id = this.Id };
        }
    }
}
